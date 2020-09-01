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
            taxacompras();
           

            if (!IsPostBack)
            {
                selectciudad();
           
            }

        }

        private void taxacompras()
        {
            SqlDataAdapter taxa = new SqlDataAdapter("SELECT DISTINCT a.Int_IdMoneda AS IdTaxa, b.Txt_Moneda AS Moneda, a.dbl_Valor AS Valor, a.Fec_Dia AS Dia FROM dbo.Tb_Taxas AS a INNER JOIN dbo.Ct_Moneda AS b ON a.Int_IdMoneda = b.Int_IdMoneda WHERE (a.Bol_Tipo = 0) AND (a.Int_IdGrupo = (SELECT MAX(Int_IdGrupo) AS Expr1 FROM dbo.Tb_Taxas AS x WHERE (Bol_Tipo = 0))) AND (a.Int_IdMoneda IN (1, 2, 3, 4, 5, 6))", con);
            DataTable result = new DataTable();
            taxa.Fill(result);
            this.Tb_taxaCompras.DataSource = (result);
            Tb_taxaCompras.DataBind();
           

        }


        private void selectciudad()
        {         
            SqlDataAdapter Ciudad = new SqlDataAdapter("select Int_IdPlazas, Txt_Plazas From Ct_Plazas", con);
            DataSet dt = new DataSet();
            Ciudad.Fill(dt);

            ListaXciudad.DataSource = dt;
            ListaXciudad.DataTextField = "Txt_Plazas";
            ListaXciudad.DataValueField = "Int_IdPlazas";
            ListaXciudad.DataBind();

        }


        private void selectsucursal()
        {
            var ciudad = ListaXciudad.SelectedValue;
            SqlDataAdapter sucursal = new SqlDataAdapter("select Lng_IdSucursal, Txt_Sucursal From Tb_Sucursal where Int_IdPlaza = " + ciudad + " ", con);
            DataSet suc = new DataSet();
            sucursal.Fill(suc);

            sucursalXciudad.DataSource = suc;
            sucursalXciudad.DataTextField = "Txt_Sucursal";
            sucursalXciudad.DataValueField = "Lng_IdSucursal";
            sucursalXciudad.DataBind();
        }

        protected void ListaXciudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectsucursal();
        }

        protected void sucursalXciudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            

        }
    }
}