  
using ems.system.Models;
using ems.utilities.Functions;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ems.system.DataAccess
{
    public class DaSamplecode
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;   
        DataTable dt_datatable; 
        Dictionary<string, object> objGetReaderScalar;
        List<Dictionary<string, object>> objGetReaderData;
        int mnResult; 
        public bool DaEmployeeProfileView(employee objemployee, string employee_gid)
        {
            try
            {
                // GetExecuteScalar => To Fetch single rows & single columns only
                msSQL = "select concat(user_code, ' || ', user_firstname,' ',user_lastname) as employee_reportingto_name" +
                        " from hrm_mst_temployee a" +
                        " left join adm_mst_tuser b on a.user_gid = b.user_gid" +
                        " where employee_gid = '" + objemployee.employee_reportingto + "'";
                objemployee.employee_reportingto_name = objdbconn.GetExecuteScalar(msSQL);


                // GetReaderScalar => To Fetch single rows & multiple columns only
                msSQL = " Select distinct a.user_gid,c.useraccess,case when c.entity_gid is null then c.entity_name else z.entity_name end as entity_name , user_firstname,user_lastname," +
                       " a.user_code,concat(a.user_firstname,' ',a.user_lastname) as user_name ,c.employee_joiningdate," +
                       " c.employee_gender,c.role_gid,employeereporting_to,  " +
                       " concat(j.address1,' ',j.address2,'/', j.city,'/', j.state,'/',k.country_name,'/', j.postal_code) as emp_address, " +
                       " d.designation_name,c.designation_gid,c.employee_gid,e.branch_name, employee_emailid,employee_mobileno,c.entity_gid," +
                       " CASE " +
                       " WHEN a.user_status = 'Y' THEN 'Active'  " +
                       " WHEN a.user_status = 'N' THEN 'Inactive' " +
                       " END as user_access,a.user_status,c.department_gid,c.branch_gid,n.role_name, e.branch_name, g.department_name,c.marital_status,c.marital_status_gid,FORMAT(c.employee_joiningdate, 'dd-MM-yyyy') AS joiningdate, " +
                       " c.employee_personalno as personal_phone_no,c.personal_emailid,c.bloodgroup as bloodgroup_name,c.bloodgroup_gid " +
                       " FROM hrm_mst_temployee c " +
                       " left join adm_mst_tuser a on a.user_gid = c.user_gid " +
                       " left join adm_mst_tdesignation d on c.designation_gid = d.designation_gid " +
                       " left join hrm_mst_tbranch e on c.branch_gid = e.branch_gid " +
                       " left join hrm_mst_tdepartment g on g.department_gid = c.department_gid " +
                       " left join adm_mst_taddress j on c.employee_gid=j.parent_gid " +
                       " left join adm_mst_tcountry k on j.country_gid=k.country_gid " +
                       " left join adm_mst_tentity z on z.entity_gid=c.entity_gid" +
                       " left join hrm_mst_trole n on n.role_gid=c.role_gid " +
                       " where c.employee_gid='" + employee_gid + "'" +
                       " order by c.employee_gid desc ";
                objGetReaderScalar = objdbconn.GetReaderScalar(msSQL);
                if (objGetReaderScalar.Count != 0)
                {
                    objemployee.company_name = objGetReaderScalar["entity_name"].ToString();
                    objemployee.entity_gid = objGetReaderScalar["entity_gid"].ToString();
                    objemployee.branch_gid = objGetReaderScalar["branch_gid"].ToString();
                    objemployee.branch_name = objGetReaderScalar["branch_name"].ToString();
                    objemployee.department_gid = objGetReaderScalar["department_gid"].ToString();
                    objemployee.department_name = objGetReaderScalar["department_name"].ToString();
                    objemployee.designation_gid = objGetReaderScalar["designation_gid"].ToString();
                    objemployee.designation_name = objGetReaderScalar["designation_name"].ToString();
                    objemployee.useraccess = objGetReaderScalar["useraccess"].ToString();
                    objemployee.user_access = objGetReaderScalar["user_access"].ToString();
                    objemployee.user_status = objGetReaderScalar["user_status"].ToString();
                    objemployee.user_firstname = objGetReaderScalar["user_firstname"].ToString();
                    objemployee.user_lastname = objGetReaderScalar["user_lastname"].ToString();
                    objemployee.gender = objGetReaderScalar["employee_gender"].ToString();
                    objemployee.employee_emailid = objGetReaderScalar["employee_emailid"].ToString();
                    objemployee.employee_mobileno = objGetReaderScalar["employee_mobileno"].ToString();
                    objemployee.user_code = objGetReaderScalar["user_code"].ToString();
                    objemployee.role_gid = objGetReaderScalar["role_gid"].ToString();
                    objemployee.role_name = objGetReaderScalar["role_name"].ToString();
                    objemployee.employee_reportingto = objGetReaderScalar["employeereporting_to"].ToString();
                    objemployee.marital_status = objGetReaderScalar["marital_status"].ToString();
                    objemployee.marital_status_gid = objGetReaderScalar["marital_status_gid"].ToString();
                    objemployee.joining_date = objGetReaderScalar["joiningdate"].ToString();
                    objemployee.personal_phone_no = objGetReaderScalar["personal_phone_no"].ToString();
                    objemployee.personal_emailid = objGetReaderScalar["personal_emailid"].ToString();
                    objemployee.bloodgroup_name = objGetReaderScalar["bloodgroup_name"].ToString();
                    objemployee.bloodgroup_gid = objGetReaderScalar["bloodgroup_gid"].ToString();
                    if (objGetReaderScalar["employee_joiningdate"].ToString() != "")
                    {
                        objemployee.joiningdate = Convert.ToDateTime(objGetReaderScalar["employee_joiningdate"].ToString());
                    }
                }

                // GetDataReader => To Fetch Multiple rows & columns
                string lsemployee_gid = "";
                msSQL = " select employee_gid,employee_emailid from hrm_mst_temployee " +
                        " where employee_gid in ('SERM20210924116', 'SERM2011230108')";
                objGetReaderData = objdbconn.GetDataReader(msSQL);
                if (objGetReaderData.Count != 0)
                {
                    foreach (var i in objGetReaderData)
                    {
                        lsemployee_gid = lsemployee_gid + "'" + i["employee_gid"].ToString() + "',";
                    }
                    lsemployee_gid = lsemployee_gid.TrimEnd(',');
                }

                // ExecuteNonQuerySQL => to execute a query 
                msSQL = "update adm_mst_tuser set password_suspend='' where user_gid=''";
                mnResult = objdbconn.ExecuteNonQuerySQL(msSQL);
                if (mnResult != 0)
                {
                }

                objemployee.status = true;
                return true;
            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                objemployee.status = false;
                return false;
            }
        }
        public bool DaGetHRDocProfilelist(string employee_gid, hrdoc_list objemployeedoc_list)
        {
            try
            {
                // GetDataTable => To Fetch collection of Multiple rows & columns
                msSQL = " select hrdoc_id,hrdocument_gid,hrdocument_name,hrdoc_name, hrdoc_path, " +
                        " documentsentforsign_flag,esignexpiry_flag,documentsigned_flag, " +
                        " concat(c.user_firstname,' ', c.user_lastname,'/' ,c.user_code) as created_by, " +
                        " CONVERT(NVARCHAR(19), a.created_date, 105) as created_date,migration_flag " +
                        " from sys_mst_temployeehrdocument a " +
                        " left join hrm_mst_temployee b on a.created_by = b.employee_gid " +
                        " left join adm_mst_tuser c on c.user_gid = b.user_gid " +
                        " where a.employee_gid = '" + employee_gid + "'";
                dt_datatable = objdbconn.GetDataTable(msSQL);
                var get_hrdoc_list = new List<hrdoc>();
                if (dt_datatable != null && dt_datatable.Rows.Count != 0)
                {
                    foreach (DataRow dr_datarow in dt_datatable.Rows)
                    {
                        get_hrdoc_list.Add(new hrdoc_list
                        {
                            hrdoc_id = dr_datarow["hrdoc_id"].ToString(),
                            hrdocument_gid = dr_datarow["hrdocument_gid"].ToString(),
                            hrdocument_name = dr_datarow["hrdocument_name"].ToString(),
                            hrdoc_name = dr_datarow["hrdoc_name"].ToString(), 
                            documentsentforsign_flag = dr_datarow["documentsentforsign_flag"].ToString(),
                            esignexpiry_flag = dr_datarow["esignexpiry_flag"].ToString(),
                            documentsigned_flag = dr_datarow["documentsigned_flag"].ToString(),
                            created_by = dr_datarow["created_by"].ToString(),
                            created_date = dr_datarow["created_date"].ToString(),
                            migration_flag = dr_datarow["migration_flag"].ToString(),
                        });
                    }
                    objemployeedoc_list.hrdoc = get_hrdoc_list;
                }
                dt_datatable.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

    }
}