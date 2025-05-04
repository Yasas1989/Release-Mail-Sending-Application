using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataAccess;

namespace BusinessLayer
{
    public class GetMails
    {
        public string ShortCode { get; set; }
        public int Code { get; set; }  


        public DataSet ListPlantations()
        {

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT TOP (100) PERCENT Code, Name FROM dbo.PlantationDetails ORDER BY Code", CommandType.Text);
            da.Fill(ds, "PlantationList");
            return ds;
        }

        public void UpdateLatestVersion(Decimal LastVersion, String ModuleShortCode)
        {
            SQLHelper.ExecuteNonQuery("Update dbo.ModuleDetails set LastVersionNumber = '" + LastVersion + "' where ModuleShortCode =  '" + ModuleShortCode + "'", CommandType.Text);
        }

        public DataSet ListModule(String Code)
        {

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            da.SelectCommand = SQLHelper.CreateCommand("SELECT Code, ModuleName, ModuleShortCode FROM     dbo.ModuleDetails WHERE  (Code = '" + Code + "') OR (Code = 'COM')", CommandType.Text);
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
