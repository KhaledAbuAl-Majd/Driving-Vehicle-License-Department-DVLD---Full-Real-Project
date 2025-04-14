using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsTestAppointmentsData
    {
        public static int AddNewTestAppointment(int TestTypeID,int LocalDrivingLicenseApplicationID, 
            DateTime AppointmentDate, float PaidFees,int CreatedByUserID,bool IsLocked,int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"INSERT INTO [dbo].[TestAppointments]
          ([TestTypeID],[LocalDrivingLicenseApplicationID] ,[AppointmentDate],[PaidFees],[CreatedByUserID],[IsLocked],[RetakeTestApplicationID])
          VALUES (@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,@IsLocked,@RetakeTestApplicationID);
               SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID == -1)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int AddedID))
                    TestAppointmentID = AddedID;
                else
                    TestAppointmentID = -1;
            }
            catch
            {
                TestAppointmentID = -1;
            }
            finally
            {
                connection.Close();
            }

            return TestAppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID,DateTime AppointmentDate, int CreatedByUserID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"UPDATE [dbo].[TestAppointments]
                SET [AppointmentDate] = @AppointmentDate ,[CreatedByUserID] = @CreatedByUserID
                 WHERE TestAppointmentID = @TestAppointmentID";

            //string query = @"UPDATE [dbo].[TestAppointments]
            //    SET [TestTypeID] = @TestTypeID ,[LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID
            //       ,[AppointmentDate] = @AppointmentDate ,[PaidFees] = @PaidFees ,[CreatedByUserID] = @CreatedByUserID
            //       ,[IsLocked] = @IsLocked  WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            //command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            //command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            //command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            //command.Parameters.AddWithValue("@IsLocked", IsLocked);

            try
            {
                connection.Open();

                int NumOfRowsAffected = command.ExecuteNonQuery();

                Result = (NumOfRowsAffected > 0) ? true : false;
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

        public static bool LockTestAppointment(int TestAppointmentID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"UPDATE [dbo].[TestAppointments]
                SET [IsLocked] = 1 WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                int NumOfRowsAffected = command.ExecuteNonQuery();

                Result = (NumOfRowsAffected > 0) ? true : false;
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

        public static bool GetTestAppointmentInfoByTestAppointmentID(int TestAppointmentID,ref int TestTypeID,ref int LocalDrivingLicenseApplicationID,
           ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    TestTypeID = Convert.ToInt32(reader["TestTypeID"]);
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsLocked = Convert.ToBoolean(reader["IsLocked"]);

                    if (reader["RetakeTestApplicationID"] == System.DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = Convert.ToInt32(reader["RetakeTestApplicationID"]);
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

        public static DataTable GetAllTestAppointments()
        {
            DataTable dtAllTestAppointments = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM TestAppointments";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtAllTestAppointments.Load(reader);

                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dtAllTestAppointments;
        }

        public static DataTable GetAllTestAppointmentsForSelectdL_D_LApplicationIDAndTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            DataTable dtAllTestAppointments = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"SELECT * FROM TestAppointments WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                              AND TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtAllTestAppointments.Load(reader);

                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dtAllTestAppointments;
        }

        //Un Locked
        public static bool CheckHasActiveTestAppointmentForL_D_LApplicationIDAndTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool IsHas = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"SELECT Found = 1 WHERE  EXISTS(
                            SELECT * FROM TestAppointments WHERE 
                             LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                             AND TestTypeID = @TestTypeID AND IsLocked = 0 )";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsHas = (result != null) ? true : false;
            }
            catch
            {
                IsHas = false;
            }
            finally
            {
                connection.Close();
            }

            return IsHas;
        }

        public static short GetCountOfTrails_TestAppointmentsNumberForL_D_LApplicationIDAndTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            short Count = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"select count(*) From TestAppointments 
                        WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND 
                       TestTypeID = @TestTypeID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int CountResult))
                    Count = (short)CountResult; 

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

    }
}
