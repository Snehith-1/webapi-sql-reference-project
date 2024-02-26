﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.system.Models;
using ems.system.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;
using System.Web.Http.Results;

namespace ems.system.Controllers
{
    [RoutePrefix("api/User")]
    [Authorize]
    public class UserController : ApiController
    {
        DaUser objdauser = new DaUser();
        session_values objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

        [ActionName("topmenu")]
        [HttpGet]
        public HttpResponseMessage getTopMenu (string user_gid)
        {
            menu_response objresult = new menu_response();
            objdauser.loadMenuFromDB(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        }

        [ActionName("privilegelevel")]
        [HttpGet]
        public HttpResponseMessage privilegelevel(string user_gid)
        {
            menu_response objresult = new menu_response();
            objdauser.Daprivilegelevel(user_gid, objresult);
            return Request.CreateResponse(HttpStatusCode.OK, objresult);
        } 
    }
}