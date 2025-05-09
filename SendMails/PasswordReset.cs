using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendMails
{
    public partial class PasswordReset : Form
    {
        BusinessLayer.User myUser = new BusinessLayer.User();

        public PasswordReset()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button2.PerformClick();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // this.Close();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String UserName = txtUserName.Text.Trim();
            bool success = !String.IsNullOrWhiteSpace(myUser.CheckValidEmail(UserName));

            if (success)
            {
                myUser.SendPasswordResetCode(UserName);
                txtResetCode.Enabled = true;
                label5.Visible = true;
                btnSubmitCode.Enabled = true;
                btnSubmitCode.Visible = true;
                MessageBox.Show("Code has been sent to your Email....!");
                button2.Enabled = false;
                button2.Visible = false;

            }
            else
            {
                MessageBox.Show("Not Registered User....!");
            }

        }

        private void btnSubmitCode_Click(object sender, EventArgs e)
        {
            String ResetCode = txtResetCode.Text.Trim();
            //Boolean status = string.Equals("Done") : true ? false;
            string result = myUser.ValidateResetCode(ResetCode);
            bool success = !string.IsNullOrWhiteSpace(result);

            if (result == "NRRF")
            {
                MessageBox.Show("No reset request found..!");
            }
            else if (result == "IC")
            {
                MessageBox.Show("Invalid code.");
            }
            else if (result == "CE")
            {
                MessageBox.Show("Code expired..!");
            }
            else
            {
                txtNewPass.Enabled = true;
                txtNewPass.Visible = true;
                txtConfirmPass.Enabled = true;
                txtConfirmPass.Visible = true;
                btnReset.Enabled = true;
                btnReset.Visible = true;
                btnSubmitCode.Enabled = false;
                btnSubmitCode.Visible = false;
                label5.Visible = false;
                lblCpass.Visible = true;
                lblPass.Visible = true;

            }

        }

        private void PasswordReset_Load(object sender, EventArgs e)
        {
            txtResetCode.Enabled = false;
            label5.Visible = false;
            btnSubmitCode.Enabled = false;
            btnSubmitCode.Visible = false;
            lblPass.Visible = false;
            lblCpass.Visible = false;
            txtNewPass.Enabled = false;
            txtNewPass.Visible = false;
            txtConfirmPass.Enabled = false;
            txtConfirmPass.Visible = false;
            btnReset.Enabled = false;
            btnReset.Visible = false;
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtResetCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnSubmitCode.PerformClick();
            }
        }

        private void txtNewPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtConfirmPass.Focus();
            }
        }

        private void txtConfirmPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                btnReset.PerformClick();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            String UserName = txtUserName.Text.Trim();
            String Password = txtConfirmPass.Text.Trim();

            if (txtNewPass.Text.Length == 0)
            {
                MessageBox.Show("Enter Password to Proceed....!");
            }
            if (txtConfirmPass.Text.Length == 0)
            {
                MessageBox.Show("Confirm Password cannot Empty....!");
            }
            else
            {
                if (txtNewPass.Text == txtConfirmPass.Text)
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
                    myUser.UpdatePassword(UserName, hashedPassword);
                    MessageBox.Show("Your Password Successfully Changed...!");
                    this.Close();
                    Login myLog = new Login();
                    myLog.Show();
                    
                }
                else
                {
                    MessageBox.Show("Passwords are not matching....!");
                }
            }
        }

        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {
            txtNewPass.PasswordChar = '*';
        }

        private void txtConfirmPass_TextChanged(object sender, EventArgs e)
        {
            txtConfirmPass.PasswordChar = '*';
        }
    }
}
