using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDDataAccess
{
    public static class clsTestTypesData
    {
        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle,string TestTypeDescription, float TestFees)
        {
            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = @"Update TestTypes SET TestTypeTitle = @TestTypeTitle,TestTypeDescription = @TestTypeDescription,
                             TestFees = @TestFees WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestFees", TestFees);

            try
            {
                connection.Open();

                int NumOfRowsAffected = command.ExecuteNonQuery();

                if (NumOfRowsAffected > 0)
                {
                    Result = true;
                }
                else
                {
                    Result = false;
                }
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

        public static bool GetTestTypeByID(int TestTypeID, ref string TestTypeTitle,ref string TestTypeDescription, ref float TestFees)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestTypeDescription = (string)reader["TestTypeDescription"];
                    TestFees = Convert.ToSingle(reader["TestFees"]);
                }
                else
                {
                    IsFound = false;
                }

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

        public static DataTable GetAllTestTypes()
        {
            DataTable TestTypes = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSetings.ConnectionString);

            string query = "SELECT * FROM TestTypes";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    TestTypes.Load(reader);

                reader.Close();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }

            return TestTypes;
        }
    }
}
