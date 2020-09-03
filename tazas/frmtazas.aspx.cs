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

        string con = ConfigurationManager.ConnectionStrings["server2"].ConnectionString;
        string  moneda = "";
        string postdivisa = "";
         

        protected void Page_Load(object sender, EventArgs e)
        { 
            string iduser1 = Convert.ToString(Session["intareadeusurio"]);
            IdUser.Text = iduser1;
            obtndivisa();
        }

        private void obtndivisa()
        {
            foreach (GridViewRow row in Tb_taxaCompras.Rows)
            {

                for ( int tb = 0; tb < 1; tb++)
                {
                    if (moneda == "")
                    {
                        moneda = ((TextBox)Tb_taxaCompras.Rows[row.RowIndex].FindControl("TextValor")).Text;
                    }
                    else
                    { 
                      moneda += "" + ((TextBox)Tb_taxaCompras.Rows[row.RowIndex].FindControl("TextValor")).Text;
                    }   
                }
            }

        }

        private void valorXsuxursal()
        {
            int valor = 0;
            int subestringvalor1 = 0;
            string sucursalcancun = "";
            string idtaxamoneda = "";

            foreach (GridViewRow row in Tb_taxaCompras.Rows)
            {

                for (int tb = 0; tb < 1; tb++)
                {
                    if (postdivisa == "")
                    {
                        postdivisa = ((TextBox)Tb_taxaCompras.Rows[row.RowIndex].FindControl("TextValor")).Text;
                    }
                    else
                    {
                        postdivisa += "" + ((TextBox)Tb_taxaCompras.Rows[row.RowIndex].FindControl("TextValor")).Text;
                    }
                }
            }


            for (int i = 0; i < CheckBoxcancun.Items.Count; i++)
            {
                if (CheckBoxcancun.Items[i].Selected)
                {
                    if (sucursalcancun == "")
                    {
                        sucursalcancun = CheckBoxcancun.Items[i].Value;
                        idtaxamoneda = Tb_taxaCompras.Rows[valor].Cells[0].Text;
                    }
                    else
                    {
                        sucursalcancun += "," + CheckBoxcancun.Items[i].Value;
                        idtaxamoneda = "";
                    }

                    if (moneda.Substring(subestringvalor1, 5) == postdivisa.Substring(subestringvalor1, 5))
                    {
                        subestringvalor1 = subestringvalor1 + 5;
                        idtaxamoneda = "";
                        valor = valor + 1;
                    }
                    else
                    {
                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([IdTaxa], [Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + idtaxamoneda + "), (" + sucursalcancun + "), (1)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();

                        subestringvalor1 = subestringvalor1 + 5;

                        idtaxamoneda = "";
                        valor = valor + 1;
                    }
                    

             

                }
            }
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            valorXsuxursal();
        }
    }
}