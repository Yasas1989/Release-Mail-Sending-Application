using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataAccess;
using System.Net.Mail;
using System.Net;

namespace BusinessLayer
{
    public class GetMails
    {
        BusinessLayer.User myUser = new BusinessLayer.User();       
     
        public DataSet ListPlantations(String LocationType)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT TOP (100) PERCENT Code, Name, LocationType FROM dbo.PlantationDetails WHERE LocationType = '" + LocationType + "'", CommandType.Text);
            da.Fill(ds, "PlantationList");
            return ds;
        }

        public DataSet ListEstateLocation()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT TOP (100) PERCENT LocationType FROM     dbo.PlantationDetails GROUP BY LocationType", CommandType.Text);
            da.Fill(ds, "LocationType");
            return ds;
        }

        public void UpdateLatestVersion(Decimal LastVersion, String ModuleShortCode)
        {
            SQLHelper.ExecuteNonQuery("Update dbo.ModuleDetails set LastVersionNumber = '" + LastVersion + "' where ModuleShortCode =  '" + ModuleShortCode + "'", CommandType.Text);
        }
        public void UpdateLatestBuild(String LatestBuild, String ModuleShortCode)
        {
            SQLHelper.ExecuteNonQuery("Update dbo.ModuleDetails set BuildNumber = '" + LatestBuild + "' where ModuleShortCode =  '" + ModuleShortCode + "'", CommandType.Text);
        }

        public DataSet ListHOModule(String Code)
        {

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT Code, ModuleName, ModuleShortCode FROM     dbo.ModuleDetails WHERE  (Code = '" + Code + "') OR (Code = 'COMHO')", CommandType.Text);
            da.Fill(ds, "PlantationList");
            return ds;
        }
        public DataSet ListWareHouseModule(String Code)
        {

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT Code, ModuleName, ModuleShortCode FROM     dbo.ModuleDetails WHERE  (Code = '" + Code + "')", CommandType.Text);
            da.Fill(ds, "PlantationList");
            return ds;
        }

        public DataSet ListEstateModule(String Code)
        {

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT Code, ModuleName, ModuleShortCode FROM     dbo.ModuleDetails WHERE  (Code = '" + Code + "') OR (Code = 'COMES')", CommandType.Text);
            da.Fill(ds, "PlantationList");
            return ds;
        }
        public DataSet ListEstateModuleBPL(String Code)
        {

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT Code, ModuleName, ModuleShortCode FROM     dbo.ModuleDetails WHERE  (Code = '" + Code + "')", CommandType.Text);
            da.Fill(ds, "PlantationList");
            return ds;
        }

        public decimal getLastVersion(String ModuleShortCode)
        {
            Decimal Version = 0;           
            SqlDataReader reader = SQLHelper.ExecuteReader("SELECT LastVersionNumber, Code, ModuleName, ModuleShortCode FROM     dbo.ModuleDetails WHERE  (ModuleShortCode = '" + ModuleShortCode + "')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    Version = reader.GetDecimal(0) + 0.01m;
                }
                else
                {
                    Version = +0.01m;
                }
            }          
            return Version;
        }

        public String getLastBuild(String ModuleShortCode)
        {
            String Build = "";
            SqlDataReader reader = SQLHelper.ExecuteReader("SELECT BuildNumber, Code, ModuleName, ModuleShortCode FROM     dbo.ModuleDetails WHERE  (ModuleShortCode = '" + ModuleShortCode + "')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    Build = reader.GetString(0).Trim();
                }

                string[] parts = Build.Split('_');
                if (int.TryParse(parts.Last(), out int lastNumber))
                {
                    lastNumber += 1;
                    parts[parts.Length - 1] = lastNumber.ToString();
                    string newValue = string.Join("_", parts);

                    Build = newValue; // Bind to your TextBox
                }

            }
            return Build;
        }
<<<<<<< HEAD
        
=======

        public bool SendEmailPayroll(List<string> toEmails, List<string> ccEmails, String ModuleName, Decimal LastVersion, String PlantationName, String BodySubject)
        {

            try
            {                              
                string fromEmail = $"{User.StatEmail}";               
                string fromPassword = $"{User.StatAppPassword}";
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
                        "<span style='padding-left:0px;'>OLAX Team <br> <br>"+

                        "<span style='font-family:Tahoma; font-size:10px; 'padding-left:0px;'>This is a system-generated release notification. If you encounter any issues with this release, feel free to reply to this email.",
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
>>>>>>> 8932502bee1cfb33a7703dbe0a84a29a8848cfcc
        public bool SendEmailCheckroll(List<string> toEmails, List<string> ccEmails, String ModuleName, String LatestBuild, String PlantationName, String BodySubject)
        {

            try
            {
                //string fromEmail = "yasas@ftservices.net";
                //string fromPassword = "xyor bpvv kxxw frep";               
                string fromEmail = $"{User.StatEmail}";
                string fromPassword = $"{User.StatAppPassword}";
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
                        Subject = $"Olax System Update - {PlantationName} {ModuleName} Module Release (Build {LatestBuild})",

                        Body =

                        "<span style='font-family:Tahoma; font-size:17px; padding-left:0px;'>Dear All,<br>" +
                        "<span style='padding-left:0px;'>There is a new update from OLAX Systems.</span><br><br>" +
                        "<span style='padding-left:0px;'>Release Contains:</span><br><br>" +
                        $"<span style='padding-left:40px;'><b>1. {PlantationName} {ModuleName} Module Release (Build {LatestBuild}) </b> We have done:</span><br><br>" +
                        "<ul style='margin-top:0px; margin-bottom:0px; '>" +
                        $"{formatted}<br><br>" +
                        "</ul>" +
                        "<span style='padding-left:-0px;'>Kindly download the latest version.<br><br>" +
                        "<span style='padding-left:0px;'>Thanks and Best Regards,<br>" +
                        "<span style='padding-left:0px;'>OLAX Team <br> <br>"+

                        "<span style='font-family:Tahoma; font-size:10px; 'padding-left:0px;'>This is a system-generated release notification. If you encounter any issues with this release, feel free to reply to this email.",
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
        public bool SendEmail(List<string> toEmails, List<string> ccEmails, String ModuleName, Decimal LastVersion, String PlantationName, String BodySubject)
        {
            try
            {
                //string fromEmail = "yasas@ftservices.net";
                //string fromPassword = "xyor bpvv kxxw frep";
                string fromEmail = $"{User.StatEmail}";
                string fromPassword = $"{User.StatAppPassword}";
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
                        Subject = $"Olax System Update - {PlantationName} {ModuleName} Module Release V {LastVersion}",

                        Body =

                        "<span style='font-family:Tahoma; font-size:17px; padding-left:0px;'>Dear All,<br>" +
                        "<span style='padding-left:0px;'>There is a new update from OLAX Systems.</span><br><br>" +
                        "<span style='padding-left:0px;'>Release Contains:</span><br><br>" +
                        $"<span style='padding-left:40px;'><b>1. {PlantationName} {ModuleName} Module Release V {LastVersion}</b> We have done:</span><br><br>" +
                        "<ul style='margin-top:0px; margin-bottom:0px; '>" +
                        $"{formatted}<br><br>" +
                        "</ul>" +
                        "<span style='padding-left:-0px;'>Kindly download the latest version.<br><br>" +
                        "<span style='padding-left:0px;'>Thanks and Best Regards,<br>" +
                        "<span style='padding-left:0px;'>OLAX Team <br> <br>" +

                        "<span style='font-family:Tahoma; font-size:10px; 'padding-left:0px;'>This is a system-generated release notification. If you encounter any issues with this release, feel free to reply to this email.",
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
        public List<string> GetEmailAddressesFromDatabaseCC(string emailType)
        {

            List<string> emails = new List<string>();
            SqlDataReader reader;

            //reader = SQLHelper.ExecuteReader("SELECT Email FROM     dbo.EmailDetails WHERE  (Code = '" + Code + "') AND (Type = '" + emailType + "')", CommandType.Text);
            reader = SQLHelper.ExecuteReader("SELECT Email FROM     dbo.EmailDetails WHERE (Type = '" + emailType + "' AND Active = 1)", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    emails.Add(reader["Email"].ToString());
                }
            }
            reader.Close();

            return emails;
        }
        public DataTable ListAllCCMails()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Name"));
            dt.Columns.Add(new DataColumn("Email"));
            dt.Columns.Add(new DataColumn("Type"));
            dt.Columns.Add(new DataColumn("Sending CC", typeof(bool)));
            
            DataRow dtrow;
            SqlDataReader dataReader;
            dtrow = dt.NewRow();
            dataReader = SQLHelper.ExecuteReader("SELECT  Name, Email, Type, Active  FROM     dbo.EmailDetails WHERE  (Type = 'CC')", CommandType.Text);

            while (dataReader.Read())
            {
                dtrow = dt.NewRow();

                if (!dataReader.IsDBNull(0))
                {
                    dtrow[0] = dataReader.GetString(0).Trim();
                }
                if (!dataReader.IsDBNull(1))
                {
                    dtrow[1] = dataReader.GetString(1).Trim();
                }
                if (!dataReader.IsDBNull(2))
                {
                    dtrow[2] = dataReader.GetString(2).Trim();
                }
                if (!dataReader.IsDBNull(3))
                {
                    dtrow[3] = dataReader.GetBoolean(3);
                }               
                dt.Rows.Add(dtrow);
            }
            dataReader.Close();
            return dt;

        }
        public List<string> GetEmailAddressesFromDatabaseTo(string emailType, String Code)
        {

            List<string> emails = new List<string>();
            SqlDataReader reader;

            reader = SQLHelper.ExecuteReader("SELECT Email FROM     dbo.EmailDetails WHERE  (Code = '" + Code + "') AND (Type = '" + emailType + "' AND Active = 1)", CommandType.Text);
            //reader = SQLHelper.ExecuteReader("SELECT Email FROM     dbo.EmailDetails WHERE (Type = '" + emailType + "')", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    emails.Add(reader["Email"].ToString());
                }
            }
            reader.Close();

            return emails;
        }

    }
}
