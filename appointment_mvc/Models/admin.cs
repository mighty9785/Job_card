using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appointment_mvc.Models
{
    public class login
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Dealer_ID { get; set; }
        public string message { get; set; }
        public string parameter { get; set; }
        public Boolean success { get; set; }

         public  string localpath1 { get; set; }

    }
    public class admininfo
    {
        public string name { get; set; }
        public string agencyid { get; set; }
        public string ip_address { get; set; }
        public string logintime { get; set; }
    }
}