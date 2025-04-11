using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsApplicationsData
    {
        public static int AddNewApplication(int PersonID ,DateTime ApplicationDate,int ApplicationTypeID,
            int ApplicationStatusID, DateTime LastStatusDate,float PaidFees,int CreatedByUserID)
        {
            int ApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"INSERT INTO [dbo].[Applications]
                              ([PersonID],[ApplicationDate],[ApplicationTypeID],[ApplicationStatusID]
                             ,[LastStatusDate],[PaidFees] ,[CreatedByUserID])
                              VALUES(@PersonID,@ApplicationDate,@ApplicationTypeID,@ApplicationStatusID,
                                     @LastStatusDate,@PaidFees,@CreatedByUserID);
                              SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatusID", ApplicationStatusID);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && (int.TryParse(result.ToString(), out int AddeddApplicationID)))
                {
                    ApplicationID = AddeddApplicationID;
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

        public static bool UpdateAplication(int ApplicationID,int PersonID, DateTime ApplicationDate, int ApplicationTypeID,
            int ApplicationStatusID, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"UPDATE Applications SET
                             PersonID=@PersonID,ApplicationDate=@ApplicationDate,ApplicationTypeID=@ApplicationTypeID
                            ,ApplicationStatusID=@ApplicationStatusID,LastStatusDate=@LastStatusDate,
                              PaidFees=@PaidFees,CreatedByUserID=@CreatedByUserID 
                               WHERE ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatusID", ApplicationStatusID);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static bool DeleteApplication(int ApplicationID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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

        public static bool GetApplicationInfoByApplicationID(int ApplicationID,ref int PersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
           ref int ApplicationStatusID, ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    ApplicationDate = Convert.ToDateTime(reader["ApplicationDate"]);
                    ApplicationTypeID = Convert.ToInt32(reader["ApplicationTypeID"]);
                    ApplicationStatusID = Convert.ToInt32(reader["ApplicationStatusID"]);
                    LastStatusDate = Convert.ToDateTime(reader["LastStatusDate"]);
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
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

        public static DataTable GetAllApplications()
        {
            DataTable dtApplications = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM Applications";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dtApplications.Load(reader);

                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return dtApplications;
        }
    }
}
