using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace DemoExercise.DataAccess
{
    public class DAL
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb; Database = demoExampleDB; Trusted_Connection = True; MultipleActiveResultSets = true";
        public string ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value;
        }

        public bool CheckIfUserExists(string username, string password)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspCheckIfUserExists", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", username.Trim());
                        cmd.Parameters.AddWithValue("@Password", password.Trim());
                        con.Open();

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = cmd;
                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count >= 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SaveError("Error at CheckIfUserExists method:", username, ex);
            }
            return false;
        }

        public void SaveError(string errorMessage, string user, Exception exception = null )
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("uspLogErrorMsg", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Error", errorMessage);
                        cmd.Parameters.AddWithValue("@ExceptionLog", exception.Message.ToString());
                        cmd.Parameters.AddWithValue("@CreatedBy", user);
                        con.Open();
                        cmd.ExecuteNonQuery();                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
