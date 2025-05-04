using DataAccess;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class User
    {
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
    }
    
}
