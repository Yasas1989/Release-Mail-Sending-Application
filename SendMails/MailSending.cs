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
            gvEmails.RowHeadersVisible = false;

        }

        //DataTable dtEmployeeDetails = new DataTable();

        DataTable dtLocationTyte = new DataTable();
        private void Form1_Load(object sender, EventArgs e)
        {
            txtBuild.Enabled = false;
            txtBuild.Visible = false;
            lblBuild.Enabled = false;
            lblBuild.Visible = false;

            Panel headerPanel = new Panel();
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 35;
            headerPanel.BackColor = Color.Orange;
            this.Controls.Add(headerPanel);

            Panel leftGreenPanel = new Panel();
            leftGreenPanel.Dock = DockStyle.Left;
            leftGreenPanel.Width = 5; // Adjust thickness
            leftGreenPanel.BackColor = Color.Orange;
            this.Controls.Add(leftGreenPanel);

            Panel rightGreenPanel = new Panel();
            rightGreenPanel.Dock = DockStyle.Right;
            rightGreenPanel.Width = 5; // Adjust thickness
            rightGreenPanel.BackColor = Color.Orange;
            this.Controls.Add(rightGreenPanel);

            Panel footerPanel = new Panel();
            footerPanel.Dock = DockStyle.Bottom;
            footerPanel.Height = 5;
            footerPanel.BackColor = Color.Orange;
            this.Controls.Add(footerPanel);

            Label title = new Label();
            title.Text = "Release Manager";
            title.ForeColor = Color.Black;
            title.Font = new Font("Tahoma", 12, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(15, 05);
            headerPanel.Controls.Add(title);

            label5.Text = $"Welcome - {User.StatUserName}";
            label5.BackColor = Color.Orange;
            label5.ForeColor = Color.Black;
            label5.Font = new Font("Tahoma", 8, FontStyle.Bold);
            title.Location = new Point(20, 04);

            Button btnMin = new Button();
            btnMin.Text = "—";
            btnMin.ForeColor = Color.Red;
            btnMin.BackColor = Color.AliceBlue;
            btnMin.FlatStyle = FlatStyle.Flat;
            btnMin.FlatAppearance.BorderSize = 0;
            btnMin.Size = new Size(30, 25);
            btnMin.Location = new Point(this.Width - 75, 0);
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
            btnClose.Location = new Point(this.Width - 40, 0);
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Click += (s, ev) => Application.Exit();
            headerPanel.Controls.Add(btnClose);

            dtLocationTyte = myMail.ListEstateLocation().Tables[0];
            gvEmails.DataSource = myMail.ListAllCCMails();

            cmbLocationType.Font = new Font("Segoe UI", 9);
            cmbLocationType.DataSource = myMail.ListEstateLocation().Tables[0];
            cmbLocationType.DisplayMember = "LocationType";
            cmbLocationType.ValueMember = "LocationType";

            cmbPlantation.Font = new Font("Segoe UI", 9);
            cmbPlantation.DataSource = myMail.ListPlantations(dtLocationTyte.Rows[0]["LocationType"].ToString()).Tables[0];
            cmbPlantation.DisplayMember = "Name";
            cmbPlantation.ValueMember = "Code";

            gvEmails.Columns["Name"].ReadOnly = true;
            gvEmails.Columns["Email"].ReadOnly = true;
            gvEmails.Columns["Type"].ReadOnly = true;
            gvEmails.Columns["Sending CC"].ReadOnly = false;

            //cmbModule.Font = new Font("Segoe UI", 9);
            //cmbModule.DataSource = myMail.ListModule(cmbPlantation.SelectedValue.ToString()).Tables[0];
            //cmbModule.DisplayMember = "ModuleName";
            //cmbModule.ValueMember = "ModuleShortCode";

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //private async void button1_Click(object sender, EventArgs e)
        //{
        //    button1.Enabled = false;

        //    //  Collect UI values before Task.Run (safe access)
        //    string moduleName = cmbModule.Text.Trim();
        //    decimal lastVersion = decimal.Parse(txtVersion.Text);
        //    string plantationName = cmbPlantation.Text.Trim();
        //    string moduleShortCode = cmbModule.SelectedValue.ToString();
        //    string bodySubject = textBox2.Text;
        //    string plantationCode = cmbPlantation.SelectedValue.ToString();
        //    string latestBuild = txtBuild.Text.Trim();


        //    List<string> selectedEmails = new List<string>();
        //    foreach (DataGridViewRow row in gvEmails.Rows)
        //    {
        //        if (!row.IsNewRow)
        //        {
        //            bool isChecked = Convert.ToBoolean(row.Cells["Sending CC"].Value);
        //            if (isChecked)
        //            {
        //                string email = row.Cells["Email"].Value?.ToString();
        //                if (!string.IsNullOrEmpty(email))
        //                {
        //                    selectedEmails.Add(email);
        //                }
        //            }
        //        }
        //    }

        //    if (string.IsNullOrWhiteSpace(bodySubject))
        //    {
        //        MessageBox.Show("Email body can't be Empty.");
        //        button1.Enabled = true;
        //        return;
        //    }

        //    if (selectedEmails.Count == 0)
        //    {
        //        MessageBox.Show("No recipients found for CC.");
        //        button1.Enabled = true;
        //        return;
        //    }

        //    List<string> toEmails = myMail.GetEmailAddressesFromDatabaseTo("To", plantationCode);
        //    if (toEmails.Count == 0)
        //    {
        //        MessageBox.Show("No recipients found for To.");
        //        button1.Enabled = true;
        //        return;
        //    }

        //    //  Show loading form
        //    var loadingForm = new LoadingForm();
        //    loadingForm.Show(this);
        //    loadingForm.BringToFront();
        //    loadingForm.Refresh();

        //    bool success = false;

        //    try
        //    {
        //        await Task.Run(() =>
        //        {
        //            if (plantationName == "Bogawanthalawa Estate")
        //            {
        //                success = myMail.SendEmail(toEmails, selectedEmails, moduleName, lastVersion, plantationName, bodySubject);
        //                if (success)
        //                {
        //                    myMail.UpdateLatestVersion(lastVersion, moduleShortCode);                          
        //                    try
        //                    {
        //                        myMail.CreateLog($"{plantationName} {moduleName} Module Release", bodySubject, lastVersion, "NA", User.StatUserName, DateTime.Now);
        //                    }
        //                    catch { MessageBox.Show("Error on update Audit Log...!"); }
        //                }
        //            }
        //            else if (moduleName == "Checkroll" || moduleName == "BoughtLeaf" || moduleName == "Payroll" || moduleName == "Statutory")
        //            {
        //                success = myMail.SendEmailCheckroll(toEmails, selectedEmails, moduleName, latestBuild, plantationName, bodySubject);
        //                if (success)
        //                {
        //                    myMail.UpdateLatestBuild(latestBuild, moduleShortCode);                          
        //                    try
        //                    {
        //                        myMail.CreateLog($"{plantationName} {moduleName} Module Release", bodySubject, 0, latestBuild, User.StatUserName, DateTime.Now);
        //                    }
        //                    catch { MessageBox.Show("Error on update Audit Log...!"); }
        //                }
        //            }
        //            else
        //            {
        //                success = myMail.SendEmail(toEmails, selectedEmails, moduleName, lastVersion, plantationName, bodySubject);
        //                if (success)
        //                {
        //                    myMail.UpdateLatestVersion(lastVersion, moduleShortCode);

        //try
        //{
        //    myMail.CreateLog($"{plantationName} {moduleName} Module Release", bodySubject, lastVersion, "NA", User.StatUserName, DateTime.Now);
        //}
        //catch { MessageBox.Show("Error on update Audit Log...!"); }
        //                }
        //                txtVersion.Text = myMail.getLastVersion(moduleShortCode).ToString("N2");
        //            }
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Occurred! Contact Yasas.\n" + ex.Message);
        //    }
        //    finally
        //    {
        //        //  Hide popup
        //        loadingForm.Close();

        //        //  Show message
        //        if (success)
        //        {
        //            MessageBox.Show("Email sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Failed to send email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }

        //        // Refresh UI
        //        button1.Enabled = true;
        //        textBox2.Clear();
        //        gvEmails.DataSource = myMail.ListAllCCMails();

        //    }
        //}


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

                List<string> selectedEmails = new List<string>();

                foreach (DataGridViewRow row in gvEmails.Rows)



                    if (!row.IsNewRow)
                    {
                        bool isChecked = Convert.ToBoolean(row.Cells["Sending CC"].Value);

                        if (isChecked)
                        {
                            string email = row.Cells["Email"].Value?.ToString();
                            if (!string.IsNullOrEmpty(email))
                            {
                                selectedEmails.Add(email);
                            }
                        }
                    }


                string result = string.Join(",", selectedEmails);
                List<string> ccEmails = selectedEmails;

                List<string> toEmails = myMail.GetEmailAddressesFromDatabaseTo("To", cmbPlantation.SelectedValue.ToString());   // Fetch To emails
                //List<string> ccEmails = myMail.GetEmailAddressesFromDatabaseCC("CC");   // Fetch CC emails


                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Email body can't be Empty.");
                    button1.Enabled = true;
                }
                else if (toEmails.Count == 0)
                {
                    MessageBox.Show("No recipients found for To.");
                    button1.Enabled = true;
                }
                else if (ccEmails.Count == 0)
                {
                    MessageBox.Show("No recipients found for CC.");
                    button1.Enabled = true;
                }
                else
                {
                    if (cmbPlantation.Text.Trim() == "Bogawanthalawa Estate")
                    {
                        //SendEmail(toEmails, ccEmails);
                        bool success = myMail.SendEmail(toEmails, ccEmails, ModuleName, LastVersion, PlantationName, BodySubject);
                        if (success)
                        {
                            myMail.UpdateLatestVersion(LastVersion, ModuleShortCode);
                            MessageBox.Show("Email sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtVersion.Text = myMail.getLastVersion(cmbModule.SelectedValue.ToString()).ToString("N2");
                            button1.Enabled = true;
                            gvEmails.DataSource = myMail.ListAllCCMails();
                            textBox2.Clear();
                            try
                            {
                                myMail.CreateLog($"{PlantationName} {ModuleName} Module Release", BodySubject, LastVersion, "NA", User.StatUserName, DateTime.Now);
                            }
                            catch { MessageBox.Show("Error on update Audit Log...!"); }
                        }
                        else
                        {
                            MessageBox.Show("Failed to send email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            button1.Enabled = true;
                            gvEmails.DataSource = myMail.ListAllCCMails();
                        }
                    }
                    else
                    {
                        if (ModuleName == "Checkroll" || ModuleName == "BoughtLeaf" || ModuleName == "Payroll" || ModuleName == "Statutory")
                        {
                            //SendEmail(toEmails, ccEmails);
                            bool success = myMail.SendEmailCheckroll(toEmails, ccEmails, ModuleName, LatestBuild, PlantationName, BodySubject);
                            if (success)
                            {
                                myMail.UpdateLatestBuild(LatestBuild, ModuleShortCode);
                                MessageBox.Show("Email sent successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtBuild.Text = myMail.getLastBuild(cmbModule.SelectedValue.ToString()).ToString();
                                button1.Enabled = true;
                                gvEmails.DataSource = myMail.ListAllCCMails();
                                textBox2.Clear();
                                try
                                {
                                    myMail.CreateLog($"{PlantationName} {ModuleName} Module Release", BodySubject, 0, LatestBuild, User.StatUserName, DateTime.Now);
                                }
                                catch { MessageBox.Show("Error on update Audit Log...!"); }
                            }
                            else
                            {
                                MessageBox.Show("Failed to send email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                button1.Enabled = true;
                                gvEmails.DataSource = myMail.ListAllCCMails();
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
                                gvEmails.DataSource = myMail.ListAllCCMails();
                                textBox2.Clear();
                                try
                                {
                                    myMail.CreateLog($"{PlantationName} {ModuleName} Module Release", BodySubject, LastVersion, "NA", User.StatUserName, DateTime.Now);
                                }
                                catch { MessageBox.Show("Error on update Audit Log...!"); }
                            }
                            else
                            {
                                MessageBox.Show("Failed to send email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                button1.Enabled = true;
                                gvEmails.DataSource = myMail.ListAllCCMails();
                            }
                        }
                    }
                }

            }
            catch
            {
                MessageBox.Show("Error Occured..! Contact Yasas.");
                button1.Enabled = true;
                gvEmails.DataSource = myMail.ListAllCCMails();
            }
        }

        private void cmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPlantation.Text.Trim() == "Bogawanthalawa Estate")
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
            else
            {
                if (cmbModule.Text == "Checkroll" || cmbModule.Text == "BoughtLeaf" || cmbModule.Text == "Payroll" || cmbModule.Text == "Statutory")
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
        }

        private void cmbPlantation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLocationType.Text == "Estate")
            {
                if (cmbPlantation.Text.Trim() == "Bogawanthalawa Estate")
                {
                    cmbModule.DataSource = myMail.ListEstateModuleBPL(cmbPlantation.SelectedValue.ToString()).Tables[0];
                    cmbModule.DisplayMember = "ModuleName";
                    cmbModule.ValueMember = "ModuleShortCode";

                }
                else
                {
                    cmbModule.DataSource = myMail.ListEstateModule(cmbPlantation.SelectedValue.ToString()).Tables[0];
                    cmbModule.DisplayMember = "ModuleName";
                    cmbModule.ValueMember = "ModuleShortCode";
                }
            }
            if (cmbLocationType.Text == "Head Office")
            {
                cmbModule.DataSource = myMail.ListHOModule(cmbPlantation.SelectedValue.ToString()).Tables[0];
                cmbModule.DisplayMember = "ModuleName";
                cmbModule.ValueMember = "ModuleShortCode";
            }
            if (cmbLocationType.Text == "WareHouse")
            {
                cmbModule.DataSource = myMail.ListWareHouseModule(cmbPlantation.SelectedValue.ToString()).Tables[0];
                cmbModule.DisplayMember = "ModuleName";
                cmbModule.ValueMember = "ModuleShortCode";
            }

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

        private void cmbLocationType_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbPlantation.Font = new Font("Segoe UI", 9);
            cmbPlantation.DataSource = myMail.ListPlantations(cmbLocationType.Text.Trim()).Tables[0];
            cmbPlantation.DisplayMember = "Name";
            cmbPlantation.ValueMember = "Code";

        }
    }


}

