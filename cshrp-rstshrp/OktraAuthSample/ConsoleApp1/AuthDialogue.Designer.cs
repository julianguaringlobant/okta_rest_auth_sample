
namespace ConsoleApp1
{
    partial class AuthDialogue
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.fUsrText = new System.Windows.Forms.TextBox();
            this.fPswText = new System.Windows.Forms.TextBox();
            this.fSignInButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(28, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email Address";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(28, 275);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Desktop Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(134, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sign In";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // fUsrText
            // 
            this.fUsrText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fUsrText.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fUsrText.Location = new System.Drawing.Point(32, 240);
            this.fUsrText.Name = "fUsrText";
            this.fUsrText.Size = new System.Drawing.Size(269, 29);
            this.fUsrText.TabIndex = 3;
            this.fUsrText.Tag = "EMAILTEXTFIELD";
            this.fUsrText.Text = "x,julian.guarin@wbgconsultant.com";
            this.fUsrText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // fPswText
            // 
            this.fPswText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fPswText.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.fPswText.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.fPswText.Location = new System.Drawing.Point(33, 303);
            this.fPswText.Name = "fPswText";
            this.fPswText.PasswordChar = '°';
            this.fPswText.Size = new System.Drawing.Size(269, 29);
            this.fPswText.TabIndex = 4;
            this.fPswText.Tag = "PASSTEXTFIELD";
            this.fPswText.Text = "Songohan3578--";
            this.fPswText.TextChanged += new System.EventHandler(this.fPswText_TextChanged);
            // 
            // fSignInButton
            // 
            this.fSignInButton.BackColor = System.Drawing.SystemColors.Highlight;
            this.fSignInButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fSignInButton.ForeColor = System.Drawing.SystemColors.Control;
            this.fSignInButton.Location = new System.Drawing.Point(34, 359);
            this.fSignInButton.Name = "fSignInButton";
            this.fSignInButton.Size = new System.Drawing.Size(268, 51);
            this.fSignInButton.TabIndex = 5;
            this.fSignInButton.Text = "Sign In";
            this.fSignInButton.UseVisualStyleBackColor = false;
            this.fSignInButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.fSignInButton_MouseUp);
            // 
            // AuthDialogue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(354, 554);
            this.Controls.Add(this.fSignInButton);
            this.Controls.Add(this.fPswText);
            this.Controls.Add(this.fUsrText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthDialogue";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AuthDialogue_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fUsrText;
        private System.Windows.Forms.TextBox fPswText;
        private System.Windows.Forms.Button fSignInButton;
    }
}