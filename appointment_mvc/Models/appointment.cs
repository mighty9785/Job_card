using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace appointment_mvc.Models
{
    public class appointment
    {
      
            [AllowHtml]
            public string id { get; set; }
            public string S_no { get; set; }
            public string Reg_no { get; set; }
            public string Chas_no { get; set; }
            public string model { get; set; }
            public string customer_name { get; set; }
            public string phone { get; set; }
            public string visit_date { get; set; }
            public string time { get; set; }
            public string complaint { get; set; }

          public  List<appointmentlist> appointment_list { get; set; }

    }
   


        public class appointmentlist
    {
        public string id { get; set; }
        public string S_no { get; set; }
        public string Reg_no { get; set; }
        public string Chas_no { get; set; }
        public string model { get; set; }
        public string customer_name { get; set; }
        public string phone { get; set; }
        public string visit_date { get; set; }
        public string time { get; set; }
        public string complaint { get; set; }

    }
    
}