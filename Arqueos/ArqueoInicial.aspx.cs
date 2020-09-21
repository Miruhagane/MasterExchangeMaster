using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using WebApplication2.Models;
using System.Threading.Tasks;

namespace WebApplication2.Arqueos
{
    public partial class ArqueoInicial : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["server2"].ConnectionString;

        string[] valoredenominacion = { "", "", "", "", "", "", "", "", "", "" };
        string[] IdCierre = { "", "", "", "", "", "", "", "", "", "" };
     

        protected void Page_Load(object sender, EventArgs e)
        {
            string iduser1 = Convert.ToString(Session["intareadeusurio"]);
            idtipo.Text = iduser1;
         
        }

        private void obtcantidades()
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            var userid = User.Identity.Name;

            SqlDataAdapter compras = new SqlDataAdapter("select a.Lng_IdCierre, b.Num_Denominacion, a.Dbl_Cantidad from Tb_Cierre as a inner join Ct_Denominaciones as b on a.Int_IdDenomicacion = b.Int_IdDenominacion where a.Int_IdStatus = 1 and a.Int_IdUsuario = "+ userid + " and a.Int_IdSucursal = "+ sucursalid + " ", con);
            DataTable tabla = new DataTable();
            compras.Fill(tabla);
            this.Tb_arqueoinicial.DataSource = (tabla);
            Tb_arqueoinicial.DataBind();

            estatus.Text = "2";
        }
     

        private void actualizarcantidad()
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            var userid = User.Identity.Name;

        
            int valor = 0;

            int v = 0;
            for (int a = 0; a < 10; a++)
            {
                if (valoredenominacion[v] == "")
                {
                    valoredenominacion[v] = ((TextBox)Tb_arqueoinicial.Rows[v].FindControl("Cantidad")).Text;
                    IdCierre[v] = ((TextBox)Tb_arqueoinicial.Rows[v].FindControl("IdCierre")).Text;

                }
                else
                {
                    valoredenominacion[v] += "" + ((TextBox)Tb_arqueoinicial.Rows[v].FindControl("Cantidad")).Text;
                    IdCierre[v] += "" + ((TextBox)Tb_arqueoinicial.Rows[v].FindControl("IdCierre")).Text;
                }


                v = v + 1;

            }

            for (int b = 0; b < 10; b++)
            {

                SqlConnection conectorbd = new SqlConnection(con);
                SqlCommand inser1 = new SqlCommand("UPDATE [dbo].[Tb_Cierre] SET [Dbl_Cantidad] = "+valoredenominacion[valor]+" WHERE Lng_IdCierre = "+ IdCierre[valor] + " and Int_IdUsuario = "+ userid + " and  Int_IdSucursal = "+ sucursalid + " ", conectorbd);
                conectorbd.Open();
                inser1.ExecuteNonQuery();
                conectorbd.Close();

                valor = valor + 1;
            }
            estatus.Text = "1";
            
        }

        protected void btnguardar_Click(object sender, EventArgs e)
        {
            actualizarcantidad();
          
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            obtcantidades();
            
        }
    }
}