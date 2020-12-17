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

        public JsonResult compracc(DateTime fecha)
        {
            int dias = 0;

            if (fecha != null )
            {
               
                DateTime hoy = DateTime.Now;
                TimeSpan kf = hoy - fecha;
                 dias = (int)kf.TotalDays;
            
            }

            string JSONresult;

            SqlDataAdapter da = new SqlDataAdapter("select * from Func_CompraCCSucursales(" + dias+")", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            JSONresult = JsonConvert.SerializeObject(dt);


            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ventascc(DateTime fecha)
        {
            int dias = 0;

            if (fecha != null)
            {
               
                DateTime hoy = DateTime.Now;
                TimeSpan kf = hoy - fecha;
                dias = (int)kf.TotalDays;

            }

            string JSONresult;

            SqlDataAdapter da = new SqlDataAdapter("select * from Func_VentaCCSucursales(" + dias+")", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            JSONresult = JsonConvert.SerializeObject(dt);


            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }




        public JsonResult cajeros(DateTime fecha)
        {
            int dias = 0;

            if (fecha != null)
            {
            
                DateTime hoy = DateTime.Now;
                TimeSpan kf = hoy - fecha;
                dias = (int)kf.TotalDays;

            }

            string JSONresult;

            SqlDataAdapter da = new SqlDataAdapter("select * from Func_ArqueoCajeros("+dias+")", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            JSONresult = JsonConvert.SerializeObject(dt);


            return Json(JSONresult, JsonRequestBehavior.AllowGet);
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

            SqlDataAdapter da = new SqlDataAdapter("select * from Func_ArqueoTesoreria("+dias+")", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            JSONresult = JsonConvert.SerializeObject(dt);


            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }

        



        public JsonResult mail()
        {
            string msn = "hola perro";

          
            SqlDataAdapter da = new SqlDataAdapter("select * from Func_CompraCCSucursales(1)", con);
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);


            SqlDataAdapter db = new SqlDataAdapter("select * from Func_VentaCCSucursales(1)", con);
            DataTable dc = new DataTable();
            dc.Locale = System.Globalization.CultureInfo.InvariantCulture;
            db.Fill(dc);

            SqlDataAdapter ac = new SqlDataAdapter("SELECT Sucursal , Monto, Fecha, Moneda FROM [dbo].[Func_DotacionSuc](1) ", con);
            DataTable dg = new DataTable();
            dg.Locale = System.Globalization.CultureInfo.InvariantCulture;
            ac.Fill(dg);

            SqlDataAdapter ad = new SqlDataAdapter("SELECT Sucursal, SaldoEntrada, Fecha, Moneda FROM [dbo].[Func_EntradasSuc](1) ", con);
            DataTable df = new DataTable();
            df.Locale = System.Globalization.CultureInfo.InvariantCulture;
            ad.Fill(df);


            string cabeceracompras = "<tr> <th>Sucursal</th> <th>TC</th> <th>Valor</th> <th>Pesos</th> <th>Moneda</th></tr>";
            string tabla = "<h2>Compras</h2> <table Border>";


            string cabeceraventas = "<tr> <th>Sucursal</th> <th>TC</th><th>Valor</th> <th>Pesos</th> <th>Moneda</th></tr>";
            string tabla2 = "<h2>Ventas</h2> <table Border>";


            string cabeceraarqueo = "<tr><th>Sucursal</th> <th>Monto</th> <th>Fecha</th> <th>Moneda</th> </tr>";
            string tabla3 = "<h2>Ingresos</h2> <table Border>";


            string cabeceratesoreria = "<tr><th>Sucursal</th> <th>Saldo de Entrada</th> <th>Fecha</th> <th>Moneda</th></tr>";
            string tabla4 = "<h2>Saldos Iniciales</h2> <table Border>";


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
                    if (b == 1 || b == 5)
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
                    if (d == 1 || d == 3 || d == 4)
                    {
                        sk.Append("<td ROWSPAN=1>");
                        sk.Append(dr[ct.ColumnName].ToString());
                        sk.Append("</td>");
                    }
                    else if (d == 2)
                    {
                        sk.Append("<td style='text-align: right' ROWSPAN=1>");
                        sk.Append(dr[ct.ColumnName].ToString());
                        sk.Append("</td>");
                    }

                    d = d + 1; 
                }
                sk.Append("</tr>");
                d = 1;
            }
            sk.Append("</table>");
            string Tesoreria = sk.ToString();

            string fecha = DateTime.Now.AddDays(-1).ToString("ddd dd MMMM yyyy");

            //, omartinez@cecgroup.mx ,mserrano@cecgroup.mx,  jecortes@cecgroup.mx, kledesma@strategias.mx

            try
            {
                string mailemisor = "soporteti@masterexchange.com.mx";
                string mailreceptor = "mserrano@cecgroup.mx, kledesma@strategias.mx, jecortes@cecgroup.mx, janavarrete@strategias.mx";
                string mailoculto = "asantos@strategias.mx, omartinez@cecgroup.mx";
                string contraseña = "masterQ2021";

                MailMessage msng = new MailMessage(mailemisor, mailreceptor,"Reportes "+fecha+" ", " "+ compras + " <br> "+Ventas+" <br> "+arqueo+" <br> "+Tesoreria+"");
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

                return Json(error, JsonRequestBehavior.AllowGet);
            }
        
          
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
