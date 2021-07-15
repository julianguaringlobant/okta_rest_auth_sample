using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp1
{
    public partial class AuthDialogue : Form
    {
        public event EventHandler eSignInButtonClicked;
        public AuthDialogue()
        {
            InitializeComponent();
        }
        public string UsrText
        {
            get { return fUsrText.Text; }
            set { fUsrText.Text = value; }
        }
        public string PswText
        {
            get { return fPswText.Text; }
            set { fPswText.Text = value; }
        }
        private void AuthDialogue_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void fSignInButton_MouseUp(object sender, MouseEventArgs e)
        {
            eSignInButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        private void fPswText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
