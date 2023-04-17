using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace appointment_mvc.Models
{
    public class job_card
    {
        [AllowHtml]
       
        public string trans_id { get; set; }
        public string tran_type { get; set; }
        public string jobcard_no { get; set; }
        public string cust_name { get; set; }
        public string mobile_f { get; set; }
        public string mobile_2 { get; set; }
        public string address { get; set; }
        public string date { get; set; }
        public string reg_no { get; set; }
        public string chass_No { get; set; }
        public string service_type { get; set; }
        public string service_adviser { get; set; }
        public string kms { get; set; }
        public string avrage { get; set; }
        public string modl_code { get; set; }
        public string eng_no { get; set; }
        public string parts_amt { get; set; }
        public string labour_charg { get; set; }
        public string total { get; set; }
        public string seervice_book { get; set; }
        public string wheel { get; set; }
        public string extend_warrnt { get; set; }
        public string toolkit { get; set; }
        public string mudflaps { get; set; }
        public string fule { get; set; }
        public string jack { get; set; }
        public string boot_mats { get; set; }
        public string Dent { get; set; }
        public string jack_handle { get; set; }
        public string Lighter { get; set; }
        public string Crack { get; set; }
        public string Stereo { get; set; }
        public string Tyres { get; set; }
        public string Peel { get; set; }
        public string cd_player { get; set; }
        public string battery { get; set; }
        public string scratch { get; set; }
        public string part_no { get; set; }
        public string part_desc { get; set; }
        public string qty { get; set; }
        public string part_rate { get; set; }
        public string part_amt { get; set; }
        public string total_amt { get; set; }
        public string labr_no { get; set; }
        public string labr_name { get; set; }
        public string labr_charg { get; set; }
        public string img1 { get; set; }
        public string img2 { get; set; }
        public string img3 { get; set; }
        public string img4 { get; set; }
        public string img5 { get; set; }
        public string img6 { get; set; }
        public string img7 { get; set; }
        public string img8 { get; set; }
        public string img9 { get; set; }
        public string img10 { get; set; }
        public string img11 { get; set; }
        public string img12 { get; set; }
        public HttpPostedFileBase fileupload1 { get; set; }
        public HttpPostedFileBase fileupload2 { get; set; }
        public HttpPostedFileBase fileupload3 { get; set; }
        public HttpPostedFileBase fileupload4 { get; set; }
        public HttpPostedFileBase fileupload5 { get; set; }
        public HttpPostedFileBase fileupload6 { get; set; }
        public HttpPostedFileBase fileupload7 { get; set; }
        public HttpPostedFileBase fileupload8 { get; set; }
        public HttpPostedFileBase fileupload9 { get; set; }
        public HttpPostedFileBase fileupload10 { get; set; }
        public HttpPostedFileBase fileupload11 { get; set; }
        public HttpPostedFileBase fileupload12 { get; set; }

        public List<item_list> item_list { get; set; }
        public List<labr_list> labr_list { get; set; }
        public List<itemNO_list> itemNO_list { get; set; }
        public List<serv_type> serv_type { get; set; }
        public List<serv_adv> serv_adv { get; set; }
        public string item_code { get; set; }
        public string cust_comp1 { get; set; }
        public string cust_comp2 { get; set; }
        public string cust_comp3 { get; set; }
        public string cust_comp4 { get; set; }
        public string cust_comp5 { get; set; }
        public string cust_comp6 { get; set; }
        public string cust_comp7 { get; set; }
        public string cust_comp8 { get; set; }
        public string Cust_Obsr1 { get; set; }
        public string Cust_Obsr2 { get; set; }
        public string Cust_Obsr3 { get; set; }
        public string Cust_Obsr4 { get; set; }
        public string Cust_Obsr5 { get; set; }
        public string Cust_Obsr6 { get; set; }
        public string Cust_Obsr7 { get; set; }
        public string Cust_Obsr8 { get; set; }
        public string source1 { get; set; }
        public string source2 { get; set; }
        public string source3 { get; set; }
        public string source4 { get; set; }
        public string source5 { get; set; }
        public string source6 { get; set; }
        public string source7 { get; set; }
        public string source8 { get; set; }
        public string tabledata { get; set; }
        public string labr_tabledata { get; set; }
        
    }

    public class item_list 
    {
     
        public string trans_id { get; set; }
        public string item_code { get; set; }
        public string tran_type { get; set; }
        public string part_no { get; set; }
        public string part_desc { get; set; }
        public string qty { get; set; }
        public string per_rate { get; set; }
        public string total_amt { get; set; }
    }

    public class serv_type
    {
        public string ser_type { get; set; }
        public string type_code { get; set; }

    }

    public class serv_adv
    {
        public string ser_adv { get; set; }
        public string adv_code { get; set; }
    }
    public class itemNO_list
    {
        public string item_code { get; set; }
        public string part_no { get; set; }
        public string part_name { get; set; }
        public string per_rate { get; set; }
        public string total_amt { get; set; }
        public string qty { get; set; }
        
    }

    public class inv_mstlist
    {
        public string Ledg_Name { get; set; }
        public string ph1 { get; set; }
        public string ph2 { get; set; }
        public string Ledg_Add1 { get; set; }
        public string modl_code { get; set; }
        public string modl_name { get; set; }

    }

    public class labr_list
    {
        public string labr_no { get; set; }
        public string labr_name { get; set; }
        public string labr_charg { get; set; }
    }

    public class MyViewModel
    {
        public DataTable TempTable { get; set; }
    }

    public class cust_cmpl_list
    {
        public string cust_comp { get; set; }
        public string Cust_Obsr { get; set; }

    }
    public class print_jobcard 
    {
        public string trans_id { get; set; }
        public string tran_type { get; set; }
        public string jobcard_no { get; set; }
        public string cust_name { get; set; }
        public string mobile_f { get; set; }
        public string mobile_2 { get; set; }
        public string address { get; set; }
        public string date { get; set; }
        public string reg_no { get; set; }
        public string chass_No { get; set; }
        public string service_type { get; set; }
        public string service_adviser { get; set; }
        public string kms { get; set; }
        public string avrage { get; set; }
        public string parts_amt { get; set; }
        public string labour_charg { get; set; }
        public string total { get; set; }
        public string labr_no { get; set; }
        public string labr_name { get; set; }
        public string labr_charg { get; set; }
        public string cust_comp1 { get; set; }
        public string cust_comp2 { get; set; }
        public string cust_comp3 { get; set; }
        public string cust_comp4 { get; set; }
        public string cust_comp5 { get; set; }
        public string cust_comp6 { get; set; }
        public string cust_comp7 { get; set; }
        public string cust_comp8 { get; set; }
        public string Cust_Obsr1 { get; set; }
        public string Cust_Obsr2 { get; set; }
        public string Cust_Obsr3 { get; set; }
        public string Cust_Obsr4 { get; set; }
        public string Cust_Obsr5 { get; set; }
        public string Cust_Obsr6 { get; set; }
        public string Cust_Obsr7 { get; set; }
        public string Cust_Obsr8 { get; set; }
        public string godwn_name { get; set; }
        public string godwn_add1 { get; set; }
        public string godwn_add2 { get; set; }
        public string godwn_phone { get; set; }

        public List<labr_list> labr_list { get; set; }
        public List<cust_cmpl_list> cust_cmpl_list { get; set; }


    }



}