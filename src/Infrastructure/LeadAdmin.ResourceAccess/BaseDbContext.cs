using LeadAdmin.Utilities.Constants;
using LeadAdmin.Utilities.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LeadAdmin.ResourceAccess
{
    public class BaseDbContext

    {
        public BaseDbContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }

        #region Stored Procedures
        public List<T> ExecuteStoredProcedure_ToList<T>(string procedureName, List<SqlParameter> parms = null) where T : new()
        {
            var result = this.ExecuteQueryOrStoredProcedure_ToDataSet(procedureName, parms, CommandType.StoredProcedure);
            return (result.Tables.Count > 0) ? result.Tables[0].ToList<T>() : new List<T>();
        }

        public DataTable ExecuteStoredProcedure_ToDataTable(string procedureName, List<SqlParameter> parms = null)
        {
            var result = this.ExecuteQueryOrStoredProcedure_ToDataSet(procedureName, parms, CommandType.StoredProcedure);
            return (result.Tables.Count > 0) ? result.Tables[0] : null;
        }

        public DataSet ExecuteQueryOrStoredProcedure_ToDataSet(string queryOrProcedure, List<SqlParameter> parms = null, CommandType commandType = CommandType.StoredProcedure)
        {
            var result = new DataSet();
            using (var sqlConnection = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(queryOrProcedure, sqlConnection))
                    {
                        sqlCommand.CommandType = commandType;
                        sqlCommand.CommandTimeout = ConfigSettings.Instance.Data.CommandTimeout;

                        if (parms != null && parms.Count > 0)
                        {
                            for (int i = 0; i < parms.ToArray().Length; i++)
                            {
                                sqlCommand.Parameters.Add(parms[i]);
                            }
                        }
                        try
                        {
                            var sqlAdapter = new SqlDataAdapter(sqlCommand);
                            sqlAdapter.Fill(result);
                        }
                        catch (SqlException sEx)
                        {
                            Console.WriteLine(sEx.Message);
                            Console.WriteLine(queryOrProcedure);
                            throw;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(queryOrProcedure);
                    throw;
                }
                finally
                {
                    try
                    {
                        sqlConnection.Close();
                    }
                    catch { }
                }
            }

            return result;
        }

        public int ExecuteNonQuery(string queryOrProcedure, List<SqlParameter> parms = null, CommandType commandType = CommandType.StoredProcedure)
        {
            var result = default(int);
            using (var sqlConnection = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(queryOrProcedure, sqlConnection))
                    {
                        sqlCommand.CommandType = commandType;
                        sqlCommand.CommandTimeout = ConfigSettings.Instance.Data.CommandTimeout;

                        if (parms != null && parms.Count > 0)
                        {
                            for (int i = 0; i < parms.ToArray().Length; i++)
                            {
                                sqlCommand.Parameters.Add(parms[i]);
                            }
                        }
                        result = sqlCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    try
                    {
                        sqlConnection.Close();
                    }
                    catch { }
                }
            }

            return result;
        }

        public string ExecuteScalar(string queryOrProcedure, List<SqlParameter> parms = null, CommandType commandType = CommandType.Text)
        {
            var result = default(string);
            using (var sqlConnection = new SqlConnection(this.ConnectionString))
            {
                try
                {
                    sqlConnection.Open();
                    using (var sqlCommand = new SqlCommand(queryOrProcedure, sqlConnection))
                    {
                        sqlCommand.CommandType = commandType;
                        sqlCommand.CommandTimeout = ConfigSettings.Instance.Data.CommandTimeout;

                        if (parms != null && parms.Count > 0)
                        {
                            for (int i = 0; i < parms.ToArray().Length; i++)
                            {
                                sqlCommand.Parameters.Add(parms[i]);
                            }
                        }
                        var rawResult = sqlCommand.ExecuteScalar();

                        if (rawResult != null && rawResult != DBNull.Value)
                        {
                            result = rawResult.ToString();
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    try
                    {
                        sqlConnection.Close();
                    }
                    catch { }
                }
            }

            return result;
        }

        #endregion

        #region Query
        public List<T> ExecuteQuery_ToList<T>(string query, List<SqlParameter> parms = null) where T : new()
        {
            var result = this.ExecuteQueryOrStoredProcedure_ToDataSet(query, parms, CommandType.Text);
            return (result.Tables.Count > 0) ? result.Tables[0].ToList<T>() : new List<T>();
        }

        public List<T> GetTable<T>(string tableName, string whereCondition = "", string orderBy = "") where T : new()
        {
            var result = default(List<T>);
            if (!string.IsNullOrWhiteSpace(whereCondition))
            {
                whereCondition = " WHERE " + whereCondition;
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                orderBy = " ORDER BY " + orderBy;
            }

            if (result == null || result.Count == 0)
            {
                var query = $"SELECT * FROM {tableName} {whereCondition} {orderBy}";
                result = this.ExecuteQuery_ToList<T>(query);
            }

            return result;
        }

        public DataTable ExecuteQuery_ToDataTable(string query, List<SqlParameter> parms = null)
        {
            var result = this.ExecuteQueryOrStoredProcedure_ToDataSet(query, parms, CommandType.Text);
            return (result.Tables.Count > 0) ? result.Tables[0] : null;
        }

        #endregion

    }

}