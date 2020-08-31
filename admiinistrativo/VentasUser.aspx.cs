using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using System.Windows;
using System.Web.WebPages;
using System.IO;

using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Functions;
using Org.BouncyCastle.Bcpg.Sig;

namespace WebApplication2.admiinistrativo
{
    public partial class Gridventas1 : System.Web.UI.Page
    {
        string con = System.Configuration.ConfigurationManager.ConnectionStrings["server2"].ConnectionString;
        

        private void datoscajeros()
        {
            var user = 1;
            
            SqlDataAdapter da = new SqlDataAdapter("select a.Lng_IdRegistro, d.Txt_TipoTran as 'Tipo Trasaccion', convert(decimal(15, 2), a.Dbl_MontoRecibir, 2) as Compraste, c.Txt_Moneda as 'Moneda Comprada', convert(decimal(15, 2), a.Dbl_MontoPagar, 2) as Pagastes,(select e.Txt_Moneda from Ct_Moneda e where a.Int_IdMonVenta = e.Int_IdMoneda) 'Moneda Vendida', convert(decimal(15, 2), Dbl_Entregar, 2) as 'Entregaste', convert(decimal(15, 2), a.Dbl_Cambio, 2) as 'Cambio en Pesos',a.Fec_Fecha as Fecha from Tb_Registros a inner join Ct_Moneda c on a.Int_IdMoneda = c.Int_IdMoneda inner join Ct_TipoTran d on a.Int_IdTipoTran = d.Int_IdTipoTran inner join Tb_RegUsu b on a.Lng_IdRegistro = b.Lng_IdRegistro where b.Int_IdUsuario = " + user + " and Fec_Fecha between CONVERT(date, GETDATE()) and GETDATE() order by 1", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.GridView1.DataSource = (dt);
            GridView1.DataBind();

        }

 

        protected void Page_Load(object sender, EventArgs e)
        {

            Button1.Visible = false;

            string tipousuario = Convert.ToString(Session["intareadeusurio"]);

            TextBox3.Text = tipousuario;
        
            datoscajeros();
        }

        public void exportar()
        {
            var cajero = Cajero1.SelectedValue;
            string f1 = fechainicio.Text ;
            string f2 = fechafinal.Text ;

            SqlDataAdapter da = new SqlDataAdapter("select a.Lng_IdRegistro, d.Txt_TipoTran as 'Tipo Trasaccion', a.Dbl_MontoRecibir as Compraste,c.Txt_Moneda as 'Moneda Comprada', a.Dbl_MontoPagar as Pagastes,(select e.Txt_Moneda from Ct_Moneda e where a.Int_IdMonVenta= e.Int_IdMoneda) 'Moneda Vendida', Dbl_Entregar as 'Entregaste', a.Dbl_Cambio as 'Cambio en Pesos',a.Fec_Fecha as Fecha from Tb_Registros a inner join Ct_Moneda c on a.Int_IdMoneda = c.Int_IdMoneda inner join Ct_TipoTran d on a.Int_IdTipoTran = d.Int_IdTipoTran inner join Tb_RegUsu b on a.Lng_IdRegistro = b.Lng_IdRegistro where b.Int_IdUsuario = " + cajero + " and Fec_Fecha between '"+f1+ " 00:00:00.000' and '"+f2+" 23:59:59.000' order by 1", con);
            DataTable cr = new DataTable();
            da.Fill(cr);

            Stream s = exportecel(cr);
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("ventas" + DateTime.Today.ToShortDateString()) + ".xlsx"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-length", ms.ToArray().Length.ToString());
                Response.BinaryWrite(ms.ToArray());
                Response.Flush();
                ms.Close();
                ms.Dispose();


            }

        }



     
        protected void DateChange(object sender, EventArgs e)
        {
       
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            datoscajeros();
        }

        protected void Buscar_Click(object sender, EventArgs e)
        {

            var cajero = Cajero1.SelectedValue;
            string f1 = fechainicio.Text;
            string f2 = fechafinal.Text;

            SqlDataAdapter da = new SqlDataAdapter("select a.Lng_IdRegistro as ID, d.Txt_TipoTran as 'Tipo Trasaccion', convert(decimal(15,2), a.Dbl_MontoRecibir, 2) as Compraste, c.Txt_Moneda as 'Moneda Comprada', convert(decimal(15,2), a.Dbl_MontoPagar, 2) as Pagastes,(select e.Txt_Moneda from Ct_Moneda e where a.Int_IdMonVenta= e.Int_IdMoneda) 'Moneda Vendida', convert(decimal(15,2),Dbl_Entregar,2) as 'Entregaste', convert(decimal(15,2),a.Dbl_Cambio,2) as 'Cambio en Pesos',a.Fec_Fecha as Fecha from Tb_Registros a inner join Ct_Moneda c on a.Int_IdMoneda = c.Int_IdMoneda inner join Ct_TipoTran d on a.Int_IdTipoTran = d.Int_IdTipoTran inner join Tb_RegUsu b on a.Lng_IdRegistro = b.Lng_IdRegistro where b.Int_IdUsuario = " +cajero+ " and Fec_Fecha between '"+f1+" 00:00:00.000' and '" + f2+ " 23:59:59.000' order by 1", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            this.GridView2.DataSource = (dt);
            GridView2.DataBind();

            Button1.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            exportar();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var user = User.Identity.Name;
            
            SqlDataAdapter da = new SqlDataAdapter("select  a.Lng_IdRegistro as ID, d.Txt_TipoTran as 'Tipo Trasaccion', a.Dbl_MontoRecibir as Compraste,c.Txt_Moneda as 'Moneda Comprada', a.Dbl_MontoPagar as Pagastes,(select e.Txt_Moneda from Ct_Moneda e where a.Int_IdMonVenta= e.Int_IdMoneda) 'Moneda Vendida', Dbl_Entregar as 'Entregaste', a.Dbl_Cambio as 'Cambio en Pesos',a.Fec_Fecha as Fecha from Tb_Registros a inner join Ct_Moneda c on a.Int_IdMoneda = c.Int_IdMoneda inner join Ct_TipoTran d on a.Int_IdTipoTran = d.Int_IdTipoTran inner join Tb_RegUsu b on a.Lng_IdRegistro = b.Lng_IdRegistro where b.Int_IdUsuario = " + user + " and Fec_Fecha between CONVERT(date, GETDATE()) and GETDATE() order by 1", con);
            DataTable cr = new DataTable();
            da.Fill(cr);

            
            Stream s = exportecel(cr);
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("ventas" + DateTime.Today.ToShortDateString()) + ".xlsx"));
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-length", ms.ToArray().Length.ToString());
                Response.BinaryWrite(ms.ToArray());
                Response.Flush();
                ms.Close();
                ms.Dispose();


            }

        }

        public Stream exportecel(DataTable cr)
        {
            XSSFWorkbook libro = new XSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = libro.CreateSheet("Ventas");
            XSSFRow cabeceras = cabeceras = (XSSFRow)sheet.CreateRow(0);


            try

            {
                foreach (DataColumn columna in cr.Columns)
                    cabeceras.CreateCell(columna.Ordinal).SetCellValue(columna.ColumnName);
                int rowindex = 1;
                foreach (DataRow linea in cr.Rows)
                {
                    XSSFRow datarow = (XSSFRow)sheet.CreateRow(rowindex);
                    foreach (DataColumn column in cr.Columns)
                        datarow.CreateCell(column.Ordinal).SetCellValue(linea[column].ToString());
                    ++rowindex;
                }
                for (int i = 0; i <= cr.Columns.Count; i++)
                    sheet.AutoSizeColumn(i);
                libro.Write(ms);
                ms.Flush();

            }


            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                ms.Close();
                sheet = null;
                cabeceras = null;
                libro = null;
            }

            return ms;
        }
    }
}