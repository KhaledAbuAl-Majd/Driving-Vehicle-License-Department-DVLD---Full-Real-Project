using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DVLDConstant;

namespace DVLDDataAccess
{
    public static class clsUserData
    {
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName,
            ref string Password, ref string Salt, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

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
                    Salt = (string)reader["Salt"];
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;

                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName,
           ref string Password, ref string Salt, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    UserName = Convert.ToString(reader["UserName"]);
                    Password = (string)reader["Password"];
                    Salt = (string)reader["Salt"];
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;

                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static bool GetUserInfoByUsernameAndPassword(string UserName, string Password, ref int UserID, ref int PersonID, ref string Salt, ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Users WHERE UserName = @UserName AND Password = @Password;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    Salt = (string)reader["Salt"];
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                }
                else
                    IsFound = false;

                reader.Close();
            }
            catch (Exception ex)
            {
                IsFound = false;

                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static int AddNewUser(int PersonID, string UserName, string Password, string Salt, bool IsActive)
        {
            int UserID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into Users (PersonID,UserName,Password,Salt,IsActive) 
                             Values(@PersonID,@UserName,@Password,@Salt,@IsActive);
                              SELECT SCOPE_IDENTITY()";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@Salt", Salt);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    UserID = InsertedID;
                }
                else
                {
                    UserID = -1;
                }

            }
            catch (Exception ex)
            {
                UserID = -1;

                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return UserID;
        }

        public static bool UpdateUser(int UserID,int PersonID,string UserName,bool IsActive)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Users 
                            SET PersonID = @PersonID, UserName = @UserName,
                            IsActive = @IsActive Where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@IsActive", IsActive);

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

        public  static DataTable GetAllUsers()
        {
            DataTable Users = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select Users.UserID,People.PersonID,
            Concat_WS(' ',People.FirstName,People.SecondName,People.ThirdName, People.LastName) As FullName,
            Users.UserName,Users.IsActive from Users INNER JOIN People ON Users.PersonID = People.PersonID;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    Users.Load(reader);

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

            return Users;
        }

        public static bool DeleteUser(int UserID)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                int NumOfRowsAffected = command.ExecuteNonQuery();

                Result = (NumOfRowsAffected > 0);
            }
            catch (Exception ex)
            {
                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return Result;
        }

        public static bool IsUserExist(int UserID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 FROM Users WHERE UserID = @UserID ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsFound = (result != null);
            }
            catch (Exception ex)
            {
                IsFound = false;

                clsLogger.LogAtEventLog(ex.Message);
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

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 FROM Users WHERE UserName = @UserName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsFound = (result != null);
            }
            catch (Exception ex)
            {
                IsFound = false;

                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }


            return IsFound;
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT Found = 1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                IsFound = (result != null);
            }
            catch (Exception ex)
            {
                IsFound = false;

                clsLogger.LogAtEventLog(ex.Message);
            }
            finally
            {
                connection.Close();
            }


            return IsFound;
        }

        public static bool ChangePassword(int UserID,string NewPassword,string NewSalt)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"UPDATE Users SET Password = @NewPassword,Salt =@NewSalt  Where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@NewPassword", NewPassword);
            command.Parameters.AddWithValue("@NewSalt", NewSalt);
  

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

        public static string GetSaltByUserName(string UserName)
        {
            string Salt;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
                {
                    string query = @"SELECT Salt FROM Users WHERE UserName = @UserName;";
                    using(SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserName", UserName);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        Salt = result?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                clsLogger.LogAtEventLog(ex.Message);
                Salt = null;
            }

            return Salt;
        }
    }
}
