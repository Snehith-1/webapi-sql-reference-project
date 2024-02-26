﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoryboardAPI.Models
{
    public class result
    {
        public bool status { get; set; }
        public string message { get; set; }
    }
    public class token
    {
        public string token_type { get; set; }
        public string scope { get; set; }
        public int expires_in { get; set; }
        public int ext_expires_in { get; set; }
        public string access_token { get; set; }
    }

    public class Rootobject
    {
        public string odatacontext { get; set; }
        public object businessPhones { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
        public object jobTitle { get; set; }
        public string mail { get; set; }
        public object mobilePhone { get; set; }
        public object officeLocation { get; set; }
        public object preferredLanguage { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
        public string id { get; set; }
    }

    public class userlog : result
    {
        public List<userloglist> userloglist { get; set; }
    }

    public class userloglist
    {
        public string businessPhones { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
        public string jobTitle { get; set; }
        public string mail { get; set; }
        public string mobilePhone { get; set; }
        public string officeLocation { get; set; }
        public string preferredLanguage { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
        public string id { get; set; }
    }


    public class loginresponse
    {
        public string token { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string user_gid { get; set; }
        public string username { get; set; } 
        public string usercode { get; set; } 
        public string c_code { get; set; }
    }
    public class logininput
    {
        public string code { get; set; }
        public string company_code { get; set; }
    }
    public class userlogininput
    {
        public string hostname { get; set; }
        public string company_code { get; set; }
        public string user_code { get; set; }
        public string user_password { get; set; }
        public string lawyer_email { get; set; }
    }

    public class loginERPinput
    {
        public string user_code { get; set; }
        public string company_code { get; set; }
    }

    public class loginVendorInput
    {
        public string user_code { get; set; }
        public string pass_word { get; set; }
    }

    public class appVendorInput
    {
        public string app_code { get; set; }
        public string password { get; set; }
    }
    //public class Mdladminlogin : result
    //{
    //    public string user_code { get; set; }
    //    public string user_password { get; set; }
    //    public string company_code { get; set; }
    //}
    public class PostUserLogin : result
    {
        public string user_code { get; set; }
        public string user_password { get; set; }
        public string company_code { get; set; }
    }

    public class otplogin
    {
        //internal string mobile_number;
        public string employee_emailid { get; set; }
        public string employee_mobileno { get; set; }
        public string message { get; set; }
        public string otpvalue { get; set; }
        public string created_time { get; set; }
        //public string expiry_time { get; set; }
        public bool status { get; set; }


    }
    public class otpverify : PostUserLogin
    {
        //internal string mobile_number;
        public string employee_emailid { get; set; }
        public string employee_mobileno { get; set; }
        public string message { get; set; }
        public string otpvalue { get; set; }
        public bool status { get; set; }

    }
    public class otpverifyresponse
    {
        //internal string mobile_number;
        public string token { get; set; }
        public string employee_emailid { get; set; }
        public string employee_mobileno { get; set; }
        public string message { get; set; }
        public string otpvalue { get; set; }
        public bool status { get; set; }
        public string user_gid { get; set; }

    }

    public class otpresponse
    {
        public string otp_flag { get; set; }
    }


}