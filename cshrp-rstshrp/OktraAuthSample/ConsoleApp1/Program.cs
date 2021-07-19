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
    class Program
    {
        static ConsoleApp1.AuthDialogue OktaAuthenticatorDlg;
        static void StartAuthentication(object sender, EventArgs e)
        {
            Console.WriteLine("YEAH!");
            Console.WriteLine($"Username: {OktaAuthenticatorDlg.UsrText}");
            Console.WriteLine("Password: {OktaAuthenticatorDlg.PswText}");
            dynamic OktaAuthenticator = new HydraOktaAuthentication();
            try
            {
                string username = OktaAuthenticatorDlg.UsrText;
                string password = OktaAuthenticatorDlg.PswText;
                OktaAuthenticator.OktaAuthenticate(
                    "wbiegames",
                    username,
                    password
                    );

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //loginOk.Wait();

        }

        static void Main(string[] args)
        {
            Application.SetCompatibleTextRenderingDefault(false);
            
            OktaAuthenticatorDlg = new ConsoleApp1.AuthDialogue();
            OktaAuthenticatorDlg.eSignInButtonClicked += StartAuthentication;

            Application.EnableVisualStyles();
            Application.Run(OktaAuthenticatorDlg);



            /*
            string caption = "Some Message from our sponsors";
            string message = "Hydra Dev Key: ";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);*/
            
        }      
}
}
