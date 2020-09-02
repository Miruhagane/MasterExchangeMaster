using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace WebApplication2.aspx
{
    public partial class frmtazas : System.Web.UI.Page
    {

        string con = System.Configuration.ConfigurationManager.ConnectionStrings["server2"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            string iduser1 = Convert.ToString(Session["intareadeusurio"]);
            IdUser.Text = iduser1;
           


        }





     

   
    }
}