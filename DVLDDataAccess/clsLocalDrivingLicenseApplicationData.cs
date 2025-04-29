using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace DVLDDataAccess
{
    public static class clsLocalDrivingLicenseApplicationData
    {
        public static bool GetLocalDrivingLicenseApplicationInfoByID(int LocalDrivingLicenseApplicationID,ref int ApplicationID,ref int LicenseClassID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
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
        
        public static bool GetLocalDrivingLicenseApplicationInfoByApplicationID(int ApplicationID, ref int LocalDrivingLicenseApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
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

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingLicenseApplications_View ORDER BY ApplicationDate DESC;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dt;
        } 

        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingApplictionID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[LocalDrivingLicenseApplications]
                              (ApplicationID,LicenseClassID)
                              VALUES(@ApplicationID,@LicenseClassID);
                              SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && (int.TryParse(result.ToString(), out int InsertedID)))
                {
                    LocalDrivingApplictionID = InsertedID;
                }
                else
                    LocalDrivingApplictionID = -1;
            }
            catch
            {
                LocalDrivingApplictionID = -1;
            }
            finally
            {
                connection.Close();
            }

            return LocalDrivingApplictionID;
        }

        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE LocalDrivingLicenseApplications SET
                              ApplicationID=@ApplicationID,LicenseClassID=@LicenseClassID 
                               WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                int NumOfRowsAffectd = command.ExecuteNonQuery();

                Result = (NumOfRowsAffectd > 0);
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

        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                int NumOfRowsAffectd = command.ExecuteNonQuery();

                Result = (NumOfRowsAffectd > 0);
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

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool HasPassed = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            //Doctor Solution
            // It Git The Last Test Result 

            string query = @"SELECT Top 1 Tests.TestResult  FROM LocalDrivingLicenseApplications LDLApp INNER JOIN
            TestAppointments ON LDLApp.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
            INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
            WHERE (LDLApp.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) AND (TestAppointments.TestTypeID = @TestTypeID) 
            ORDER BY TestAppointments.TestAppointmentID DESC;";

            // My Solution, it's best but i want to do as doctor to be with him
            //string query = @"SELECT IsPass = 1 FROM LocalDrivingLicenseApplications LDLApp INNER JOIN 
            // TestAppointments ON LDLApp.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
            // INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID 
            // WHERE (LDLApp.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
            //AND (TestAppointments.TestTypeID = @TestTypeID) AND (Tests.TestResult = 1);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && bool.TryParse(result.ToString(), out bool ReturnedResult))
                {
                    HasPassed = ReturnedResult;
                }

                // My Solution
                //HasPassed = (result != null);
            }
            catch
            {
                HasPassed = false;
            }
            finally
            {
                connection.Close();
            }

            return HasPassed;
        }

        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            //To Check if the first time , or no to make it retake test

            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TOP 1 Found = 1 FROM LocalDrivingLicenseApplications LDLApp INNER JOIN
            TestAppointments ON LDLApp.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
            INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
            WHERE (LDLApp.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) AND (TestAppointments.TestTypeID = @TestTypeID) 
            ORDER BY TestAppointments.TestAppointmentID DESC;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            byte TotalTrialsPerTestCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TotalTrialsPerTest = COUNT(Tests.TestID) FROM LocalDrivingLicenseApplications LDLApp INNER JOIN
             TestAppointments ON LDLApp.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
             INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
             WHERE (LDLApp.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
             AND (TestAppointments.TestTypeID = @TestTypeID);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte Trials))
                {
                    TotalTrialsPerTestCount = Trials;
                }

            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }

            return TotalTrialsPerTestCount;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Top 1 Found = 1  FROM LocalDrivingLicenseApplications LDLApp INNER JOIN
            TestAppointments ON LDLApp.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
            WHERE (LDLApp.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
            AND (TestAppointments.TestTypeID = @TestTypeID)  AND (IsLocked = 0)
            ORDER BY TestAppointments.TestAppointmentID DESC;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                Result = (result != null);
             
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
    }
}
