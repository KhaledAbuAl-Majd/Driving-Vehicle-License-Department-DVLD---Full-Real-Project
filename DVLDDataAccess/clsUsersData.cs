using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsUsersData
    {
        public static int AddNewUser(int PersonID,string UserName,string Password,bool IsActive)
        {
            int UserID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"Insert Into Users (PersonID,UserName,Password,IsActive) 
                             Values(@PersonID,@UserName,@Password,@IsActive);
                              SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if(result!=null && int.TryParse(result.ToString(),out int AddeddUserID))
                {
                    UserID = AddeddUserID;
                }
                else
                {
                    UserID = -1;
                }

            }
            catch
            {
                UserID = -1;
            }
            finally
            {
                connection.Close();
            }

            return UserID;
        }

        public static bool UpdateUser(int UserID,int PersonID,string UserName,string Password,bool IsActive)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"UPDATE Users 
                             SET PersonID = @PersonID, UserName = @UserName,
                              Password = @Password, IsActive = @IsActive
                             Where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();

                int NumOfRowsAffected = command.ExecuteNonQuery();

                if (NumOfRowsAffected > 0)
                    Result = true;
                else
                    Result = false;
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

        public static bool DeleteUser(int UserID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "DELETE FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                int NumOfRowsAffected = command.ExecuteNonQuery();

                if (NumOfRowsAffected > 0)
                    Result = true;
                else
                    Result = false;
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

        public static bool GetPersonInfoByUserID(int UserID,ref int PersonID, ref string UserName,
            ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
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

        public static bool GetPersonInfoByUserName(string UserName, ref int UserID, ref int PersonID,
           ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    Password = (string)reader["Password"];
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
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

        public static bool IsUserExist(int UserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"SELECT Found = 1 From Users WHERE EXISTS (
                             SELECT * FROM Users WHERE UserID = @UserID )";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
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

        public static bool IsUserExist(string UserName)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"SELECT Found = 1 From Users WHERE EXISTS(
                             SELECT * FROM Users WHERE UserName = @UserName )";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    IsFound = true;
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

        public  static DataTable GetAllUsers()
        {
            DataTable Users = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM Users";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Users.Load(reader);

                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return Users;
        }
    }
}
