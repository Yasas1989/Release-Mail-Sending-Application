using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;
using BusinessLayer;

namespace SendMails
{
    public partial class MailSending : Form
    {
        BusinessLayer.GetMails myMail = new BusinessLayer.GetMails();
        BusinessLayer.User myUser = new BusinessLayer.User();
        
        public MailSending()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;

        } 

        DataTable dtEmployeeDetails = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            txtBuild.Enabled = false;
            txtBuild.Visible = false;
            lblBuild.Enabled = false;
            lblBuild.Visible = false;

            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 30;
            headerPanel.BackColor = Color.CornflowerBlue;
            this.Controls.Add(headerPanel);

            Label title = new Label();
            title.Text = "Release Manager";
            title.ForeColor = Color.White;
            title.Font = new Font("Tahoma", 12, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(15, 05);
            headerPanel.Controls.Add(title);

            label5.Text = $"Welcome - {User.StatUserName}";
            label5.BackColor = Color.CornflowerBlue;
            label5.ForeColor = Color.White;
            label5.Font = new Font("Tahoma", 8, FontStyle.Bold);
            title.Location = new Point(20, 04);

            Button btnMin = new Button();
            btnMin.Text = "—";
            btnMin.ForeColor = Color.Black;
            btnMin.BackColor = Color.Aquamarine;
            btnMin.FlatStyle = FlatStyle.Flat;
            btnMin.FlatAppearance.BorderSize = 0;
            btnMin.Size = new Size(30, 25);
            btnMin.Location = new Point(this.Width - 65, 0);
            btnMin.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMin.Click += (s, ev) => this.WindowState = FormWindowState.Minimized;
            headerPanel.Controls.Add(btnMin);

            // Close button
            Button btnClose = new Button();
            btnClose.Text = "✖";
            btnClose.ForeColor = Color.Red;
            btnClose.BackColor = Color.AliceBlue;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Size = new Size(30, 25);
            btnClose.Location = new Point(this.Width - 30, 0);
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Click += (s, ev) => Application.Exit();
            headerPanel.Controls.Add(btnClose);

            cmbPlantation.Font = new Font("Segoe UI", 9);           
            cmbPlantation.DataSource = myMail.ListPlantations().Tables[0];
            cmbPlantation.DisplayMember = "Name";
            cmbPlantation.ValueMember = "Code";

            cmbModule.Font = new Font("Segoe UI", 9);
            cmbModule.DataSource = myMail.ListModule(cmbPlantation.SelectedValue.ToString()).Tables[0];
            cmbModule.DisplayMember = "ModuleName";
            cmbModule.ValueMember = "ModuleShortCode";
         
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
       
        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            try
            {
                                
                 String ModuleName = cmbModule.Text.Trim();
                 Decimal LastVersion = decimal.Parse(txtVersion.Text);
                 string PlantationName = cmbPlantation.Text.Trim();
                 String ModuleShortCode = cmbModule.SelectedValue.ToString();
                 String BodySubject = textBox2.Text;
                 String PlantationCode = cmbPlantation.SelectedValue.ToString();
                 String LatestBuild = txtBuild.Text.Trim();

                List<string> toEmails = myMail.GetEmailAddressesFromDatabaseTo("To", cmbPlantation.SelectedValue.ToString());   // Fetch To emails
                List<string> ccEmails = myMail.GetEmailAddressesFromDatabaseCC("CC");   // Fetch CC emails


                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Email body can't be Empty.");
                }
                else if (toEmails.Count == 0)
                {
                    MessageBox.Show("No recipients found for To.");
                }
                else if (ccEmails.Count == 0)
                {
                    MessageBox.Show("No recipients found for CC.");
                }               
                else
                {
                    if(ModuleName == "Payroll")
                    {
                        //SendEmail(toEmails, ccEmails);
                        bool success = myMail.SendEmailPayroll(toEmails, ccEmails, ModuleName, LastVersion, PlantationName, BodySubject);
                        if (success)
                        {
                            myMail.UpdateLatestVersion(LastVersion, ModuleShortCode);
                            MessageBox.Show("Email sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtVersion.Text = myMail.getLastVersion(cmbModule.SelectedValue.ToString()).ToString("N2");
                            button1.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Failed to send email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            button1.Enabled = true;
                        }
                    }
                    if (ModuleName == "Checkroll")
                    {
                        //SendEmail(toEmails, ccEmails);
                        bool success = myMail.SendEmailCheckroll(toEmails, ccEmails, ModuleName, LatestBuild, PlantationName, BodySubject);
                        if (success)
                        {
                            myMail.UpdateLatestBuild(LatestBuild, ModuleShortCode);
                            MessageBox.Show("Email sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtBuild.Text = myMail.getLastBuild(cmbModule.SelectedValue.ToString()).ToString();
                            button1.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Failed to send email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            button1.Enabled = true;
                        }
                    }
                    else
                    {
                        //SendEmail(toEmails, ccEmails);
                        bool success = myMail.SendEmail(toEmails, ccEmails, ModuleName, LastVersion, PlantationName, BodySubject);
                        if (success)
                        {
                            myMail.UpdateLatestVersion(LastVersion, ModuleShortCode);
                            MessageBox.Show("Email sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtVersion.Text = myMail.getLastVersion(cmbModule.SelectedValue.ToString()).ToString("N2");
                            button1.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Failed to send email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            button1.Enabled = true;
                        }
                    }
                    
                    
                    
                }

            }
            catch
            {
                MessageBox.Show("Error Occured..! Contact Yasas.");
                button1.Enabled = true;
            }
        }     

        private void cmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if (cmbModule.Text == "Checkroll")
            {
                txtBuild.Text = myMail.getLastBuild(cmbModule.SelectedValue.ToString()).ToString();

                txtBuild.Enabled = true;
                txtBuild.Visible = true;
                lblBuild.Enabled = true;
                lblBuild.Visible = true;

                txtVersion.Enabled = false;
                txtVersion.Visible = false;
                label2.Enabled = false;
                label2.Visible = false;
            }
            else
            {
                
                txtVersion.Text = myMail.getLastVersion(cmbModule.SelectedValue.ToString()).ToString("N2");

                txtBuild.Enabled = false;
                txtBuild.Visible = false;
                lblBuild.Enabled = false;
                lblBuild.Visible = false;

                txtVersion.Enabled = true;
                txtVersion.Visible = true;
                label2.Enabled = true;
                label2.Visible = true;
            }


        }

        private void cmbPlantation_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbModule.DataSource = myMail.ListModule(cmbPlantation.SelectedValue.ToString()).Tables[0];
            cmbModule.DisplayMember = "ModuleName";
            cmbModule.ValueMember = "ModuleShortCode";
        }

        private void txtVersion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }


    }

