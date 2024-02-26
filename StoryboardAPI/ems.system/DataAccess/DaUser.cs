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
    public class DaUser
    {
        dbconn objdbconn = new dbconn();
        cmnfunctions objcmnfunctions = new cmnfunctions();
        string msSQL = string.Empty;
        SqlDataReader objSqlDataReader;
        DataTable dt_levelone, dt_leveltwo, dt_levelthree, dt_levelfour;
        string menu_ind_up_first = string.Empty;
        string menu_ind_down_first = string.Empty;
        string menu_ind_up_second = string.Empty;
        string menu_ind_down_second = string.Empty;
        DataTable dt_datatable;
        Fnazurestorage objcmnstorage = new Fnazurestorage();
        Dictionary<string, object> objGetReaderScalar;
        List<Dictionary<string, object>> objGetReaderData;
        int mnResult;
        public void loadMenuFromDB(string user_gid, menu_response values)
        {
            var dt_data = new DataTable();
            List<sys_menu> getmenu = new List<sys_menu>();
            List<mdlMenuData> mdlMenuData = new List<mdlMenuData>();

            msSQL = " EXEC dbo.adm_mst_spGetMenuData " +
                           " @user_gid='" + user_gid + "'";
            dt_levelone = objdbconn.GetDataTable(msSQL);
            if (dt_levelone != null)
            {
                mdlMenuData = cmnfunctions.ConvertDataTable<mdlMenuData>(dt_levelone);
                try
                {
                    List<mdlMenuData> getFirstLevel = mdlMenuData.Where(a => a.menu_level == "1").ToList();
                    if (getFirstLevel.Count != 0)
                    {
                        foreach (var i in getFirstLevel)
                        {
                            List<mdlMenuData> getSecondLevel = mdlMenuData.Where(a => a.menu_level == "2"
                                   && a.module_gid_parent == i.module_gid).OrderBy(a => a.display_order).GroupBy(a => a.module_gid)
                                   .Select(group => new mdlMenuData
                                   {
                                       module_gid = group.Key,
                                       module_name = group.First().module_name,
                                       sref = group.First().sref,
                                       icon = group.First().icon,
                                       menu_level = group.First().menu_level,
                                       module_gid_parent = group.First().module_gid_parent,
                                       display_order = group.First().display_order
                                   }).ToList();
                            List<sys_submenu> getmenu2 = new List<sys_submenu>();
                            if (getSecondLevel != null)
                            {
                                foreach (var j in getSecondLevel)
                                {
                                    List<mdlMenuData> getThirdLevel = mdlMenuData.Where(a => a.menu_level == "3"
                                    && a.module_gid_parent == j.module_gid).OrderBy(a => a.display_order).GroupBy(a => a.module_gid)
                                    .Select(group => new mdlMenuData
                                    {
                                        module_gid = group.Key,
                                        module_name = group.First().module_name,
                                        sref = group.First().sref,
                                        icon = group.First().icon,
                                        menu_level = group.First().menu_level,
                                        module_gid_parent = group.First().module_gid_parent,
                                        display_order = group.First().display_order
                                    }).ToList();
                                    List<sys_sub1menu> getmenu3 = new List<sys_sub1menu>();
                                    if (getThirdLevel != null)
                                    {
                                        foreach (var k in getThirdLevel)
                                        {
                                            var getFourthLevel = mdlMenuData.Where(a => a.menu_level == "4"
                                                                 && a.module_gid_parent == k.module_gid)
                                                                 .OrderBy(a => a.display_order)
                                                                 .GroupBy(a => a.module_gid).ToList();
                                            List<sys_sub2menu> getmenu4 = new List<sys_sub2menu>();
                                            if (getFourthLevel != null)
                                            {
                                                menu_ind_up_second = "fa fa-angle-up";
                                                menu_ind_down_second = "fa fa-angle-down";
                                                getmenu4 = getFourthLevel.SelectMany(group => group).Select(row => new sys_sub2menu
                                                {
                                                    text = row.module_name,
                                                    sref = row.sref,
                                                    icon = row.icon,
                                                }).ToList();
                                            }
                                            getmenu3.Add(new sys_sub1menu
                                            {
                                                text = k.module_name,
                                                sref = k.sref,
                                                sub2menu = getmenu4,
                                            });
                                        }
                                    }
                                    getmenu2.Add(new sys_submenu
                                    {
                                        text = j.module_name,
                                        sref = j.sref,
                                        sub1menu = getmenu3
                                    });
                                }
                            }
                            else
                            {
                                menu_ind_up_first = "";
                                menu_ind_down_first = "";
                            }
                            getmenu.Add(new sys_menu
                            {
                                text = i.module_name,
                                sref = i.sref,
                                icon = i.icon,
                                menu_indication = menu_ind_up_first,
                                menu_indication1 = menu_ind_down_first,
                                label = "label label-success",
                                submenu = getmenu2
                            });
                            values.menu_list = getmenu;
                        }
                    }
                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                }
                finally
                {
                }
                dt_levelone.Dispose();
                values.status = true;
                return;
            }
            values.message = "No data Found";
            dt_levelone.Dispose();

        }
        public void Daprivilegelevel(string user_gid, menu_response values)
        {
            List<sys_menu> getmenu = new List<sys_menu>();
            List<mdlMenuData> mdlMenuDataList = new List<mdlMenuData>();

            msSQL = " SELECT t1.module_gid as module_gid, t2.module_gid as access_module_gid, " +
                    " case when t1.module_gid = t2.module_gid then 'true' else 'false' end as menu_access, " +
                    " t1.module_name as module_name, t1.sref as sref, t1.menu_level as menu_level, " +
                    " t1.module_gid_parent as module_gid_parent, t1.display_order as display_order " +
                    " FROM( " +
                    " SELECT module_gid, module_name, sref, menu_level, module_gid_parent, display_order " +
                    " FROM adm_mst_tmodule " +
                    " WHERE lw_flag = 'Y' " +
                    " ) AS t1 " +
                    " left JOIN( " +
                    " SELECT module_gid " +
                    " FROM adm_mst_tprivilege " +
                    " WHERE user_gid = '" + user_gid + "' " +
                    " ) AS t2 " +
                    " ON t1.module_gid = t2.module_gid ";
            dt_levelone = objdbconn.GetDataTable(msSQL);
            if (dt_levelone != null)
            {
                mdlMenuDataList = cmnfunctions.ConvertDataTable<mdlMenuData>(dt_levelone);
                try
                {
                    List<mdlMenuData> getFirstLevel = mdlMenuDataList.Where(a => a.menu_level == "1").ToList();
                    if (getFirstLevel.Count != 0)
                    {
                        foreach (var i in getFirstLevel)
                        {
                            List<mdlMenuData> getSecondLevel = mdlMenuDataList.Where(a => a.menu_level == "2"
                                   && a.module_gid_parent == i.module_gid).OrderBy(a => a.display_order).GroupBy(a => a.module_gid)
                                   .Select(group => new mdlMenuData
                                   {
                                       module_gid = group.Key,
                                       module_name = group.First().module_name,
                                       sref = group.First().sref,
                                       icon = group.First().icon,
                                       menu_level = group.First().menu_level,
                                       module_gid_parent = group.First().module_gid_parent,
                                       display_order = group.First().display_order
                                   }).ToList();
                            List<sys_submenu> getmenu2 = new List<sys_submenu>();
                            if (getSecondLevel != null)
                            {
                                foreach (var j in getSecondLevel)
                                {
                                    List<mdlMenuData> getThirdLevel = mdlMenuDataList.Where(a => a.menu_level == "3"
                                    && a.module_gid_parent == j.module_gid).OrderBy(a => a.display_order).GroupBy(a => a.module_gid)
                                    .Select(group => new mdlMenuData
                                    {
                                        module_gid = group.Key,
                                        module_name = group.First().module_name,
                                        sref = group.First().sref,
                                        icon = group.First().icon,
                                        menu_level = group.First().menu_level,
                                        module_gid_parent = group.First().module_gid_parent,
                                        display_order = group.First().display_order
                                    }).ToList();
                                    List<sys_sub1menu> getmenu3 = new List<sys_sub1menu>();
                                    if (getThirdLevel != null)
                                    {
                                        foreach (var k in getThirdLevel)
                                        {
                                            var getFourthLevel = mdlMenuDataList.Where(a => a.menu_level == "4"
                                                                 && a.module_gid_parent == k.module_gid)
                                                                 .OrderBy(a => a.display_order)
                                                                 .GroupBy(a => a.module_gid).ToList();
                                            List<sys_sub2menu> getmenu4 = new List<sys_sub2menu>();
                                            if (getFourthLevel != null)
                                            {
                                                menu_ind_up_second = "fa fa-angle-up";
                                                menu_ind_down_second = "fa fa-angle-down";
                                                getmenu4 = getFourthLevel.SelectMany(group => group).Select(row => new sys_sub2menu
                                                {
                                                    text = row.module_name,
                                                    sref = row.sref,
                                                    icon = row.icon,
                                                    menu_access = row.menu_access,
                                                }).ToList();
                                            }
                                            getmenu3.Add(new sys_sub1menu
                                            {
                                                text = k.module_name,
                                                sref = k.sref,
                                                sub2menu = getmenu4,
                                            });
                                        }
                                    }
                                    getmenu2.Add(new sys_submenu
                                    {
                                        text = j.module_name,
                                        sref = j.sref,
                                        sub1menu = getmenu3
                                    });
                                }
                            }
                            else
                            {
                                menu_ind_up_first = "";
                                menu_ind_down_first = "";
                            }
                            getmenu.Add(new sys_menu
                            {
                                text = i.module_name,
                                sref = i.sref,
                                icon = i.icon,
                                menu_indication = menu_ind_up_first,
                                menu_indication1 = menu_ind_down_first,
                                label = "label label-success",
                                submenu = getmenu2
                            });
                            values.menu_list = getmenu;
                        }
                    }
                }
                catch (Exception ex)
                {
                    values.message = ex.ToString();
                    values.status = false;
                }
                finally
                {
                }
                dt_levelone.Dispose();
                values.status = true;
                return;
            }
            values.message = "No data Found";
            dt_levelone.Dispose();
        }
    
    }
}