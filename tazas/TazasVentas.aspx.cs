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

namespace WebApplication2.tazas
{
    public partial class TazasVentas : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["server2"].ConnectionString;

        string[] valoresmoneda = { "", "", "", "", "", "" };
        int[] idmonedas = {1,2,3,4,5,6};

        protected void Page_Load(object sender, EventArgs e)
        {
            string iduser1 = Convert.ToString(Session["intareadeusurio"]);
            IdUser.Text = iduser1;
          
        }

        private void obtndivisa()
        {
            int sucursalid = Convert.ToInt32(sucursalselect.SelectedValue);
            SqlDataAdapter compras = new SqlDataAdapter("SELECT DISTINCT a.Int_IdMoneda AS IdTaxa, b.Txt_Moneda AS Moneda, a.dbl_Valor AS Valor, a.Fec_Dia AS Dia, a.Lng_IdTaxa, b.Int_IdMoneda FROM dbo.Tb_Taxas AS a INNER JOIN dbo.Ct_Moneda AS b ON a.Int_IdMoneda = b.Int_IdMoneda inner join dbo.Tb_TaxSuc as c on a.Lng_IdTaxa = c.Lng_IdTaxSuc where(a.Int_IdMoneda IN (1, 2, 3, 4, 5, 6)) and a.Lng_IdTaxa in (SELECT TOP (6) Lng_IdTaxa from Tb_Taxas as d inner join dbo.Tb_TaxSuc as c on d.Lng_IdTaxa = c.Lng_IdTaxSuc where Bol_Tipo = 1 and c.Lng_IdSucursal = "+sucursalid+" order by 1 desc) and a.Bol_Tipo = 1 order by 1", con);
            DataTable tabla = new DataTable();
            compras.Fill(tabla);
            this.Tb_taxaVentas.DataSource = (tabla);
            Tb_taxaVentas.DataBind();

           

        }

        private void valorXsuxursal()
        {
           

            string sucursalcancun = "";

            for (int i = 0; i < CheckBoxcancun.Items.Count; i++)
            {
                if (CheckBoxcancun.Items[i].Selected)
                {
                    if (sucursalcancun == "")
                    {
                        sucursalcancun = CheckBoxcancun.Items[i].Value;

                    }

                }
                if (sucursalcancun != "")
                {
                    int valor = 0;
                    int v = 0;
                    int val = 0; 

                    for (int tb = 0; tb < 6; tb++)
                    {

                        if (valoresmoneda[v] == "")
                        {
                            valoresmoneda[v] = ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                        }
                        else
                        {
                            valoresmoneda[v] += "" + ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                        }

                        v = v + 1;
                    }

                    for (int b = 0; b < 6; b++)
                    {
                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES (" + idmonedas[valor] + "," + valoresmoneda[valor] + ", 1, GETDATE(), '', 2)", conectorbd);
                        conectorbd.Open();
                        inser1.ExecuteNonQuery();
                        conectorbd.Close();

                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + sucursalcancun + ", 2)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();
                        valor = valor + 1;
                    }

                    for (int l = 0; l < 6; l++)
                    {
                        valoresmoneda[val] = "";
                        val = val + 1;
                    }

                }

               
                sucursalcancun = "";
                

            }
        }

        private void valorXplaya()
        {
            
            string carmen = "";

            for (int i = 0; i < CheckBoxcarmen.Items.Count; i++)
            {
                if (CheckBoxcarmen.Items[i].Selected)
                {
                    if (carmen == "")
                    {
                        carmen = CheckBoxcarmen.Items[i].Value;

                    }

                }
                if (carmen != "")
                {
                    int valor = 0;
                    int v = 0;
                    int val = 0;
                    for (int tb = 0; tb < 6; tb++)
                    {

                        if (valoresmoneda[v] == "")
                        {
                            valoresmoneda[v] = ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                        }
                        else
                        {
                            valoresmoneda[v] += "" + ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                        }

                        v = v + 1;
                    }

                    for (int b = 0; b < 6; b++)
                    {
                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES (" + idmonedas[valor] + "," + valoresmoneda[valor] + ", 1, GETDATE(), '', 1)", conectorbd);
                        conectorbd.Open();
                        inser1.ExecuteNonQuery();
                        conectorbd.Close();

                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + carmen + ", 1)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();
                        valor = valor + 1;

                    }
                    for (int l = 0; l < 6; l++)
                    {
                        valoresmoneda[val] = "";
                        val = val + 1;
                    }

                }

                carmen = "";
           

            }
        }

        private void valorXcabos()
        {
           
     
            string cabos = "";

            for (int i = 0; i < CheckBoxcabos.Items.Count; i++)
            {
                if (CheckBoxcabos.Items[i].Selected)
                {
                    if (cabos == "")
                    {
                        cabos = CheckBoxcabos.Items[i].Value;

                    }

                }
                if (cabos != "")
                {
                    int v = 0;
                    int valor = 0;
                    int val = 0;
                    for (int tb = 0; tb < 6; tb++)
                    {

                        if (valoresmoneda[v] == "")
                        {
                            valoresmoneda[v] = ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                        }
                        else
                        {
                            valoresmoneda[v] += "" + ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                        }

                        v = v + 1;
                    }

                    for (int b = 0; b < 6; b++)
                    {
                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES (" + idmonedas[valor] + "," + valoresmoneda[valor] + ", 1, GETDATE(), '', 1)", conectorbd);
                        conectorbd.Open();
                        inser1.ExecuteNonQuery();
                        conectorbd.Close();

                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + cabos + ", 1)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();
                        valor = valor + 1;

                    }
                    for (int l = 0; l < 6; l++)
                    {
                        valoresmoneda[val] = "";
                        val = val + 1;
                    }
                }

                
                cabos = "";
                
            }
        }

        private void valorXtulum()
        {
         
   
            string tulum = "";

            for (int i = 0; i < CheckBoxtulum.Items.Count; i++)
            {
                if (CheckBoxcabos.Items[i].Selected)
                {
                    if (tulum == "")
                    {
                        tulum = CheckBoxtulum.Items[i].Value;

                    }

                }
                if (tulum != "")
                {
                    int valor = 0;
                    int v = 0;
                    int val = 0;
                    for (int tb = 0; tb < 6; tb++)
                    {

                        if (valoresmoneda[v] == "")
                        {
                            valoresmoneda[v] = ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                        }
                        else
                        {
                            valoresmoneda[v] += "" + ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                        }

                        v = v + 1;
                    }

                    for (int b = 0; b < 6; b++)
                    {
                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES (" + idmonedas[valor] + "," + valoresmoneda[valor] + ", 1, GETDATE(), '', 1)", conectorbd);
                        conectorbd.Open();
                        inser1.ExecuteNonQuery();
                        conectorbd.Close();

                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + tulum + ", 1)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();
                        valor = valor + 1;
                    }

                    for (int l = 0; l < 6; l++)
                    {
                        valoresmoneda[val] = "";
                        val = val + 1;
                    }
                }

               
            }
        }

        private void valorxcdmx()
        {
            
            
            string cdmx = "";

            for (int i = 0; i < CheckBoxcdmx.Items.Count; i++)
            {
                if (CheckBoxcdmx.Items[i].Selected)
                {
                    if (cdmx == "")
                    {
                        cdmx = CheckBoxcdmx.Items[i].Value;

                    }

                }
                if (cdmx != "")
                {
                    int valor = 0;
                    int v = 0;
                    int val = 0;
                    for (int tb = 0; tb < 6; tb++)
                    {

                        if (valoresmoneda[v] == "")
                        {
                            valoresmoneda[v] = ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                        }
                        else
                        {
                            valoresmoneda[v] += "" + ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                        }

                        v = v + 1;
                    }


                    for (int b = 0; b < 6; b++)
                    {
                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES (" + idmonedas[valor] + "," + valoresmoneda[valor] + ", 1, GETDATE(), '', 1)", conectorbd);
                        conectorbd.Open();
                        inser1.ExecuteNonQuery();
                        conectorbd.Close();

                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + cdmx + ", 1)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();
                        valor = valor + 1;
                    }

                    for (int l = 0; l < 6; l++)
                    {
                        valoresmoneda[val] = "";
                        val = val + 1;
                    }
                }
             
                cdmx = "";

            }
        }

        private void valorxleon()
        {
          
           
            string leon = "";

            for (int i = 0; i < CheckBoxleon.Items.Count; i++)
            {
                if (CheckBoxleon.Items[i].Selected)
                {
                    if (leon == "")
                    {
                        leon = CheckBoxleon.Items[i].Value;

                    }

                }
                if (leon != "")
                {
                    int valor = 0;
                    int v = 0;
                    int val = 0;

                    for (int b = 0; b < 6; b++)
                    {
                 

                        for (int tb = 0; tb < 6; tb++)
                        {

                            if (valoresmoneda[v] == "")
                            {
                                valoresmoneda[v] = ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                            }
                            else
                            {
                                valoresmoneda[v] += "" + ((TextBox)Tb_taxaVentas.Rows[v].FindControl("TextValor")).Text;
                            }

                            v = v + 1;
                        }

                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES (" + idmonedas[valor] + "," + valoresmoneda[valor] + ", 1, GETDATE(), '', 1)", conectorbd);
                        conectorbd.Open();
                        inser1.ExecuteNonQuery();
                        conectorbd.Close();

                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + leon + ", 1)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();

                        valor = valor + 1;
                    }

                    for (int l = 0; l < 6; l++)
                    {
                        valoresmoneda[val] = "";
                        val = val + 1;
                    }
                }
            
                leon = "";

            }
        }

        protected void Buscarsucursal_Click(object sender, EventArgs e)
        {
            obtndivisa();
        }

        protected void Guardar_Click(object sender, EventArgs e)
        {
            valorXsuxursal();
            valorXplaya();
            valorXcabos();
            valorXtulum();
            valorxcdmx();
            valorxleon();
        }
    }
}