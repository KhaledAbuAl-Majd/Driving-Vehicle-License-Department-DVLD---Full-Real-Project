using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsTestData
    {
        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TESTS WHERE TestID = @TestID ;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = Convert.ToBoolean(reader["TestResult"]);

                    if (reader["Notes"] == System.DBNull.Value)
                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID, ref int TestID,
              ref int TestAppointmentID, ref bool TestResult,ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TOP 1 Tests.TestID, Tests.TestAppointmentID,Tests.TestResult,Tests.Notes,Tests.CreatedByUserID,Applications.ApplicantPersonID
             From TestAppointments INNER JOIN Tests ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
             INNER JOIN LocalDrivingLicenseApplications LDLApplication ON LDLApplication.LocalDrivingLicenseApplicationID
              = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN Applications ON LDLApplication.ApplicationID = Applications.ApplicationID 
             WHERE (Applications.ApplicantPersonID = @PersonID) AND (LDLApplication.LicenseClassID = @LicenseClassID) AND (TestAppointments.TestTypeID = @TestTypeID)
             ORDER BY Tests.TestAppointmentID DESC;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    TestID = (int)reader["TestID"];  
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = Convert.ToBoolean(reader["TestResult"]);

                    if (reader["Notes"] == System.DBNull.Value)
                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static DataTable GetAllTests()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Tests order by TestID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch 
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int NewTestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Tests]
                     ([TestAppointmentID],[TestResult],[Notes] ,[CreatedByUserID])
                      VALUES (@TestAppointmentID, @TestResult,@Notes,@CreatedByUserID);
                      SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);

            if (string.IsNullOrWhiteSpace(Notes))
                command.Parameters.AddWithValue("@Notes", System.DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int AddedId))
                    NewTestID = AddedId;
                else
                    NewTestID = -1;
            }
            catch
            {
                NewTestID = -1;
            }
            finally
            {
                connection.Close();
            }

            return NewTestID;
          }

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  Tests  
                            set TestAppointmentID = @TestAppointmentID,
                                TestResult=@TestResult,
                                Notes = @Notes,
                                CreatedByUserID=@CreatedByUserID
                                where TestID = @TestID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
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

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT COUNT(*) FROM  Tests INNER JOIN TestAppointments ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
             WHERE TESTS.TestResult = 1 AND LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID;";

         
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }

            return PassedTestCount;
        }

        public static bool CheckPersonPassedThisTextBefore(int LocalDrvingApplicationID,int TestTypeID)
        {
            bool IsPass = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 
                           Where Exists(
                SELECT * FROM LocalDrivingApplictions LDApp INNER JOIN
                TestAppointments ON LDApp.LocalDrvingApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                INNER JOIN Tests ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                WHERE LDApp.LocalDrvingApplicationID = @LocalDrvingApplicationID 
               AND TestAppointments.TestTypeID = @TestTypeID AND Tests.TestResult = 1);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrvingApplicationID", LocalDrvingApplicationID);
            command.Parameters.AddWithValue("@TestTypeID",TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsPass = (result != null) ? true : false;
            }
            catch
            {
                IsPass = false;
            }
            finally
            {
                connection.Close();
            }

            return IsPass;
        }
    }
}
