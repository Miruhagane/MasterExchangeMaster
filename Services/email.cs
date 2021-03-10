using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;


namespace WebApplication2.Services
{
    public class email : IHostedService
    {
        string con = System.Configuration.ConfigurationManager.ConnectionStrings["server2"].ConnectionString;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            mailscript();
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            mailscript();
            throw new NotImplementedException();
        }

        public void mailscript()
        {
            string msn = "hola perro";

            SqlDataAdapter dx = new SqlDataAdapter("select Valor, Moneda,Fecha from Func_MonedasTotales(1) order by 2", con);
            DataTable dv = new DataTable();
            dv.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dx.Fill(dv);


            SqlDataAdapter da = new SqlDataAdapter("select * from Func_CompraCCSucursales(1)", con);
            DataTable dt = new DataTable();
            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
            da.Fill(dt);


            SqlDataAdapter db = new SqlDataAdapter("select * from Func_VentaCCSucursales(1)", con);
            DataTable dc = new DataTable();
            dc.Locale = System.Globalization.CultureInfo.InvariantCulture;
            db.Fill(dc);

            SqlDataAdapter ac = new SqlDataAdapter("select Sucursal, monto, Fecha, Moneda from Func_DotacionSucCon(1) order by Sucursal", con);
            DataTable dg = new DataTable();
            dg.Locale = System.Globalization.CultureInfo.InvariantCulture;
            ac.Fill(dg);

            SqlDataAdapter ad = new SqlDataAdapter("SELECT Sucursal, SaldoEntrada, Fecha, Moneda, Turno FROM Func_EntradasSucCon(1) order by Sucursal", con);
            DataTable df = new DataTable();
            df.Locale = System.Globalization.CultureInfo.InvariantCulture;
            ad.Fill(df);

            SqlDataAdapter ae = new SqlDataAdapter("select Sucursal, monto, Fecha, Moneda ,Turno from Func_ArqueoSucCon (1) order by Sucursal", con);
            DataTable dk = new DataTable();
            dk.Locale = System.Globalization.CultureInfo.InvariantCulture;
            ae.Fill(dk);

            SqlDataAdapter ab = new SqlDataAdapter("select Moneda, Monto, MontoSuc, Usuario, Fecha, Sucursal from  Func_ArqueoTesoreria(1)", con);
            DataTable dn = new DataTable();
            dn.Locale = System.Globalization.CultureInfo.InvariantCulture;
            ab.Fill(dn);

            string arqueotesoreria = "<tr style='background - color: darkgray;'> <th>Moneda</th> <th>Monto</th> <th>Monto Sucursal</th> <th>Cajero</th> <th>Fecha</th> <th>Sucursal</th></tr>";
            string tesoreria = "<h2>Arqueos de Tesoreria</h2> <table Border>";


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
                            DateTime fc = Convert.ToDateTime(dm[ct.ColumnName]);
                            pt5 = fc.ToString("dd/MM/yyyy");


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

            string fecha = DateTime.Now.AddDays(-1).ToString("ddd dd MMMM yyyy");

            //omartinez@cecgroup.mx, mserrano@cecgroup.mx, kledesma@strategias.mx, jecortes@cecgroup.mx, jnavarrete@strategias.mx , fledesma@cecgroup.mx, jcanul@masterexchange.com.mx 

            try
            {
                string mailemisor = "soporteti@masterexchange.com.mx";
                string mailreceptor = "asantos@strategias.mx";
                string mailoculto = "asantos@strategias.mx";
                string contraseña = "masterQ2021";

                MailMessage msng = new MailMessage(mailemisor, mailreceptor, "Reportes Master Exchange " + fecha + " ", " " + divisas + " <br> , " + compras + " <br> " + Ventas + " <br> " + arqueo + " <br> " + Tesoreria + " <br> " + finales + " ");
                msng.Bcc.Add(mailoculto);
                msng.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(mailemisor, contraseña);

                smtpClient.Send(msng);
                smtpClient.Dispose();



                string mailemisor2 = "soporteti@masterexchange.com.mx";
                string mailreceptor2 = "asantos@strategias.mx";
                string mailoculto2 = "asantos@strategias.mx";
                string contraseña2 = "masterQ2021";

                MailMessage msng2 = new MailMessage(mailemisor2, mailreceptor2, "Reportes Tesoreria " + fecha + " ", " " + rttesoreria + " ");
                msng2.Bcc.Add(mailoculto2);
                msng2.IsBodyHtml = true;

                SmtpClient smtpClient2 = new SmtpClient("smtp.gmail.com");
                smtpClient2.EnableSsl = true;
                smtpClient2.UseDefaultCredentials = false;
                smtpClient2.Port = 587;
                smtpClient2.Credentials = new System.Net.NetworkCredential(mailemisor2, contraseña2);

                smtpClient2.Send(msng2);
                smtpClient2.Dispose();

            }
            catch (Exception error)
            {
                string merr = Convert.ToString(error);
               
            }

        }
    }
}