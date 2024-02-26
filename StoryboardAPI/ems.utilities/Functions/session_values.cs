using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ems.utilities.Models;

namespace ems.utilities.Functions
{
    public class session_values
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions(); 
        string msSQL = string.Empty;
        Dictionary<string, object> objGetReaderData;

        public logintoken gettokenvalues(string token)
        {
            logintoken getlogintoken = new logintoken();
           
            msSQL = " select employee_gid,user_gid,department_gid from adm_mst_ttoken WHERE token = '" + token + "'";
            objGetReaderData = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderData.Count > 0)
            {
                getlogintoken.employee_gid = objGetReaderData["employee_gid"].ToString();
                getlogintoken.user_gid = objGetReaderData["user_gid"].ToString();
                getlogintoken.department_gid = objGetReaderData["department_gid"].ToString();  
            } 
            return getlogintoken;
        }
    }
}