using DAL.Helper.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
    public class DatabaseHelper : IDatabaseHelper, IDisposable
    {
        public string StrConnection {  get; set; }
        private SqlConnection sqlConnection;
        public string img_folder_path;
        public DatabaseHelper(IConfiguration configuration) 
        {
            StrConnection = configuration["ConnectionStrings:DefaultConnection"];
            img_folder_path = configuration["Appsettings:PosterPATH"];
        }

        public void SetConnectionString(string connectionString)
        {
            StrConnection = connectionString;
        }

        public string OpenConnection()
        {
            try
            {
                if (sqlConnection == null)
                    sqlConnection = new SqlConnection(StrConnection);

                if (sqlConnection.State != ConnectionState.Open)
                    sqlConnection.Open();

                return "";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string CloseConnection()
        {
            try
            {
                if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                    sqlConnection.Close();
                return "";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public string ExecuteNoneQuery(string strquery)
        {
            string msgError = "";
            try
            {
                OpenConnection();
                var sqlCommand = new SqlCommand(strquery, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
            }
            catch (Exception exception)
            {
                msgError = exception.ToString();
            }
            finally
            {
                CloseConnection();
            }
            return msgError;
        }

        public DataTable ExecuteQueryToDataTable(string strquery, out string msgError)
        {
            msgError = "";
            var result = new DataTable();
            var sqlDataAdapter = new SqlDataAdapter(strquery, StrConnection);
            try
            {
                sqlDataAdapter.Fill(result);
            }
            catch (Exception exception)
            {
                msgError = exception.ToString();
                result = null;
            }
            finally
            {
                sqlDataAdapter.Dispose();
            }
            return result;
        }

        public object ExecuteScalar(string strquery, out string msgError)
        {
            object result = null;
            try
            {
                OpenConnection();
                var npgsqlCommand = new SqlCommand(strquery, sqlConnection);
                result = npgsqlCommand.ExecuteScalar();
                npgsqlCommand.Dispose();
                msgError = "";
            }
            catch (Exception ex) { msgError = ex.StackTrace; }
            finally
            {
                CloseConnection();
            }
            return result;
        }

        public string ExecuteSProcedure(string sprocedureName, params object[] paramObjects)
        {
            string result = "";
            SqlConnection connection = new SqlConnection(StrConnection);
            try
            {
                SqlCommand cmd = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = sprocedureName };
                connection.Open();
                cmd.Connection = connection;
                int parameterInput = (paramObjects.Length) / 2;
                int j = 0;
                for (int i = 0; i < parameterInput; i++)
                {
                    string paramName = Convert.ToString(paramObjects[j++]);
                    object value = paramObjects[j++];
                    if (paramName.ToLower().Contains("json"))
                    {
                        cmd.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = paramName,
                            Value = value ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar
                        });
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
                    }
                }
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception exception)
            {
                result = exception.ToString();
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public DataTable ExecuteSProcedureReturnDataTable(out string msgError, string sprocedureName, params object[] paramObjects)
        {
            DataTable tb = new DataTable();
            msgError = string.Empty;
            try
            {
                using var connection = new SqlConnection(StrConnection);
                using var cmd = new SqlCommand(sprocedureName, connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                connection.Open();
                AddParameters(cmd, paramObjects);

                using var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(tb);
            }
            catch (Exception exception)
            {
                tb = null;
                msgError = exception.ToString();
            }
            return tb;
        }

        private void AddParameters(SqlCommand cmd, object[] paramObjects)
        {
            int parameterInput = paramObjects.Length / 2;
            int j = 0;
            for (int i = 0; i < parameterInput; i++)
            {
                string paramName = Convert.ToString(paramObjects[j++]).Trim();
                object value = paramObjects[j++];

                cmd.Parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
            }
        }

        public object ExecuteScalarSProcedure(out string msgError, string sprocedureName, params object[] paramObjects)
        {
            msgError = "";
            object result = null;
            SqlConnection connection = new SqlConnection(StrConnection);

            try
            {
                SqlCommand cmd = new SqlCommand { CommandType = CommandType.StoredProcedure, CommandText = sprocedureName };
                connection.Open();
                cmd.Connection = connection;
                int parameterInput = (paramObjects.Length) / 2;
                int j = 0;
                for (int i = 0; i < parameterInput; i++)
                {
                    string paramName = Convert.ToString(paramObjects[j++]);
                    object value = paramObjects[j++];
                    if (paramName.ToLower().Contains("jsonb"))
                    {
                        cmd.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = paramName,
                            Value = value ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar
                        });
                    }
                    else if (paramName.ToLower().Contains("json"))
                    {
                        cmd.Parameters.Add(new SqlParameter()
                        {
                            ParameterName = paramName,
                            Value = value ?? DBNull.Value,
                            SqlDbType = SqlDbType.NVarChar
                        });
                    }
                    else
                    {
                        cmd.Parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
                    }
                }

                result = cmd.ExecuteScalar();
                cmd.Dispose();
            }
            catch (Exception exception)
            {
                result = null;
                msgError = exception.ToString();
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public object ExecuteScalarSProcedureWithTransaction(out string msgError, string sprocedureName, params object[] paramObjects)
        {
            msgError = "";
            object result = null;
            using (SqlConnection connection = new SqlConnection(StrConnection))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlCommand cmd = connection.CreateCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = sprocedureName;
                        cmd.Transaction = transaction;
                        cmd.Connection = connection;

                        int parameterInput = (paramObjects.Length) / 2;
                        int j = 0;
                        for (int i = 0; i < parameterInput; i++)
                        {
                            string paramName = Convert.ToString(paramObjects[j++]);
                            object value = paramObjects[j++];
                            if (paramName.ToLower().Contains("json"))
                            {
                                cmd.Parameters.Add(new SqlParameter()
                                {
                                    ParameterName = paramName,
                                    Value = value ?? DBNull.Value,
                                    SqlDbType = SqlDbType.NVarChar
                                });
                            }
                            else
                            {
                                cmd.Parameters.Add(new SqlParameter(paramName, value ?? DBNull.Value));
                            }
                        }

                        result = cmd.ExecuteScalar();
                        cmd.Dispose();
                        transaction.Commit();
                    }
                    catch (Exception exception)
                    {

                        result = null;
                        msgError = exception.ToString();
                        try
                        {
                            transaction.Rollback();
                        }
                        catch (Exception ex) { }
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return result;
        }
        public void Dispose()
        {
            CloseConnection();
            sqlConnection?.Dispose();
        }

        public string GetImgFolder()
        {
            return img_folder_path;
        }

    }
}
