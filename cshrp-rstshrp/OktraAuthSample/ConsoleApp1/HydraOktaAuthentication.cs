using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace ConsoleApplication

{
    class HydraOktaAuthentication
    {
        enum OktaAuthState
        {
            UserPass,
            SendPush,
            HydraOktaAccess,
            Finished_OK,
            Finished_Rejected
        }

        private string HydraUrl = "https://int-api.wbagora.com";
        
        static RestRequest RequestCreator(string endpoint)
        {
            return (RestRequest)new RestRequest(endpoint, Method.POST).AddHeader("Accept", "application/json").AddHeader("Content-Type", "application/json");
        }
        public void OktaAuthenticate(string oktadmn, string usr, string psw)
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
                else if (oastate == OktaAuthState.HydraOktaAccess)
                {
                    dynamic code = responseJson["sessionToken"].Value;
                    clnt = new RestClient(HydraUrl);
                    req = RequestCreator("/developers/okta_access");
                    bodyobj["code"] = code;
                    bodyobj["redirect_uri"] = "account";
                }

                //Ok do. 
                req.AddJsonBody(JsonConvert.SerializeObject(bodyobj));
                dynamic postresponse = clnt.Execute<dynamic>(req);
                responseJson = JsonConvert.DeserializeObject(postresponse.Content);
                Console.WriteLine("There was a response");
                if (oastate == OktaAuthState.UserPass)
                {
                    dynamic status = responseJson["status"].Value;
                    if (status == "MFA_REQUIRED")
                    {
                        oastate = OktaAuthState.SendPush;
                    }
                    else if (status == "SUCCESS")
                    {
                        oastate = OktaAuthState.HydraOktaAccess;
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
                        oastate = OktaAuthState.HydraOktaAccess;
                    }
                }
                else if (oastate == OktaAuthState.HydraOktaAccess)
                {
                    string message = postresponse.Content; ;
                    string caption = "developer/okta_access response:";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    DialogResult result;

                    // Displays the MessageBox.
                    result = MessageBox.Show(message, caption, buttons);
                    Console.WriteLine(postresponse.Content);
                    oastate = OktaAuthState.Finished_OK;
                }
            }
        }

    }
}
