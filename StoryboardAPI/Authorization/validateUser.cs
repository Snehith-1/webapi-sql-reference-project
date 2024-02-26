using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ems.utilities.Functions;
using System.Data;
using System.Data.Odbc;
using System.Configuration;
using System.IO;
using System.Text.Json;
using System.Data.SqlClient;

namespace StoryboardAPI.Authorization
{

    public class validateUser
    {
        dbconn objdbconn = new dbconn();
        SqlDataReader objSqlDataReader;
        string mssql;
        public bool isvalid(string username, string password, string companycode = null)
        {   
            mssql = " use  " + companycode + " ; SELECT user_gid FROM adm_mst_tuser " +
                   " WHERE user_code='" + username + "' AND user_password='" + password + "'"; 
            Dictionary<string, object> data = objdbconn.GetReaderScalar(mssql, companycode);
            if (data.Count > 0)
                return true;
            else
                return false;
        } 
        public class MdlCmnConn
        {
            public string connection_string { get; set; }
            public string company_code { get; set; }
            public string company_dbname { get; set; }
        }
    }
}