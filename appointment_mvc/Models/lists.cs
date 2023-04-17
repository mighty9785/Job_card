using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace appointment_mvc.Models
{
    

        public class list
        {
           public List<cash_bank> cash_bank { get; set; }
            public List<book_name> book_name { get; set; }
        }
        public class book_name
        {
            public string book_code { get; set; }
            public string Book_Name { get; set; }
           
        }

        public class cash_bank
        {
            public string Ledg_Code { get; set; }
            public string Ledg_Name { get; set; }
            public string loc_code { get; set; }

        }

    public class chas_mst
    {
        public string chas_id { get; set; }
        public string chas_no { get; set; }
        public string reg_no { get; set; }
        public string eng_no { get; set; }
        public string modl_code { get; set; }
        public string modl_name { get; set; }
        public string modl_id { get; set; }

    }

    public class model_list
    {
        public string model_no { get; set; }
        public string model_name { get; set; }
        public string model_id { get; set; }

    }


}