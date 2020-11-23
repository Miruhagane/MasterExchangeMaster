using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;

namespace WebApplication2.Arqueos
{
    public partial class arqueocierre : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["server2"].ConnectionString;

        string[] valoredenominacion = { "", "", "", "", "", "", "", "", "", "", "" };

        string[] IdCierre = { "", "", "", "", "", "", "", "", "", "","" };

        string[] valoresmonedaarqueo = {"","","","","","" ,""};
        string[] idmonedaarqueo = {"","","","","","",""};

        protected void Page_Load(object sender, EventArgs e)
        {
            string iduser1 = Convert.ToString(Session["intareadeusurio"]);
            string n = Convert.ToString(Session["usuario"]);
            string p = Convert.ToString(Session["apellido"]);


            cajero.Text = n + " " + p;

            fecha.Text = Convert.ToString(DateTime.Now);

            idtipo.Text = iduser1;
        }

        private void obtcantidades() 
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            var userid = User.Identity.Name;
            int turno = Convert.ToInt32(Session["turno"]);

            SqlDataAdapter compras = new SqlDataAdapter("select a.Lng_IdCierre, b.Num_Denominacion, a.Dbl_Cantidad from Tb_Arqueo as a inner join Ct_Denominaciones as b on a.Int_IdDenomicacion = b.Int_IdDenominacion where a.Int_IdStatus = 1 and a.Int_IdUsuario = "+userid+" and a.Int_IdSucursal = "+sucursalid+" and  a.Bol_Congelar = 0", con);
            DataTable tabla = new DataTable();
            compras.Fill(tabla);
            this.Tb_arqueofinal.DataSource = (tabla);
            Tb_arqueofinal.DataBind();


            SqlDataAdapter monedaarqueo = new SqlDataAdapter("select a.Lng_IdArqueoMoneda as 'Codigo', b.Txt_Moneda as 'Moneda', a.Dbl_Valor as 'Valor' from [Tb_ArqueoMoneda] as a inner join dbo.Ct_Moneda as b on a.Int_IdMoneda = b.Int_IdMoneda where a.Int_IdStatus = 1 and a.Int_IdTurno = " + turno + " and a.Int_IdSucursal = " + sucursalid + " and a.Int_IdUsuario = " + userid + " and a.Bol_Congelar = 0", con);
            DataTable arqueoXmoneda = new DataTable();
            monedaarqueo.Fill(arqueoXmoneda);
            this.Tb_arqueoXmoenda.DataSource = (arqueoXmoneda);
            Tb_arqueoXmoenda.DataBind();


            estatus.Text = "2";
        }

        private void actualizarcantidad()
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            var userid = User.Identity.Name;

            int turno = Convert.ToInt32(Session["turno"]);

            int valor = 0;

            int v = 0;
            for (int a = 0; a < 11; a++)
            {
                if (valoredenominacion[v] == "")
                {
                    valoredenominacion[v] = ((TextBox)Tb_arqueofinal.Rows[v].FindControl("Cantidad")).Text;
                    IdCierre[v] = ((TextBox)Tb_arqueofinal.Rows[v].FindControl("IdCierre")).Text;

                }
                else
                {
                    valoredenominacion[v] += "" + ((TextBox)Tb_arqueofinal.Rows[v].FindControl("Dbl_Cantidad")).Text;
                    IdCierre[v] += "" + ((TextBox)Tb_arqueofinal.Rows[v].FindControl("IdCierre")).Text;
                }
                v = v + 1;
            }
        
            v = 0;

            for (int a = 0; a < 6; a++)
            {
                if (valoresmonedaarqueo[v] == "")
                {
                    valoresmonedaarqueo[v] = ((TextBox)Tb_arqueoXmoenda.Rows[v].FindControl("Valor")).Text;
                    idmonedaarqueo[v] = ((TextBox)Tb_arqueoXmoenda.Rows[v].FindControl("Codigo")).Text;
                }
                else
                {
                    valoresmonedaarqueo[v] += "" +((TextBox)Tb_arqueoXmoenda.Rows[v].FindControl("Valor")).Text;
                    idmonedaarqueo[v] += "" +((TextBox)Tb_arqueoXmoenda.Rows[v].FindControl("Codigo")).Text;
                }
                v = v + 1;

            }

            for (int b = 0; b < 11; b++)
            {

                SqlConnection conectorbd = new SqlConnection(con);
                SqlCommand inser1 = new SqlCommand("UPDATE [dbo].[Tb_Arqueo] SET [Dbl_Cantidad] = " + valoredenominacion[valor] + ", Bol_Congelar = 1 WHERE Lng_IdCierre = " + IdCierre[valor] + " and Int_IdUsuario = " + userid + " and  Int_IdSucursal = " + sucursalid + " ", conectorbd);
                conectorbd.Open();
                inser1.ExecuteNonQuery();
                conectorbd.Close();

                valor = valor + 1;
            }
            valor = 0;

            for (int b = 0; b < 6; b++)
            {
                SqlConnection conectorbd = new SqlConnection(con);
                SqlCommand inser2 = new SqlCommand("UPDATE [dbo].[Tb_ArqueoMoneda] SET [Dbl_Valor] = " + valoresmonedaarqueo[valor] + ",  [Bol_Congelar] = 1 WHERE Lng_IdArqueoMoneda = "+ idmonedaarqueo[valor]+ " and Int_IdTurno = "+turno+ " and Int_IdUsuario = "+userid+ " and Int_IdSucursal = "+sucursalid+"", conectorbd);
                conectorbd.Open();
                inser2.ExecuteNonQuery();
                conectorbd.Close();

                valor = valor + 1;
            }

            valor = 0;
            v = 0;

            estatus.Text = "5";


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