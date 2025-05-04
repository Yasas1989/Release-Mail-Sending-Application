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

namespace SendMails
{
    public partial class MailSending : Form
    {
        BusinessLayer.GetMails myMail = new BusinessLayer.GetMails();
        public MailSending()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //public String ModuleName { get; set; }
        //public Decimal LastVersion { get; set; }
        //public String PlantationName { get; set; }
        //public String ModuleShortCode { get; set; }
        //public String BodySubject { get; set; }

        DataTable dtEmployeeDetails = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {         

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
            try
            {
                                
                 String ModuleName = cmbModule.Text.Trim();
                 Decimal LastVersion = decimal.Parse(txtVersion.Text);
                 string PlantationName = cmbPlantation.Text.Trim();
                 String ModuleShortCode = cmbModule.SelectedValue.ToString();
                 String BodySubject = textBox2.Text;
                 String PlantationCode = cmbPlantation.SelectedValue.ToString();

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
                        bool success = SendEmailPayroll(toEmails, ccEmails, ModuleName, LastVersion, PlantationName, BodySubject);
                        if (success)
                        {
                            myMail.UpdateLatestVersion(LastVersion, ModuleShortCode);
                            MessageBox.Show("Email sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtVersion.Text = myMail.getLastVersion(cmbModule.SelectedValue.ToString()).ToString("N2");
                        }
                        else
                        {
                            MessageBox.Show("Failed to send email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        //SendEmail(toEmails, ccEmails);
                        bool success = SendEmail(toEmails, ccEmails, ModuleName, LastVersion, PlantationName, BodySubject);
                        if (success)
                        {
                            myMail.UpdateLatestVersion(LastVersion, ModuleShortCode);
                            MessageBox.Show("Email sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtVersion.Text = myMail.getLastVersion(cmbModule.SelectedValue.ToString()).ToString("N2");
                        }
                        else
                        {
                            MessageBox.Show("Failed to send email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    
                    
                    
                }

            }
            catch
            {
                MessageBox.Show("Error Occured..! Contact Yasas.");
            }
        }
            
        
        public bool SendEmail(List<string> toEmails, List<string> ccEmails, String ModuleName, Decimal LastVersion, String PlantationName, String BodySubject)
        {          
            try
            {
                string fromEmail = "yasas@ftservices.net";
                string fromPassword = "xyor bpvv kxxw frep";
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
           
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    smtpClient.EnableSsl = true;
                    smtpClient.ServicePoint.MaxIdleTime = 2 * 60 * 1000; // Keep SMTP connection alive
                    smtpClient.ServicePoint.ConnectionLimit = 10; // Allow multiple connections
                    
                    // Split based on period + space + capital letter
                    string[] sentences = System.Text.RegularExpressions.Regex.Split(BodySubject, @"(?<=[.?!])\s+(?=[A-Z])");
                    // Add bullets
                    StringBuilder formatted = new StringBuilder();
                    formatted.Append("<ul>"); // Start list

                    foreach (string sentence in sentences)
                    {
                        formatted.Append("<li>" + sentence.Trim() + "</li>");
                    }
                    formatted.Append("</ul>"); // End list                 
              
                    MailMessage mail = new MailMessage
                    {
                        
                        From = new MailAddress(fromEmail),                     
                        Subject = $"Olax System Update - {PlantationName} {ModuleName} Module Release V{LastVersion}",
                      
                        Body =

                        "<span style='font-family:Tahoma; font-size:17px; padding-left:0px;'>Dear All,<br>" +
                        "<span style='padding-left:0px;'>There is a new update from OLAX Systems.</span><br><br>" +
                        "<span style='padding-left:0px;'>Release Contains:</span><br><br>" +
                        $"<span style='padding-left:40px;'><b>1. {PlantationName} {ModuleName} Module Release V {LastVersion}</b> We have done some fine-tunings for:</span><br><br>"+
                        "<ul style='margin-top:0px; margin-bottom:0px; '>" +
                        $"{formatted}<br><br>" +
                        "</ul>"+ 
                        "<span style='padding-left:-0px;'>Kindly download the latest version.<br><br>" +
                        "<span style='padding-left:0px;'>Thanks and Best Regards,<br>" +
                        "<span style='padding-left:0px;'>OLAX Team",
                               
                     
                        IsBodyHtml = true // Use true if sending HTML
                                      
                    };

                    foreach (var email in toEmails) mail.To.Add(email);
                    foreach (var email in ccEmails) mail.CC.Add(email);

                    smtpClient.Send(mail); // Blocking call, but handled inside ThreadPool
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Email Error: " + ex.Message);
                return false;
            }
        }

        public bool SendEmailPayroll(List<string> toEmails, List<string> ccEmails, String ModuleName, Decimal LastVersion, String PlantationName, String BodySubject)
        {
            try
            {
                string fromEmail = "yasas@ftservices.net";
                string fromPassword = "xyor bpvv kxxw frep";
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;

                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    smtpClient.EnableSsl = true;
                    smtpClient.ServicePoint.MaxIdleTime = 2 * 60 * 1000; // Keep SMTP connection alive
                    smtpClient.ServicePoint.ConnectionLimit = 10; // Allow multiple connections

                    // Split based on period + space + capital letter
                    string[] sentences = System.Text.RegularExpressions.Regex.Split(BodySubject, @"(?<=[.?!])\s+(?=[A-Z])");
                    // Add bullets
                    StringBuilder formatted = new StringBuilder();
                    formatted.Append("<ul>"); // Start list

                    foreach (string sentence in sentences)
                    {
                        formatted.Append("<li>" + sentence.Trim() + "</li>");
                    }
                    formatted.Append("</ul>"); // End list                 

                    MailMessage mail = new MailMessage
                    {

                        From = new MailAddress(fromEmail),
                        Subject = $"Olax System Update - {PlantationName} {ModuleName} Module Release",

                        Body =

                        "<span style='font-family:Tahoma; font-size:17px; padding-left:0px;'>Dear All,<br>" +
                        "<span style='padding-left:0px;'>There is a new update from OLAX Systems.</span><br><br>" +
                        "<span style='padding-left:0px;'>Release Contains:</span><br><br>" +
                        $"<span style='padding-left:40px;'><b>1. {PlantationName} {ModuleName} Module Release </b> We have done some fine-tunings for:</span><br><br>" +
                        "<ul style='margin-top:0px; margin-bottom:0px; '>" +
                        $"{formatted}<br><br>" +
                        "</ul>" +
                        "<span style='padding-left:-0px;'>Kindly download the latest version.<br><br>" +
                        "<span style='padding-left:0px;'>Thanks and Best Regards,<br>" +
                        "<span style='padding-left:0px;'>OLAX Team",


                        IsBodyHtml = true // Use true if sending HTML

                    };

                    foreach (var email in toEmails) mail.To.Add(email);
                    foreach (var email in ccEmails) mail.CC.Add(email);

                    smtpClient.Send(mail); // Blocking call, but handled inside ThreadPool
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Email Error: " + ex.Message);
                return false;
            }
        }

        private void cmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
           txtVersion.Text = myMail.getLastVersion(cmbModule.SelectedValue.ToString()).ToString("N2");
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

