using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Text;

using appointment_mvc.Models;
public class SendMail
{
    string admintomail = "aliaadil107@gmail.com";
    string noreplyemail = "aliaadil107@gmail.com";
    string host = "aliaadil107@gmail.com";

    string logo = "https://alhawatourstravel.com/assets/images/common/logo.png";
    string uploadfile = "https://alhawatourstravel.com/Content/Uploadimg/";

  //  string siteurl = System.Configuration.ConfigurationManager.AppSettings["SiteUrl"].ToString();
    string smtpemail = "aliaadil107@gmail.com";
    string smtpemailpassword = "aadilali786aa";

    DataTable dtemail = new DataTable();
    private string _emailid;
    private string _BookingID;
    public string emailid
    {
        get { return _emailid; }
        set { _emailid = value; }
    }
    public string BookingID
    {
        get { return _BookingID; }
        set { _BookingID = value; }
    }

    private string _agencyId;
    public string agencyId
    {
        get { return _agencyId; }
        set { _agencyId = value; }
    }

    public SendMail()
    {
        //
        // TODO: Add constructor logic here
        //
        //fillemail();
    }

    public void fillemail()
    {


        //string query = "select * from tbl_superadmin";
        //dtemail = SqlHelper.ExecuteDataset(CommandType.Text, query).Tables[0];
        dtemail.ReadXml(HttpContext.Current.Server.MapPath("TestXML/adminemail.xml"));
        if (dtemail.Rows.Count > 0)
        {
            admintomail = dtemail.Rows[0]["Email"].ToString(); //"vijay.teamindiawebdesign@gmail.com";//dtemail.Rows[0]["adminemail"].ToString();
        }
        else
        {
            admintomail = "bookings@mtt4travel.com";
        }
    }

    //Send email to admin and user after submitting visa


}