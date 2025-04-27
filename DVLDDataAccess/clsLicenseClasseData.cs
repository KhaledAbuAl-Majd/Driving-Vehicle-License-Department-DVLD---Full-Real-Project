using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsLicenseClasseData
    {
        public static bool GetLicenseClassInfoByID(int LicenseClassID,ref string ClassName, ref string ClassDiscription,
            ref byte MinumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicneseClasses WHERE LicenseClassID = @LicenseClassID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ClassName = (string)reader["ClassName"];
                    ClassDiscription = (string)reader["ClassDiscription"];
                    MinumAllowedAge = Convert.ToByte(reader["MinumAllowedAge"]);
                    DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                }
                else
                    IsFound = false;    
            }
            catch
            {
                IsFound = false;    
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }
        public static bool GetLicenseClassInfoByClassName(string ClassName ,ref int LicenseClassID, ref string ClassDiscription,
            ref byte MinumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicneseClasses WHERE ClassName = @ClassName;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    ClassDiscription = (string)reader["ClassDiscription"];
                    MinumAllowedAge = Convert.ToByte(reader["MinumAllowedAge"]);
                    DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                }
                else
                    IsFound = false;    
            }
            catch
            {
                IsFound = false;    
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }
        public static DataTable GetAllLicenseClasses()
        {
            DataTable LicneseClasses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicneseClasses;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    LicneseClasses.Load(reader);

                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return LicneseClasses;
        }
    }
}
