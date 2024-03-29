﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ems.utilities.Functions;
using ems.utilities.Models;
using StoryboardAPI.Models;
using System.Data;
using System.Data.Odbc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace StoryboardAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/AdminLogin")]
    public class AdminLoginController : ApiController
    {
        dbconn objdbconn = new dbconn();
        Dictionary<string, object> objGetReaderScalar;
        cmnfunctions objcmnfunctions = new cmnfunctions();
        DataTable ds_datatable;
        string msSQL = string.Empty;
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();
        // Get Session Values

        [ActionName("SValues")]
        [HttpGet]
        public HttpResponseMessage getSessionvalues()
        {
            adminlogin objadminlogin = new adminlogin();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            msSQL = " SELECT c.user_code,c.user_password,a.company_code FROM adm_mst_ttoken a " +
                  " LEFT JOIN hrm_mst_temployee b ON a.employee_gid = b.employee_gid " +
                  " LEFT JOIN  adm_mst_tuser c ON b.user_gid = c.user_gid " +
                  " WHERE a.employee_gid = '" + getsessionvalues.employee_gid + "'";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if(objGetReaderScalar.Count!=0)
            {
                objadminlogin.company_code = objGetReaderScalar["company_code"].ToString();
                objadminlogin.user_code = objGetReaderScalar["user_code"].ToString();
                objadminlogin.user_password = objGetReaderScalar["user_password"].ToString(); 
            } 
            objadminlogin.status = true;
            
            return Request.CreateResponse(HttpStatusCode.OK, objadminlogin);
        }
    }
}
