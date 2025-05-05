using DataAccess;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
        public static String StatEmail { get; set; }
        public bool LoginUser(string username, string password)
        {
            string storedHash = null;
            bool isValid = false;
            SqlDataReader reader;
            reader = DataAccess.SQLHelper.ExecuteReader("SELECT PasswordHash FROM Users WHERE Username = '" + username + "'", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    storedHash = reader.GetString(0);
                    isValid = BCrypt.Net.BCrypt.Verify(password, storedHash);

                }
                else
                {
                    isValid = false;

                }
            }
            return isValid;

        }
        public void InsertPassword(String UserName, String hashedPassword)
        {
            SQLHelper.ExecuteNonQuery("INSERT INTO Users (Username, PasswordHash) VALUES ('" + UserName + "', '" + hashedPassword + "')", CommandType.Text);
        }

        public String CheckValidEmail(string UserName)
        {
            String Email = String.Empty;
            SqlDataReader reader;
            reader = DataAccess.SQLHelper.ExecuteReader("SELECT Email, Username FROM     dbo.Users WHERE  Username = '" + UserName + "'", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    //Email = reader.GetString(0);
                    Email = reader.GetString(0);
                    StatEmail = Email;

                }
            }
            return Email;

        }
        public void SendPasswordResetCode(string UserName)
        {
            //String Email = "";
            string code = Guid.NewGuid().ToString().Substring(0, 6).ToUpper(); // Example: "A9F3B2"
            DateTime Dtime = DateTime.Now;

            //SqlDataReader reader;
            //reader = DataAccess.SQLHelper.ExecuteReader("SELECT Email, Username FROM     dbo.Users WHERE  Username = '" + UserName + "'", CommandType.Text);
            //while (reader.Read())
            //{
            //    if (!reader.IsDBNull(0))
            //    {
            //        Email = reader.GetString(0);
            //    }
            //}
            SQLHelper.ExecuteNonQuery("UPDATE Users SET ResetCode = '" + code + "', ResetCodeCreatedAt = '" + Dtime + "'  WHERE Email = '" + StatEmail + "'", CommandType.Text);

            // Now send the email
            MailMessage message = new MailMessage("yasas@ftservices.net", StatEmail);
            message.Subject = "This is Your Password Reset Code";
            message.Body = $"Your password reset code is: {code}. It will expire in 2 minutes.";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("yasas@ftservices.net", "xyor bpvv kxxw frep"),
                EnableSsl = true
            };
            smtp.Send(message);
        }

        public String ValidateResetCode(string inputCode)
        {
            string storedCode = null;
            DateTime? createdAt = null;
            String Status = "";

            SqlDataReader reader;
            reader = DataAccess.SQLHelper.ExecuteReader("SELECT ResetCode, ResetCodeCreatedAt FROM Users WHERE Email = '" + StatEmail + "'", CommandType.Text);
            while (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    storedCode = reader.IsDBNull(0) ? null : reader.GetString(0);

                }
                if (!reader.IsDBNull(1))
                {
                    createdAt = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1);
                }
            }         

            if (storedCode == null || createdAt == null)
            {
               // Console.WriteLine("No reset request found.");
                Status = "NRRF";
            }

            if (storedCode != inputCode)
            {
                Console.WriteLine("Invalid code.");
                Status = "IC";
            }

            if ((DateTime.Now - createdAt.Value).TotalMinutes > 2)
            {
                Console.WriteLine("Code expired.");
                Status = "CE";
            }

            return Status;
        }

    }

}
