using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appointment_mvc.Models
{
    public class common_response
    {
        public string message { get; set; }
        public string parameter { get; set; }
        public Boolean success { get; set; }
    }
}