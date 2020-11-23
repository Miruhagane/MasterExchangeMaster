using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.tazas
{
    public partial class Historialtazas : System.Web.UI.Page
    {
        string con = ConfigurationManager.ConnectionStrings["server2"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            int sucursalid = 1;
            SqlDataAdapter compras = new SqlDataAdapter("SELECT DISTINCT a.Int_IdMoneda AS IdTaxa, b.Txt_Moneda AS Moneda, a.dbl_Valor AS Valor, a.Fec_Dia AS Dia, a.Lng_IdTaxa, b.Int_IdMoneda FROM dbo.Tb_Taxas AS a INNER JOIN dbo.Ct_Moneda AS b ON a.Int_IdMoneda = b.Int_IdMoneda inner join dbo.Tb_TaxSuc as c on a.Lng_IdTaxa = c.Lng_IdTaxSuc where(a.Int_IdMoneda IN (1, 2, 3, 4, 5, 6)) and a.Lng_IdTaxa in (SELECT TOP (6) Lng_IdTaxa from Tb_Taxas as d inner join dbo.Tb_TaxSuc as c on d.Lng_IdTaxa = c.Lng_IdTaxSuc where Bol_Tipo = 0 and c.Lng_IdSucursal = " + sucursalid + " order by 1 desc) and a.Bol_Tipo = 0 order by 1", con);
            DataTable tabla = new DataTable();
            compras.Fill(tabla);
            this.Tb_taxaCompras.DataSource = (tabla);
            Tb_taxaCompras.DataBind();


            SqlDataAdapter ventas = new SqlDataAdapter("SELECT DISTINCT a.Int_IdMoneda AS IdTaxa, b.Txt_Moneda AS Moneda, a.dbl_Valor AS Valor, a.Fec_Dia AS Dia, a.Lng_IdTaxa, b.Int_IdMoneda FROM dbo.Tb_Taxas AS a INNER JOIN dbo.Ct_Moneda AS b ON a.Int_IdMoneda = b.Int_IdMoneda inner join dbo.Tb_TaxSuc as c on a.Lng_IdTaxa = c.Lng_IdTaxSuc where(a.Int_IdMoneda IN (1, 2, 3, 4, 5, 6)) and a.Lng_IdTaxa in (SELECT TOP (6) Lng_IdTaxa from Tb_Taxas as d inner join dbo.Tb_TaxSuc as c on d.Lng_IdTaxa = c.Lng_IdTaxSuc where Bol_Tipo = 1 and c.Lng_IdSucursal = " + sucursalid + " order by 1 desc) and a.Bol_Tipo = 1 order by 1", con);
            DataTable tablav = new DataTable();
            ventas.Fill(tablav);
            this.Tb_taxaVentas.DataSource = (tablav);
            Tb_taxaVentas.DataBind();

        }

        private void obtndivisa()
        {
            int sucursalid = Convert.ToInt32(sucursalselect.SelectedValue);
            SqlDataAdapter compras = new SqlDataAdapter("SELECT DISTINCT a.Int_IdMoneda AS IdTaxa, b.Txt_Moneda AS Moneda, a.dbl_Valor AS Valor, a.Fec_Dia AS Dia, a.Lng_IdTaxa, b.Int_IdMoneda FROM dbo.Tb_Taxas AS a INNER JOIN dbo.Ct_Moneda AS b ON a.Int_IdMoneda = b.Int_IdMoneda inner join dbo.Tb_TaxSuc as c on a.Lng_IdTaxa = c.Lng_IdTaxSuc where(a.Int_IdMoneda IN (1, 2, 3, 4, 5, 6)) and a.Lng_IdTaxa in (SELECT TOP (6) Lng_IdTaxa from Tb_Taxas as d inner join dbo.Tb_TaxSuc as c on d.Lng_IdTaxa = c.Lng_IdTaxSuc where Bol_Tipo = 0 and c.Lng_IdSucursal = "+ sucursalid + " order by 1 desc) and a.Bol_Tipo = 0 order by 1", con);
            DataTable tabla = new DataTable();
            compras.Fill(tabla);
            this.Tb_taxaCompras.DataSource = (tabla);
            Tb_taxaCompras.DataBind();

     
            SqlDataAdapter ventas = new SqlDataAdapter("SELECT DISTINCT a.Int_IdMoneda AS IdTaxa, b.Txt_Moneda AS Moneda, a.dbl_Valor AS Valor, a.Fec_Dia AS Dia, a.Lng_IdTaxa, b.Int_IdMoneda FROM dbo.Tb_Taxas AS a INNER JOIN dbo.Ct_Moneda AS b ON a.Int_IdMoneda = b.Int_IdMoneda inner join dbo.Tb_TaxSuc as c on a.Lng_IdTaxa = c.Lng_IdTaxSuc where(a.Int_IdMoneda IN (1, 2, 3, 4, 5, 6)) and a.Lng_IdTaxa in (SELECT TOP (6) Lng_IdTaxa from Tb_Taxas as d inner join dbo.Tb_TaxSuc as c on d.Lng_IdTaxa = c.Lng_IdTaxSuc where Bol_Tipo = 1 and c.Lng_IdSucursal = " + sucursalid + " order by 1 desc) and a.Bol_Tipo = 1 order by 1", con);
            DataTable tablav = new DataTable();
            ventas.Fill(tablav);
            this.Tb_taxaVentas.DataSource = (tablav);
            Tb_taxaVentas.DataBind();

        }


        public void copiardivisas()
        {
            DateTime fecha = Convert.ToDateTime(Session["fecha"]);

            DateTime f1 = fecha.AddDays(-1);

            SqlConnection conectorbd = new SqlConnection(con);
            SqlCommand inser1 = new SqlCommand("insert into Tb_TaxSuc (Lng_IdSucursal, Int_IdGrupo) select a.Lng_IdSucursal, a.Int_IdGrupo from Tb_TaxSuc as a where a.Lng_IdTaxSuc >= (select min(Lng_IdTaxa) from Tb_Taxas where Fec_Dia > '"+f1+"')", conectorbd);
            conectorbd.Open();
            inser1.ExecuteNonQuery();
            conectorbd.Close();

            SqlConnection conectorct = new SqlConnection(con);
            SqlCommand inser = new SqlCommand("insert into Tb_Taxas (Int_IdMoneda, dbl_Valor, Bol_Tipo, Fec_Dia, Fec_Vencimiento, Int_IdGrupo) select a.Int_IdMoneda, a.dbl_Valor, a.Bol_Tipo, GETDATE(), a.Fec_Vencimiento, a.Int_IdGrupo from Tb_Taxas as a where a.Fec_Dia > '"+f1+"'", conectorbd);
            conectorct.Open();
            inser.ExecuteNonQuery();
            conectorct.Close();


        }


        protected void sucursalselect_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtndivisa();
        }
    }
}