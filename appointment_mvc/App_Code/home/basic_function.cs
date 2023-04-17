using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;

namespace appointment_mvc.App_Code.home
{
    public class basic_function
    {
        #region login

        public Models.common_response login(string username, string password)
        {
            Models.common_response Response = new Models.common_response();

            #region Validation

            if (username == null || username == "")
            {
                Response.message = "Invalid Username.";
                return Response;
            }

            if (password == null || password == "")
            {
                Response.message = "Invalid password (must include atleast 8 charaters,uppercase and lowercase alphabhet, one number and one special charater).";
                return Response;
            }

            username = username.Replace("'", "''").Trim();
            password = password.Replace("'", "''").Trim();

           
            #endregion;

            #region Check User
            {

                //SqlParameter[] para = new SqlParameter[3];

                //para[0] = new SqlParameter("@username", SqlDbType.NVarChar);
                //para[0].Value = username;

                //para[1] = new SqlParameter("@password", SqlDbType.NVarChar);
                //para[1].Value = password;

                //DataTable dt = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "sp_checksuperadmin", para).Tables[0];
                string item_st = "Select * from database_testing_connection where  user_name='" + username + "' and password='" + password + "'";
                DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, item_st).Tables[0];
                if (dt.Rows.Count != 1)
                {
                    Response.message = "Invalid username or password!.";
                    return Response;
                }




                //#region Login the User
                //{
                //    string refrence = System.Guid.NewGuid().ToString() + System.Guid.NewGuid().ToString() + System.Guid.NewGuid().ToString() + System.Guid.NewGuid().ToString();
                //    //string agencyid = dt.Rows[0]["id"].ToString();
                //    string createdon = DateTime.UtcNow.ToString();
                //    string expire_on = DateTime.UtcNow.AddDays(1).ToString();
                //    string ip = HttpContext.Current.Request.UserHostAddress;
                //    string ip_city = "";


                //    string login_querystring = " delete from tbl_admin_current_login where admin_id='" + agencyid + "' or expire_on<='" + createdon + "'  insert into tbl_admin_current_login(admin_id,refrence,created_on,ip_address,ip_city,expire_on) values('" + agencyid + "','" + refrence + "','" + createdon + "','" + ip + "','" + ip_city + "','" + expire_on.ToString() + "')   insert into tbl_admin_login_log(admin_id,refrence,created_on,ip_address,ip_city) values('" + agencyid + "','" + refrence + "','" + createdon + "','" + ip + "','" + ip_city + "')  ";

                //    SqlHelper.ExecuteNonQuery(CommandType.Text, login_querystring);


                //    Response.success = true;
                //    Response.parameter = agencyid;
                //}
                //#endregion;

                Response.success = true;
                Response.parameter = username;
            }
            #endregion;

            return Response;
        }
        #endregion

        #region check Session

        public Boolean adminssioncheck(string viewdashboaradds)
        {
            Boolean ischeck = false;
            if (HttpContext.Current.Session["adminname"] != null)
            {
                ischeck = true;
            }

            return ischeck;
        }
        #endregion

        #region admin info

        public appointment_mvc.Models.admininfo admininfo(string agencyid)
        {
            Models.admininfo Response = new Models.admininfo();
            string str = "select * from tbl_super_admin where id='" + agencyid + "'       select * from  tbl_admin_current_login";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, str);
            DataTable dt = ds.Tables[0];
            DataTable dc = ds.Tables[1];
            if (dt.Rows.Count == 0)
            {
                return Response;
            }


            Response.name = dt.Rows[0]["username"].ToString();
            Response.agencyid = dt.Rows[0]["id"].ToString();
            Response.ip_address = dc.Rows[0]["ip_address"].ToString();
            Response.logintime = dc.Rows[0]["created_on"].ToString();

            if (Response.logintime != null && Response.logintime != "")
            {
                Response.logintime = Convert.ToDateTime(Response.logintime).ToString("dd MMM yyyy  hh : mm tt");

            }

            return Response;
        }


        #endregion
    }
}