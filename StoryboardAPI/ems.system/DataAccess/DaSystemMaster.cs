using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ems.utilities.Functions;
using ems.system.Models;
using ems.utilities.Functions;
using System.Data.Odbc;
using OfficeOpenXml.Style;

namespace ems.system.DataAccess
{
    public class DaSystemMaster
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        Dictionary<string, object> objGetReaderScalar;
        DataTable dt_datatable;
        OdbcDataReader objODBCDatareader, objODBCDatareader1;
        string msSQL, msGetGid, msGetcodeGid,
msGetteam2member_gid,
 msGet_LocationGid, clusterGID, msGet_clusterGid, regionGID, msGet_regionGid, msGetTaskCode, msGetUserCode,
            msGetTask2AssignedToGid, msGetTask2EscalationMailToGid, msGetHRCode, msGetHR2NotifyToGid, msGetAPICode, msGetsystem_ownername_gid;
        int mnResult, mnResultSub1, mnResultSub2;
        string lslevelonemodule_gid, module_gid, lsleveltwomodule_gid, module_gid_parent, lsleveltwomodulestatus_gid, lslevelonemodulestatus_gid;
        string lsmaster_value, lslms_code, lsbureau_code, lsbase_value, lssalutation_value, lsproject_value, lsbloodgroup_value, lsdocumentgid;
        string lsleveloneparent_gid, lsleveltwoparent_gid, lslevelthreeparent_gid, lsleveltwo_name, lslevelone_name,
           lslevelthree_name1, lsleveltwo_name1, lslevelone_name1;
        string lscreated_date, lscreated_by, lsleveltwomodule1_gid, lslevelthreeparent1_gid, lsleveltwoparent1_gid, lsleveloneparent1_gid,
             lsleveltwomodulemenu_gid, lslevelonemodulemenu_gid, lsuser_gid;
        string lsemployee_gid, lsemployee_name, lsemployeegroup_gid, lsemployeegroup_name, lslevelfourparent_gid, lslevelthree_name, lslevelthreemodule_gid;
        string lsuser_code, lsexternalsystem_name;

        public string msGetSeqGid { get; private set; }
        public string lsentity_code { get; private set; }


     

        public void DaGetFirstLevelMenu(menu objmaster)
        {
            try
            {
                msSQL = " SELECT module_gid,module_name FROM adm_mst_tmodule where module_gid_parent='$'" +
                        " order by display_order asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmenu_list = new List<menu_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmenu_list.Add(new menu_list
                        {
                            module_gid = (dr_datarow["module_gid"].ToString()),
                            module_name = (dr_datarow["module_name"].ToString()),
                        });
                    }
                    objmaster.menu_list = getmenu_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }
        public void DaGetSecondLevelMenu(menu objmaster, string module_gid_parent)
        {
            try
            {
                msSQL = " SELECT module_gid,module_name FROM adm_mst_tmodule where module_gid_parent='" + module_gid_parent + "'" +
                        " order by display_order asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmenu_list = new List<menu_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmenu_list.Add(new menu_list
                        {
                            module_gid = (dr_datarow["module_gid"].ToString()),
                            module_name = (dr_datarow["module_name"].ToString()),
                        });
                    }
                    objmaster.menu_list = getmenu_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }
        public void DaGetThirdLevelMenu(menu objmaster, string module_gid_parent)
        {
            try
            {
                msSQL = " SELECT module_gid,module_name FROM adm_mst_tmodule where module_gid_parent='" + module_gid_parent + "'" +
                        "  order by display_order asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmenu_list = new List<menu_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmenu_list.Add(new menu_list
                        {
                            module_gid = (dr_datarow["module_gid"].ToString()),
                            module_name = (dr_datarow["module_name"].ToString()),
                        });
                    }
                    objmaster.menu_list = getmenu_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetFourthLevelMenu(menu objmaster, string module_gid_parent)
        {
            try
            {
                msSQL = " SELECT module_gid,module_name FROM adm_mst_tmodule where module_gid_parent='" + module_gid_parent + "'" +
                        " order by display_order asc";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmenu_list = new List<menu_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmenu_list.Add(new menu_list
                        {
                            module_gid = (dr_datarow["module_gid"].ToString()),
                            module_name = (dr_datarow["module_name"].ToString()),
                        });
                    }
                    objmaster.menu_list = getmenu_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaPostMenudAdd(menu values, string employee_gid)
        {
            try
            {
                msSQL = " SELECT module_gid_parent FROM adm_mst_tmodule where module_gid='" + values.module_gid + "'";
                lslevelfourparent_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT module_gid_parent FROM adm_mst_tmodule where module_gid='" + lslevelfourparent_gid + "'";
                lslevelthreeparent_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT module_gid_parent FROM adm_mst_tmodule where module_gid='" + lslevelthreeparent_gid + "'";
                lsleveltwoparent_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT module_gid_parent FROM adm_mst_tmodule where module_gid='" + lsleveltwoparent_gid + "'";
                lsleveloneparent_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT module_name FROM adm_mst_tmodule where module_gid='" + lslevelfourparent_gid + "'";
                lslevelthree_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT module_name FROM adm_mst_tmodule where module_gid='" + lslevelthreeparent_gid + "'";
                lsleveltwo_name = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT module_name FROM adm_mst_tmodule where module_gid='" + lsleveltwoparent_gid + "'";
                lslevelone_name = objdbconn.GetExecuteScalar(msSQL);

                msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lsleveltwoparent_gid + "'";
                lslevelonemodule_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lslevelthreeparent_gid + "'";
                lsleveltwomodule_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lslevelfourparent_gid + "'";
                lslevelthreemodule_gid = objdbconn.GetExecuteScalar(msSQL);

                if (String.IsNullOrEmpty(lslevelonemodule_gid))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("MENU");
                    msSQL = " insert into sys_mst_tmenumapping(" +
                            " menu_gid," +
                            " module_gid_parent ," +
                            " module_gid ," +
                            " module_name," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + lsleveloneparent_gid + "'," +
                            "'" + lsleveltwoparent_gid + "'," +
                            "'" + lslevelone_name + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lsleveltwoparent_gid + "' and status ='Y'";
                    lslevelonemodulestatus_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (String.IsNullOrEmpty(lslevelonemodulestatus_gid))
                    {

                        msSQL = " Update sys_mst_tmenumapping set status ='Y' where module_gid='" + lsleveltwoparent_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                if (String.IsNullOrEmpty(lsleveltwomodule_gid))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("MENU");
                    msSQL = " insert into sys_mst_tmenumapping(" +
                            " menu_gid," +
                            " module_gid_parent ," +
                            " module_gid ," +
                            " module_name," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + lsleveltwoparent_gid + "'," +
                            "'" + lslevelthreeparent_gid + "'," +
                            "'" + lsleveltwo_name + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lslevelthreeparent_gid + "'and status ='Y'";
                    lsleveltwomodulestatus_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (String.IsNullOrEmpty(lsleveltwomodulestatus_gid))
                    {

                        msSQL = " Update sys_mst_tmenumapping set status ='Y' where module_gid='" + lslevelthreeparent_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                if (String.IsNullOrEmpty(lslevelthreemodule_gid))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("MENU");
                    msSQL = " insert into sys_mst_tmenumapping(" +
                            " menu_gid," +
                            " module_gid_parent ," +
                            " module_gid ," +
                            " module_name," +
                            " created_by," +
                            " created_date)" +
                            " values(" +
                            "'" + msGetGid + "'," +
                            "'" + lslevelthreeparent_gid + "'," +
                            "'" + lslevelfourparent_gid + "'," +
                            "'" + lslevelthree_name + "'," +
                            "'" + employee_gid + "'," +
                            "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
                else
                {
                    msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + lslevelfourparent_gid + "'and status ='Y'";
                    lsleveltwomodulestatus_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (String.IsNullOrEmpty(lsleveltwomodulestatus_gid))
                    {

                        msSQL = " Update sys_mst_tmenumapping set status ='Y' where module_gid='" + lslevelfourparent_gid + "'";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }
                msGetAPICode = objcmnfunctions.GetApiMasterGID("MMAC");
                msGetGid = objcmnfunctions.GetMasterGID("MENU");
                msSQL = " insert into sys_mst_tmenumapping(" +
                        " menu_gid," +
                        " api_code," +
                        " module_gid_parent," +
                        " module_gid ," +
                        " module_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + msGetAPICode + "'," +
                        "'" + lslevelfourparent_gid + "'," +
                        "'" + values.module_gid + "'," +
                        "'" + values.module_name + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Menu Added successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DaGetMenuMappingSummary(menu objmaster)
        {
            try
            {
                msSQL = " SELECT a.menu_gid,a.module_gid ,a.module_name, CONVERT(NVARCHAR(19), a.created_date, 105) as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tmenumapping a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where module_gid like '%_________%' order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmenusummary_list = new List<menusummary_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmenusummary_list.Add(new menusummary_list
                        {
                            menu_gid = (dr_datarow["menu_gid"].ToString()),
                            module_gid = (dr_datarow["module_gid"].ToString()),
                            module_name = (dr_datarow["module_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString())
                        });
                    }
                    objmaster.menusummary_list = getmenusummary_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetMenuMappingEdit(string menu_gid, menu values)
        {
            try
            {
                msSQL = " select menu_gid, module_gid_parent, module_gid, module_name, status as Status from sys_mst_tmenumapping " +
                        " where menu_gid='" + menu_gid + "' ";

                objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
                if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)

                {
                    values.menu_gid = objGetReaderScalar["menu_gid"].ToString();
                    values.module_gid_parent = objGetReaderScalar["module_gid_parent"].ToString();
                    values.module_gid = objGetReaderScalar["module_gid"].ToString();
                    values.module_name = objGetReaderScalar["module_name"].ToString();
                    values.Status = objGetReaderScalar["Status"].ToString();
                }
                  
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaGetMenuMappingInactivate(menu values, string employee_gid)
        {
            msSQL = " update sys_mst_tmenumapping set status='" + values.rbo_status + "'" +
                    " where menu_gid ='" + values.menu_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                msSQL = " SELECT module_gid_parent FROM sys_mst_tmenumapping where menu_gid='" + values.menu_gid + "'";
                module_gid = objdbconn.GetExecuteScalar(msSQL);
                msSQL = " SELECT module_gid_parent FROM sys_mst_tmenumapping where module_gid='" + module_gid + "'";
                module_gid_parent = objdbconn.GetExecuteScalar(msSQL);
                if (values.rbo_status == 'N')
                {
                    msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid_parent='" + module_gid + "' and status='Y'";
                    lsleveltwoparent_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (String.IsNullOrEmpty(lsleveltwoparent_gid))
                    {
                        msSQL = " update sys_mst_tmenumapping set status='" + values.rbo_status + "'" +
                       " where module_gid ='" + module_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT module_gid_parent FROM sys_mst_tmenumapping where module_gid_parent='" + module_gid_parent + "' and status='Y'";
                        lsleveloneparent_gid = objdbconn.GetExecuteScalar(msSQL);

                        if (String.IsNullOrEmpty(lsleveloneparent_gid))
                        {
                            msSQL = " update sys_mst_tmenumapping set status='" + values.rbo_status + "'" +
                           " where module_gid ='" + module_gid_parent + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }
                else
                {
                    msSQL = " SELECT module_gid FROM sys_mst_tmenumapping where module_gid='" + module_gid + "' and status='Y'";
                    lsleveltwoparent_gid = objdbconn.GetExecuteScalar(msSQL);

                    if (String.IsNullOrEmpty(lsleveltwoparent_gid))
                    {
                        msSQL = " update sys_mst_tmenumapping set status='" + values.rbo_status + "'" +
                       " where module_gid ='" + module_gid + "' ";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                        msSQL = " SELECT module_gid_parent FROM sys_mst_tmenumapping where module_gid='" + module_gid_parent + "' and status='Y'";
                        lsleveloneparent_gid = objdbconn.GetExecuteScalar(msSQL);

                        if (String.IsNullOrEmpty(lsleveloneparent_gid))
                        {
                            msSQL = " update sys_mst_tmenumapping set status='" + values.rbo_status + "'" +
                           " where module_gid ='" + module_gid_parent + "' ";
                            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                        }
                    }
                }



                msGetGid = objcmnfunctions.GetMasterGID("UMNU");

                msSQL = " insert into sys_mst_tmenumappinginactivelog (" +
                      " menuinactive_gid, " +
                      " menu_gid," +
                      " module_name," +
                      " status," +
                      " remarks," +
                      " created_by," +
                      " created_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.menu_gid + "'," +
                      " '" + values.module_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Menu Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Menu Type Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaGetMenuMappingInactivateview(string menu_gid, menu values)
        {
            try
            {
                msSQL = " SELECT menu_gid,CONVERT(NVARCHAR(19), a.created_date, 105) as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tmenumappinginactivelog a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where menu_gid ='" + menu_gid + "' order by a.menuinactive_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<menusummary_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new menusummary_list
                        {
                            menu_gid = (dr_datarow["menu_gid"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.menusummary_list = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }




        public void DaCreateSubFunction(master values, string employee_gid)
        {

            

            string  subfunction_name;

            msSQL = " SELECT subfunction_name FROM sys_mst_tsubfunction ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getSegment = new List<CalendarGroupComparison_List>();

            if (dt_datatable != null && dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    //getSegment.Add(new EncoreProductComparison_List
                    //{

                    subfunction_name = (dr_datarow["subfunction_name"].ToString());

                    if (subfunction_name == values.subfunction_name)
                    {
                        values.message = "This Sub Function Already Exists";
                        values.status = false;
                        return;
                    }
                }

                dt_datatable.Dispose();
            }
            msGetGid = objcmnfunctions.GetMasterGID("SCRT");
            msGetAPICode = objcmnfunctions.GetApiMasterGID("SUBF");
            msSQL = " insert into sys_mst_tsubfunction(" +
                    " subfunction_gid  ," +
                     " api_code," +
                    " subfunction_name  ," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + values.subfunction_name.Replace("'", "\\'") + "'," +
                    "'" + employee_gid + "'," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;

                values.message = "Sub Function Added Successfully";

                values.message = "Sub Function Added Successfully";

                values.message = "Sub Function Added Successfully";

            }
            else
            {
                values.message = "Error Occured while Adding";
                values.status = false;
            }
        }


        public void DaGetSubFunction(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.subfunction_gid ,a.subfunction_name , CONVERT(NVARCHAR(19), a.created_date, 105) as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tsubfunction a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.subfunction_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            subfunction_gid = (dr_datarow["subfunction_gid"].ToString()),

                            subfunction_name = (dr_datarow["subfunction_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }

            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }


        public void DaEditSubFunction(string subfunction_gid, master values)
        {
            try
            {
                msSQL = " SELECT subfunction_gid ,subfunction_name , status as Status FROM sys_mst_tsubfunction " +
                        " where subfunction_gid ='" + subfunction_gid + "' ";

                objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
                if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
                {
                    values.subfunction_gid = objGetReaderScalar["subfunction_gid"].ToString();
                    values.subfunction_name = objGetReaderScalar["subfunction_name"].ToString();
                    values.Status = objGetReaderScalar["Status"].ToString();
                }
                  
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }



        public void DaDeleteSubFunction(string subfunction_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tsubfunction  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where subfunction_gid='" + subfunction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Sub Function Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }


        public void DaSubFunctionInactiveLogview(string subfunction_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT a.subfunction_gid ,CONVERT(NVARCHAR(19), a.updated_date, 105) as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tsubfunctioninactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.subfunction_gid  ='" + subfunction_gid + "' order by a.subfunctioninactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            subfunction_gid = (dr_datarow["subfunction_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        public void DaInactiveSubFunction(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tsubfunction set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "\\'") + "'" +
                    " where subfunction_gid ='" + values.subfunction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SCRI");

                msSQL = " insert into sys_mst_tsubfunctioninactivelog (" +
                      " subfunctioninactivelog_gid   , " +
                      " subfunction_gid ," +
                      " subfunction_name  ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.subfunction_gid + "'," +
                      " '" + values.subfunction_name.Replace("'", "\\'") + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "\\'") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Sub Function Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Sub Function Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaUpdateSubFunction(string employee_gid, master values)
        {
   

            string  subfunction_name;

            msSQL = " SELECT subfunction_name FROM sys_mst_tsubfunction ";

            dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getSegment = new List<CalendarGroupComparison_List>();

            if (dt_datatable != null && dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dr_datarow in dt_datatable.Rows)
                {

                    //getSegment.Add(new EncoreProductComparison_List
                    //{

                  
                    subfunction_name = (dr_datarow["subfunction_name"].ToString());

                    if ( subfunction_name == values.subfunction_name)
                    {
                        values.message = "This Sub Function Already Exists";
                        values.status = false;
                        return;
                    }
                }

                dt_datatable.Dispose();
            }

            msSQL = "select updated_by, updated_date,subfunction_gid from sys_mst_tsubfunction where subfunction_gid  ='" + values.subfunction_gid + "' ";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);

            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                string lsUpdatedBy = objGetReaderScalar["updated_by"].ToString();
                string lsUpdatedDate = objGetReaderScalar["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("SCRL");
                    msSQL = " insert into sys_mst_tsubfunctionlog(" +
                              " subfunction_loggid   ," +
                              " subfunction_gid ," +
                              " subfunction_name , " +
                              " created_by, " +
                              " created_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.subfunction_gid + "'," +
                              "'" + values.subfunction_name.Replace("'", "\\'") + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }
            msSQL = " update sys_mst_tsubfunction set " +
                    " subfunction_name ='" + values.subfunction_name.Replace("'", "\\'") + "'," +
                     " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where subfunction_gid ='" + values.subfunction_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;

                values.message = "Sub Function Updated Successfully";

    

            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating";
            }
        }


        //Designation

        public void DaGetDesignation(MdlDesignation objMdlDesignation)
        {
            try
            {
                msSQL = " SELECT a.designation_gid,a.designation_name,status_log, " +
                    " CONVERT(NVARCHAR(19), a.created_date, 105) as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by " +
                    " from adm_mst_tdesignation a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid   order by a.designation_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getdesignation = new List<designation_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getdesignation.Add(new designation_list
                        {
                            designation_gid = (dr_datarow["designation_gid"].ToString()),

                            designation_type = (dr_datarow["designation_name"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objMdlDesignation.designation_list = getdesignation;
                }
                objMdlDesignation.status = true;
            }
            catch
            {
                objMdlDesignation.status = false;
            }
        }

        public void DaCreateDesignation(designation values, string employee_gid)
        {

           
            msSQL = "select designation_name from adm_mst_tdesignation where designation_name = '" + values.designation_type.Replace("'", "\\'") + "' ";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                values.status = false;
                values.message = "Designation Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("SDGM");
                
                msSQL = " insert into adm_mst_tdesignation(" +
                        " designation_gid," +
                        " designation_name," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                      "'" + values.designation_type.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Designation Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }

        public void DaEditDesignation(string designation_gid, designation values)
        {
            try
            {
                msSQL = " select designation_gid,status_log ,designation_name from adm_mst_tdesignation where designation_gid='" + designation_gid + "' ";

                objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
                if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
                {
                    values.designation_type = objGetReaderScalar["designation_name"].ToString();
                    values.designation_gid = objGetReaderScalar["designation_gid"].ToString();
                    values.status_log = objGetReaderScalar["status_log"].ToString();
                }
                  
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateDesignation(string employee_gid, designation values)
        {
            msSQL = "select designation_gid from adm_mst_tdesignation where designation_name = '" + values.designation_type.Replace("'", "\\'") + "'";
            lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != "")
            {
                if (lsdocumentgid == values.designation_gid)
                {
                    values.message = "Designation Type Already Exist";
                    values.status = false;
                    return;
                }
            }

            

            msSQL = " update adm_mst_tdesignation set " +
        " designation_name='" + values.designation_type.Replace("'", "") + "'," +
        " updated_by='" + employee_gid + "'," +
        " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
        " where designation_gid='" + values.designation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " select designation_name from adm_mst_tdesignation where designation_gid='" + values.designation_gid + "'";
            lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
            if (mnResult != 0)
            {
               
                msGetGid = objcmnfunctions.GetMasterGID("DLOG");
                msSQL = " insert into ocs_trn_tauditdesignationlog(" +
                          " auditdesignationlog_gid," +
                          " designation_gid," +
                          " designation_name, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.designation_gid + "'," +
                          "'" + lsmaster_value + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Designation Type Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }


        public void DaDeleteDesignation(string designation_gid, string employee_gid, designation values)
        {

            msSQL = "select application_gid from ocs_mst_tapplication where designation_gid = '" + designation_gid + "'";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                  
                values.message = "Can't able to delete Designation, Because it is tagged to Application Creation";
                values.status = false;
                return;
            }
            else
            {

                msSQL = " select institution_gid from ocs_mst_tinstitution where designation_gid='" + designation_gid + "'";
                objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
                if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
                {
                      
                    values.message = "Can't able to delete Designation, Because it is tagged to Application Creation";
                    values.status = false;
                    return;
                }
                else
                {
                    
                    msSQL = " select designation_name from adm_mst_tdesignation where designation_gid='" + designation_gid + "'";
                    lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
                    msSQL = " delete from adm_mst_tdesignation where designation_gid='" + designation_gid + "'";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    if (mnResult != 0)
                    {
                        values.status = true;
                        msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                        msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                                 "master_gid, " +
                                 "master_name, " +
                                 "master_value, " +
                                 "deleted_by, " +
                                 "deleted_date) " +
                                 " values(" +
                                 "'" + msGetGid + "'," +
                                 "'Designation'," +
                                 "'" + lsmaster_value + "'," +
                                 "'" + employee_gid + "'," +
                                 "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                    }
                    else
                    {
                        values.status = false;
                    }
                }

            }
        }


        public void DaDesignationStatusUpdate(string employee_gid, designation values)
        {

            msSQL = " update adm_mst_tdesignation set status_log='" + values.status_log + "'," +
                " remarks='" + values.remarks.Replace("'", " ") + "'," +
                " updated_by='" + employee_gid + "'," +
                " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                " where designation_gid='" + values.designation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("DLOG");
                msSQL = " insert into ocs_trn_tdesignationtatuslog(" +
                          " designationstatuslog_gid," +
                          " designation_gid," +
                          " status_log, " +
                          " remarks, " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.designation_gid + "'," +
                          "'" + values.status_log + "'," +
                          "'" + values.remarks.Replace("'", " ") + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.status_log == "N")
                {
                    values.status = true;
                    values.message = "Designation Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Designation Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }



        }

        //Get Active Status Log
        public void DaGetActiveLog(string designation_gid, MdlDesignation objgetsegment)
        {
            try
            {
                msSQL = " SELECT d.designation_name,a.status_log,a.remarks, " +
                    "CONVERT(NVARCHAR(19), a.created_date, 105) as created_date,concat(c.user_firstname,' ' ,c.user_lastname,'||',c.user_code) as created_by" +

                    " FROM ocs_trn_tdesignationtatuslog a" +
                    " left join hrm_mst_temployee b on a.created_by=b.employee_gid" +
                    " left join adm_mst_tuser c on c.user_gid=b.user_gid " +
                    "  left join adm_mst_tdesignation d on a.designation_gid=d.designation_gid where a.designation_gid='" + designation_gid + "' order by a.designationstatuslog_gid desc ";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getSegment = new List<designation_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getSegment.Add(new designation_list
                        {
                            designation_type = (dr_datarow["designation_name"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                            status_log = (dr_datarow["status_log"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                        });
                    }
                    objgetsegment.designation_list = getSegment;
                }
                dt_datatable.Dispose();
                objgetsegment.status = true;

            }
            catch
            {
                objgetsegment.status = false;
            }
        }







        public void DaGetBaseLocationlist(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.baselocation_gid ,a.baselocation_name " +
                        " FROM sys_mst_tbaselocation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlocation_list = new List<location_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlocation_list.Add(new location_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),

                        });
                    }
                    objmaster.location_list = getlocation_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }
        public bool DaPopSubfunction(employee_list objemployee_list)
        {
            try
            {
                msSQL = "select subfunction_gid,subfunction_name from sys_mst_tsubfunction where status='Y' and delete_flag='N'; ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_subfunction_list = new List<employee>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_row in dt_datatable.Rows)
                    {
                        get_subfunction_list.Add(new employee
                        {
                            subfunction_gid = dr_row["subfunction_gid"].ToString(),
                            subfunction_name = dr_row["subfunction_name"].ToString()
                        });
                    }
                    objemployee_list.employee = get_subfunction_list;
                    objemployee_list.status = true;
                    dt_datatable.Dispose();
                    return true;
                }

                else
                {
                    objemployee_list.status = false;
                    dt_datatable.Dispose();
                    return false;
                }
            }
            catch (Exception ex)
            {
                objemployee_list.status = false;
                return false;
            }

        }



        //Branch Summary

        public void DaGetBranchSummary(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.branch_gid,api_code,branch_code,branch_name, branch_prefix, " +
                        " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as branchmanager_gid," +
                        " branch_location FROM hrm_mst_tbranch a " +
                        " left join hrm_mst_temployee b on a.branchmanager_gid = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.branch_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            branch_gid = (dr_datarow["branch_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            branch_code = (dr_datarow["branch_code"].ToString()),
                            branch_name = (dr_datarow["branch_name"].ToString()),
                            branch_prefix = (dr_datarow["branch_prefix"].ToString()),
                            branchmanager_gid = (dr_datarow["branchmanager_gid"].ToString()),
                            branch_location = (dr_datarow["branch_location"].ToString()),
                            //status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaGetDepartmentSummary(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.department_gid,api_code,department_code,department_prefix, department_name, " +
                       " concat(c.user_firstname, ' ', c.user_lastname, ' / ', c.user_code) as department_manager " +
                       " FROM hrm_mst_tdepartment a " +
                       " left join hrm_mst_temployee b on a.department_manager = b.employee_gid " +
                       " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.department_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            department_gid = (dr_datarow["department_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            department_code = (dr_datarow["department_code"].ToString()),
                            department_prefix = (dr_datarow["department_prefix"].ToString()),
                            department_name = (dr_datarow["department_name"].ToString()),
                            department_manager = (dr_datarow["department_manager"].ToString()),

                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        //Base Location

        public void DaGetBaseLocation(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.baselocation_gid ,a.api_code,a.baselocation_name, CONVERT(NVARCHAR(19), a.created_date, 105) as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tbaselocation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }


        public void DaGetBaseLocationlistActive(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.baselocation_gid ,a.baselocation_name " +
                        " FROM sys_mst_tbaselocation a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' and status='Y' order by a.baselocation_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getlocation_list = new List<location_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getlocation_list.Add(new location_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            baselocation_name = (dr_datarow["baselocation_name"].ToString()),

                        });
                    }
                    objmaster.location_list = getlocation_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateBaseLocation(master values, string employee_gid)
        {
           
            msSQL = "select baselocation_name from sys_mst_tbaselocation where baselocation_name = '" + values.baselocation_name.Replace("'", "\\'") + "' ";

            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                values.status = false;
                values.message = "Base Location Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("SBLT");
                msGetAPICode = objcmnfunctions.GetApiMasterGID("BSLN");
                msSQL = " insert into sys_mst_tbaselocation(" +
                        " baselocation_gid ," +
                        " api_code," +
                        " baselocation_name ," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                       "'" + msGetAPICode + "'," +
                      "'" + values.baselocation_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Base Location Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }
        }
        public void DaEditBaseLocation(string baselocation_gid, master values)
        {
            try
            {
                msSQL = " SELECT baselocation_gid,baselocation_name, status as Status FROM sys_mst_tbaselocation " +
                        " where baselocation_gid='" + baselocation_gid + "' ";


                objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
                if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
                {
                    values.baselocation_gid = objGetReaderScalar["baselocation_gid"].ToString();
                    values.baselocation_name = objGetReaderScalar["baselocation_name"].ToString();
                  
                    //values.status_baselocation = objODBCDatareader["status_baselocation"].ToString();
                    values.Status = objGetReaderScalar["Status"].ToString();

                }
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateBaseLocation(string employee_gid, master values)
        {
            msSQL = "select baselocation_gid from sys_mst_tbaselocation where delete_flag='N' and baselocation_name = '" + values.baselocation_name.Replace("'", "\\'") + "'";
          if (lsdocumentgid != null & lsdocumentgid != "")
                if (lsdocumentgid != null & lsdocumentgid != "")
                {
                if (lsdocumentgid != values.baselocation_gid)
                {
                    values.message = "Base Location Already Exist";
                    values.status = false;
                    return;
                }
            }
          

            msSQL = " update sys_mst_tbaselocation set " +
            " baselocation_name='" + values.baselocation_name.Replace("'", "") + "'," +
            " updated_by='" + employee_gid + "'," +
            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where baselocation_gid='" + values.baselocation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                //msGetGid = objcmnfunctions.GetMasterGID("MELG");
                msGetGid = objcmnfunctions.GetMasterGID("SBLL");
                msSQL = " insert into sys_mst_tbaselocationlog(" +
                          " baselocation_loggid," +
                          " baselocation_gid," +
                          " baselocation_name , " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.baselocation_gid + "'," +
                          "'" + lsbase_value + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Base Location Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }


        public void DaInactiveBaseLocation(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tbaselocation set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where baselocation_gid='" + values.baselocation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SBLI");

                msSQL = " insert into sys_mst_tbaselocationinactivelog (" +
                      " baselocationinactivelog_gid   , " +
                      " baselocation_gid," +
                      " baselocation_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.baselocation_gid + "'," +
                      " '" + values.baselocation_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Base Location Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Base Location Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteBaseLocation(string baselocation_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tbaselocation  set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where baselocation_gid='" + baselocation_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Base Location Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaBaseLocationInactiveLogview(string baselocation_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT a.baselocation_gid,CONVERT(NVARCHAR(19), a.updated_date, 105) as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tbaselocationinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.baselocation_gid ='" + baselocation_gid + "' order by a.baselocationinactivelog_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            baselocation_gid = (dr_datarow["baselocation_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
              
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }


        //Blood Group

        public void DaGetBloodGroup(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.bloodgroup_gid ,a.api_code,a.bloodgroup_name, CONVERT(NVARCHAR(19), a.created_date, 105) as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_tbloodgroup a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.bloodgroup_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            bloodgroup_gid = (dr_datarow["bloodgroup_gid"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                            bloodgroup_name = (dr_datarow["bloodgroup_name"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }


        public void DaGetBloodGroupActive(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.bloodgroup_gid ,a.bloodgroup_name FROM sys_mst_tbloodgroup a where a.delete_flag='N' and a.status='Y' and delete_flag='N' order by a.bloodgroup_gid desc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            bloodgroup_gid = (dr_datarow["bloodgroup_gid"].ToString()),
                            bloodgroup_name = (dr_datarow["bloodgroup_name"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateBloodGroup(master values, string employee_gid)
        {
            msSQL = "select bloodgroup_name from sys_mst_tbloodgroup where bloodgroup_name = '" + values.bloodgroup_name.Replace("'", "\\'") + "' and delete_flag='N' ";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                values.status = false;
                values.message = "Blood Group Already Exist";
            }
            else
            {
                msGetGid = objcmnfunctions.GetMasterGID("SBGT");
                msGetAPICode = objcmnfunctions.GetApiMasterGID("BLOD");
                msSQL = " insert into sys_mst_tbloodgroup(" +
                        " bloodgroup_gid ," +
                        " api_code," +
                        " bloodgroup_name ," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + msGetAPICode + "'," +
                        "'" + values.bloodgroup_name.Replace("'", "") + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Blood Group Added Successfully";
                }
                else
                {
                    values.message = "Error Occured while Adding";
                    values.status = false;
                }
            }
        }
        public void DaEditBloodGroup(string bloodgroup_gid, master values)
        {
            try
            {
                msSQL = " SELECT bloodgroup_gid,bloodgroup_name, status as Status FROM sys_mst_tbloodgroup " +
                        " where bloodgroup_gid='" + bloodgroup_gid + "' ";

                objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
                if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
                {
                    values.bloodgroup_gid = objGetReaderScalar["bloodgroup_gid"].ToString();
                    values.bloodgroup_name = objGetReaderScalar["bloodgroup_name"].ToString();
                    values.Status = objGetReaderScalar["Status"].ToString();
                }
         
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }


        public void DaUpdateBloodGroup(string employee_gid, master values)
        {
            msSQL = "select bloodgroup_gid from sys_mst_tbloodgroup where delete_flag='N' and bloodgroup_name = '" + values.bloodgroup_name.Replace("'", "\\'") + "'";
            lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != null & lsdocumentgid != "")
            {
                if (lsdocumentgid != values.bloodgroup_gid)
                {
                    values.message = "Blood group Name Already Exist";
                    values.status = false;
                    return;
                }
            }
            

            msSQL = " update sys_mst_tbloodgroup set " +
            " bloodgroup_name='" + values.bloodgroup_name.Replace("'", "") + "'," +
            " updated_by='" + employee_gid + "'," +
            " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
            " where bloodgroup_gid='" + values.bloodgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


            if (mnResult != 0)
            {
                //msGetGid = objcmnfunctions.GetMasterGID("MELG");
                msGetGid = objcmnfunctions.GetMasterGID("SBGL");
                msSQL = " insert into sys_mst_tbloodgrouplog(" +
                          " bloodgroup_loggid   ," +
                          " bloodgroup_gid," +
                          " bloodgroup_name , " +
                          " created_by, " +
                          " created_date) " +
                          " values(" +
                          "'" + msGetGid + "'," +
                          "'" + values.bloodgroup_gid + "'," +
                          "'" + lsbloodgroup_value + "'," +
                          "'" + employee_gid + "'," +
                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Blood Group Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }
        }


        public void DaInactiveBloodGroup(master values, string employee_gid)
        {
            msSQL = " update sys_mst_tbloodgroup set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where bloodgroup_gid='" + values.bloodgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("SBGI");

                msSQL = " insert into sys_mst_tbloodgroupinactivelog (" +
                      " bloodgroupinactivelog_gid   , " +
                      " bloodgroup_gid," +
                      " bloodgroup_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.bloodgroup_gid + "'," +
                      " '" + values.bloodgroup_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Blood Group Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Blood Group Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteBloodGroup(string bloodgroup_gid, string employee_gid, master values)
        {
            msSQL = " update sys_mst_tbloodgroup   set delete_flag='Y'," +
                    " deleted_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                   " deleted_by='" + employee_gid + "'" +
                   " where bloodgroup_gid='" + bloodgroup_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Blood Group Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }

        public void DaBloodGroupInactiveLogview(string bloodgroup_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT a.bloodgroup_gid,CONVERT(NVARCHAR(19),a.updated_date, 105) as updated_date , " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tbloodgroupinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.bloodgroup_gid ='" + bloodgroup_gid + "' order by a.bloodgroupinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            bloodgroup_gid = (dr_datarow["bloodgroup_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }



        // Entity

        public void DaGetEntity(MdlSystemMaster objmaster)
        {
            try
            {
                msSQL = " SELECT a.entity_gid,a.entity_name,a.entity_code, CONVERT(NVARCHAR(19), a.created_date, 105) as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM adm_mst_tentity a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.entity_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<application_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new application_list
                        {
                            entity_gid = (dr_datarow["entity_gid"].ToString()),
                            entity_name = (dr_datarow["entity_name"].ToString()),
                            entity_code = (dr_datarow["entity_code"].ToString()),

                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                        });
                    }
                    objmaster.application_list = getapplication_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

        public void DaCreateEntity(application360 values, string employee_gid)
        {
           

            if (values.entity_code == null || values.entity_code == "")
            {
                lsentity_code = "";
            }
            else
            {
                lsentity_code = values.entity_code.Replace("'", "");
            }
            msSQL = "select entity_name from adm_mst_tentity where entity_name = '" + values.entity_name.Replace("'", "\\'") + "' ";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                values.status = false;
                values.message = "Entity Name Already Exist";
            }
            else
            {
                msGetAPICode = objcmnfunctions.GetApiMasterGID("ENAC");
                msGetGid = objcmnfunctions.GetMasterGID("CENT");
                msSQL = " insert into adm_mst_tentity(" +
                        " entity_gid," +
                        " entity_name," +
                        " entity_code," +
                        " api_code," +
                        " created_by," +
                        " created_date)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                        "'" + values.entity_name.Replace("'", "") + "'," +
                        "'" + lsentity_code + "'," +
                        "'" + msGetAPICode + "'," +
                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                //if (mnResult != 0)
                //{

                //    msGetSeqGid = objcmnfunctions.GetMasterGID("ENSE");
                //    msSQL = " insert into ocs_mst_tentitysequenceno(" +
                //            " entitysequenceno," +
                //            " entity_gid," +
                //            " colending_dbs," +
                //            " verticalref_no," +
                //            " colending_aar," +
                //            " colending_livfin," +
                //            " colending_visage)" +
                //            " values(" +
                //            "'" + msGetSeqGid + "'," +
                //            "'" + msGetGid + "'," +
                //            "'0'," +
                //            "'0'," +
                //            "'0'," +
                //            "'0'," +
                //            "'0')";
                //    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Entity Added Successfully";
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred While Adding";
                }
            }
        }
        public void DaEditEntity(string entity_gid, application360 values)
        {
            try
            {
                msSQL = " SELECT entity_gid,entity_name,entity_code, status as Status FROM adm_mst_tentity where entity_gid='" + entity_gid + "' ";

                objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
                if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
                {
                    values.entity_gid = objGetReaderScalar["entity_gid"].ToString();
                    values.entity_name = objGetReaderScalar["entity_name"].ToString();
                    values.entity_code = objGetReaderScalar["entity_code"].ToString();
                    values.Status = objGetReaderScalar["Status"].ToString();
                }
                  
                values.status = true;

            }
            catch
            {
                values.status = false;
            }
        }

        public void DaUpdateEntity(string employee_gid, application360 values)
        {
            msSQL = "select entity_gid from adm_mst_tentity where entity_name = '" + values.entity_name.Replace("'", "\\'") + "'";
            lsdocumentgid = objdbconn.GetExecuteScalar(msSQL);
            if (lsdocumentgid != null & lsdocumentgid != "")
            {
                if (lsdocumentgid != values.entity_gid)
                {
                    values.message = "Entity Name Already Exist";
                    values.status = false;
                    return;
                }
            }
            if (values.entity_code == null || values.entity_code == "")
            {
                lsentity_code = "";
            }
            else
            {
                lsentity_code = values.entity_code.Replace("'", "");
            }
            

            msSQL = " update adm_mst_tentity set " +
             " entity_name='" + values.entity_name.Replace("'", "") + "'," +
             " entity_code='" + lsentity_code + "'," +
             " updated_by='" + employee_gid + "'," +
             " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
             " where entity_gid='" + values.entity_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("MELG");

                msSQL = " insert into ocs_mst_tentitylog (" +
                       " entity_LOGgid, " +
                       " entity_gid, " +
                       " entity_name," +
                       " updated_by," +
                       " updated_date) " +
                       " values (" +
                       " '" + msGetGid + "'," +
                       " '" + values.entity_gid + "'," +
                       " '" + values.entity_name.Replace("'", "") + "'," +
                       " '" + employee_gid + "'," +
                       " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                values.status = true;
                values.message = "Entity Updated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred While Updating";
            }

        }
        public void DaInactiveEntity(application360 values, string employee_gid)
        {
            msSQL = " update adm_mst_tentity set status='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where entity_gid='" + values.entity_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("EILG");

                msSQL = " insert into ocs_mst_tentityinactivelog (" +
                      " entityinactivelog_gid, " +
                      " entity_gid," +
                      " entity_name," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.entity_gid + "'," +
                      " '" + values.entity_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (values.rbo_status == 'N')
                {
                    values.status = true;
                    values.message = "Entity Inactivated Successfully";
                }
                else
                {
                    values.status = true;
                    values.message = "Entity Activated Successfully";
                }
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void DaDeleteEntity(string entity_gid, string employee_gid, result values)
        {
            msSQL = " select entity_name from adm_mst_tentity where entity_gid='" + entity_gid + "'";
            lsmaster_value = objdbconn.GetExecuteScalar(msSQL);
            msSQL = " delete from adm_mst_tentity where entity_gid='" + entity_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Entity Deleted Successfully";
                msGetGid = objcmnfunctions.GetMasterGID("MSTD");
                msSQL = " insert into ocs_mst_tmasterdeletelog(" +
                         "master_gid, " +
                         "master_name, " +
                         "master_value, " +
                         "deleted_by, " +
                         "deleted_date) " +
                         " values(" +
                         "'" + msGetGid + "'," +
                         "'Entity'," +
                         "'" + lsmaster_value + "'," +
                         "'" + employee_gid + "'," +
                         "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";

                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";

            }
        }

        public void DaEntityInactiveLogview(string entity_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT a.entity_gid,CONVERT(NVARCHAR(19), a.updated_date, 105) as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM ocs_mst_tentityinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.entity_gid ='" + entity_gid + "' order by a.entityinactivelog_gid desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getapplication_list = new List<application_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getapplication_list.Add(new application_list
                        {
                            entity_gid = (dr_datarow["entity_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.application_list = getapplication_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }















        //Task

        public void DaPostTaskAdd(MdlTask values, string employee_gid)
        {
            string lstat, lstask_description, lstask_name, lsteam_name = "";

            
           
            if (values.tat == null || values.tat == "")
                lstat = "";
            else
                lstat = values.tat.Replace("'", "\\'");
            if (values.task_description == null || values.task_description == "")
                lstask_description = "";
            else
                lstask_description = values.task_description.Replace("'", "\\'");
            if (values.task_name == null || values.task_name == "")
                lstask_name = "";
            else
                lstask_name = values.task_name.Replace("'", "\\'");
            if (values.team_name == null || values.team_name == "")
                lsteam_name = "";
            else
                lsteam_name = values.team_name.Replace("'", "\\'");


            msSQL = " SELECT task_name FROM sys_mst_ttask where task_name ='" + lstask_name + "'";
            string GetTaskName = objdbconn.GetExecuteScalar(msSQL);
            if (GetTaskName == lstask_name)
            {
                values.message = "Task Name Already Exists";
                values.status = false;
                return;
            }

            msGetAPICode = objcmnfunctions.GetApiMasterGID("TAAC");
            msGetGid = objcmnfunctions.GetMasterGID("STSK");
            msGetTaskCode = objcmnfunctions.GetMasterGID("TSKC");
            msSQL = " insert into sys_mst_ttask(" +
                    " task_gid ," +
                    " api_code ," +
                    " task_code ," +
                    " task_name," +
                    " team_name," +
                    " team_gid,"+
                    " task_description," +
                    " tat," +
                    " created_by," +
                    " created_date)" +
                    " values(" +
                    "'" + msGetGid + "'," +
                    "'" + msGetAPICode + "'," +
                    "'" + msGetTaskCode + "'," +
                    "'" + lstask_name + "'," +
                    "'" + lsteam_name + "'," +
                    "'" + values.team_gid + "'," +
                    "'" + lstask_description + "'," +
                    "'" + lstat + "'," +
                    "'" + employee_gid + "'," +
                      "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            //for (var i = 0; i < values.assigned_to.Count; i++)
            //{
            //    msGetTask2AssignedToGid = objcmnfunctions.GetMasterGID("TAST");
            //    msSQL = "Insert into sys_mst_ttask2assignedto( " +
            //           " task2assignedto_gid, " +
            //           " task_gid," +
            //           " assignedto_gid," +
            //           " assignedto_name," +
            //           " created_by," +
            //           " created_date)" +
            //           " values(" +
            //           "'" + msGetTask2AssignedToGid + "'," +
            //           "'" + msGetGid + "'," +
            //           "'" + values.assigned_to[i].employee_gid + "'," +
            //           "'" + values.assigned_to[i].employee_name + "'," +
            //           "'" + employee_gid + "'," +
            //           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
            //    mnResultSub1 = objdbconn.ExecuteNonQuerySQL(msSQL);
            //} 
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Task Added Successfully";
            }
            else
            {
                values.message = "Error Occured While Adding Task";
                values.status = false;
            }
        }

        public void DaGetTaskSummary(MdlSystemMaster objmaster)
        {
            try
            {

                msSQL = " SELECT a.task_gid ,a.task_name,a.team_name,a.team_gid,CONVERT(NVARCHAR(19), a.created_date, 105) as created_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by,api_code," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                        " FROM sys_mst_ttask a" +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid where a.delete_flag='N' order by a.task_gid  desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            task_gid = (dr_datarow["task_gid"].ToString()),
                            task_name = (dr_datarow["task_name"].ToString()),
                            team_name = (dr_datarow["team_name"].ToString()),
                            team_gid = (dr_datarow["team_gid"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            api_code = (dr_datarow["api_code"].ToString()),
                        });
                    }
                    objmaster.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch
            {
                objmaster.status = false;
            }
        }

            
        public void DaEditTask(string task_gid, MdlTask objmaster)
        {

            msSQL = " SELECT task_gid,task_code,task_name,team_name,team_gid,task_description,tat, status as Status FROM sys_mst_ttask " +
                    " where task_gid='" + task_gid + "' ";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                objmaster.task_gid = objGetReaderScalar["task_gid"].ToString();
                objmaster.task_code = objGetReaderScalar["task_code"].ToString();
                objmaster.task_name = objGetReaderScalar["task_name"].ToString();
                objmaster.team_name = objGetReaderScalar["team_name"].ToString();
                objmaster.team_gid = objGetReaderScalar["team_gid"].ToString();
                objmaster.task_description = objGetReaderScalar["task_description"].ToString();
                objmaster.tat = objGetReaderScalar["tat"].ToString();
                objmaster.Status = objGetReaderScalar["Status"].ToString();
            }
              

            //msSQL = " select assignedto_gid,assignedto_name from sys_mst_ttask2assignedto " +
            //" where task_gid='" + task_gid + "'";
            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //var getassignedtoList = new List<assignedto_list>();
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    foreach (DataRow dt in dt_datatable.Rows)
            //    {
            //        getassignedtoList.Add(new assignedto_list
            //        {
            //            employee_gid = dt["assignedto_gid"].ToString(),
            //            employee_name = dt["assignedto_name"].ToString(),
            //        });
            //        objmaster.assigned_to = getassignedtoList;
            //    }
            //}

            //msSQL = " SELECT a.user_firstname,a.user_gid , " +
            //    " concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
            //  " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
            //  " where user_status<>'N' order by a.user_firstname asc";

            //dt_datatable = objdbconn.GetDataTable(msSQL);
            //if (dt_datatable.Rows.Count != 0)
            //{
            //    objmaster.assignedto_general = dt_datatable.AsEnumerable().Select(row =>
            //      new assignedto_list
            //      {
            //          employee_gid = row["employee_gid"].ToString(),
            //          employee_name = row["employee_name"].ToString()
            //      }
            //    ).ToList();
            //}
            //dt_datatable.Dispose();  
        }

        public bool DaUpdateTask(string employee_gid, MdlTask values)
        {
            string  lstat, lstask_description, lstask_name, lsteam_name = "";
            if (values.tat == null || values.tat == "")
                lstat = "";
            else
                lstat = values.tat.Replace("'", "\\'");
            if (values.task_description == null || values.task_description == "")
                lstask_description = "";
            else
                lstask_description = values.task_description.Replace("'", "\\'");
            if (values.task_name == null || values.task_name == "")
                lstask_name = "";
            else
                lstask_name = values.task_name.Replace("'", "\\'");
            if (values.team_name == null || values.team_name == "")
                lsteam_name = "";
            else
                lsteam_name = values.team_name.Replace("'", "\\'");



            msSQL = " SELECT task_name FROM sys_mst_ttask where lcase(task_name) ='" + lstask_name.ToLower() + "'" +
                    " and task_gid != '" + values.task_gid + "'";
            string GetTaskName = objdbconn.GetExecuteScalar(msSQL);
            if (GetTaskName != "" && GetTaskName != null)
            {
                values.message = "Task Name Already Exists";
                values.status = false;
                return false;
            }

            msSQL = "select task_gid, task_name, updated_by, updated_date from sys_mst_ttask where task_gid='" + values.task_gid + "'";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                string lsUpdatedBy = objGetReaderScalar["updated_by"].ToString();
                string lsUpdatedDate = objGetReaderScalar["updated_date"].ToString();

                if (!(String.IsNullOrEmpty(lsUpdatedBy)) && !(String.IsNullOrEmpty(lsUpdatedDate)))
                {
                    msGetGid = objcmnfunctions.GetMasterGID("PMSL");
                    msSQL = " insert into sys_mst_ttasklog(" +
                              " tasklog_gid  ," +
                              " task_gid," +
                              " task_name, " +
                              "team_name," +
                              "team_gid," +
                              " updated_by, " +
                              " updated_date) " +
                              " values(" +
                              "'" + msGetGid + "'," +
                              "'" + values.task_gid + "'," +
                              "'" + lstask_name + "'," +
                               "'" + values.team_gid + "'," +
                               "'" + lsteam_name + "'," +
                              "'" + employee_gid + "'," +
                              "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }
            }

            msSQL = " update sys_mst_ttask set " +
                    " task_name='" + lstask_name + "'," +
                     " team_name='" + lsteam_name + "'," +
                    " tat='" + lstat + "'," +
                    " task_description='" + lstask_description + "'," +
                    " updated_by='" + employee_gid + "'," +
                     " updated_date='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                     " where task_gid='" + values.task_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Task Updated Successfully";
                return true;
            }
            else
            {
                values.status = false;
                values.message = "Error Occured While Updating Task";
                return false;
            }
        }


        public void DaInactiveTask(master values, string employee_gid)
        {
            msSQL = " select taskinitiate_gid from sys_mst_ttaskinitiate where task_gid='" + values.task_gid + "' and (task_status= 'null' or task_status = 'Initiated')";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                objODBCDatareader1.Close();
                values.message = "Can't able to inactive Task, Because it is tagged to Employee Onboarding";
                values.status = false;
            }
            else
            {
                msSQL = " update sys_mst_ttask set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "'" +
                    " where task_gid='" + values.task_gid + "' ";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                if (mnResult != 0)
                {
                    msGetGid = objcmnfunctions.GetMasterGID("STKI");

                    msSQL = " insert into sys_mst_ttaskinactivelog (" +
                          " taskinactivelog_gid  , " +
                          " task_gid," +
                          " task_name," +
                          " status," +
                          " remarks," +
                          " updated_by," +
                          " updated_date) " +
                          " values (" +
                          " '" + msGetGid + "'," +
                          " '" + values.task_gid + "'," +
                          " '" + values.task_name + "'," +
                          " '" + values.rbo_status + "'," +
                          " '" + values.remarks.Replace("'", "") + "'," +
                          " '" + employee_gid + "'," +
                          " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    if (values.rbo_status == 'N')
                    {
                        values.status = true;
                        values.message = "Task Inactivated Successfully";
                    }
                    else
                    {
                        values.status = true;
                        values.message = "Task Activated Successfully";
                    }
                }
                else
                {
                    values.status = false;
                    values.message = "Error Occurred";
                }

            }
        }

        public void DaTaskInactiveLogview(string task_gid, MdlSystemMaster values)
        {
            try
            {
                msSQL = " SELECT task_gid,CONVERT(NVARCHAR(19), a.updated_date, 105) as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_ttaskinactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where task_gid ='" + task_gid + "' order by a.taskinactivelog_gid    desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            task_gid = (dr_datarow["task_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["Status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                dt_datatable.Dispose();
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaDeleteTask(string task_gid,  result values)
        {

            msSQL = " delete from sys_mst_ttask where task_gid='" + task_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
            if (mnResult != 0)
            {
                values.status = true;
                values.message = "Task Deleted Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occured..!";
            }


        }
        public void DaGetTaskMultiselectList(string task_gid, MdlTask objmaster)
        {

            msSQL = " SELECT GROUP_CONCAT(distinct(b.assignedto_name) SEPARATOR ', ') as assignedto_name, " +
                    " GROUP_CONCAT(distinct(c.escalationmailto_name) SEPARATOR ', ') as escalationmailto_name FROM sys_mst_ttask a " +
                    " left join sys_mst_ttask2assignedto b on a.task_gid = b.task_gid" +
                    " left join sys_mst_ttask2escalationmailto c on a.task_gid = c.task_gid" +
                    " where a.task_gid='" + task_gid + "' ";

            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                objmaster.assignedto_name = objGetReaderScalar["assignedto_name"].ToString();
                objmaster.escalationmailto_name = objGetReaderScalar["escalationmailto_name"].ToString();

            }
            objODBCDatareader.Close();
        }
        public void DaGetEmployeelist(mdlemployee objmaster)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' || ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<employeelist>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    objmaster.employeelist = dt_datatable.AsEnumerable().Select(row =>
                      new employeelist
                      {
                          employee_gid = row["employee_gid"].ToString(),
                          employee_name = row["employee_name"].ToString()
                      }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objmaster.status = true;
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }
        }


        public void DaPostTeammaster(Mdlteam values, string employee_gid)
        {

            msSQL = "select team_name from sys_mst_tteam where team_name = '" + values.team_name.Replace("'", "\\'") + "'";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                values.status = false;
                values.message = "Team Name Already Exist";
            }
            else
            {

                msGetGid = objcmnfunctions.GetMasterGID("SMTT");
                msGetcodeGid = objcmnfunctions.GetMasterGID("TEON");
                msSQL = " insert into sys_mst_tteam(" +
                        " team_gid ," +
                        " team_code ," +
                        " team_name," +
                        " teammanager_gid," +
                        " teammanager_name," +
                        " created_by," +
                        " created_date," +
                        " status)" +
                        " values(" +
                        "'" + msGetGid + "'," +
                         "'" + msGetcodeGid + "'," +
                        "'" + values.team_name.Replace("'", "") + "'," +
                        "'" + values.teammanager_gid + "'," +
                        "'" + values.teammanager_name + "'," +

                        "'" + employee_gid + "'," +
                        "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                        "'Y')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);


                for (var i = 0; i < values.teammembers.Count; i++)
                {
                    msGetteam2member_gid = objcmnfunctions.GetMasterGID("TEMM");


                    msSQL = "Insert into sys_mst_tteam2member( " +
                           " team2member_gid, " +
                           " team_gid," +
                           " member_gid," +
                           " member_name," +
                           " created_by," +
                           " created_date," +
                            " status)" +
                           " values(" +
                           "'" + msGetteam2member_gid + "'," +
                           "'" + msGetGid + "'," +
                           "'" + values.teammembers[i].employee_gid + "'," +
                           "'" + values.teammembers[i].employee_name + "'," +
                           "'" + employee_gid + "'," +
                           "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                           "'Y')";
                    mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Team Added Successfully";
                }
                else
                {
                    values.message = "Error Occured While Adding";
                    values.status = false;
                }
            }

        }


        public void DaGetTeammembersEdit(string team_gid, Mdlteam objmaster)
        {
            msSQL = " select team_gid,team_name,teammanager_gid,teammanager_name,status as Status from sys_mst_tteam" +
                    " where team_gid='" + team_gid + "'";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                objmaster.team_gid = objGetReaderScalar["team_gid"].ToString();
                objmaster.team_name = objGetReaderScalar["team_name"].ToString();
                objmaster.teammanager_gid = objGetReaderScalar["teammanager_gid"].ToString();
                objmaster.teammanager_name = objGetReaderScalar["teammanager_name"].ToString();
                objmaster.Status = objGetReaderScalar["Status"].ToString();
            }
               

            msSQL = " select team2member_gid ,member_gid,member_name from sys_mst_tteam2member " +
                " where team_gid='" + team_gid + "'";
            dt_datatable = objdbconn.GetDataTable(msSQL);
            var getteammembersList = new List<teammembersdtl>();
            if (dt_datatable != null && dt_datatable.Rows.Count != 0)
            {
                foreach (DataRow dt in dt_datatable.Rows)
                {
                    getteammembersList.Add(new teammembersdtl
                    { 
                        employee_gid = dt["member_gid"].ToString(),
                        employee_name = dt["member_name"].ToString(),
                    });
                    objmaster.teammembersdtl = getteammembersList;
                }
            }
            dt_datatable.Dispose(); 
        }

        public void DaGetteammastermembers(string team_gid, teammemberslist values)
        {


            msSQL = " select STRING_AGG(member_name,',') as member_name  from sys_mst_tteam2member " +
                 " where team_gid='" + team_gid + "'";
            objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
            if (objGetReaderScalar != null & objGetReaderScalar.Count != 0)
            {
                values.member_name = objGetReaderScalar["member_name"].ToString();
                values.member_name = values.member_name.Replace(",", ", ");
            }
        }










        public void DaGetTeammaster(Mdlteam objmaster)
        {
            try
            {






                msSQL = " SELECT a.team_gid ,a.team_name,a.teammanager_name, CONVERT(NVARCHAR(19), a.created_date, 105) as created_date, " +

                                   " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as created_by, " +
                                  " case when a.status='N' then 'Inactive' else 'Active' end as status" +
                                   " FROM sys_mst_tteam a" +
                                   " left join hrm_mst_temployee b on a.created_by = b.employee_gid" +
                                   " left join adm_mst_tuser c on c.user_gid = b.user_gid order by a.created_date desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getteamgroup_list = new List<teamgroup>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getteamgroup_list.Add(new teamgroup
                        {
                            team_gid = (dr_datarow["team_gid"].ToString()),
                            team_name = (dr_datarow["team_name"].ToString()),
                            teammanager_name = (dr_datarow["teammanager_name"].ToString()),
                            created_date = (dr_datarow["created_date"].ToString()),
                            created_by = (dr_datarow["created_by"].ToString()),
                            status = (dr_datarow["status"].ToString()),

                        });

                    }
                    objmaster.teamgroup = getteamgroup_list;
                }
               
                objmaster.status = true;
                 dt_datatable.Dispose();
            }
            catch (Exception ex)
            {
                objmaster.status = false;
            }

        }

        public void DaGetEmployee(MdlEmployee objemployee)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<taskemployee_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    objemployee.taskemployee_list = dt_datatable.AsEnumerable().Select(row => new taskemployee_list
                    {
                        employee_gid = row["employee_gid"].ToString(),
                        employee_name = row["employee_name"].ToString()
                    }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }

        public void DaGetmemberEmployee(MdlEmployee objemployee)
        {
            try
            {
                msSQL = " SELECT a.user_firstname,a.user_gid ,concat(a.user_firstname,' ',a.user_lastname,' / ',a.user_code) as employee_name,b.employee_gid from adm_mst_tuser a " +
                   " LEFT JOIN hrm_mst_temployee b ON a.user_gid=b.user_gid " +
                   " where user_status<>'N' order by a.user_firstname asc";

                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_employee = new List<taskmemberemployee_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    objemployee.taskmemberemployee_list = dt_datatable.AsEnumerable().Select(row => new taskmemberemployee_list
                    {
                        employee_gid = row["employee_gid"].ToString(),
                        employee_name = row["employee_name"].ToString()
                    }
                    ).ToList();
                }
                dt_datatable.Dispose();
                objemployee.status = true;
            }
            catch (Exception ex)
            {
                objemployee.status = false;
            }


        }

        public void DaUpdateTeamDtl(string employee_gid, Mdlteam values)
        {
            msSQL = " select team_name from sys_mst_tteam " +
                    " where lcase(team_name) = '" + values.team_name.Replace("'", "\\'").ToLower() + "'" +
                    " and team_gid !='" + values.team_gid + "'";
            string lsTeamname = objdbconn.GetExecuteScalar(msSQL);
            if (lsTeamname != "" && lsTeamname != null)
            {
                values.status = false;
                values.message = "Team Name Already Exist";
            }
            else
            {
                msSQL = " update sys_mst_tteam set " +
                        " team_name ='" + values.team_name + "', " +
                        " teammanager_gid ='" + values.teammanager_gid + "', " +
                        " teammanager_name ='" + values.teammanager_name + "' " +
                        " where team_gid='" + values.team_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

                msSQL = "delete from sys_mst_tteam2member where team_gid='" + values.team_gid + "'";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult == 1)
                {
                    for (var i = 0; i < values.teammembers.Count; i++)
                    {
                        msGetteam2member_gid = objcmnfunctions.GetMasterGID("TEMM");
                        msSQL = "Insert into sys_mst_tteam2member( " +
                               " team2member_gid, " +
                               " team_gid," +
                               " member_gid," +
                               " member_name," +
                               " created_by," +
                               " created_date," +
                                " status)" +
                               " values(" +
                               "'" + msGetteam2member_gid + "'," +
                               "'" + values.team_gid + "'," +
                               "'" + values.teammembers[i].employee_gid + "'," +
                               "'" + values.teammembers[i].employee_name + "'," +
                               "'" + employee_gid + "'," +
                               "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                               "'Y')";
                        mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                    }
                }

                if (mnResult != 0)
                {
                    values.status = true;
                    values.message = "Team Updated Successfully";
                }
                else
                {
                    values.message = "Error Occured !";
                    values.status = false;
                }
            }

        }


        public void DaInactiveTeamMaster(Mdlteam values, string employee_gid)
        {
            msSQL = " update sys_mst_tteam set status ='" + values.rbo_status + "'," +
                    " remarks='" + values.remarks.Replace("'", "") + "' " +
                    " where team_gid='" + values.team_gid + "' ";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {
                msGetGid = objcmnfunctions.GetMasterGID("TMSL");

                msSQL = " insert into sys_mst_tteaminactivelog (" +
                      " teammasterinactivelog_gid   , " +
                      " team_gid," +
                      " team_name ," +
                      " status," +
                      " remarks," +
                      " updated_by," +
                      " updated_date) " +
                      " values (" +
                      " '" + msGetGid + "'," +
                      " '" + values.team_gid + "'," + 
                      " '" + values.team_name + "'," +
                      " '" + values.rbo_status + "'," +
                      " '" + values.remarks.Replace("'", "") + "'," +
                      " '" + employee_gid + "'," +
                      " '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                values.status = true;
                if (values.rbo_status == 'N')
                    values.message = "Team Inactivated Successfully";
                else
                    values.message = "Team Activated Successfully";
            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }
        }

        public void TeamMasterInactiveLogview(string team_gid, Mdlteam values)
        {
            try
            {
                msSQL = " SELECT a.team_gid,CONVERT(NVARCHAR(19), a.updated_date, 105) as updated_date, " +
                        " concat(c.user_firstname,' ',c.user_lastname,' / ',c.user_code) as updated_by," +
                        " case when a.status='N' then 'Inactive' else 'Active' end as Status, a.remarks" +
                        " FROM sys_mst_tteaminactivelog a" +
                        " left join hrm_mst_temployee b on a.updated_by = b.employee_gid" +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.team_gid ='" + team_gid + "' order by a.teammasterinactivelog_gid   desc ";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var getmaster_list = new List<master_list>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        getmaster_list.Add(new master_list
                        {
                            baselocation_gid = (dr_datarow["team_gid"].ToString()),
                            updated_by = (dr_datarow["updated_by"].ToString()),
                            updated_date = (dr_datarow["updated_date"].ToString()),
                            status = (dr_datarow["status"].ToString()),
                            remarks = (dr_datarow["remarks"].ToString()),
                        });
                    }
                    values.master_list = getmaster_list;
                }
                values.status = true;
            }
            catch
            {
                values.status = false;
            }
        }

        public void DaDeleteTeammaster(string team_gid, string employee_gid, Mdlteam values)
        {
            msSQL = " delete from sys_mst_tteam where team_gid='" + team_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            msSQL = " delete from sys_mst_tteam2member where team_gid='" + team_gid + "'";
            mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);

            if (mnResult != 0)
            {

                values.status = true;
                values.message = "Team Deleted Successfully";

            }
            else
            {
                values.status = false;
                values.message = "Error Occurred";
            }

        }
    }
}









