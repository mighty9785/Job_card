using appointment_mvc.Models;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
namespace appointment_mvc.Controllers
{
    public class HomeController : Controller
    {
        string path = System.Configuration.ConfigurationManager.AppSettings["importantfile"].ToString();

       

        public ActionResult Index(string url)
        {


            return View();
        }


        #region checklogin

        [HttpPost]
        public JsonResult Login(string username, string password, string Dealer_ID, string url)
        {
            appointment_mvc.App_Code.home.basic_function basic_function = new appointment_mvc.App_Code.home.basic_function();
            Models.common_response Response = basic_function.login(username, password);
            if (Response.success == true)
            {
                if (url != null && url.ToString() != "")
                {
                    Response.message = (HttpUtility.HtmlDecode(url));
                }
                else
                {
                    Response.message = "/home/About";
                }
                Session["adminname"] = Response.parameter.ToString();



                string item_st = "Select * from database_testing_connection where  user_name='" + username + "' and password='" + password + "'";
                DataTable dts = SqlHelper.ExecuteDataset(CommandType.Text, item_st).Tables[0];
                if (dts.Rows.Count > 0)
                {
                    Session["data_source"] = dts.Rows[0]["dbData_Source"].ToString();
                    Session["int_cat"] = dts.Rows[0]["dbinitial_catalog"].ToString();
                    Session["user_id"] = dts.Rows[0]["dbuser_id"].ToString();
                    Session["pwd"] = dts.Rows[0]["dbpassword"].ToString();
                    Session["intsec"] = dts.Rows[0]["integerated_sec"].ToString();

                    string localpath1 = "server=" + Session["data_source"] +";Initial Catalog=" + Session["int_cat"] + ";User ID=" + Session["user_id"] + ";Password=" + Session["pwd"] + "; pooling=false";

                    Session["data_con"] = localpath1;

                    //SqlHelper.UpdateConnectionString(localpath1);

                }


            }
            return Json(Response);
        }

        #endregion

        #region  admin info
        [ChildActionOnly]
        public ActionResult topbar()
        {
            appointment_mvc.App_Code.home.basic_function basic_function = new appointment_mvc.App_Code.home.basic_function();
            appointment_mvc.Models.admininfo admininfo = new appointment_mvc.Models.admininfo();
            string agencyid = "";
            if (Session["adminname"] != null && Session["adminname"].ToString() != "")
            {
                agencyid = Session["adminname"].ToString();
            }

            admininfo = basic_function.admininfo(agencyid);
            return PartialView(admininfo);

        }
        #endregion

        #region logout

        public ActionResult logout()
        {
            if (Session["adminname"] != null)
            {
                Session["adminname"] = null;
                Session["data_con"] = null;
            }

            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";



            #region item_list for job card

            List<itemNO_list> itemNO_list = new List<itemNO_list>();
            string item_st = "select * from item_mst";
            DataSet dts = SqlHelper.ExecuteDataset(CommandType.Text, item_st);
            if (dts.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dts.Tables[0].Rows)
                {
                    itemNO_list.Add(new Models.itemNO_list()
                    {
                        item_code = dr["item_code"].ToString(),
                        part_no = dr["item_no"].ToString(),
                        part_name = dr["item_name"].ToString(),
                        per_rate = dr["mrp_price"].ToString(),

                    });
                }
            }

            string path = System.Configuration.ConfigurationManager.AppSettings["importantfile"].ToString();

            System.IO.File.WriteAllText(path + "itemNO_list.json", JsonConvert.SerializeObject(itemNO_list));

            #endregion

            #region item_list for job card

            //List <inv_mstlist> inv_mstlist = new List<inv_mstlist>();
            //string inv_st = "select * from inv_mst";
            //DataSet dns = SqlHelper.ExecuteDataset(CommandType.Text, inv_st);
            //if (dns.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dns.Tables[0].Rows)
            //    {
            //        inv_mstlist.Add(new Models.inv_mstlist()
            //        {
            //            Ledg_Name = dr["Ledg_Name"].ToString(),
            //            ph1 = dr["ph1"].ToString(),
            //            ph2 = dr["ph2"].ToString(),
            //            Ledg_Add1 = dr["Ledg_Add1"].ToString(),

            //        });
            //    }
            //}


            //System.IO.File.WriteAllText(path + "invmst_list.json", JsonConvert.SerializeObject(inv_mstlist));

            #endregion

            #region chas_list for job card

            List<chas_mst> chas_mst = new List<chas_mst>();
            string chas_st = "select * from chas_mst";
            DataSet dqs = SqlHelper.ExecuteDataset(CommandType.Text, chas_st);
            if (dqs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dqs.Tables[0].Rows)
                {

                    chas_mst.Add(new Models.chas_mst()
                    {
                        chas_id = dr["chas_id"].ToString(),
                        chas_no = dr["chas_no"].ToString(),
                        reg_no = dr["reg_no"].ToString(),
                        eng_no = dr["eng_no"].ToString(),
                        modl_id = dr["modl_code"].ToString(),

                    });
                    //DataTable dis = SqlHelper.ExecuteDataset(CommandType.Text, "select * from inv_mst where chas_id ='"+  +"'").Tables[0];
                    //if (dqs.Tables[0].Rows.Count > 0)
                    //{

                    //}
                }
            }

            System.IO.File.WriteAllText(path + "chas_mst_list.json", JsonConvert.SerializeObject(chas_mst));

            #endregion

            #region model_list for job card

            List<model_list> model_list = new List<model_list>();
            string modl_st = "select * from modl_mst";
            DataSet dms = SqlHelper.ExecuteDataset(CommandType.Text, modl_st);
            if (dms.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dms.Tables[0].Rows)
                {
                    model_list.Add(new Models.model_list()
                    {
                        model_no = dr["modl_code"].ToString(),
                        model_name = dr["modl_name"].ToString(),
                        model_id = dr["item_code"].ToString(),

                    });
                }
            }

            System.IO.File.WriteAllText(path + "model_list.json", JsonConvert.SerializeObject(model_list));

            #endregion

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Add_appoint(string id)
        {
            appointment_mvc.App_Code.home.basic_function basic_function = new appointment_mvc.App_Code.home.basic_function();
            if (basic_function.adminssioncheck("") == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/home/Index?url=" + HttpUtility.UrlEncode(url) + "");
            }

            appointment_mvc.Models.appointment appointment = new appointment_mvc.Models.appointment();
            List<appointment_mvc.Models.appointmentlist> appointmentlist = new List<appointment_mvc.Models.appointmentlist>();

            if (id != null)
            {

                string str = "select * from  Appointment_test where id='" + id + "'";
                DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, str).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    appointment.S_no = dt.Rows[0]["s.NO."].ToString();
                    appointment.Reg_no = dt.Rows[0]["REG.NO."].ToString();
                    appointment.Chas_no = dt.Rows[0]["CHAS_NO"].ToString();
                    appointment.model = dt.Rows[0]["MODEL"].ToString();
                    appointment.customer_name = dt.Rows[0]["CUSTOMER"].ToString();
                    appointment.phone = dt.Rows[0]["PHONE"].ToString();
                    appointment.visit_date = dt.Rows[0]["VISIT_DATE"].ToString();
                    appointment.time = dt.Rows[0]["TIME"].ToString();
                    appointment.complaint = dt.Rows[0]["COMPLAINT"].ToString();
                    appointment.id = dt.Rows[0]["id"].ToString();
                }

            }

           

            string sql = "select * from Appointment_test";

            DataTable dtr = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            if (dtr.Rows.Count > 0)
            {
                foreach (DataRow dr in dtr.Rows)
                {

                    appointmentlist.Add(new appointment_mvc.Models.appointmentlist()
                    {
                        S_no = dr["s.NO."].ToString(),
                        Reg_no = dr["REG.NO."].ToString(),
                        Chas_no = dr["CHAS_NO"].ToString(),
                        model = dr["MODEL"].ToString(),
                        customer_name = dr["CUSTOMER"].ToString(),
                        visit_date = dr["VISIT_DATE"].ToString(),
                        phone = dr["PHONE"].ToString(),
                        time = dr["TIME"].ToString(),
                        complaint = dr["COMPLAINT"].ToString(),
                        id = dr["id"].ToString(),
                       



                    });
                }
            }

            appointment.appointment_list = appointmentlist;
            return View(appointment);

        }

        [HttpPost]
        public ActionResult insert_appoint(appointment_mvc.Models.appointment add_donation)
        {

            if (add_donation.id != null)
            {
                string sql = "update  Appointment_test set [s.NO.]='" + add_donation.S_no + "',[REG.NO.]='" + add_donation.Reg_no + "',CHAS_NO='" + add_donation.Chas_no + "',MODEL='" + add_donation.model + "', PHONE='" + add_donation.phone + "',CUSTOMER='" + add_donation.customer_name + "',VISIT_DATE='" + add_donation.visit_date + "',TIME='" + add_donation.time + "',COMPLAINT='" + add_donation.complaint + "'  where id='" + add_donation.id + "' ";
                SqlHelper.ExecuteNonQuery(CommandType.Text, sql);

            }

            else
            {
                string sql = "insert into Appointment_test([s.NO.],[REG.NO.],CHAS_NO,MODEL,PHONE,CUSTOMER,VISIT_DATE,TIME,COMPLAINT) values('" + add_donation.S_no + "','" + add_donation.Reg_no + "','" + add_donation.Chas_no + "','" + add_donation.model + "','" + add_donation.phone + "','" + add_donation.customer_name + "','" + add_donation.visit_date + "','" + add_donation.time + "','" + add_donation.complaint + "')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, sql);


            }

            return RedirectToAction("Add_appoint");
        }

        public ActionResult delet_appoint(string id)
        {
           string str = "delete from Appointment_test where CHAS_NO='" + id+"'";

            SqlHelper.ExecuteNonQuery(CommandType.Text, str);

            return RedirectToAction("Add_appoint");
        }

        public ActionResult job_card(string job_id)
        {
            appointment_mvc.Models.job_card job_card = new appointment_mvc.Models.job_card();
            appointment_mvc.App_Code.home.basic_function basic_function = new appointment_mvc.App_Code.home.basic_function();
            if (basic_function.adminssioncheck("") == false)
            {
                string url = Request.Url.PathAndQuery;
                return Redirect("/home/Index?url=" + HttpUtility.UrlEncode(url) + "");
            }
          

           

            if (job_id != null)
            {

                string strr = "select tran_id from  INV_MST where INV_No='" + job_id + "'";
                DataTable drrt = SqlHelper.ExecuteDataset(CommandType.Text, strr).Tables[0];

              if (drrt.Rows.Count > 0)
              {
                    job_card.trans_id = drrt.Rows[0]["tran_id"].ToString();

                string view_inv_str = "select * from  INV_MST where tran_id='" + job_card.trans_id +"'    select * from  Job_Para where tran_id='" + job_card.trans_id + "'   select * from  Job_cmpl where tran_id='" + job_card.trans_id +"' select * from  DOC_UPLOAD where tran_id='" + job_card.trans_id + "'   ";
                DataSet dtt = SqlHelper.ExecuteDataset(CommandType.Text, view_inv_str);

                DataTable inv_dt = dtt.Tables[0];
                DataTable jobpara_dt = dtt.Tables[1];
                DataTable jobcmpl_dt = dtt.Tables[2];
                DataTable doc_dt = dtt.Tables[3];

                    if (inv_dt.Rows.Count > 0) {
                        job_card.jobcard_no = inv_dt.Rows[0]["INV_No"].ToString();
                        job_card.cust_name = inv_dt.Rows[0]["Ledg_Name"].ToString();
                        job_card.mobile_f = inv_dt.Rows[0]["ph1"].ToString();
                        job_card.mobile_2 = inv_dt.Rows[0]["ph2"].ToString();
                        job_card.address = inv_dt.Rows[0]["Ledg_Add1"].ToString();
                        job_card.date = inv_dt.Rows[0]["INV_Date"].ToString();
                        job_card.parts_amt = inv_dt.Rows[0]["Item_Amt"].ToString();
                        job_card.labour_charg = inv_dt.Rows[0]["Lbr_Amt"].ToString();
                        job_card.total = inv_dt.Rows[0]["Bill_Amt"].ToString();
                        job_card.service_type = inv_dt.Rows[0]["srv_type"].ToString();
                        job_card.service_adviser = inv_dt.Rows[0]["srv_adv"].ToString();

                    }
                        var chasid = inv_dt.Rows[0]["chas_id"].ToString();
                    
                   string chas_str = "select * from CHAS_MST where chas_id = '"+ chasid +"'";
                    DataTable dct = SqlHelper.ExecuteDataset(CommandType.Text, chas_str).Tables[0];
                    if (dct.Rows.Count > 0) {

                        job_card.chass_No = dct.Rows[0]["chas_no"].ToString();
                        job_card.reg_no = dct.Rows[0]["reg_no"].ToString();
                        job_card.modl_code = dct.Rows[0]["chas_no"].ToString();
                        job_card.kms = dct.Rows[0]["km_run"].ToString();
                        job_card.avrage = dct.Rows[0]["avj_km"].ToString();
                        job_card.eng_no = dct.Rows[0]["eng_no"].ToString();
                    }


                    if (jobpara_dt.Rows.Count > 0)
                    {
                        job_card.seervice_book = jobpara_dt.Rows[0]["chk_para1"].ToString();
                        job_card.toolkit = jobpara_dt.Rows[0]["chk_para2"].ToString();
                        job_card.jack = jobpara_dt.Rows[0]["chk_para3"].ToString();
                        job_card.jack_handle = jobpara_dt.Rows[0]["chk_para4"].ToString();
                        job_card.Stereo = jobpara_dt.Rows[0]["chk_para5"].ToString();
                        job_card.wheel = jobpara_dt.Rows[0]["chk_para6"].ToString();
                        job_card.mudflaps = jobpara_dt.Rows[0]["chk_para7"].ToString();
                        job_card.boot_mats = jobpara_dt.Rows[0]["chk_para8"].ToString();
                        job_card.Lighter = jobpara_dt.Rows[0]["chk_para9"].ToString();
                        job_card.Tyres = jobpara_dt.Rows[0]["chk_para10"].ToString();
                        job_card.extend_warrnt = jobpara_dt.Rows[0]["chk_para11"].ToString();
                        job_card.fule = jobpara_dt.Rows[0]["chk_para12"].ToString();
                        job_card.Dent = jobpara_dt.Rows[0]["chk_para13"].ToString();
                        job_card.Crack = jobpara_dt.Rows[0]["chk_para14"].ToString();
                        job_card.Peel = jobpara_dt.Rows[0]["chk_para15"].ToString();
                        job_card.cd_player = jobpara_dt.Rows[0]["chk_para16"].ToString();
                        job_card.battery = jobpara_dt.Rows[0]["chk_para17"].ToString();
                        job_card.scratch = jobpara_dt.Rows[0]["chk_para18"].ToString();

                    }
                    if (jobcmpl_dt.Rows.Count > 0)
                    {
                        job_card.cust_comp1 = jobcmpl_dt.Rows[0]["Cust_Cmpl1"].ToString();
                        job_card.cust_comp2 = jobcmpl_dt.Rows[0]["Cust_Cmpl2"].ToString();
                        job_card.cust_comp3 = jobcmpl_dt.Rows[0]["Cust_Cmpl3"].ToString();
                        job_card.cust_comp4 = jobcmpl_dt.Rows[0]["Cust_Cmpl4"].ToString();
                        job_card.cust_comp5 = jobcmpl_dt.Rows[0]["Cust_Cmpl5"].ToString();
                        job_card.cust_comp6 = jobcmpl_dt.Rows[0]["Cust_Cmpl6"].ToString();
                        job_card.cust_comp7 = jobcmpl_dt.Rows[0]["Cust_Cmpl7"].ToString();
                        job_card.cust_comp8 = jobcmpl_dt.Rows[0]["Cust_Cmpl8"].ToString();

                        job_card.Cust_Obsr1 = jobcmpl_dt.Rows[0]["Cmpl_Obsr1"].ToString();
                        job_card.Cust_Obsr2 = jobcmpl_dt.Rows[0]["Cmpl_Obsr2"].ToString();
                        job_card.Cust_Obsr3 = jobcmpl_dt.Rows[0]["Cmpl_Obsr3"].ToString();
                        job_card.Cust_Obsr4 = jobcmpl_dt.Rows[0]["Cmpl_Obsr4"].ToString();
                        job_card.Cust_Obsr5 = jobcmpl_dt.Rows[0]["Cmpl_Obsr5"].ToString();
                        job_card.Cust_Obsr6 = jobcmpl_dt.Rows[0]["Cmpl_Obsr6"].ToString();
                        job_card.Cust_Obsr7 = jobcmpl_dt.Rows[0]["Cmpl_Obsr7"].ToString();
                        job_card.Cust_Obsr8 = jobcmpl_dt.Rows[0]["Cmpl_Obsr8"].ToString();

                        job_card.source1 = jobcmpl_dt.Rows[0]["road_test1"].ToString();
                        job_card.source2 = jobcmpl_dt.Rows[0]["road_test2"].ToString();
                        job_card.source3 = jobcmpl_dt.Rows[0]["road_test3"].ToString();
                        job_card.source4 = jobcmpl_dt.Rows[0]["road_test4"].ToString();
                        job_card.source5 = jobcmpl_dt.Rows[0]["road_test5"].ToString();
                        job_card.source6 = jobcmpl_dt.Rows[0]["road_test6"].ToString();
                        job_card.source7 = jobcmpl_dt.Rows[0]["road_test7"].ToString();
                        job_card.source8 = jobcmpl_dt.Rows[0]["road_test8"].ToString();
                    }

                    if (doc_dt.Rows.Count > 0)
                    {

                        job_card.img1 = doc_dt.Rows[0]["file_name"].ToString();
                        job_card.img2 = doc_dt.Rows[1]["file_name"].ToString();
                        job_card.img3 = doc_dt.Rows[2]["file_name"].ToString();
                        job_card.img4 = doc_dt.Rows[3]["file_name"].ToString();
                        job_card.img5 = doc_dt.Rows[4]["file_name"].ToString();
                        job_card.img6 = doc_dt.Rows[5]["file_name"].ToString();
                        job_card.img7 = doc_dt.Rows[6]["file_name"].ToString();
                        job_card.img8 = doc_dt.Rows[7]["file_name"].ToString();
                        job_card.img9 = doc_dt.Rows[8]["file_name"].ToString();
                        job_card.img10 = doc_dt.Rows[9]["file_name"].ToString();
                        job_card.img11 = doc_dt.Rows[10]["file_name"].ToString();
                        job_card.img12 = doc_dt.Rows[11]["file_name"].ToString();
                    }




                    List<item_list> item_list = new List<item_list>();
                    string item_str = "select * from item_dtl where tran_id='" + job_card.trans_id + "'";
                    DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, item_str);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            item_list.Add(new Models.item_list()
                            {
                                item_code = dr["item_code"].ToString(),
                                part_desc = dr["item_disc"].ToString(),
                                per_rate = dr["unit_rate"].ToString(),
                                qty = dr["dmd_qty"].ToString(),
                                total_amt = dr["amount"].ToString(),
                            });
                        }
                    }
                    job_card.item_list = item_list;

                    List<labr_list> labr_list = new List<labr_list>();
                    string labr_list1 = "select * from labr_dtl where tran_id='" + job_card.trans_id + "'";
                    DataSet dls = SqlHelper.ExecuteDataset(CommandType.Text, labr_list1);
                    if (dls.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dls.Tables[0].Rows)
                        {
                            labr_list.Add(new Models.labr_list()
                            {
                                labr_no = dr["lbr_code"].ToString(),
                                labr_name = dr["lbr_disc"].ToString(),
                                labr_charg = dr["lbr_rate"].ToString(),
                            });
                        }
                    }
                    job_card.labr_list = labr_list;


                }
              else
              {
                  job_card.jobcard_no = job_id;
              }

              return View(job_card);
            }


            #region Job NO
            string str = "select max(INV_No) as INV_No from INV_MST";
                DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, str).Tables[0];
                if (dt.Rows.Count > 0)
                {
                   var a = dt.Rows[0]["INV_No"].ToString();
                   int z = 1 + int.Parse(a);
                   job_card.jobcard_no = z.ToString();


                }

                if(dt.Rows.Count == 0)
                {
                job_card.jobcard_no = "1";
                }
            #endregion

            List<serv_type> serv_type = new List<serv_type>();
            string adv_str = "select * from Misc_mst where misc_type='31'";
            DataSet des = SqlHelper.ExecuteDataset(CommandType.Text, adv_str);
            if (des.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in des.Tables[0].Rows)
                {
                    serv_type.Add(new Models.serv_type()
                    {
                        ser_type = dr["misc_name"].ToString(),
                        type_code = dr["misc_code"].ToString(),
                    });
                }
            }
            job_card.serv_type = serv_type;


            List<serv_adv> serv_adv = new List<serv_adv>();
            string ser_str = "select * from Misc_mst where misc_type='32'";
            DataSet drs = SqlHelper.ExecuteDataset(CommandType.Text, ser_str);
            if (drs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in drs.Tables[0].Rows)
                {
                    serv_adv.Add(new Models.serv_adv()
                    {
                        ser_adv = dr["misc_name"].ToString(),
                        adv_code = dr["misc_code"].ToString(),
                    });
                }
            }
            job_card.serv_adv = serv_adv;



            return View(job_card);
        }

        public JsonResult airportsearch(string search)
        {
            List<appointment_mvc.Models.itemNO_list> citylist = new List<appointment_mvc.Models.itemNO_list>();
            string response1 = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["importantfile"].ToString() + "itemNO_list.json");

            List<appointment_mvc.Models.itemNO_list> alllist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<appointment_mvc.Models.itemNO_list>>(response1);
            citylist = alllist.FindAll(x => x.item_code.ToString().ToLower().StartsWith(search.ToLower().Trim()) || x.part_no.ToString().ToLower().StartsWith(search.ToLower().Trim()) || x.part_name.ToString().ToLower().StartsWith(search.ToLower().Trim()));

            var top10 = citylist.OrderByDescending(o => o.item_code).Take(10);

            return Json(top10, JsonRequestBehavior.AllowGet);


        }

        public JsonResult chas_search(string search)
        {
            List<appointment_mvc.Models.chas_mst> citylist = new List<appointment_mvc.Models.chas_mst>();
            string response1 = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["importantfile"].ToString() + "chas_mst_list.json");

            List<appointment_mvc.Models.chas_mst> alllist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<appointment_mvc.Models.chas_mst>>(response1);
            citylist = alllist.FindAll(x => x.chas_id.ToString().ToLower().StartsWith(search.ToLower().Trim()) || x.chas_no.ToString().ToLower().StartsWith(search.ToLower().Trim()));

            var top10 = citylist.OrderByDescending(o => o.chas_no).Take(10);

            return Json(top10, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult inv_search(string search, string modl)
        {
            inv_mstlist inv_mstlist = new inv_mstlist();
            string inv_st = "select * from inv_mst where chas_id= '"+search+"'";
            DataTable dns = SqlHelper.ExecuteDataset(CommandType.Text, inv_st).Tables[0];
            if (dns.Rows.Count > 0)
            {
                inv_mstlist.Ledg_Name = dns.Rows[0]["Ledg_Name"].ToString();
                inv_mstlist.ph1 = dns.Rows[0]["ph1"].ToString();
                inv_mstlist.ph2 = dns.Rows[0]["ph2"].ToString();
                inv_mstlist.Ledg_Add1 = dns.Rows[0]["Ledg_Add1"].ToString();
            }
            string modl_st = "select * from modl_mst where item_code= '" + modl + "'";
            DataTable dms = SqlHelper.ExecuteDataset(CommandType.Text, modl_st).Tables[0];
            if (dms.Rows.Count > 0)
            {
                inv_mstlist.modl_code = dms.Rows[0]["modl_code"].ToString();
                inv_mstlist.modl_name = dms.Rows[0]["modl_name"].ToString();
            }


            return Json(inv_mstlist);

        }

        public JsonResult model_search(string search)
        {
            List<appointment_mvc.Models.model_list> model_list = new List<appointment_mvc.Models.model_list>();
            string response1 = System.IO.File.ReadAllText(System.Configuration.ConfigurationManager.AppSettings["importantfile"].ToString() + "model_list.json");

            List<appointment_mvc.Models.model_list> alllist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<appointment_mvc.Models.model_list>>(response1);
            model_list = alllist.FindAll(x => x.model_no.ToString().ToLower().StartsWith(search.ToLower().Trim()) || x.model_name.ToString().ToLower().StartsWith(search.ToLower().Trim()));

            var top10 = model_list.OrderByDescending(o => o.model_no).Take(10);

            return Json(top10, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult insert_jobcard(appointment_mvc.Models.job_card job_card)
        {
            common_response common_response = new common_response();

            string str = "select max(tran_id) as tran_id  from  inv_mst";
            DataTable dt = SqlHelper.ExecuteDataset(CommandType.Text, str).Tables[0];
            string z = "";
            string x = "20";
            if (dt.Rows.Count > 0)
            {
                var a = dt.Rows[0]["tran_id"].ToString();
                 var d = 1 + int.Parse(a);
                z = d.ToString();
              

            }

            string chasstr = "select max(chas_id) as chas_id  from  chas_mst";
            DataTable dyt = SqlHelper.ExecuteDataset(CommandType.Text, chasstr).Tables[0];
            string zy = "1";
            if (dyt.Rows.Count > 0)
            {
                var ay = dyt.Rows[0]["chas_id"].ToString();
                var dy = 1 + int.Parse(ay);
                zy = dy.ToString();

            }

            var date = DateTime.Now.ToString("dd/MM/YY");

            List<object> tableData  = JsonConvert.DeserializeObject<List<object>>(job_card.tabledata);
            
            foreach (JObject dr in tableData /*var row in tableData*/)
            {
                string part_no = dr["part_no"].ToString();
                string part_name = dr["part_name"].ToString();
                string qty = dr["qty"].ToString();
                string per_rate = dr["per_rate"].ToString();
                string total = dr["total"].ToString();

                if (part_no == null || part_no == "")
                {
                    part_no = "12345";
                }

                string item_str = "insert into item_dtl(tran_id,tran_type,item_code, item_disc,unit_rate, dmd_qty,amount,export_type, serverid) values('" + z + "','" + x + "','" + part_no + "','" + part_name + "','" + per_rate + "','" + qty + "','" + total + "','1','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, item_str);
            }


            List<object> labr_tableData = JsonConvert.DeserializeObject<List<object>>(job_card.labr_tabledata);

            foreach (JObject dr in labr_tableData /*var row in tableData*/)
            {
                string labr_no = dr["labr_no"].ToString();
                string labr_name = dr["labr_name"].ToString();
                string labr_chrg = dr["labr_charge"].ToString();

                if (labr_no == null || labr_no == "")
                {
                    labr_no = "12345";
                }

                string labr_str = "insert into labr_dtl(tran_id,tran_type,lbr_code, lbr_disc, lbr_rate, export_type, serverid) values('" + z + "','" + x + "','" + labr_no + "','" + labr_name + "','" + labr_chrg + "','1','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, labr_str);
            }

            #region image upload

            #region variable

            HttpPostedFileBase file1 = Request.Files["fileupload1"];
            HttpPostedFileBase file2 = Request.Files["fileupload2"];
            HttpPostedFileBase file3 = Request.Files["fileupload3"];
            HttpPostedFileBase file4 = Request.Files["fileupload4"];
            HttpPostedFileBase file5 = Request.Files["fileupload5"];
            HttpPostedFileBase file6 = Request.Files["fileupload6"];
            HttpPostedFileBase file7 = Request.Files["fileupload7"];
            HttpPostedFileBase file8 = Request.Files["fileupload8"];
            HttpPostedFileBase file9 = Request.Files["fileupload9"];
            HttpPostedFileBase file10 = Request.Files["fileupload10"];
            HttpPostedFileBase file11 = Request.Files["fileupload11"];
            HttpPostedFileBase file12 = Request.Files["fileupload12"];

            string img1 = "";
            string img2 = "";
            string img3 = "";
            string img4 = "";
            string img5 = "";
            string img6 = "";
            string img7 = "";
            string img8 = "";
            string img9 = "";
            string img10 = "";
            string img11 = "";
            string img12 = "";


            string uploadpayslipss1;
            string uploadpayslipss2;
            string uploadpayslipss3;
            string uploadpayslipss4;
            string uploadpayslipss5;
            string uploadpayslipss6;
            string uploadpayslipss7;
            string uploadpayslipss8;
            string uploadpayslipss9;
            string uploadpayslipss10;
            string uploadpayslipss11;
            string uploadpayslipss12;

            #endregion

            if (job_card.img1 != null && job_card.img1 != "")
            {
                img1 = job_card.img1;
            }

            if (file1 != null && file1.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss1 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file1.FileName.Split('.')[1];
                file1.SaveAs(path + uploadpayslipss1);
                img1 = uploadpayslipss1;

            }



            if (job_card.img2 != null && job_card.img2 != "")
            {
                img2 = job_card.img2;
            }

            if (file2 != null && file2.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss2 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file2.FileName.Split('.')[1];
                file2.SaveAs(path + uploadpayslipss2);
                img2 = uploadpayslipss2;

            }



            if (job_card.img3 != null && job_card.img3 != "")
            {
                img3 = job_card.img3;
            }

            if (file3 != null && file3.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss3 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file3.FileName.Split('.')[1];
                file3.SaveAs(path + uploadpayslipss3);
                img3 = uploadpayslipss3;

            }



            if (job_card.img4 != null && job_card.img4 != "")
            {
                img4 = job_card.img4;
            }

            if (file4 != null && file4.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss4 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file4.FileName.Split('.')[1];
                file4.SaveAs(path + uploadpayslipss4);
                img4 = uploadpayslipss4;

            }



            if (job_card.img5 != null && job_card.img5 != "")
            {
                img5 = job_card.img5;
            }

            if (file5 != null && file5.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss5 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file5.FileName.Split('.')[1];
                file5.SaveAs(path + uploadpayslipss5);
                img5 = uploadpayslipss5;

            }


            if (job_card.img6 != null && job_card.img6 != "")
            {
                img6 = job_card.img6;
            }

            if (file6 != null && file6.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss6 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file6.FileName.Split('.')[1];
                file6.SaveAs(path + uploadpayslipss6);
                img6 = uploadpayslipss6;

            }


            if (job_card.img7 != null && job_card.img7 != "")
            {
                img7 = job_card.img7;
            }

            if (file7 != null && file7.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss7 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file7.FileName.Split('.')[1];
                file7.SaveAs(path + uploadpayslipss7);
                img7 = uploadpayslipss7;

            }

            if (job_card.img8 != null && job_card.img8 != "")
            {
                img8 = job_card.img8;
            }

            if (file8 != null && file8.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss8 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file8.FileName.Split('.')[1];
                file8.SaveAs(path + uploadpayslipss8);
                img8 = uploadpayslipss8;

            }


            if (job_card.img9 != null && job_card.img9 != "")
            {
                img9 = job_card.img9;
            }

            if (file9 != null && file9.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss9 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file9.FileName.Split('.')[1];
                file9.SaveAs(path + uploadpayslipss9);
                img9 = uploadpayslipss9;

            }

            if (job_card.img10 != null && job_card.img10 != "")
            {
                img10 = job_card.img10;
            }

            if (file10 != null && file10.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss10 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file10.FileName.Split('.')[1];
                file10.SaveAs(path + uploadpayslipss10);
                img10 = uploadpayslipss10;

            }


            if (job_card.img11 != null && job_card.img11 != "")
            {
                img11 = job_card.img11;
            }

            if (file11 != null && file11.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss11 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file11.FileName.Split('.')[1];
                file11.SaveAs(path + uploadpayslipss11);
                img11 = uploadpayslipss11;

            }


            if (job_card.img12 != null && job_card.img12 != "")
            {
                img12 = job_card.img12;
            }

            if (file12 != null && file12.FileName.ToString() != "")
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["tempimage"].ToString();
                uploadpayslipss12 = DateTime.Now.ToString("ssMMHHmmyyyydd") + System.Guid.NewGuid() + "." + file12.FileName.Split('.')[1];
                file12.SaveAs(path + uploadpayslipss12);
                img12 = uploadpayslipss12;

            }
            #endregion

            string inv_str = "insert into INV_MST(tran_id,tran_type,INV_No, Ledg_Name, ph1, Ph2, Ledg_Add1, INV_Date, Item_Amt, Lbr_Amt, Bill_Amt,chas_id,srv_type,srv_adv) values('"+ z + "','" + x + "','" + job_card.jobcard_no + "','" + job_card.cust_name + "','" + job_card.mobile_f + "','" + job_card.mobile_2 + "','" + job_card.address + "','" + date + "','" + job_card.parts_amt + "','" + job_card.labour_charg + "','" + job_card.total + "','" + zy + "','"+job_card.service_type+ "','" + job_card.serv_adv + "')";

            string jobpara_str = "insert into Job_Para (tran_id,chk_para1,chk_para2,chk_para3,chk_para4,chk_para5,chk_para6,chk_para7,chk_para8,chk_para9,chk_para10,chk_para11, chk_para12, chk_para13, chk_para14, chk_para15, chk_para16, chk_para17, chk_para18, chk_para19, chk_para20, chk_para21, chk_para22, chk_para23, chk_para24, chk_para25, chk_para26, chk_para27, chk_para28, chk_para29, chk_para30, chk_para31, chk_para32, chk_para33, chk_para34, export_type, serverid, loc_code) values('" + z + "','" + job_card.seervice_book + "','" + job_card.toolkit + "','" + job_card.jack + "','" + job_card.jack_handle + "','" + job_card.Stereo + "','" + job_card.wheel + "','" + job_card.mudflaps + "','" + job_card.boot_mats + "','" + job_card.Lighter + "','" + job_card.Tyres + "','" + job_card.extend_warrnt + "','" + job_card.fule + "','" + job_card.Dent + "','" + job_card.Crack + "','" + job_card.Peel + "','" + job_card.cd_player + "','" + job_card.battery + "','" + job_card.scratch + "','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','0','1','1','1')";

            string jobcmpl_str = "insert into Job_Cmpl (tran_id,tran_type,Cust_Cmpl1,Cust_Cmpl2,Cust_Cmpl3,Cust_Cmpl4,Cust_Cmpl5,Cust_Cmpl6,Cust_Cmpl7,Cust_Cmpl8,Cmpl_Obsr1,Cmpl_Obsr2,Cmpl_Obsr3, Cmpl_Obsr4, Cmpl_Obsr5, Cmpl_Obsr6, Cmpl_Obsr7, Cmpl_Obsr8, export_type, serverid, loc_code, road_test1, road_test2, road_test3, road_test4, road_test5, road_test6, road_test7, road_test8) values('" + z + "','" + x + "','"+job_card.cust_comp1+ "','" + job_card.cust_comp2 + "','" + job_card.cust_comp3 + "','" + job_card.cust_comp4 + "','" + job_card.cust_comp5 + "','" + job_card.cust_comp6 + "','" + job_card.cust_comp7 + "','" + job_card.cust_comp8 + "','" + job_card.Cust_Obsr1 + "','" + job_card.Cust_Obsr2 + "','" + job_card.Cust_Obsr3 + "','" + job_card.Cust_Obsr4 + "','" + job_card.Cust_Obsr5 + "','" + job_card.Cust_Obsr6 + "','" + job_card.Cust_Obsr7 + "','" + job_card.Cust_Obsr8 + "', '1','1','1','" + job_card.source1 + "','" + job_card.source2 + "','" + job_card.source3 + "','" + job_card.source4 + "','" + job_card.source5 + "','" + job_card.source6 + "','" + job_card.source7 + "','" + job_card.source8 + "')";

            string chas_str = "insert into chas_mst(chas_id,chas_no,reg_no, km_run, avj_km, modl_code, eng_no,export_type) values('" + zy + "','" + job_card.chass_No + "','" + job_card.reg_no + "','" + job_card.kms + "','" + job_card.avrage + "','" + job_card.modl_code + "','" + job_card.eng_no + "','1')";



            #region insert images
            if (img1 != null && img1 != "")
            {
                string img1_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','1','','" + img1 + "','" + date + "','1')";
                SqlHelper.ExecuteNonQuery(CommandType.Text, img1_str);
            }

            if (img2 != null && img2 != "")
            {
                string img2_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','2','','" + img2 + "','" + date + "','1')";
                SqlHelper.ExecuteNonQuery(CommandType.Text, img2_str);
            }

            if (img3 != null && img3 != "")
            {
                string img3_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','3','','" + img3 + "','" + date + "','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, img3_str);
            }

            if (img4 != null && img4 != "")
            {
                string img4_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','4','','" + img4 + "','" + date + "','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, img4_str);
            }

            if (img5 != null && img5 != "")
            {
                string img5_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','5','','" + img5 + "','" + date + "','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, img5_str);
            }

            if (img6 != null && img6 != "")
            {
                string img6_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','6','','" + img6 + "','" + date + "','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, img6_str);
            }

            if (img7 != null && img7 != "")
            {
                string img7_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','7','','" + img7 + "','" + date + "','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, img7_str);
            }

            if (img8 != null && img8 != "")
            {
                string img8_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','8','','" + img8 + "','" + date + "','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, img8_str);
            }

            if (img9 != null && img9 != "")
            {
                string img9_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','9','','" + img9 + "','" + date + "','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, img9_str);
            }

            if (img10 != null && img10 != "")
            {
                string img10_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','10','','" + img10 + "','" + date + "','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, img10_str);
            }

            if (img11 != null && img11 != "")
            {
                string img11_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','11','','" + img11 + "','" + date + "','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, img11_str);
            }

            if (img12 != null && img12 != "")
            {
                string img12_str = "insert into DOC_UPLOAD (Doc_Type,TRAN_ID,SRNO,path,File_Name,Upload_Date,Export_type) values('Jobcard','" + z + "','12','','" + img12 + "','" + date + "','1')";

                SqlHelper.ExecuteNonQuery(CommandType.Text, img12_str);
            }

            #endregion

            try
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, inv_str);
                SqlHelper.ExecuteNonQuery(CommandType.Text, jobpara_str);
                SqlHelper.ExecuteNonQuery(CommandType.Text, jobcmpl_str);
                SqlHelper.ExecuteNonQuery(CommandType.Text, chas_str);

                common_response.success = true;
                common_response.message = "Data Submitted Successfully";

            }
            catch
            {
                common_response.success = false;
                common_response.message = "invalid data!";
            }
           
            return Json(common_response);
        }

     
        public ActionResult print_card(string job_id)
        {
            appointment_mvc.Models.print_jobcard print_jobcard = new appointment_mvc.Models.print_jobcard();

            if (job_id != null)
            {

                string strr = "select tran_id from  INV_MST where INV_No='" + job_id + "'";
                DataTable drrt = SqlHelper.ExecuteDataset(CommandType.Text, strr).Tables[0];

                if (drrt.Rows.Count > 0)
                {
                    print_jobcard.trans_id = drrt.Rows[0]["tran_id"].ToString();

                    string view_inv_str = "select * from  INV_MST where tran_id='" + print_jobcard.trans_id + "'  select * from  Job_cmpl where tran_id='" + print_jobcard.trans_id + "' select * from  godown_mst where godw_code='1' ";
                    DataSet dtt = SqlHelper.ExecuteDataset(CommandType.Text, view_inv_str);

                    DataTable inv_dt = dtt.Tables[0];
                    DataTable jobcmpl_dt = dtt.Tables[1];
                    DataTable godwn_dt = dtt.Tables[2];


                    print_jobcard.jobcard_no = inv_dt.Rows[0]["INV_No"].ToString();
                    print_jobcard.cust_name = inv_dt.Rows[0]["Ledg_Name"].ToString();
                    print_jobcard.mobile_f = inv_dt.Rows[0]["ph1"].ToString();
                    print_jobcard.mobile_2 = inv_dt.Rows[0]["ph2"].ToString();
                    print_jobcard.address = inv_dt.Rows[0]["Ledg_Add1"].ToString();
                    print_jobcard.date = inv_dt.Rows[0]["INV_Date"].ToString();
                    print_jobcard.parts_amt = inv_dt.Rows[0]["Item_Amt"].ToString();
                    print_jobcard.labour_charg = inv_dt.Rows[0]["Lbr_Amt"].ToString();
                    print_jobcard.total = inv_dt.Rows[0]["Bill_Amt"].ToString();


                    print_jobcard.godwn_name = godwn_dt.Rows[0]["godw_name"].ToString();
                    print_jobcard.godwn_add1 = godwn_dt.Rows[0]["godw_add1"].ToString();
                    print_jobcard.godwn_add2 = godwn_dt.Rows[0]["godw_add2"].ToString();
                    print_jobcard.godwn_phone = godwn_dt.Rows[0]["mob_no"].ToString();
                    
                    
                    

                    List<labr_list> labr_list = new List<labr_list>();
                    string item_str = "select * from labr_dtl where tran_id='" + print_jobcard.trans_id + "'";
                    DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, item_str);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            labr_list.Add(new Models.labr_list()
                            {
                                labr_no = dr["lbr_code"].ToString(),
                                labr_name = dr["lbr_disc"].ToString(),
                                labr_charg = dr["lbr_rate"].ToString(),
                               
                            });
                        }
                    }
                    print_jobcard.labr_list = labr_list;

                    List<cust_cmpl_list> cust_cmpl_list = new List<cust_cmpl_list>();
                    string cmpl_str = "select * from Job_cmpl where tran_id='" + print_jobcard.trans_id + "'";
                    DataSet dss = SqlHelper.ExecuteDataset(CommandType.Text, cmpl_str);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dss.Tables[0].Rows)
                        {
                            cust_cmpl_list.Add(new Models.cust_cmpl_list()
                            {
                                cust_comp = dr["Cust_Cmpl1"].ToString(),
                                Cust_Obsr = dr["Cmpl_Obsr1"].ToString(),

                            });
                        }
                    }
                    print_jobcard.cust_cmpl_list = cust_cmpl_list;


                }


                else
                {
                    return RedirectToAction("job_card");
                }
                
            }


            return View(print_jobcard);
        }


        public ActionResult acc_voucher()
        {

            #region item_list for job card

            List<book_name> book_name = new List<book_name>();
            string book_st = "select book_code,Book_Name from Book_Mst";
            DataSet dt_book = SqlHelper.ExecuteDataset(CommandType.Text, book_st);
            if (dt_book.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dt_book.Tables[0].Rows)
                {
                    book_name.Add(new Models.book_name()
                    {
                        book_code = dr["book_code"].ToString(),
                        Book_Name = dr["Book_Name"].ToString(),


                    });
                }
            }

            System.IO.File.WriteAllText(path + "book_name_list.json", JsonConvert.SerializeObject(book_name));

            #endregion


            return View();
        }


    }
}