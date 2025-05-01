using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsLicenseData
    {
        public static bool GetLicenseInfoByID(int LicneseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID, ref DateTime IsuueDate,
            ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsAcitve, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

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
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
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
          ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsAcitve, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

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
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
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

        public static DataTable GetAllLicenses()
        {
            DataTable dtAllLicneses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

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

        public static DataTable GetDriverLicenses(int DriverID)
        {
            DataTable dtAllLicneses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" SELECT Licenses.LicneseID,Licenses.ApplicationID,LicneseClasses.ClassName,Licenses.IsuueDate,
              Licenses.ExpirationDate,Licenses.IsAcitve FROM Licenses INNER JOIN LicneseClasses ON 
             Licenses.LicenseClassID = LicneseClasses.LicenseClassID
             WHERE DriverID = @DriverID ORDER BY IsAcitve DESC, IsuueDate DESC;";

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

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClassID, DateTime IsuueDate, DateTime ExpirationDate,
            string Notes, float PaidFees, bool IsAcitve, byte IssueReason, int CreatedByUserID)
        {
            int LicneseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

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

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    LicneseID = InsertedID;
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

        public static bool UpdateLicense(int LicneseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IsuueDate, DateTime ExpirationDate,
            string Notes, float PaidFees, bool IsAcitve, byte IssueReason, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE [dbo].[Licenses]
                 SET [ApplicationID] = @ApplicationID,[DriverID] = @riverID ,[LicenseClassID] = @LicenseClassID
                    ,[IsuueDate] = @IsuueDate ,[ExpirationDate] = @ExpirationDate ,[Notes] = @Notes
                    ,[PaidFees] = @PaidFees ,[IsAcitve] = @IsAcitve ,[IssueReason] = @IssueReason
                    ,[CreatedByUserID] = @CreatedByUserID WHERE LicneseID = @LicneseID;";


            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicneseID", LicneseID);
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
                rowsAffected = command.ExecuteNonQuery();

            }
            catch 
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Licenses.LicneseID
             FROM Licenses INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
             WHERE Licenses.LicenseClassID = @LicenseClassID  AND Drivers.PersonID = @PersonID And IsAcitve=1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    LicenseID = ID;
                }
            }

            catch 
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return LicenseID;
        }

        public static bool DeactivateLicense(int LicneseID)
        {
            bool IsDeActivated = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update Licenses SET IsAcitve = 0 WHERE LicneseID = @LicneseID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicneseID", LicneseID);

            try
            {
                connection.Open();

                int NumOfRowsAffected = command.ExecuteNonQuery();

                IsDeActivated = (NumOfRowsAffected > 0);
            }
            catch
            {
                IsDeActivated = false;
            }
            finally
            {
                connection.Close();
            }

            return IsDeActivated;
        }

        public static bool IsLicenseExist(int LicneseID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

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

        //
        public static bool IsPersonHaveLicenseActiveANDNOT(int PersonID,int LicenseClassID)  
        {
            bool IsHave = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 WHERE EXISTS(
                             SELECT * FROM Licenses INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID
                             WHERE Drivers.PersonID = @PersonID AND Licenses.LicenseClassID = @LicenseClassID);";

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

        public static int GetLicenseIDByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Licenses.LicneseID FROM Licenses INNER JOIN LocalDrivingLicenseApplications LDLApplication 
            ON Licenses.ApplicationID = LDLApplication.ApplicationID
              WHERE LDLApplication.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    LicenseID = ID;
                }
            }

            catch
            {
                LicenseID = -1;

            }

            finally
            {
                connection.Close();
            }


            return LicenseID;
        }
    }
}
