using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;

namespace DVLDDataAccess
{
    public static class clsLocalDrivingApplictionsData
    {
        public static int AddNewLocalDrivingAppliction(int ApplicationID, int LicenseClassID, byte PassedTests)
        {
            int LocalDrivingApplictionID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"INSERT INTO [dbo].[LocalDrivingApplictions]
                              (ApplicationID,LicenseClassID,PassedTests)
                              VALUES(@ApplicationID,@LicenseClassID,@PassedTests);
                              SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@PassedTests", PassedTests);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && (int.TryParse(result.ToString(), out int AddedID)))
                {
                    LocalDrivingApplictionID = AddedID;
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

        public static bool UpdateAplication(int LocalDrvingApplicationID, int ApplicationID, int LicenseClassID,byte PassedTests)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"UPDATE LocalDrivingApplictions SET
                              ApplicationID=@ApplicationID,LicenseClassID=@LicenseClassID, PassedTests = @PassedTests  
                               WHERE LocalDrvingApplicationID=@LocalDrvingApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@LocalDrvingApplicationID", LocalDrvingApplicationID);
            command.Parameters.AddWithValue("@PassedTests", PassedTests);

            try
            {
                connection.Open();

                int NumOfRowsAffectd = command.ExecuteNonQuery();

                Result = (NumOfRowsAffectd > 0) ? true : false;
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

        public static bool DeleteLocalDrivingApplications(int LocalDrvingApplicationID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "DELETE FROM LocalDrivingApplictions WHERE LocalDrvingApplicationID = @LocalDrvingApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrvingApplicationID", LocalDrvingApplicationID);

            try
            {
                connection.Open();

                int NumOfRowsAffectd = command.ExecuteNonQuery();

                Result = (NumOfRowsAffectd > 0) ? true : false;
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

        public static bool GetLocalDrivingApplicationsInfoByLDLApplicationID(int LocalDrvingApplicationID,ref int ApplicationID,ref int LicenseClassID,ref byte PassedTests)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingApplictions WHERE LocalDrvingApplicationID = @LocalDrvingApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrvingApplicationID", LocalDrvingApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    PassedTests = Convert.ToByte(reader["PassedTests"]);
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
        
        public static bool GetLocalDrivingApplicationsInfoByApplicationID(int ApplicationID, ref int LocalDrvingApplicationID, ref int LicenseClassID,ref byte PassedTests)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingApplictions WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    LocalDrvingApplicationID = Convert.ToInt32(reader["LocalDrvingApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    PassedTests = Convert.ToByte(reader["PassedTests"]);
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

        public static DataTable GetAllLocalDrivingApplications()
        {
            DataTable dtLocalDrivingApplictionss = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM LocalDrivingApplictions";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtLocalDrivingApplictionss.Load(reader);

                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dtLocalDrivingApplictionss;
        } 

        public static int GetApplicationIDIfPersonHasActiveApplicationFromThisClass(int PersonID,int LicenseClassID)
        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"SELECT LDApplications.ApplicationID  FROM LocalDrivingApplictions LDApplications
              INNER JOIN Applications ON LDApplications.ApplicationID = Applications.ApplicationID 
              INNER JOIN ApplicationStatuses ON ApplicationStatuses.ApplicationStatusID  = Applications.ApplictionStatusID
              Where ApplicationStatuses.ApplicationStatus = 'New' AND LicenseClassID = @LicenseClassID
              AND PersonID = @PersonID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(),out int ID))
                {
                    ApplicationID = ID;
                }
                else
                    ApplicationID = -1;
            }
            catch
            {
                ApplicationID = -1;
            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;
        }

        //To Show All Data For FK For All LDLApplications
        public static DataTable GetAllLocalDrivingApplicationsWithINNERJOIN()
        {
            DataTable dtAll = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"SELECT * FROM LocalDrivingApplictions INNER JOIN Applications
             ON LocalDrivingApplictions.ApplicationID = Applications.ApplicationID  INNER JOIN
             People ON Applications.PersonID = People.PersonID  INNER JOIN LicneseClasses 
              ON LocalDrivingApplictions.LicenseClassID = LicneseClasses.LicenseClassID
              INNER JOIN ApplicationStatuses ON ApplicationStatuses.ApplicationStatusID
               = Applications.ApplicationStatusID;";

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

        //To Show All Data For FK For a selected LDLApplication
        public static DataTable GetLocalDrivingApplicationInfoWithINNERJOIN(int LocalDrvingApplicationID)
        {
            DataTable dtAll = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"SELECT * FROM LocalDrivingApplictions INNER JOIN Applications
             ON LocalDrivingApplictions.ApplicationID = Applications.ApplicationID  INNER JOIN
             People ON Applications.PersonID = People.PersonID  INNER JOIN LicneseClasses 
              ON LocalDrivingApplictions.LicenseClassID = LicneseClasses.LicenseClassID
              INNER JOIN ApplicationStatuses ON ApplicationStatuses.ApplicationStatusID
               = Applications.ApplicationStatusID WHERE LocalDrvingApplicationID = @LocalDrvingApplicationID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@int LocalDrvingApplicationID", LocalDrvingApplicationID);

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
