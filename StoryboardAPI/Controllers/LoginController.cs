using System;
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
using StoryboardAPI.Authorization;
using System.Data;
using System.Data.Odbc;
using Newtonsoft.Json;
using RestSharp;
using System.Web.UI;
using System.Web.UI.WebControls;
using Spire.Pdf;
using System.IO;
using System.Data.SqlClient;

namespace StoryboardAPI.Controllers
{
    [RoutePrefix("api/Login")]
    [AllowAnonymous]
    public class LoginController : ApiController
   	{
        dbconn objdbconn = new dbconn();
        SqlDataReader objSqlDataReader;
        Dictionary<string, object> objGetReaderData;
        cmnfunctions objcmnfunctions = new cmnfunctions();
        
        string msSQL = string.Empty;
        int mnResult;
        string user_status;
        string vendoruser_status;
        string tokenvalue = string.Empty;
        string user_gid = string.Empty;
        string employee_gid = string.Empty;
        string department_gid = string.Empty;
        string password = string.Empty;
        string username = string.Empty;
        string departmentname = string.Empty;
        string lscompany_code;
        string domain = string.Empty;
        string lsexpiry_time;
        DataTable dt_datatable;



        [HttpPost]
        [ActionName("UserLogin")]
        public HttpResponseMessage PostUserLogin(PostUserLogin values)
        {
            loginresponse GetLoginResponse = new loginresponse();
            try
            { 
                domain = Request.RequestUri.Host.ToLower(); 
                //string jsonFilePath = @" " + ConfigurationManager.AppSettings["CmnConfigfile_path"].ToString();
                //string jsonString = File.ReadAllText(jsonFilePath); 
                //var jsonDataArray = JsonConvert.DeserializeObject<MdlCmnConn[]>(jsonString);
                //string lscompany_dbname = (from a in jsonDataArray
                //                           where a.company_code == values.company_code
                //                           select a.company_dbname).FirstOrDefault();

                var ObjToken = Token(values.user_code, objcmnfunctions.ConvertToAscii(values.user_password), values.company_code);
                dynamic newobj = JsonConvert.DeserializeObject(ObjToken);
                tokenvalue = "Bearer " + newobj.access_token;  
                if (tokenvalue != null && tokenvalue.TrimEnd() != "Bearer")
                { 
                    msSQL = " EXEC dbo.adm_mst_spstoretoken " +
                            " @token='" + tokenvalue + "'," +
                            " @usercode='" + values.user_code + "'," +
                            " @user_password='" + objcmnfunctions.ConvertToAscii(values.user_password) + "'," +
                            " @companycode='" + values.company_code + "'," +
                            " @LoginFrom='Web'," +
                            " @WebToken=''";
                    user_gid = objdbconn.GetExecuteScalar(msSQL, values.company_code);
                     
                    GetLoginResponse.status = true;
                    GetLoginResponse.message = "Login Successfully!";
                    GetLoginResponse.token = tokenvalue;
                    GetLoginResponse.user_gid = user_gid;
                    GetLoginResponse.c_code = values.company_code;

                    return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse); 
                }
                else
                {
                    GetLoginResponse.status = false;
                    return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
                }
            }
            catch (Exception ex)
            {
                GetLoginResponse.status = false;
                GetLoginResponse.message = ex.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
            }
        }

        public class MdlCmnConn
        {
            public string connection_string { get; set; }
            public string company_code { get; set; }
            public string company_dbname { get; set; }
        }

        // ------------- For SSO Login & OTP Validation ------------------
        [AllowAnonymous]
        [ActionName("LoginReturn")]
        [HttpPost]
        public HttpResponseMessage GetLoginReturn(logininput values)
        { 

            loginresponse GetLoginResponse = new loginresponse();
            string code = values.code; 
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;  
            var client = new RestSharp.RestClient("https://login.microsoftonline.com/d71b0d7f-10d7-46cf-80f6-b8dc3924a66a/oauth2/v2.0/token");
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("client_id", ConfigurationManager.AppSettings["client_id"]);
            request.AddParameter("scope", "https://graph.microsoft.com/User.Read");
            request.AddParameter("code", code); 
            request.AddParameter("client_secret", ConfigurationManager.AppSettings["client_secret"]);
            request.AddParameter("redirect_uri", ConfigurationManager.AppSettings["redirect_url"]);
            request.AddParameter("code_verifier", "c775e7b757ede630cd0aa1113bd102661ab38829ca52a6422ab782862f268646"); 
            request.AddParameter("grant_type", "authorization_code");
            IRestResponse response = client.Execute(request);
            token json = JsonConvert.DeserializeObject<token>(response.Content);

            var client1 = new RestSharp.RestClient("https://graph.microsoft.com/v1.0/me");
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request1 = new RestRequest(Method.GET);
            request1.AddHeader("Authorization", "Bearer " + json.access_token);
            IRestResponse response1 = client1.Execute(request1);
            Rootobject json1 = JsonConvert.DeserializeObject<Rootobject>(response1.Content);
            object lsDBmobilePhone;
            
            if (json1.userPrincipalName != null && json1.userPrincipalName != "")
            {
                string jsonFilePath = @" " + ConfigurationManager.AppSettings["CmnConfigfile_path"].ToString();
                string jsonString = File.ReadAllText(jsonFilePath);
                var jsonDataArray = JsonConvert.DeserializeObject<MdlCmnConn[]>(jsonString);
                string lscompany_dbname = (from a in jsonDataArray
                                           where a.company_code == values.company_code
                                           select a.company_dbname).FirstOrDefault();

                msSQL = " SELECT b.user_gid,a.department_gid, a.employee_gid, user_password, user_code, a.employee_mobileno, concat(user_firstname, ' ', user_lastname) as username FROM hrm_mst_temployee a " +
                        " INNER JOIN adm_mst_tuser b on b.user_gid = a.user_gid " +
                        " WHERE employee_emailid = '" + json1.userPrincipalName + "' and b.user_status = 'Y'";
                objGetReaderData = objdbconn.GetReaderScalar(msSQL, lscompany_dbname);
                if (objGetReaderData.Count > 0)
                { 
                    var tokenresponse = Token(objGetReaderData["user_code"].ToString(), objGetReaderData["user_password"].ToString(), lscompany_dbname);
                    dynamic newobj = Newtonsoft.Json.JsonConvert.DeserializeObject(tokenresponse);
                    tokenvalue = newobj.access_token;
                    employee_gid = objGetReaderData["employee_gid"].ToString();
                    user_gid = objGetReaderData["user_gid"].ToString();
                    department_gid = objGetReaderData["department_gid"].ToString();
                    GetLoginResponse.username = objGetReaderData["username"].ToString();
                    lsDBmobilePhone = objGetReaderData["employee_mobileno"].ToString(); 
                } 

                msSQL = " INSERT INTO adm_mst_ttoken ( " +
                         " token, " +
                         " employee_gid, " +
                         " user_gid, " +
                         " department_gid, " +
                         " company_code " +
                         " )VALUES( " +
                         " 'Bearer " + tokenvalue + "'," +
                         " '" + employee_gid + "'," +
                         " '" + user_gid + "'," +
                         " '" + department_gid + "'," +
                         " '" + values.company_code + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL, lscompany_dbname);
                GetLoginResponse.status = true;
                GetLoginResponse.message = "Login Successfully!";
                GetLoginResponse.token = "Bearer " + tokenvalue;
                GetLoginResponse.user_gid = user_gid;
                GetLoginResponse.c_code = values.company_code;

            }
            else
            {
                GetLoginResponse.user_gid = null; 
            }
            return Request.CreateResponse(HttpStatusCode.OK, GetLoginResponse);
        }  

        public string Token(string userName, string password, string company_code = null)
        {

            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "grant_type", "password" ),
                            new KeyValuePair<string, string>( "username", userName ),
                            new KeyValuePair<string, string> ( "Password", password ),
                            new KeyValuePair<string, string>("Scope",company_code)
                        };
            var content = new FormUrlEncodedContent(pairs);
            using (var client = new HttpClient()) 
            { 
                domain = Request.RequestUri.Authority.ToLower(); 
                var host = HttpContext.Current.Request.Url.Host;
                var response = client.PostAsync(ConfigurationManager.AppSettings["protocol"].ToString() + domain +
                                "/StoryboardAPI/token", new FormUrlEncodedContent(pairs)).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        } 

        public void LoginErrorLog(string strVal)
        {
            try
            {
                string lspath = ConfigurationManager.AppSettings["file_path"].ToString() + "/erpdocument/LOGIN_ERRLOG/" + DateTime.Now.Year + @"\" + DateTime.Now.Month;
                if ((!System.IO.Directory.Exists(lspath)))
                    System.IO.Directory.CreateDirectory(lspath);

                lspath = lspath + @"\" + DateTime.Now.ToString("yyyy-MM-dd HH") + ".txt";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(lspath, true);
                sw.WriteLine(strVal);
                sw.Close();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
