using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace ConsoleApp1
{
    class Program
    {
        enum OktaAuthState
        {
            UserPass,
            SendPush,
            OktaAccess,
            Finished_OK,
            Finished_Rejected
        }

        public static string hydra_dev_key = "";
        public static string HydraUrl = "https://int-api.wbagora.com";
        static RestRequest RequestCreator(string endpoint)
        {
            return (RestRequest) new RestRequest(endpoint, Method.POST).AddHeader("Accept", "application/json").AddHeader("Content-Type", "application/json");
        }
        static async Task OktaAuthenticate(string oktadmn, string usr, string psw)
        {
            dynamic oastate = OktaAuthState.UserPass;
            dynamic responseJson = JsonConvert.DeserializeObject("{}");
            dynamic href = "";
            while (oastate != OktaAuthState.Finished_OK && oastate != OktaAuthState.Finished_Rejected)
            {
                dynamic clnt = new RestClient();
                dynamic req = RequestCreator("");
                dynamic bodyobj = new Dictionary<string, string>()
                {

                };

                if (oastate == OktaAuthState.UserPass)
                {
                    
                    clnt = new RestClient("https://" + oktadmn + ".okta.com");
                    req = RequestCreator("/api/v1/authn");
                    bodyobj["username"] = usr;
                    bodyobj["password"] = psw;

                }
                else if (oastate == OktaAuthState.SendPush)
                {
                    
                    dynamic stateToken = responseJson["stateToken"].Value;                    
                    if (String.IsNullOrEmpty(href))
                    {
                        dynamic factors = responseJson["_embedded"]["factors"];
                        foreach (dynamic factor in factors)
                        {
                            if (!String.IsNullOrEmpty(href) || factor["factorType"] != "push") continue;
                            href = factor["_links"]["verify"]["href"].Value;
                        }
                    }
                    req = RequestCreator(href);
                    bodyobj["stateToken"] = stateToken;
                }
                else if (oastate == OktaAuthState.OktaAccess)
                {
                    dynamic code = responseJson["sessionToken"].Value;
                    clnt = new RestClient(HydraUrl);
                    req = RequestCreator("/developers/okta_access");
                    bodyobj["code"] = code;
                    bodyobj["redirect_uri"] = "account";                                        
                }

                //Ok do. 
                req.AddJsonBody(JsonConvert.SerializeObject(bodyobj));
                dynamic postresponse = await clnt.ExecutePostAsync<dynamic>(req);
                responseJson = JsonConvert.DeserializeObject(postresponse.Content);

                if (oastate == OktaAuthState.UserPass)
                {
                    dynamic status = responseJson["status"].Value;
                    if (status == "MFA_REQUIRED")
                    {
                        oastate = OktaAuthState.SendPush;
                    }
                    else if (status == "SUCCESS")
                    {
                        oastate = OktaAuthState.OktaAccess;
                    }
                }
                else if (oastate == OktaAuthState.SendPush)
                {
                    dynamic status = responseJson["status"].Value;
                    if (status == "MFA_CHALLENGE")
                    {
                        dynamic factorResult = responseJson["factorResult"].Value;
                        if (factorResult == "REJECTED")
                        {
                            oastate = OktaAuthState.Finished_Rejected;
                        }
                    }
                    else if (status == "SUCCESS")
                    {
                        oastate = OktaAuthState.OktaAccess;
                    }
                }
                else if (oastate == OktaAuthState.OktaAccess)
                {
                    string message = postresponse.Content;;
                    string caption = "Some Message from our sponsors";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    // Displays the MessageBox.
                    result = MessageBox.Show(message, caption, buttons);
                    Console.WriteLine(postresponse.Content);
                    oastate = OktaAuthState.Finished_OK;
                }
            }
        }
        static async Task CheckForOktaMFARequiredTask(dynamic response)
        {
            dynamic contentObject = JsonConvert.DeserializeObject(response.Content);
            dynamic stateToken = contentObject["stateToken"].Value;
            dynamic isResponseSuccessful = contentObject["status"] == "SESSION";
            dynamic isMFA_REQUIRED = contentObject["status"] == "MFA_REQUIRED";
            
            if (isResponseSuccessful )
            {
                hydra_dev_key = contentObject["sessionToken"];
                return;
            
            } else if (!isMFA_REQUIRED)
            {
                return;
                
            }

            try
            {
                Console.WriteLine("Multi Factor Authentication Required, the following factors are available:");
                var factors = contentObject["_embedded"]["factors"];
                var href = "";
                var stateTokenObject = new Dictionary<string, string>()
                {
                    {"stateToken" , stateToken}
                };
                    
                foreach(var factor in factors)
                {
                    if (!String.IsNullOrEmpty(href) || factor["factorType"] != "push")
                    {
                        continue;
                    }
                    
                    href = factor["_links"]["verify"]["href"];
                    var clnt = new RestClient();
                    var req = RequestCreator(href);
                    req.AddJsonBody(JsonConvert.SerializeObject(stateTokenObject));
                    
                    bool push = true;
                    while (push)
                    {
                        dynamic pushResponse = await clnt.ExecutePostAsync<dynamic>(req);
                        var pushResponseContent = JsonConvert.DeserializeObject(pushResponse.Content);
                        switch (pushResponseContent["status"].Value)
                        {
                            case "MFA_CHALLENGE":
                                var factorResult = pushResponseContent["factorResult"];
                                push = factorResult == "REJECTED" ? false : true;
                                break;
                            
                            case "SUCCESS":
                                push = false;
                                hydra_dev_key = pushResponseContent["sessionToken"];
                                break;
                            
                            default:
                                push = false;
                                break;
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        static async Task<bool> OktaLoginTask(string dmn, string usr, string psw)
        {
            Console.WriteLine("Performing OKTA LOGIN......");
            try
            {
                var url = "https://" + dmn + ".okta.com";
                var endpoint = "/api/v1/authn/";
                var clnt = new RestClient(url);
                var req = RequestCreator(endpoint);
                var body = new Dictionary<string, string>()
                {
                    {"username", usr},
                    {"password", psw}
                };
                req.AddJsonBody(JsonConvert.SerializeObject(body));

                Console.WriteLine("Calling Okta...... [AUTH]");

                dynamic rsp = await clnt.ExecutePostAsync<dynamic>(req);
                dynamic completeRequestTask = await CheckForOktaMFARequiredTask(rsp);
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           

                
            return true;
        }
        
        static void Main(string[] args)
        {

            var loginOk = Program.OktaAuthenticate("wbiegames","x.julian.guarin@wbgconsultant.com","Songohan3578--");
            loginOk.Wait();


            string message = "Hydra Dev Key: " + hydra_dev_key;
            string caption = "Some Message from our sponsors";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
            
        }      
}
}
