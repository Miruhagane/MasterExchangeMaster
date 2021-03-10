using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Text;
using System.Globalization;
using System.IO;
using Aspose.Cells;
using Aspose.Cells.Utility;
using ClosedXML.Excel;

namespace WebApplication2.Controllers
{
    public class ReportesController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();
        string con = System.Configuration.ConfigurationManager.ConnectionStrings["server2"].ConnectionString;

        // GET: Reportes

        public ActionResult reportes(int id)
        {
            ViewBag.tipo = id;
            return View();
        }


        public ActionResult Ponderados()
        {

            return View();
        }


        [HttpPost]
        public FileResult excels(DateTime f1, DateTime f2)
        {
            string titulo1 = "Compra/venta" + DateTime.Now;

            f2 = f2.AddHours(23);

            string fec1 = f1.ToString("yyyy-MM-dd HH:mm:ss");
            string fec2 = f2.ToString("yyyy-MM-dd HH:mm:ss");

            SqlDataAdapter da = new SqlDataAdapter("select a.Int_IdTipoTran, f.Txt_TipoTran as Tipo, b.Txt_Moneda as Moneda, a.Dbl_MontoRecibir as 'Se Recibio', a.Dbl_TipoCambio as 'Tipo de Cambio(Compra)', a.Dbl_TipoCambioven as 'Tipo de Cambio(Venta)', a.Dbl_MontoPagar as 'Se Pago', a.Dbl_Entregar as 'Pago Real', a.Dbl_Cambio as 'Cambio en Pesos', c.Txt_Sucursal as 'Sucursal', d.Txt_Nombre as 'Nombre', d.Txt_Apellido as 'Apellidos', a.Fec_Fecha as 'Fecha' from Tb_Registros as a, Ct_Moneda as b, Tb_Sucursal as c, Tb_Usuarios as d, Tb_RegUsu as e, Ct_TipoTran as f where a.Int_IdTipoTran = f.Int_IdTipoTran and  a.Int_IdMoneda = b.Int_IdMoneda and  a.Fec_Fecha between '" + fec1 + "' and '" + fec2 + "' and a.Bol_Cancelar = 0 and a.Lng_IdSucursal = c.Lng_IdSucursal and a.Lng_IdRegistro = e.Lng_IdRegistro and e.Int_IdUsuario = d.Int_Idusuario   or a.Int_IdTipoTran = f.Int_IdTipoTran and a.Int_IdMonVenta = b.Int_IdMoneda and a.Fec_Fecha between '" + fec1 + "' and '" + fec2 + "' and a.Bol_Cancelar = 0 and a.Int_IdTipoTran = 2 and a.Lng_IdSucursal = c.Lng_IdSucursal and a.Lng_IdRegistro = e.Lng_IdRegistro and e.Int_IdUsuario = d.Int_Idusuario", con);
            DataTable dt = new DataTable();
            da.Fill(dt);



            DataTable excel = new DataTable("compra_venta");
            excel.Columns.Add("Tipo", typeof(string));
            excel.Columns.Add("Moneda", typeof(string));
            excel.Columns.Add("Recibido", typeof(decimal));
            excel.Columns.Add("T/C", typeof(decimal));
            excel.Columns.Add("Entregado", typeof(decimal));
            excel.Columns.Add("pago_real", typeof(decimal));
            excel.Columns.Add("Cambio", typeof(decimal));
            excel.Columns.Add("Sucursal", typeof(string));
            excel.Columns.Add("Cajero", typeof(string));
            excel.Columns.Add("Fecha", typeof(DateTime));

            int compra = 1;
            int venta = 2;

            int ciclo = 1;
            int tipo = 0;


            string t = "";
            string moneda = "";
            decimal recibido = 0;
            decimal tc = 0;
            decimal entregado = 0;
            decimal real = 0;
            decimal cambio = 0;
            string sucursal = "";
            string namea = "";
            string nameb = "";
            DateTime fecha = DateTime.Now;

     

            foreach (DataRow lineas in dt.Rows)
            {

                ciclo = 1;

                foreach (DataColumn columnas in dt.Columns)
                {
                    if (ciclo == 1)
                    {
                        tipo = Convert.ToInt32(lineas[columnas.ColumnName]);

                    }

                    else
                    {

                        if (tipo == compra)
                        {
                            if (ciclo == 2)
                            {
                                t = Convert.ToString(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 3)
                            {
                                moneda = Convert.ToString(lineas[columnas.ColumnName]);

                            }
                            else if (ciclo == 4)
                            {
                                recibido = Convert.ToDecimal(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 5)
                            {
                                tc = Convert.ToDecimal(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 6)
                            {
                               
                            }
                            else if (ciclo == 7)
                            {
                                entregado = Convert.ToDecimal(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 8)
                            {
                                real = 0;
                            }
                            else if (ciclo == 9)
                            {
                                cambio = 0;
                            }
                            else if (ciclo == 10)
                            {
                                sucursal =  Convert.ToString(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 11)
                            {
                                namea = Convert.ToString(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 12)
                            {
                                nameb = Convert.ToString(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 13)
                            {
                                fecha = Convert.ToDateTime(lineas[columnas.ColumnName]);
                            }
                           
                        }

                        else if (tipo == venta)
                        {
                            if (ciclo == 2)
                            {
                                t = Convert.ToString(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 3)
                            {
                                moneda = Convert.ToString(lineas[columnas.ColumnName]);

                            }
                            else if (ciclo == 4)
                            {
                                entregado = Convert.ToDecimal(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 5)
                            {
                                
                            }
                            else if (ciclo == 6)
                            {
                                tc = Convert.ToDecimal(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 7)
                            {
                                recibido = Convert.ToDecimal(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 8)
                            {
                                real = 0;
                            }
                            else if (ciclo == 9)
                            {
                                cambio = Convert.ToDecimal(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 10)
                            {
                                sucursal = Convert.ToString(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 11)
                            {
                                namea = Convert.ToString(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 12)
                            {
                                nameb = Convert.ToString(lineas[columnas.ColumnName]);
                            }
                            else if (ciclo == 13)
                            {
                                fecha = Convert.ToDateTime(lineas[columnas.ColumnName]);
                            }
                        }


                    }


                    ciclo = ciclo + 1;
                }

                excel.Rows.Add(t, moneda, recibido, tc, entregado, real, cambio, sucursal, namea + " " + nameb , fecha);

            }



          


                        
            using (XLWorkbook xL = new XLWorkbook())
            {
                xL.Worksheets.Add(excel);
                using (MemoryStream data = new MemoryStream())
                {
                    xL.SaveAs(data);
                    return File(data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetms.sheet", "" + titulo1 + ".xlsx");
                }
            }

        }


        public FileResult compracc(DateTime fecha)
        {
            int dias = 0;
            string titulo1 = "Compras" + DateTime.Now;
            if (fecha != null)
            {

                DateTime hoy = DateTime.Now;
                TimeSpan kf = hoy - fecha;
                dias = (int)kf.TotalDays;

            }

            string JSONresult;

            SqlDataAdapter da = new SqlDataAdapter("select * from Func_CompraCCSucursales(" + dias + ")", con);
            DataTable dt = new DataTable("Compras");
            da.Fill(dt);
            JSONresult = JsonConvert.SerializeObject(dt);

            using (XLWorkbook xL = new XLWorkbook())
            {
                xL.Worksheets.Add(dt);
                using (MemoryStream data = new MemoryStream())
                {
                    xL.SaveAs(data);
                    return File(data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetms.sheet", "" + titulo1 + ".xlsx");
                }
            }

         
        }

        public FileResult ventascc(DateTime fecha)
        {
            int dias = 0;
            string titulo1 = "ventas" + DateTime.Now;
            if (fecha != null)
            {

                DateTime hoy = DateTime.Now;
                TimeSpan kf = hoy - fecha;
                dias = (int)kf.TotalDays;

            }


            string JSONresult;

            SqlDataAdapter da = new SqlDataAdapter("select * from Func_VentaCCSucursales(" + dias + ")", con);
            DataTable dt = new DataTable("Ventas");
            da.Fill(dt);
            JSONresult = JsonConvert.SerializeObject(dt);

            using (XLWorkbook xL = new XLWorkbook())
            {
                xL.Worksheets.Add(dt);
                using (MemoryStream data = new MemoryStream())
                {
                    xL.SaveAs(data);
                    return File(data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetms.sheet", "" + titulo1 + ".xlsx");
                }
            }


        }



        public FileResult cajeros(DateTime fecha)
        {
            int dias = 0;
            string titulo1 = "ventas" + DateTime.Now;
            if (fecha != null)
            {

                DateTime hoy = DateTime.Now;
                TimeSpan kf = hoy - fecha;
                dias = (int)kf.TotalDays;

            }

            string JSONresult;

            SqlDataAdapter da = new SqlDataAdapter("select * from Func_ArqueoCajeros(" + dias + ")", con);
            DataTable dt = new DataTable("Ponderado Compras");
            da.Fill(dt);
            JSONresult = JsonConvert.SerializeObject(dt);

            using (XLWorkbook xL = new XLWorkbook())
            {
                xL.Worksheets.Add(dt);
                using (MemoryStream data = new MemoryStream())
                {
                    xL.SaveAs(data);
                    return File(data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetms.sheet", "" + titulo1 + ".xlsx");
                }
            }
        }

        public FileResult comprapon(DateTime fec1, DateTime fec2)
        {
            string titulo1 = "Ponderados Compras " + DateTime.Now;

            string fecha1 = fec1.ToString("yyyyMMdd");
            string fecha2 = fec2.ToString("yyyyMMdd");

            SqlDataAdapter da = new SqlDataAdapter("select sucursaL as Sucursal, NomCorto as 'Cajero', TC, Valor,txt_moneda as 'Moneda' ,PesosNew as 'Pesos', fecha   From Func_CompraCCPonderadoRep('"+fecha1+"', '"+fecha2+"') order by fecha", con);
            DataTable dt = new DataTable("Compras");
            da.Fill(dt);


            using (XLWorkbook xL = new XLWorkbook())
            {
                xL.Worksheets.Add(dt);
                using (MemoryStream data = new MemoryStream())
                {
                    xL.SaveAs(data);
                    return File(data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetms.sheet", "" + titulo1 + ".xlsx");
                }
            }

        }

        public FileResult ventapon(DateTime fec1, DateTime fec2)
        {
            string titulo1 = "Ponderados Ventas " + DateTime.Now;

            string fecha1 = fec1.ToString("yyyyMMdd");
            string fecha2 = fec2.ToString("yyyyMMdd");

            SqlDataAdapter da = new SqlDataAdapter("select sucursaL as Sucursal, NomCorto as 'Cajero', TC, Valor,txt_moneda as 'Moneda' , Pesos, fecha  from Func_VentaCCPonderadoRep('"+fecha1+"', '"+fecha2+"') order by fecha", con);
            DataTable dt = new DataTable("Ventas Ponderado");
            

            using (XLWorkbook xL = new XLWorkbook())
            {
                xL.Worksheets.Add(dt);
                using (MemoryStream data = new MemoryStream())
                {
                    xL.SaveAs(data);
                    return File(data.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetms.sheet", "" + titulo1 + ".xlsx");
                }
            }


        }

        public JsonResult tesoreria(DateTime fecha)
        {
            int dias = 0;

            if (fecha != null)
            {

                DateTime hoy = DateTime.Now;
                TimeSpan kf = hoy - fecha;
                dias = (int)kf.TotalDays;

            }

            string JSONresult;

            SqlDataAdapter da = new SqlDataAdapter("select * from Func_ArqueoTesoreria(" + dias + ")", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            JSONresult = JsonConvert.SerializeObject(dt);


            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult mail(DateTime dias)
        {
            int dia = 1;

            if (dias != null)
            {
                DateTime hoy = DateTime.Now;
                TimeSpan kf = hoy - dias;
                dia = (int)kf.TotalDays;
            }
           

            string msn = "1";

            SqlDataAdapter dx = new SqlDataAdapter("select Valor, Moneda,Fecha from Func_MonedasTotales("+dia+") order by 2", con);
            DataTable dv = new DataTable();
            dv.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dx.Fill(dv);


            SqlDataAdapter da = new SqlDataAdapter("select * from Func_CompraCCSucursales(" + dia + ")", con);
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);


            SqlDataAdapter db = new SqlDataAdapter("select * from Func_VentaCCSucursales(" + dia + ")", con);
            DataTable dc = new DataTable();
            dc.Locale = System.Globalization.CultureInfo.InvariantCulture;
            db.Fill(dc);

            SqlDataAdapter ac = new SqlDataAdapter("select Sucursal, monto, Fecha, Moneda from Func_DotacionSucCon(" + dia + ") order by Sucursal", con);
            DataTable dg = new DataTable();
            dg.Locale = System.Globalization.CultureInfo.InvariantCulture;
            ac.Fill(dg);

            SqlDataAdapter ad = new SqlDataAdapter("SELECT Sucursal, SaldoEntrada, Fecha, Moneda, Turno FROM Func_EntradasSucCon(" + dia + ") order by Sucursal", con);
            DataTable df = new DataTable();
            df.Locale = System.Globalization.CultureInfo.InvariantCulture;
            ad.Fill(df);

            SqlDataAdapter ae = new SqlDataAdapter("select Sucursal, monto, Fecha, Moneda ,Turno from Func_ArqueoSucCon (" + dia + ") order by Sucursal", con);
            DataTable dk = new DataTable();
            dk.Locale = System.Globalization.CultureInfo.InvariantCulture;
            ae.Fill(dk);


            string divisascabezeras = "<tr style='background - color: darkgray;'> <th>Valor</th> <th>Moneda</th> <th>Fecha</th></tr>";
            string ctdivisas = "<h2>Totales de Divisas</h2> <table Border>";

            string cabeceracompras = "<tr style='background - color: darkgray;'> <th>Sucursal</th> <th>TC</th> <th>Valor</th> <th>Pesos</th> <th>Moneda</th></tr>";
            string tabla = "<h2>Compras</h2> <table Border>";

            string cabeceraventas = "<tr style='background - color: darkgray;'> <th>Sucursal</th> <th>TC</th><th>Valor</th> <th>Pesos</th> <th>Moneda</th></tr>";
            string tabla2 = "<h2>Ventas</h2> <table Border>";

            string cabeceraarqueo = "<tr style='background - color: darkgray;'><th>Sucursal</th> <th>Monto</th> <th>Fecha</th> <th>Moneda</th> </tr>";
            string tabla3 = "<h2>Ingresos</h2> <table Border>";

            string cabeceratesoreria = "<tr style='background - color: darkgray;'><th>Sucursal</th> <th>Monto</th> <th>Fecha</th> <th>Moneda</th> <th>Turno</th> </tr>";
            string tabla4 = "<h2>Saldos iniciales</h2> <table Border>";

            string cabezerafinales = "<tr style='background - color: darkgray;'><th>Sucursal</th> <th>Monto</th> <th>Fecha</th> <th>Moneda</th> <th>Turno</th></tr>";
            string tabla5 = "<h2>Saldos finales</h2> <table Border>";


            StringBuilder cc = new StringBuilder();
            cc.Append(ctdivisas);
            cc.Append(divisascabezeras);
            int m = 1;

            foreach (DataRow dr in dv.Rows)
            {

                cc.Append("<tr>");
                foreach (DataColumn ct in dv.Columns)
                {
                    if (m == 2 || m == 3)
                    {
                        cc.Append("<td ROWSPAN=1>");
                        cc.Append(dr[ct.ColumnName].ToString());
                        cc.Append("</td>");
                    }
                    else if (m == 1)
                    {
                        cc.Append("<td style='text-align: right' ROWSPAN=1>");
                        cc.Append(dr[ct.ColumnName].ToString());
                        cc.Append("</td>");
                    }

                    m = m + 1;
                }
                cc.Append("</tr>");
                m = 1;
            }

            cc.Append("</table>");
            string divisas = cc.ToString();




            StringBuilder sb = new StringBuilder();
            sb.Append(tabla);
            sb.Append(cabeceracompras);
            int a = 1;

            foreach (DataRow dr in dt.Rows)
            {

                sb.Append("<tr>");
                foreach (DataColumn ct in dt.Columns)
                {
                    if (a == 1 || a == 5)
                    {
                        sb.Append("<td ROWSPAN=1>");
                        sb.Append(dr[ct.ColumnName].ToString());
                        sb.Append("</td>");
                    }
                    else if (a == 2 || a == 3 || a == 4)
                    {
                        sb.Append("<td style='text-align: right' ROWSPAN=1>");
                        sb.Append(dr[ct.ColumnName].ToString());
                        sb.Append("</td>");
                    }

                    a = a + 1;
                }
                sb.Append("</tr>");
                a = 1;
            }

            sb.Append("</table>");
            string compras = sb.ToString();



            StringBuilder sa = new StringBuilder();

            int b = 1;
            sa.Append(tabla2);
            sa.Append(cabeceraventas);
            foreach (DataRow dr in dc.Rows)
            {



                sa.Append("<tr>");
                foreach (DataColumn ct in dc.Columns)
                {
                    if (b == 1 || b == 5 || b == 6)
                    {
                        sa.Append("<td ROWSPAN=1>");
                        sa.Append(dr[ct.ColumnName].ToString());
                        sa.Append("</td>");
                    }
                    else if (b == 2 || b == 3 || b == 4)
                    {
                        sa.Append("<td style='text-align: right' ROWSPAN=1>");
                        sa.Append(dr[ct.ColumnName].ToString());
                        sa.Append("</td>");
                    }

                    b = b + 1;

                }
                b = 1;
                sa.Append("</tr>");

            }

            sa.Append("</table>");
            string Ventas = sa.ToString();

            StringBuilder sn = new StringBuilder();
            sn.Append(tabla3);

            int c = 1;
            sn.Append(cabeceraarqueo);
            foreach (DataRow dr in dg.Rows)
            {
                sn.Append("<tr>");
                foreach (DataColumn ct in dg.Columns)
                {
                    if (c == 1 || c == 3 || c == 4)
                    {
                        sn.Append("<td ROWSPAN=1>");
                        sn.Append(dr[ct.ColumnName].ToString());
                        sn.Append("</td>");
                    }

                    else if (c == 2)
                    {

                        sn.Append("<td style='text-align: right' ROWSPAN=1>");
                        sn.Append(dr[ct.ColumnName].ToString());
                        sn.Append("</td>");
                    }
                    c = c + 1;
                }
                c = 1;
                sn.Append("</tr>");

            }
            sn.Append("</table>");
            string arqueo = sn.ToString();

            StringBuilder sk = new StringBuilder();

            int d = 1;
            sk.Append(tabla4);
            sk.Append(cabeceratesoreria);

            foreach (DataRow dr in df.Rows)
            {
                sk.Append("<tr>");
                foreach (DataColumn ct in df.Columns)
                {
                    if (d == 1 || d == 3 || d == 4 || d == 5)
                    {
                        sk.Append("<td ROWSPAN=1>");
                        sk.Append(dr[ct.ColumnName].ToString());
                        sk.Append("</td>");
                    }
                    else if (d == 2)
                    {
                        sk.Append("<td style='text-align: right' ROWSPAN=1>");
                        sk.Append(string.Format("{0:c}", dr[ct.ColumnName]));
                        sk.Append("</td>");
                    }

                    d = d + 1;
                }
                sk.Append("</tr>");
                d = 1;
            }
            sk.Append("</table>");
            string Tesoreria = sk.ToString();

            StringBuilder se = new StringBuilder();
            se.Append(tabla5);
            se.Append(cabezerafinales);
            int e = 1;

            foreach (DataRow dr in dk.Rows)
            {

                se.Append("<tr>");
                foreach (DataColumn ct in dk.Columns)
                {
                    if (e == 1 || e == 3 || e == 4 || e == 5)
                    {
                        se.Append("<td ROWSPAN=1>");
                        se.Append(dr[ct.ColumnName].ToString());
                        se.Append("</td>");
                    }
                    else if (e == 2)
                    {
                        se.Append("<td style='text-align: right' ROWSPAN=1>");
                        se.Append(dr[ct.ColumnName].ToString());
                        se.Append("</td>");
                    }

                    e = e + 1;
                }
                se.Append("</tr>");
                e = 1;
            }

            se.Append("</table>");
            string finales = se.ToString();




            string fecha = DateTime.Now.AddDays(-dia).ToString("ddd dd MMMM yyyy");

            //omartinez@cecgroup.mx, mserrano@cecgroup.mx, kledesma@strategias.mx, jecortes@cecgroup.mx, jnavarrete@strategias.mx , fledesma@cecgroup.mx, jcanul@masterexchange.com.mx 

            try
            {
                string mailemisor = "soporteti@masterexchange.com.mx";
                string mailreceptor = "asantos@strategias.mx, mserrano@cecgroup.mx, kledesma@strategias.mx, jecortes@cecgroup.mx, jnavarrete@strategias.mx , fledesma@cecgroup.mx, jcanul@masterexchange.com.mx, plauria@cecgroup.mx";
                string mailoculto = "asantos@strategias.mx, omartinez@cecgroup.mx";
                string contraseña = "masterQ2021";

                MailMessage msng = new MailMessage(mailemisor, mailreceptor, "Reporte General Master Exchange " + fecha + " ", " " + divisas + " <br> , " + compras + " <br> " + Ventas + " <br> " + arqueo + " <br> " + Tesoreria + " <br> " + finales + " ");
                msng.Bcc.Add(mailoculto);
                msng.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(mailemisor, contraseña);

                smtpClient.Send(msng);
                smtpClient.Dispose();

                return Json(msn, JsonRequestBehavior.AllowGet);
            }
            catch (Exception error)
            {
                string merr = "2";
                string er = Convert.ToString(error);

                return Json(merr, JsonRequestBehavior.AllowGet);
            }


        }

        public JsonResult mailtesoreria(DateTime dias)
        {
            int dia = 1;

            string msn = "";

            if (dias != null)
            {
                DateTime hoy = DateTime.Now;
                TimeSpan kf = hoy - dias;
                dia = (int)kf.TotalDays;
            }



            try
            {
             

                string fecha = DateTime.Now.AddDays(-dia).ToString("ddd dd MMMM yyyy");

                SqlDataAdapter ab = new SqlDataAdapter("select Moneda, Monto, MontoSuc, Usuario, Fecha, Sucursal from  Func_ArqueoTesoreria("+ dia + ")", con);
                DataTable dn = new DataTable();
                dn.Locale = System.Globalization.CultureInfo.InvariantCulture;
                ab.Fill(dn);

              string gh = dn.Rows.Count.ToString();



                if (gh != "0")
                {
                    string arqueotesoreria = "<tr style='background - color: darkgray;'> <th>Moneda</th> <th>Monto</th> <th>Monto Sucursal</th> <th>Cajero</th> <th>Fecha</th> <th>Sucursal</th></tr>";
                    string tesoreria = "<h2>Captacion de Divisas</h2> <table Border>";


                    StringBuilder op = new StringBuilder();
                    op.Append(tesoreria);
                    op.Append(arqueotesoreria);
                    int l = 1;

                    decimal m1 = 0;
                    decimal m2 = 0;

                    string pt1 = "";


                    string pt4 = "";
                    string pt5 = "";

                    foreach (DataRow dm in dn.Rows)
                    {
                        op.Append("<tr>");
                        foreach (DataColumn ct in dn.Columns)
                        {
                            if (l == 1)
                            {
                                pt1 = dm[ct.ColumnName].ToString();
                            }

                            else if (l == 2)
                            {
                                m1 = Convert.ToDecimal(dm[ct.ColumnName]);
                            }


                            else if (l == 3)
                            {
                                m2 = Convert.ToDecimal(dm[ct.ColumnName]);
                            }


                            else if (l == 4 || l == 5 || l == 6)
                            {

                                if (l == 4)
                                {
                                    pt4 = dm[ct.ColumnName].ToString();
                                }

                                else if (l == 5)
                                {
                                    pt5 = dm[ct.ColumnName].ToString();
                                }
                                else if (l == 6)
                                {

                                    if (m1 == 0 && m2 == 0)
                                    {

                                    }

                                    else if (m1 != m2)
                                    {
                                        op.Append("<td style='text-align: Center; background-color: red; color: white;' ROWSPAN=1>");
                                        op.Append(pt1);
                                        op.Append("</td>");

                                        op.Append("<td style='text-align: right; background-color: red; color: white;' ROWSPAN=1>");
                                        op.Append(string.Format(new CultureInfo("en-US"), "{0:c}", m1));
                                        op.Append("</td>");

                                        op.Append("<td style='text-align: right; background-color: red; color: white;' ROWSPAN=1>");
                                        op.Append(string.Format(new CultureInfo("en-US"), "{0:c}", m2));
                                        op.Append("</td>");

                                        op.Append("<td style='text-align: Center; background-color: red; color: white;' ROWSPAN=1>");
                                        op.Append(pt4);
                                        op.Append("</td>");

                                        op.Append("<td style='text-Center: Center; background-color: red; color: white;' ROWSPAN=1>");
                                        op.Append(pt5);
                                        op.Append("</td>");

                                        op.Append("<td style='text-Center: right; background-color: red; color: white;' ROWSPAN=1>");
                                        op.Append(dm[ct.ColumnName].ToString());
                                        op.Append("</td>");
                                    }

                                    else if (m1 == m2)
                                    {
                                        op.Append("<td style='text-align: Center; background-color: green; color: white;'  ROWSPAN=1>");
                                        op.Append(pt1);
                                        op.Append("</td>");

                                        op.Append("<td style='text-align: right; background-color: green; color: white;'  ROWSPAN=1>");
                                        op.Append(string.Format(new CultureInfo("en-US"), "{0:c}", m1));
                                        op.Append("</td>");

                                        op.Append("<td style='text-align: right; background-color: green; color: white;' ROWSPAN=1>");
                                        op.Append(string.Format(new CultureInfo("en-US"), "{0:c}", m2));
                                        op.Append("</td>");

                                        op.Append("<td style='text-align: Center; background-color: green; color: white;' ROWSPAN=1>");
                                        op.Append(pt4);
                                        op.Append("</td>");

                                        op.Append("<td style='text-align: Center; background-color: green; color: white;' ROWSPAN=1>");
                                        op.Append(pt5);
                                        op.Append("</td>");

                                        op.Append("<td style='text-Center: right; background-color: green; color: white;' ROWSPAN=1>");
                                        op.Append(dm[ct.ColumnName].ToString());
                                        op.Append("</td>");
                                    }



                                }


                            }

                            l = l + 1;
                        }
                        op.Append("</tr>");
                        l = 1;
                    }
                   

                    op.Append("</table>");
                    string rttesoreria = op.ToString();

                    string mailemisor2 = "soporteti@masterexchange.com.mx";
                    string mailreceptor2 = "kledesma@strategias.mx ,  jecortes@cecgroup.mx, jnavarrete@strategias.mx, fledesma@cecgroup.mx, plauria@cecgroup.mx";
                    string mailoculto2 = "asantos@strategias.mx, omartinez@cecgroup.mx";
                    string contraseña2 = "masterQ2021";

                    MailMessage msng2 = new MailMessage(mailemisor2, mailreceptor2, "Validacion de Divisas " + fecha + " ", " " + rttesoreria + " ");
                    msng2.Bcc.Add(mailoculto2);
                    msng2.IsBodyHtml = true;

                    SmtpClient smtpClient2 = new SmtpClient("smtp.gmail.com");
                    smtpClient2.EnableSsl = true;
                    smtpClient2.UseDefaultCredentials = false;
                    smtpClient2.Port = 587;
                    smtpClient2.Credentials = new System.Net.NetworkCredential(mailemisor2, contraseña2);

                    smtpClient2.Send(msng2);
                    smtpClient2.Dispose();

                    msn = "1";
                }
                else 
                {
                    msn = "2";
                }

               


                return Json(msn, JsonRequestBehavior.AllowGet);

            }
            catch (Exception error)
            {
                string merr = Convert.ToString(error);

                string exe = "500";
                return Json(exe, JsonRequestBehavior.AllowGet);

            }

        }

        public JsonResult especiales(DateTime dias)
        {
            string msn = "";
            int dia = 1;

            if (dias != null)
            {
                DateTime hoy = DateTime.Now;
                TimeSpan kf = hoy - dias;
                dia = (int)kf.TotalDays;
            }


            string fecha = DateTime.Now.AddDays(-dia).ToString("ddd dd MMMM yyyy");

            SqlDataAdapter ab = new SqlDataAdapter("select Sucursal, TC,  Valor , Txt_Moneda , Pesos, cliente, Nombre, fecha from Func_CompraCCDiaESP (" + dia+")", con);
                DataTable dn = new DataTable();
                dn.Locale = System.Globalization.CultureInfo.InvariantCulture;
                ab.Fill(dn);

                string divisascabezeras = "<tr style='background - color: darkgray;'> <th>Sucursal</th> <th>T/C</th> <th>Valor</th> <th>Moneda</th> <th>V.Pesos</th> <th>Cliente</th> <th>Usuario</th> <th>Fecha</th></tr>";
                string ctdivisas = "<h2>Compras a Mayoreo</h2> <table Border>";

                StringBuilder se = new StringBuilder();
                se.Append(ctdivisas);
                se.Append(divisascabezeras);
                int e = 1;

                foreach (DataRow dr in dn.Rows)
                {

                    se.Append("<tr>");
                    foreach (DataColumn ct in dn.Columns)
                    {
                    if (e == 1 || e == 4 || e == 6 || e == 7 )
                    {
                        se.Append("<td ROWSPAN=1>");
                        se.Append(dr[ct.ColumnName].ToString());
                        se.Append("</td>");
                    }
                    else if (e == 2 || e == 3 || e== 5)
                    {
                        se.Append("<td style='text-align: right' ROWSPAN=1>");
                        se.Append(string.Format("{0:c}", dr[ct.ColumnName]));
                        se.Append("</td>");
                    }
                    else if (e == 8)
                    {

                        DateTime f1 = Convert.ToDateTime(dr[ct.ColumnName]);
                        string fd = f1.ToString("dd/mm/yyyy");

                        se.Append("<td ROWSPAN=1>");
                        se.Append(fd);
                        se.Append("</td>");
                    }

                        e = e + 1;
                    }
                    se.Append("</tr>");
                    e = 1;
                }

                se.Append("</table>");
                string listcomesp = se.ToString();


            SqlDataAdapter ac = new SqlDataAdapter("select Sucursal, TC,  Valor,  Txt_Moneda,  Pesos, cliente, Nombre, fecha from Func_VentaCCDiaESP(" + dia + ")", con);
            DataTable dm = new DataTable();
            dm.Locale = System.Globalization.CultureInfo.InvariantCulture;
            ac.Fill(dm);


            string divisascabezerasesp = "<tr style='background - color: darkgray;'> <th>Sucursal</th> <th>T/C</th>  <th>Valor</th> <th>Moneda</th> <th>V.Pesos</th> <th>Cliente</th> <th>Usuario</th> <th>Fecha</th></tr>";
            string ctdivisasesp = "<h2>Ventas a Mayoreo</h2> <table Border>";

            StringBuilder sa = new StringBuilder();
            sa.Append(ctdivisasesp);
            sa.Append(divisascabezerasesp);
            int k = 1;

            foreach (DataRow dr in dm.Rows)
            {

                sa.Append("<tr>");
                foreach (DataColumn ct in dm.Columns)
                {
                    if (k == 1 || k == 4 || k == 6 || k == 7 )
                    {
                        sa.Append("<td ROWSPAN=1>");
                        sa.Append(dr[ct.ColumnName].ToString());
                        sa.Append("</td>");
                    }
                    else if (k == 2 || k == 3 || k == 5)
                    {
                        sa.Append("<td style='text-align: right' ROWSPAN=1>");
                        sa.Append(string.Format("{0:c}", dr[ct.ColumnName]));
                        sa.Append("</td>");
                    }

                    else if (k == 8)
                    {

                        DateTime f1 = Convert.ToDateTime(dr[ct.ColumnName]);
                        string fd = f1.ToString("dd/MM/yyyy");

                        sa.Append("<td ROWSPAN=1>");
                        sa.Append(fd);
                        sa.Append("</td>");
                    }

                    k = k + 1;
                }
                sa.Append("</tr>");
                k = 1;
            }

            sa.Append("</table>");
            string listvenesp = sa.ToString();

            string gh = dn.Rows.Count.ToString();
            string ga = dm.Rows.Count.ToString();


            try
            {

                if (gh != "0" && ga != "0")
                {
                    string mailemisor = "soporteti@masterexchange.com.mx";
                    string mailreceptor = " kledesma@strategias.mx ,jnavarrete@strategias.mx, fledesma@cecgroup.mx";
                    string mailoculto = "asantos@strategias.mx, omartinez@cecgroup.mx";
                    string contraseña = "masterQ2021";

                    MailMessage msng = new MailMessage(mailemisor, mailreceptor, "Reporte de Compras y Ventas por Mayoreo " + fecha + " ", "  <br> " + listcomesp + " <br> " + listvenesp + " ");
                    msng.Bcc.Add(mailoculto);
                    msng.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new System.Net.NetworkCredential(mailemisor, contraseña);

                    smtpClient.Send(msng);
                    smtpClient.Dispose();

                    msn = "1";

                }

                else if (gh != "0")
                {
                    string mailemisor = "soporteti@masterexchange.com.mx";
                    string mailreceptor = " kledesma @strategias.mx, jnavarrete@strategias.mx, fledesma @cecgroup.mx";
                    string mailoculto = "asantos@strategias.mx, omartinez@cecgroup.mx";
                    string contraseña = "masterQ2021";

                    MailMessage msng = new MailMessage(mailemisor, mailreceptor, "Reporte de Compras y Ventas por Mayoreo " + fecha + " ", " <br> " + listcomesp + " ");
                    msng.Bcc.Add(mailoculto);
                    msng.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new System.Net.NetworkCredential(mailemisor, contraseña);

                    smtpClient.Send(msng);
                    smtpClient.Dispose();

                    msn = "1";
                }

                else if (ga != "0")
                {
                    string mailemisor = "soporteti@masterexchange.com.mx";
                    string mailreceptor = "kledesma@strategias.mx, jnavarrete@strategias.mx, fledesma @cecgroup.mx";
                    string mailoculto = "asantos@strategias.mx, omartinez@cecgroup.mx";
                    string contraseña = "masterQ2021";

                    MailMessage msng = new MailMessage(mailemisor, mailreceptor, "Reporte de Compras y Ventas por Mayoreo " + fecha + " ", "  <br> " + listvenesp + " <br> ");
                    msng.Bcc.Add(mailoculto);
                    msng.IsBodyHtml = true;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new System.Net.NetworkCredential(mailemisor, contraseña);

                    smtpClient.Send(msng);
                    smtpClient.Dispose();

                    msn = "1";
                }
                else 
                {
                    msn = "2";
                }



                return Json(msn, JsonRequestBehavior.AllowGet);

            }

            catch(Exception err) 
            {
                string error500 = Convert.ToString(err);

                msn = "500";
                return Json(msn, JsonRequestBehavior.AllowGet);
            }

            

        }

        public JsonResult errores()
        {
            string divisascabezeras = "<tr style='background - color: darkgray;'> <th>Usuario</th> <th>Sucursal</th> <th>Fecha</th></tr>";
            string ctdivisas = "<h2>Usuarios sin Turnos Finalizados</h2> <table Border>";

            string msn = "";

            StringBuilder sa = new StringBuilder();

            sa.Append(ctdivisas);
            sa.Append(divisascabezeras);

            bool f = false;
            var numuser = db.Tb_Usuarios.Max(x => x.Int_Idusuario);

            for (int i = 1; i < numuser; i++)
            {

                var trueuser = db.Tb_Arqueo.FirstOrDefault(e => e.Int_IdUsuario == i && e.Bol_Congelar == f);

                if (trueuser != null)
                {

                    var user = db.Tb_Usuarios.FirstOrDefault(e => e.Int_Idusuario == i);

                    var suc = db.Tb_Sucursal.FirstOrDefault(e => e.Lng_IdSucursal == user.Lng_IdSucursal);

                    sa.Append("<tr>");

                     sa.Append("<td ROWSPAN=1>");
                     sa.Append(user.Txt_Nombre + " " + user.Txt_Apellido);
                     sa.Append("</td>"); 

                     sa.Append("<td ROWSPAN=1>");
                     sa.Append(suc.Txt_Sucursal);
                     sa.Append("</td>");

                    sa.Append("<td ROWSPAN=1>");
                    DateTime f1 = Convert.ToDateTime(trueuser.Fec_Cierre);
                    string fecha = f1.ToString("dd/MM/yyyy");
                    sa.Append(fecha);
                    sa.Append("</td>");

                    sa.Append("<tr>");
                }

            }

            sa.Append("</table>");
            string msnj = sa.ToString();

            string mailemisor = "soporteti@masterexchange.com.mx";
            string mailreceptor = "asantos@strategias.mx, omartinez@cecgroup.mx, kledesma@strategias.mx, jnavarrete@strategias.mx, plauria@cecgroup.mx";
         
            string contraseña = "masterQ2021";

            MailMessage msng = new MailMessage(mailemisor, mailreceptor, "Reporte Usuarios " + DateTime.Now + " ", "  <br> " + msnj + " <br> ");
           
            msng.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Port = 587;
            smtpClient.Credentials = new System.Net.NetworkCredential(mailemisor, contraseña);

            smtpClient.Send(msng);
            smtpClient.Dispose();

            msn = "1";

            return Json(msn, JsonRequestBehavior.AllowGet);
          
        }

       

     

        public ActionResult intervalos()
        {


            return View();
        }

      

        public ActionResult Mantenimiento()
        {

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
