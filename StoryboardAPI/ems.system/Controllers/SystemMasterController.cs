using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ems.system.Models;
using ems.system.DataAccess;
using ems.utilities.Functions;
using ems.utilities.Models;

namespace ems.system.Controllers
{
    [RoutePrefix("api/SystemMaster")]
    [Authorize]
    public class SystemMasterController : ApiController
    {
        DaSystemMaster objDaSystemMaster = new DaSystemMaster();
        session_values Objgetgid = new session_values();
        logintoken getsessionvalues = new logintoken();

      

        // First Level Menu List
        [ActionName("GetFirstLevelMenu")]
        [HttpGet]
        public HttpResponseMessage GetFirstLevelMenu()
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetFirstLevelMenu(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Second Level Menu List Based on First Level
        [ActionName("GetSecondLevelMenu")]
        [HttpGet]
        public HttpResponseMessage GetSecondLevelMenu(string module_gid_parent)
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetSecondLevelMenu(objmaster, module_gid_parent);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Third Level Menu List Based on Second Level
        [ActionName("GetThirdLevelMenu")]
        [HttpGet]
        public HttpResponseMessage GetThirdLevelMenu(string module_gid_parent)
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetThirdLevelMenu(objmaster, module_gid_parent);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Fourth Level Menu List Based on Second Level
        [ActionName("GetFourthLevelMenu")]
        [HttpGet]
        public HttpResponseMessage GetFourthLevelMenu(string module_gid_parent)
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetFourthLevelMenu(objmaster, module_gid_parent);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        // Menu Add
        [ActionName("PostMenudAdd")]
        [HttpPost]
        public HttpResponseMessage PostMenudAdd(menu values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostMenudAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Menu Mapping Summary

        [ActionName("GetMenuMappingSummary")]
        [HttpGet]
        public HttpResponseMessage GetMenuMappingSummary()
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetMenuMappingSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        [ActionName("GetMenuMappingEdit")]
        [HttpGet]
        public HttpResponseMessage GetMenuMappingEdit(string menu_gid)
        {
            menu objmaster = new menu();
            objDaSystemMaster.DaGetMenuMappingEdit(menu_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetMenuMappingInactivate")]
        [HttpPost]
        public HttpResponseMessage GetMenuMappingInactivate(menu values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaGetMenuMappingInactivate(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetMenuMappingInactivateview")]
        [HttpGet]
        public HttpResponseMessage GetMenuMappingInactivateview(string menu_gid)
        {
            menu values = new menu();
            objDaSystemMaster.DaGetMenuMappingInactivateview(menu_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateSubFunction")]
        [HttpPost]
        public HttpResponseMessage CreateSubFunction(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateSubFunction(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetSubFunction")]
        [HttpGet]
        public HttpResponseMessage GetSubFunction()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetSubFunction(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("EditSubFunction")]
        [HttpGet]
        public HttpResponseMessage EditSubFunction(string subfunction_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditSubFunction(subfunction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DeleteSubFunction")]
        [HttpGet]
        public HttpResponseMessage DeleteSubFunction(string subfunction_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteSubFunction(subfunction_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("SubFunctionInactiveLogview")]
        [HttpGet] 
        public HttpResponseMessage SubFunctionInactiveLogview(string subfunction_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaSubFunctionInactiveLogview(subfunction_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("InactiveSubFunction")]
        [HttpPost]
        public HttpResponseMessage InactiveSubFunction(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveSubFunction(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("UpdateSubFunction")]
        [HttpPost]
        public HttpResponseMessage UpdateSubFunction(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateSubFunction(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Designation


        [ActionName("GetDesignation")]
        [HttpGet]
        public HttpResponseMessage getDesignation()
        {
            MdlDesignation objMdlDesignation = new MdlDesignation();
            objDaSystemMaster.DaGetDesignation(objMdlDesignation);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlDesignation);
        }


        [ActionName("CreateDesignation")]
        [HttpPost]
        public HttpResponseMessage createdesignation(designation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateDesignation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditDesignation")]
        [HttpGet]
        public HttpResponseMessage editdesignation(string designation_gid)
        {
            designation values = new designation();
            objDaSystemMaster.DaEditDesignation(designation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateDesignation")]
        [HttpPost]
        public HttpResponseMessage updatedesignation(designation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateDesignation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteDesignation")]
        [HttpGet]
        public HttpResponseMessage deletedesignation(string designation_gid)
        {
            designation values = new designation();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteDesignation(designation_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("DesignationStatusUpdate")]
        [HttpPost]
        public HttpResponseMessage DesignationStatusUpdate(designation values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDesignationStatusUpdate(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("GetActiveLog")]
        [HttpGet]
        public HttpResponseMessage GetActiveLog(string designation_gid)
        {
            MdlDesignation values = new MdlDesignation();
            objDaSystemMaster.DaGetActiveLog(designation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Base Location
        [ActionName("GetBaseLocation")]
        [HttpGet]
        public HttpResponseMessage GetBaseLocation()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBaseLocation(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("GetBaseLocationlist")]
        [HttpGet]
        public HttpResponseMessage GetBaseLocationlist()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBaseLocationlist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }
        [ActionName("GetBaseLocationlistActive")]
        [HttpGet]
        public HttpResponseMessage GetBaseLocationlistActive()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBaseLocationlistActive(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("CreateBaseLocation")]
        [HttpPost]
        public HttpResponseMessage CreateBaseLocation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateBaseLocation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditBaseLocation")]
        [HttpGet]
        public HttpResponseMessage EditBaseLocation(string baselocation_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditBaseLocation(baselocation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateBaseLocation")]
        [HttpPost]
        public HttpResponseMessage UpdateBaseLocation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateBaseLocation(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBaseLocation")]
        [HttpPost]
        public HttpResponseMessage InactiveBaseLocation(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveBaseLocation(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBaseLocation")]
        [HttpGet]
        public HttpResponseMessage DeleteBaseLocation(string baselocation_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteBaseLocation(baselocation_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BaseLocationInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BaseLocationInactiveLogview(string baselocation_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaBaseLocationInactiveLogview(baselocation_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //Branch Summary     



        [ActionName("GetBranchSummary")]
        [HttpGet]
        public HttpResponseMessage GetBranchSummary()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBranchSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //Department Summary     

        [ActionName("GetDepartmentSummary")]
        [HttpGet]
        public HttpResponseMessage GetDepartmentSummary()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetDepartmentSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

       
        //Blood Group
        [ActionName("GetBloodGroup")]
        [HttpGet]
        public HttpResponseMessage GetBloodGroup()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBloodGroup(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }




        [ActionName("GetBloodGroupActive")]
        [HttpGet]
        public HttpResponseMessage GetBloodGroupActive()
            {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetBloodGroupActive(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        [ActionName("CreateBloodGroup")]
        [HttpPost]
        public HttpResponseMessage CreateBloodGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateBloodGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        [ActionName("EditBloodGroup")]
        [HttpGet]
        public HttpResponseMessage EditBloodGroup(string bloodgroup_gid)
        {
            master values = new master();
            objDaSystemMaster.DaEditBloodGroup(bloodgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        // Entity
        [ActionName("GetEntity")]
        [HttpGet]
        public HttpResponseMessage GetEntity()
        {
            MdlSystemMaster objapplication360 = new MdlSystemMaster();
            objDaSystemMaster.DaGetEntity(objapplication360);
            return Request.CreateResponse(HttpStatusCode.OK, objapplication360);
        }



        [ActionName("UpdateBloodGroup")]
        [HttpPost]
        public HttpResponseMessage UpdateBloodGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateBloodGroup(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveBloodGroup")]
        [HttpPost]
        public HttpResponseMessage InactiveBloodGroup(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveBloodGroup(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteBloodGroup")]
        [HttpGet]
        public HttpResponseMessage DeleteBloodGroup(string bloodgroup_gid)
        {
            master values = new master();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteBloodGroup(bloodgroup_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("BloodGroupInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage BloodGroupInactiveLogview(string bloodgroup_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaBloodGroupInactiveLogview(bloodgroup_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("CreateEntity")]
        [HttpPost]
        public HttpResponseMessage CreateEntity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaCreateEntity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EditEntity")]
        [HttpGet]
        public HttpResponseMessage EditEntity(string entity_gid)
        {
            application360 values = new application360();
            objDaSystemMaster.DaEditEntity(entity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("UpdateEntity")]
        [HttpPost]
        public HttpResponseMessage UpdateEntity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateEntity(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveEntity")]
        [HttpPost]
        public HttpResponseMessage InactiveEntity(application360 values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveEntity(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteEntity")]
        [HttpGet]
        public HttpResponseMessage DeleteEntity(string entity_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteEntity(entity_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("EntityInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage EntityInactiveLogview(string entity_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaEntityInactiveLogview(entity_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



       // Team Master -Team Manager

        [ActionName("Employee")]
        [HttpGet]
        public HttpResponseMessage getEmployee()
        {
            MdlEmployee objMdlEmployee = new MdlEmployee();
            objDaSystemMaster.DaGetEmployee(objMdlEmployee);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }

        [ActionName("TeamEmployee")]
        [HttpGet]
        public HttpResponseMessage getteamEmployee()
        {
            MdlEmployee objMdlEmployee = new MdlEmployee();
            objDaSystemMaster.DaGetmemberEmployee(objMdlEmployee);
            return Request.CreateResponse(HttpStatusCode.OK, objMdlEmployee);
        }




        //Task

        [ActionName("PostTaskAdd")]
        [HttpPost]
        public HttpResponseMessage PostTaskAdd(MdlTask values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostTaskAdd(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        [ActionName("GetTaskSummary")]
        [HttpGet]
        public HttpResponseMessage GetTaskSummary()
        {
            MdlSystemMaster objmaster = new MdlSystemMaster();
            objDaSystemMaster.DaGetTaskSummary(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("GetEmployeelist")]
        [HttpGet]
        public HttpResponseMessage GetEmployeelist()
        {
            mdlemployee objmaster = new mdlemployee();
            objDaSystemMaster.DaGetEmployeelist(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("EditTask")]
        [HttpGet]
        public HttpResponseMessage EditTask(string task_gid)
        {
            MdlTask objmaster = new MdlTask();
            objDaSystemMaster.DaEditTask(task_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("UpdateTask")]
        [HttpPost]
        public HttpResponseMessage UpdateTask(MdlTask values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateTask(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("InactiveTask")]
        [HttpPost]
        public HttpResponseMessage InactiveTask(master values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveTask(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TaskInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage TaskInactiveLogview(string task_gid)
        {
            MdlSystemMaster values = new MdlSystemMaster();
            objDaSystemMaster.DaTaskInactiveLogview(task_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("DeleteTask")]
        [HttpGet]
        public HttpResponseMessage DeleteTask(string task_gid)
        {
            result values = new result();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteTask(task_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("GetTaskMultiselectList")]
        [HttpGet]
        public HttpResponseMessage GetTaskMultiselectList(string task_gid)
        {
            MdlTask objmaster = new MdlTask();
            objDaSystemMaster.DaGetTaskMultiselectList(task_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        //Add

        [ActionName("PostTeammaster")]
        [HttpPost]
        public HttpResponseMessage PostTeammaster(Mdlteam values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaPostTeammaster(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        //summary
        [ActionName("GetTeammaster")]
        [HttpGet]
        public HttpResponseMessage GetTeammaster()
        {
            Mdlteam objmaster = new Mdlteam();
            objDaSystemMaster.DaGetTeammaster(objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }


        [ActionName("Getteammastermembers")]
        [HttpGet]
        public HttpResponseMessage Getteammastermembers(string team_gid)
        {
            teammemberslist values = new teammemberslist();
            objDaSystemMaster.DaGetteammastermembers(team_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Edit

        [ActionName("GetTeammembersEdit")]
        [HttpGet]
        public HttpResponseMessage GetTeammembersEdit(string team_gid)
        {
            Mdlteam objmaster = new Mdlteam();
            objDaSystemMaster.DaGetTeammembersEdit(team_gid, objmaster);
            return Request.CreateResponse(HttpStatusCode.OK, objmaster);
        }

        [ActionName("UpdateTeamDtl")]
        [HttpPost]
        public HttpResponseMessage UpdateTeamDtl(Mdlteam values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaUpdateTeamDtl(getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }
        //updatae
        [ActionName("InactiveTeamMaster")]
        [HttpPost]
        public HttpResponseMessage InactiveTeamMaster(Mdlteam values)
        {
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaInactiveTeamMaster(values, getsessionvalues.employee_gid);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }

        [ActionName("TeamMasterInactiveLogview")]
        [HttpGet]
        public HttpResponseMessage TeamMasterInactiveLogview(string team_gid)
        {
            Mdlteam values = new Mdlteam();
            objDaSystemMaster.TeamMasterInactiveLogview(team_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }


        //Delete

        [ActionName("DeleteTeammaster")]
        [HttpGet]
        public HttpResponseMessage DeleteTeammaster(string team_gid)
        {
            Mdlteam values = new Mdlteam();
            string token = Request.Headers.GetValues("Authorization").FirstOrDefault();
            getsessionvalues = Objgetgid.gettokenvalues(token);
            objDaSystemMaster.DaDeleteTeammaster(team_gid, getsessionvalues.employee_gid, values);
            return Request.CreateResponse(HttpStatusCode.OK, values);
        }



    }

}

       

