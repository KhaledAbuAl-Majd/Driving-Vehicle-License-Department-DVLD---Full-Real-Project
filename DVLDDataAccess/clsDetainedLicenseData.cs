using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDConstant;

namespace DVLDDataAccess
{
    public static class clsDetainedLicenseData
    {
        public static bool GetDetainedLicenseInfoByID(int DetainID, ref int LicenseID, ref DateTime DetainDate, ref float FineFees,
          ref int CreatedByUserID, ref bool IsReleased,ref DateTime ReleasedDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
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

                    if (reader["ReleasedDate"] == DBNull.Value)
                        ReleasedDate = DateTime.MaxValue;
                    else
                        ReleasedDate = (DateTime)reader["ReleasedDate"];

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = true;

                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID , ref int DetainID, ref DateTime DetainDate, ref float FineFees,
       ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleasedDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            //My Solution
            string query = @"SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

            //Doctor Solution
            // string query = "SELECT top 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID order by DetainID desc";

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

                    if (reader["ReleasedDate"] == DBNull.Value)
                        ReleasedDate = DateTime.MaxValue;
                    else
                        ReleasedDate = (DateTime)reader["ReleasedDate"];

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = true;

                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dtAll = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM detainedLicenses_View ORDER BY IsReleased, DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtAll.Load(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dtAll;
        }

        //DetainLicense
        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID)
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
            catch (Exception ex)
            {
                DetainID = -1;

                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return DetainID;
        }

        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,float FineFees, int CreatedByUserID)
        {
            bool Result = false;
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE dbo.DetainedLicenses
                              SET LicenseID = @LicenseID, 
                              DetainDate = @DetainDate, 
                              FineFees = @FineFees,
                              CreatedByUserID = @CreatedByUserID,   
                              WHERE DetainID=@DetainID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DetainedLicenseID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

                Result = (rowsAffected > 0);

            }
            catch (Exception ex)
            {
                Result = false;
                clsLogger.LogAtEventLog(ex.Message);
            }

            finally
            {
                connection.Close();
            }

            return Result ;
        }

        public static bool ReleaseDetainedLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE DetainedLicenses SET ReleasedDate = @ReleasedDate,
            ReleasedByUserID = @ReleasedByUserID,ReleaseApplicationID = @ReleaseApplicationID, IsReleased = 1 
            WHERE DetainID = @DetainID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            command.Parameters.AddWithValue("@ReleasedDate", DateTime.Now);

            try
            {
                connection.Open();

                int NumOfRowsAffected = command.ExecuteNonQuery();

                Result = (NumOfRowsAffected > 0);
            }
            catch (Exception ex)
            {
                Result = false;

                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return Result;
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetained = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT IsDetained = 1 FROM DetainedLicenses WHERE LicenseID = @LicenseID AND  IsReleased = 0;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsDetained = (result != null);
            }
            catch (Exception ex)
            {
                IsDetained = false;

                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsDetained;
        }

    }
}
