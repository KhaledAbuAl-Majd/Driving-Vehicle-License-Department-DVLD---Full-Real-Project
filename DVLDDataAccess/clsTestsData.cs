using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsTestsData
    {
        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int NewTestID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

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

        public static bool CheckPersonPassedThisTextBefore(int LocalDrvingApplicationID,int TestTypeID)
        {
            bool IsPass = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

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
