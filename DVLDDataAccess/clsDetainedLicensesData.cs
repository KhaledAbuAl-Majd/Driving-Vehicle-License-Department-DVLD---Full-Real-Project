using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsDetainedLicensesData
    {
        public static int DetainLicense(int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int DetainID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO DetainedLicenses (LicenseID,DetainDate,FineFees,CreatedByUserID,IsReleased)
                            VALUES (@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,0);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ID))
                    DetainID = ID;
                else
                    DetainID = -1;
            }
            catch
            {
                DetainID = -1;
            }
            finally
            {
                connection.Close();
            }

            return DetainID;
        }

        public static bool ReleaseLicense(int DetainID, DateTime ReleasedDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses SET ReleasedDate = @ReleasedDate,
            ReleasedByUserID = @ReleasedByUserID,ReleaseApplicationID = @ReleaseApplicationID, IsReleased = 1 
            WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ReleasedDate", ReleasedDate);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                int NumOfRowsAffected = command.ExecuteNonQuery();

                Result = (NumOfRowsAffected > 0);
            }
            catch
            {
                Result = false;
            }
            finally
            {
                connection.Close();
            }

            return Result;
        }

        public static bool GetDetainLicenseInfoByDetainID(int DetainID,ref int LicenseID,ref DateTime DetainDate,ref float FineFees,
          ref  int CreatedByUserID,ref bool IsReleased)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);
                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch
            {
                IsFound = true;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool GetDetainLicenseInfoByLicenseID(int LicenseID , ref int DetainID, ref DateTime DetainDate, ref float FineFees,
       ref int CreatedByUserID, ref bool IsReleased)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    DetainID = Convert.ToInt32(reader["DetainID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);
                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch
            {
                IsFound = true;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool IsLicenseDetainedByDetainID(int DetainID)
        {
            bool IsDetained = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 WHERE EXISTS(
                             SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID AND  IsReleased = 0);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsDetained = (result != null);
            }
            catch
            {
                IsDetained = false;
            }
            finally
            {
                connection.Close();
            }

            return IsDetained;
        }

        public static bool IsLicenseDetainedByLicenseID(int LicenseID)
        {
            bool IsDetained = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 WHERE EXISTS(
                             SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID AND  IsReleased = 0);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsDetained = (result != null);
            }
            catch
            {
                IsDetained = false;
            }
            finally
            {
                connection.Close();
            }

            return IsDetained;
        }

        public static DataTable GetAllLicensesDetainedAndNot()
        {
            DataTable dtAll = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM DetainedLicenses;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtAll.Load(reader);

                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dtAll;
        }
    }
}
