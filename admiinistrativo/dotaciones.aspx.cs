using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.admiinistrativo
{
    public partial class dotaciones : System.Web.UI.Page
    {

        string con = System.Configuration.ConfigurationManager.ConnectionStrings["server2"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            agrupacion();
        }


        public void agrupacion()
        {
            SqlDataAdapter da = new SqlDataAdapter("select sum(Dbl_Monto) as total,MAX(Fec_Registro) as fecha,MAX(Txt_Nombre) as usuario from Tb_Dotaciones as a inner join Tb_DotGrupo as b on a.Lng_IdDotacion = b.Lng_IdDotacion inner join Tb_Usuarios as c on a.Int_Idusuario = c.Int_Idusuario where a.Int_Idusuario = 1", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.rmvc.DataSource = (dt);
            rmvc.DataBind();

        }
    }
}