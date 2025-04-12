using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsLicensesData
    {
        public static int AddNewLicnese(int ApplicationID, int DriverID, int LicenseClassID, DateTime IsuueDate, DateTime ExpirationDate,
            string Notes, float PaidFees, bool IsAcitve, string IssueReason, int CreatedByUserID)
        {
            int LicneseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Licenses]
                     ([ApplicationID] ,[DriverID],[LicenseClassID] ,[IsuueDate] ,[ExpirationDate] 
                     ,[Notes],[PaidFees] ,[IsAcitve],[IssueReason] ,[CreatedByUserID])
                     VALUES (@ApplicationID,@DriverID,@LicenseClassID,@IsuueDate,@ExpirationDate
                            ,@Notes,@PaidFees ,@IsAcitve ,@IssueReason,@CreatedByUserID);
                     SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@IsuueDate", IsuueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (string.IsNullOrWhiteSpace(Notes))
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsAcitve", IsAcitve);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int AddedID))
                {
                    LicneseID = AddedID;
                }
                else
                    LicneseID = -1;
            }
            catch
            {
                LicneseID = -1;
            }
            finally
            {
                connection.Close();
            }

            return LicneseID;
        }

        public static bool GetLicneseInfoByLicneseID(int LicneseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID, ref DateTime IsuueDate,
            ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsAcitve, ref string IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE LicneseID = @LicneseID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicneseID", LicneseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    IsuueDate = Convert.ToDateTime(reader["IsuueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);

                    if (reader["Notes"] == System.DBNull.Value)
                        Notes = "";
                    else
                        Notes = Convert.ToString(reader["Notes"]);

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsAcitve = Convert.ToBoolean(reader["IsAcitve"]);
                    IssueReason = Convert.ToString(reader["IssueReason"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                }
                else
                    IsFound = false;

                reader.Close();
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

        public static bool GetLicneseInfoByApplicationID(int ApplicationID , ref  int LicneseID, ref int DriverID, ref int LicenseClassID, ref DateTime IsuueDate,
          ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsAcitve, ref string IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE ApplicationID = @ApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    LicneseID = Convert.ToInt32(reader["LicneseID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    IsuueDate = Convert.ToDateTime(reader["IsuueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);

                    if (reader["Notes"] == System.DBNull.Value)
                        Notes = "";
                    else
                        Notes = Convert.ToString(reader["Notes"]);

                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsAcitve = Convert.ToBoolean(reader["IsAcitve"]);
                    IssueReason = Convert.ToString(reader["IssueReason"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                }
                else
                    IsFound = false;

                reader.Close();
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
        public static DataTable GetAllLicneses()
        {
            DataTable dtAllLicneses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM Licenses;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtAllLicneses.Load(reader);

                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dtAllLicneses;
        }

        public static DataTable GetAllLicnesesByDriverID(int DriverID)
        {
            DataTable dtAllLicneses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM Licenses WHERE DriverID = @DriverID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtAllLicneses.Load(reader);

                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dtAllLicneses;
        }

        public static bool IsPersonHaveAnActiveLicneseWithTheSameLicneseClass(int PersonID,int LicenseClassID)  
        {
            bool IsHave = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"SELECT Found = 1 WHERE EXISTS(
                             SELECT * FROM Licenses INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
                             WHERE Drivers.PersonID = @PersonID AND Licenses.LicenseClassID = @LicenseClassID
                             AND Licenses.IsAcitve = 1);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsHave = (result != null);
            }
            catch
            {
                IsHave = false; 
            }
            finally
            {
                connection.Close();
            }

            return IsHave;
        }

        public static byte GetNumberOfActiveLicnesesByDriverID(int DriverID)
        {
            byte Count = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT COUNT(*) FROM Licenses WHERE DriverID = @DriverID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int Number))
                    Count = Convert.ToByte(Number);
                else
                    Count = 0;
            }
            catch
            {
                Count = 0;
            }
            finally
            {
                connection.Close();
            }

            return Count;
        }

        public static bool IsLicenseExist(int LicneseID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"SELECT Found = 1 WHERE EXISTS(
                            SELECT * FROM Licenses WHERE LicneseID = @LicneseID);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicneseID", LicneseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsFound = (result != null);
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

    }
}
