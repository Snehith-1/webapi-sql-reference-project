using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text.Json;
using System.Web;
using System.Web.Http; 

namespace ems.utilities.Functions
{
    public class dbconn
    {
        private string lsConnectionString = string.Empty;

        // Get Connection String 

        public string GetConnectionString(string companyCode = "")
        {
            try
            {
                //string jsonFilePath = @" " + ConfigurationManager.AppSettings["CmnConfigfile_path"].ToString();
                //string jsonString = File.ReadAllText(jsonFilePath);
                //var jsonDataArray = JsonSerializer.Deserialize<MdlCmnConn[]>(jsonString);
                //string baseConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ConnectionString;

                if (HttpContext.Current.Request.Headers["Authorization"] == null || HttpContext.Current.Request.Headers["Authorization"] == "null")
                {
                    //string databaseName = (from a in jsonDataArray
                    //                       where a.company_dbname == companyCode
                    //                       select a.company_dbname).FirstOrDefault();
                    //lsConnectionString = $"{baseConnectionString}Database={databaseName};";
                    lsConnectionString = ConfigurationManager.ConnectionStrings["AuthConn"].ConnectionString;
                }
                else
                {
                    try
                    {
                        //string lsHeadercode = HttpContext.Current.Request.Headers["c_code"].ToString();
                        //string databaseName = (from a in jsonDataArray
                        //                       where a.company_code == lsHeadercode
                        //                       select a.company_dbname).FirstOrDefault();
                        //string lsDBConnectionString = $"{baseConnectionString}Database={databaseName};";

                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AuthConn"].ConnectionString))
                        {
                            //string mssql = "SELECT company_code FROM adm_mst_ttoken WHERE token = @token"; // Use named parameter @token
                            //SqlCommand cmd = new SqlCommand(mssql, connection);
                            //cmd.Parameters.AddWithValue("@token", HttpContext.Current.Request.Headers["Authorization"].ToString());
                            //connection.Open();
                            //object result = cmd.ExecuteScalar();
                            //if (result != null)
                            //    lsConnectionString = lsDBConnectionString;
                            //else
                            //    lsConnectionString = "error";
                            //connection.Close();
                            using (SqlCommand cmd = new SqlCommand())
                            {

                                cmd.CommandType = CommandType.Text;
                            cmd.CommandText = " EXEC dbo.adm_mst_spgetconnectionstring @tokenvalue='" + HttpContext.Current.Request.Headers["Authorization"].ToString() + "'";
                            cmd.Connection = connection;
                            connection.Open();
                            lsConnectionString = cmd.ExecuteScalar().ToString();
                            connection.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LogForAudit($"Error getting connection string: {ex.ToString()}", "GetConnectionString");
                        lsConnectionString = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                LogForAudit($"Error getting connection string: {ex.ToString()}", "GetConnectionString");
                lsConnectionString = "error";
            }
            return lsConnectionString;
        }

        public class MdlCmnConn
        { 
            public string connection_string { get; set; }
            public string company_code { get; set; }
            public string company_dbname { get; set; }
        }

        // Open Connection 

        public SqlConnection OpenConn(string companyCode= "")
        {
            try
            {
                SqlConnection gs_ConnDB;
                gs_ConnDB = new SqlConnection(GetConnectionString(companyCode));
                if (gs_ConnDB.State != ConnectionState.Open)
                {
                    gs_ConnDB.Open();
                }
                return gs_ConnDB;
            }
            catch (Exception e)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "UnAuthorized" };
                throw new HttpResponseException(msg);
            }

        }

        // Close Connection



        public void CloseConn()
        {
            if (OpenConn().State != ConnectionState.Closed)
            {
                OpenConn().Dispose();
                OpenConn().Close();
            }
        }

        // Execute a Query
        public int ExecuteNonQuerySQL(string query, string companyCode = "", string user_gid = null, string module_reference = null, string module_name = "Log")
        {
            int mnResult = 0;
            using (SqlConnection ObjSqlConnection = OpenConn(companyCode))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(query, ObjSqlConnection);
                    mnResult = cmd.ExecuteNonQuery();
                    mnResult = 1;
                }
                catch (Exception e)
                {
                    LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);
                }
            }
            return mnResult;
        } 

        // Get Scalar Value 
        public string GetExecuteScalar(string query, string companyCode = "", string user_gid = null, string module_reference = null, string module_name = "Log")
        {
            string val;
            try
            {
                using (SqlConnection ObjSqlConnection = OpenConn(companyCode))
                {
                    SqlCommand cmd = new SqlCommand(query, ObjSqlConnection);
                    val = cmd.ExecuteScalar()?.ToString();
                }
            }
            catch (Exception e)
            {
                LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);
                val = "";
            }
            return val;
        }

        // Get Data Reader

        public List<Dictionary<string, object>> GetDataReader(string query, string companyCode = "", string user_gid = null, string module_reference = null, string module_name = "Log")
        {
            List<Dictionary<string, object>> resultsList = new List<Dictionary<string, object>>();

            try
            {
                using (SqlConnection connection = OpenConn(companyCode))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (!reader.HasRows)
                                return resultsList;
                            while (reader.Read())
                            {
                                Dictionary<string, object> results = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object columnValue = reader[i];
                                    results[columnName] = columnValue;
                                }
                                resultsList.Add(results);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH:mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);
            }

            return resultsList;
        }
        public Dictionary<string, object> GetReaderScalar(string query, string companyCode = "", string user_gid = null, string module_reference = null, string module_name = "Log")
        {
            Dictionary<string, object> results = new Dictionary<string, object>();

            try
            {
                using (SqlConnection connection = OpenConn(companyCode))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (!reader.HasRows)
                                return results;
                            if (reader.Read())
                            { 
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    string columnName = reader.GetName(i);
                                    object columnValue = reader[i];
                                    results[columnName] = columnValue;
                                }
                            } 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH:mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);
            }

            return results;
        }
        //public SqlDataReader GetDataReader(string query, string companyCode = "", string user_gid = null, string module_reference = null, string module_name = "Log")
        //{
        //    SqlDataReader rdr = null;
        //    SqlConnection connection = OpenConn(companyCode);
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand(query, connection);
        //        return rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        //    }
        //    catch (Exception e)
        //    {
        //        connection.Close();
        //        connection.Dispose();
        //        LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);
        //        return null;
        //    }
        //} 
         
        public DataTable GetDataTable(string query, string user_gid = null, string module_reference = null, string module_name = "Log")
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection ObjSqlConnection = OpenConn())
                using (SqlDataAdapter da = new SqlDataAdapter(query, ObjSqlConnection))
                {
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception e)
            {
                LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);
                return null;
            }
        }

        // Get Data Set

        public DataSet GetDataSet(string query, string table, string user_gid = null, string module_reference = null, string module_name = "Log")
        {
            try
            {
                using (SqlConnection connection = OpenConn())
                {
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(query, connection);
                    da.Fill(ds, table);
                    return ds;
                }
            }
            catch (Exception e)
            {
                LogForAudit("*******Date*****" + DateTime.Now.ToString("yyyy - MM - dd HH: mm:ss") + "***********" + e.Message.ToString() + "*****Query****" + query + "*******Apiref********" + module_reference, module_name);
                return null;
            }
        } 
        public void LogForAudit(string strVal, string module_name)
        {

            try
            {
                string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erpdocument/ExceptionLOG/" + module_name + "/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);

                lspath = lspath + @"\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt"; 
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true))
                {
                    sw.WriteLine(strVal);
                }  
            }
            catch (Exception ex)
            {
            }
        }
    }
}