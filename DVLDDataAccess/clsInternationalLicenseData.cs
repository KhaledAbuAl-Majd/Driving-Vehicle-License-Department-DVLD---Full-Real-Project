using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsInternationalLicenseData
    {
        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    IssuedUsingLocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
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

        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dtAllLicneses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT InternationalLicenseID,ApplicationID,DriverID,
             IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive
             FROM InternationalLicenses ORDER BY IsActive, ExpirationDate DESC;";

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

        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            DataTable dtAllLicneses = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT InternationalLicenseID,ApplicationID,DriverID,
             IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive
             FROM InternationalLicenses WHERE DriverID = @DriverID
             ORDER BY ExpirationDate DESC;";

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

        public static int AddNewInternationalLicense(int ApplicationID,int DriverID,int IssuedUsingLocalLicenseID,
          DateTime IssueDate,DateTime ExpirationDate,bool IsActive,int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            //DeActivate Old International LIcenses , he can have only one Active Licnese
            string query = @"
              UPDATE InternationalLicenses SET IsActive = 0 WHERE DriverID = @DriverID;

             INSERT INTO [dbo].[InternationalLicenses]
            ([ApplicationID],[DriverID] ,[IssuedUsingLocalLicenseID],[IssueDate],
             [ExpirationDate] ,[IsActive],[CreatedByUserID])  VALUES
           (@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID);
            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                    InternationalLicenseID = ID;
                else
                    InternationalLicenseID = -1;
            }
            catch
            {
                InternationalLicenseID = -1;
            }
            finally
            {
                connection.Close();
            }

            return InternationalLicenseID;
        }

        public static bool UpdateInternationalLicense( int InternationalLicenseID, int ApplicationID,  int DriverID, int IssuedUsingLocalLicenseID,
           DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE InternationalLicenses
                           SET 
                              ApplicationID=@ApplicationID,
                              DriverID = @DriverID,
                              IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,
                              IssueDate = @IssueDate,
                              ExpirationDate = @ExpirationDate,
                              IsActive = @IsActive,
                              CreatedByUserID = @CreatedByUserID
                         WHERE InternationalLicenseID=@InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            command.Parameters.AddWithValue("@IsActive", IsActive);
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

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int InternationalLicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            //string query = @"SELECT InternationalLicenses.InternationalLicenseID FROM InternationalLicenses 
            //    WHERE DriverID = @DriverID AND IsActive = 1";

            string query = @"  
                            SELECT Top 1 InternationalLicenseID
                            FROM InternationalLicenses 
                            where DriverID=@DriverID and GetDate() between IssueDate and ExpirationDate 
                            order by ExpirationDate Desc;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                    InternationalLicenseID = ID;
                else
                    InternationalLicenseID = -1;
            }
            catch
            {
                InternationalLicenseID = -1;
            }
            finally
            {
                connection.Close();
            }

            return InternationalLicenseID;
        }

        public static int GetInternationalLicenseIfPersonHasActiveOne(int PersonID)
        {
            int InternationalLicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT InternationalLicenses.InternationalLicenseID FROM InternationalLicenses 
             INNER JOIN Drivers ON InternationalLicenses.DriverID = Drivers.DriverID WHERE
             Drivers.PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                    InternationalLicenseID = ID;
                else
                    InternationalLicenseID = -1;
            }
            catch
            {
                InternationalLicenseID = -1;
            }
            finally
            {
                connection.Close();
            }

            return InternationalLicenseID;
        }
    }
}
