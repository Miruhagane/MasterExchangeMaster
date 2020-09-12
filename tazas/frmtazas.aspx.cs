﻿using System;
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
        
        string[] valoresmoneda = {"","","","","",""};
         

        protected void Page_Load(object sender, EventArgs e)
        { 
            string iduser1 = Convert.ToString(Session["intareadeusurio"]);
            IdUser.Text = iduser1;
            
        }

        private void obtndivisa()
        {
            int sucursalid = Convert.ToInt32(sucursalselect.SelectedValue);
            SqlDataAdapter compras = new SqlDataAdapter("SELECT DISTINCT a.Int_IdMoneda AS IdTaxa, b.Txt_Moneda AS Moneda, a.dbl_Valor AS Valor, a.Fec_Dia AS Dia, a.Lng_IdTaxa, b.Int_IdMoneda FROM dbo.Tb_Taxas AS a INNER JOIN dbo.Ct_Moneda AS b ON a.Int_IdMoneda = b.Int_IdMoneda inner join dbo.Tb_TaxSuc AS c On a.Lng_IdTaxa = c.Lng_IdTaxSuc WHERE(a.Bol_Tipo = 0) AND(a.Int_IdGrupo = (SELECT MAX(Int_IdGrupo) AS Expr1 FROM dbo.Tb_Taxas AS x WHERE(Bol_Tipo = 0))) AND(a.Int_IdMoneda IN(1, 2, 3, 4, 5, 6)) and c.Lng_IdSucursal = "+ sucursalid + "", con);
            DataTable tabla = new DataTable();
            compras.Fill(tabla);
            this.Tb_taxaCompras.DataSource = (tabla);
            Tb_taxaCompras.DataBind();
;
            //obtener valores de cada divisa
            int v = 0;

            for ( int tb = 0; tb < 6; tb++)
                {
                    
                if (valoresmoneda[v] == "")
                    {
                        valoresmoneda[v] = ((TextBox)Tb_taxaCompras.Rows[v].FindControl("textvalor")).Text;
                    }
                    else
                    {
                        valoresmoneda[v] += "" + ((TextBox)Tb_taxaCompras.Rows[v].FindControl("textvalor")).Text;
                    }

                    v = v + 1;
                }

        }

        private void valorXsuxursal()
        {
            int valor = 0;
            
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
                
                   for (int b = 0; b < 6; b++)
                {
                    SqlConnection conectorbd = new SqlConnection(con);
                    SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES ( " + valoresmoneda[valor]+ ", 1, GETDATE(), '', 1)", conectorbd);
                    conectorbd.Open();
                    inser1.ExecuteNonQuery();
                    conectorbd.Close();

                    SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + sucursalcancun + ", 1)", conectorbd);
                    conectorbd.Open();
                    query.ExecuteNonQuery();
                    conectorbd.Close();
                    
                 }
 
                }
                
                valor = valor + 1;
                sucursalcancun = "";

            }
        }

        private void valorXplaya()
        {
            int valor = 0;
            
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

                    for (int b = 0; b < 6; b++)
                    {
                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES ( " + valoresmoneda[valor] + ", 1, GETDATE(), '', 1)", conectorbd);
                        conectorbd.Open();
                        inser1.ExecuteNonQuery();
                        conectorbd.Close();

                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + carmen + ", 1)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();

                    }

                }

                valor = valor + 1;
                carmen = "";

            }
        }

        private void valorXcabos()
        {
            int valor = 0;

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

                    for (int b = 0; b < 6; b++)
                    {
                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES ( " + valoresmoneda[valor] + ", 1, GETDATE(), '', 1)", conectorbd);
                        conectorbd.Open();
                        inser1.ExecuteNonQuery();
                        conectorbd.Close();

                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + cabos + ", 1)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();

                    }

                }

                valor = valor + 1;
                cabos = "";

            }
        }

        private void valorXtulum()
        {
            int valor = 0;

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

                    for (int b = 0; b < 6; b++)
                    {
                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES ( " + valoresmoneda[valor] + ", 1, GETDATE(), '', 1)", conectorbd);
                        conectorbd.Open();
                        inser1.ExecuteNonQuery();
                        conectorbd.Close();

                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + tulum + ", 1)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();

                    }

                }

                valor = valor + 1;
                tulum = "";

            }
        }

        private void valorxcdmx()
        {
            int valor = 0;

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

                    for (int b = 0; b < 6; b++)
                    {
                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES ( " + valoresmoneda[valor] + ", 1, GETDATE(), '', 1)", conectorbd);
                        conectorbd.Open();
                        inser1.ExecuteNonQuery();
                        conectorbd.Close();

                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + cdmx + ", 1)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();

                    }

                }

                valor = valor + 1;
                cdmx = "";

            }
        }

        private void valorxleon()
        {
            int valor = 0;

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

                    for (int b = 0; b < 6; b++)
                    {
                        SqlConnection conectorbd = new SqlConnection(con);
                        SqlCommand inser1 = new SqlCommand("INSERT INTO [dbo].[Tb_Taxas] ([Int_IdMoneda],[dbl_Valor], [Bol_Tipo] ,[Fec_Dia], [Fec_Vencimiento], [Int_IdGrupo]) VALUES ( " + valoresmoneda[valor] + ", 1, GETDATE(), '', 1)", conectorbd);
                        conectorbd.Open();
                        inser1.ExecuteNonQuery();
                        conectorbd.Close();

                        SqlCommand query = new SqlCommand("INSERT INTO [dbo].[Tb_TaxSuc] ([Lng_IdSucursal], [Int_IdGrupo]) VALUES (" + leon + ", 1)", conectorbd);
                        conectorbd.Open();
                        query.ExecuteNonQuery();
                        conectorbd.Close();

                    }

                }

                valor = valor + 1;
                leon = "";

            }
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

        protected void Buscarsucursal_Click(object sender, EventArgs e)
        {
            obtndivisa();
        }
    }
}