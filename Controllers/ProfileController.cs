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
using WebApplication2.Models.ViewModels;
using System.Runtime.Remoting.Messaging;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Security;
using System.Web.UI;
using WebApplication2.Services;
using Microsoft.Ajax.Utilities;
using System.Web.ModelBinding;
using System.Security.Cryptography;
using System.Web.WebPages;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.AspNet.Identity;
using System.Data.Entity.SqlServer;
using System.Globalization;
using Microsoft.VisualBasic;
using System.Data.Entity.Infrastructure;
using System.Buffers.Text;
using System.IO;
using Microsoft.Win32;
using System.Windows.Documents;
using System.Collections;
using EntityState = System.Data.Entity.EntityState;

namespace WebApplication2.Controllers
{


    [Authorize]
    public class profileController : Controller
    {

        string con = System.Configuration.ConfigurationManager.ConnectionStrings["server2"].ConnectionString;

        public MasterExchangeEntities db = new MasterExchangeEntities();


        [HttpGet]
        public JsonResult valores2(Nullable<decimal> idmoneda)
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            List<Elementos1> comp = new List<Elementos1>();
            using (Models.MasterExchangeEntities dbm = new Models.MasterExchangeEntities())
            {
                comp = (from x in dbm.TaxaVentas
                        join db in dbm.Tb_TaxSuc on x.Lng_IdTaxa equals db.Lng_IdTaxSuc
                        where x.Int_IdMoneda == idmoneda && db.Lng_IdSucursal == sucursalid
                        orderby x.Int_IdMoneda
                        select new Elementos1
                        {
                            value = x.Int_IdMoneda,
                            Text = x.Valor
                        }
                     ).ToList();

            }

            return Json(comp, JsonRequestBehavior.AllowGet);
        }

        private class Elementos1
        {
            public Nullable<int> value { get; set; }
            public Nullable<decimal> Text { get; set; }
        }

        [HttpGet]
        public JsonResult valores(Nullable<decimal> idmoneda)
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            List<Elementos> lst = new List<Elementos>();
            using (Models.MasterExchangeEntities dk = new Models.MasterExchangeEntities())
            {
                lst = (from d in dk.TaxaCompras
                       join db in dk.Tb_TaxSuc on d.Lng_IdTaxa equals db.Lng_IdTaxSuc
                       where d.Int_IdMoneda == idmoneda && db.Lng_IdSucursal == sucursalid
                       orderby d.Int_IdMoneda
                       select new Elementos
                       {
                           value = d.Int_IdMoneda,
                           Text = d.Valor
                       }
                     ).ToList();

            }

            return Json(lst, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult valores_compra(Nullable<decimal> idmoneda)
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);

            List<Elementos> lst = new List<Elementos>();
            using (Models.MasterExchangeEntities dk = new Models.MasterExchangeEntities())
            {
                lst = (from d in dk.Taxacompcombs
                       join db in dk.Tb_TaxSuc on d.Lng_IdTaxa equals db.Lng_IdTaxSuc
                       where d.Int_IdMoneda == idmoneda && db.Lng_IdSucursal == sucursalid
                       orderby d.Int_IdMoneda
                       select new Elementos
                       {
                           value = d.Int_IdMoneda,
                           Text = d.Valor
                       }
                     ).ToList();

            }

            return Json(lst, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public JsonResult divisaventa(Nullable<decimal> idmoneda)
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            List<Elementos> lst = new List<Elementos>();
            using (Models.MasterExchangeEntities dk = new Models.MasterExchangeEntities())
            {
                lst = (from d in dk.TaxaVentas
                       join db in dk.Tb_TaxSuc on d.Lng_IdTaxa equals db.Lng_IdTaxSuc
                       where d.Int_IdMoneda == idmoneda && db.Lng_IdSucursal == sucursalid
                       orderby d.Int_IdMoneda
                       select new Elementos
                       {
                           value = d.Int_IdMoneda,
                           Text = d.Valor
                       }
                     ).ToList();

            }

            return Json(lst, JsonRequestBehavior.AllowGet);

        }


        //buscar Clientes
        [HttpGet]
        public JsonResult buscar(string numero)
        {
            List<listaclientes> ward;

            using (Models.MasterExchangeEntities db = new Models.MasterExchangeEntities())
            {
                ward = (from c in db.Tb_Clientes
                        where c.Txt_Identificacion.Contains(numero) || c.Txt_Email.Contains(numero) || c.Txt_Telefono.Contains(numero) || c.Txt_Cliente.Contains(numero)
                        select new listaclientes
                        {
                            Lng_IdCliente = c.Lng_IdCliente,
                            Txt_Cliente = c.Txt_Cliente,
                            Txt_Email = c.Txt_Email,
                            Txt_Direccion = c.Txt_Direccion,
                            Txt_Identificacion = c.Txt_Identificacion,
                            Txt_Telefono = c.Txt_Telefono
                        }).ToList();
            }

            return Json(ward, JsonRequestBehavior.AllowGet);
        }

        private class Elementos
        {
            public Nullable<int> value { get; set; }
            public Nullable<decimal> Text { get; set; }
        }

        public void modulodivisas()
        {

            List<moneda> lst = null;
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            using (Models.MasterExchangeEntities dr = new Models.MasterExchangeEntities())
            {
                lst = (
                     from d in dr.TaxaCompras
                     join db in dr.Tb_TaxSuc on d.Lng_IdTaxa equals db.Lng_IdTaxSuc
                     where db.Lng_IdSucursal == sucursalid
                     orderby d.Int_IdMoneda
                     select new moneda
                     {
                         Int_IdMoneda = d.Int_IdMoneda,
                         Moneda = d.Moneda

                     }).ToList();
            }


            List<SelectListItem> items = lst.ConvertAll(df =>
            {
                return new SelectListItem()
                {
                    Text = df.Moneda.ToString(),
                    Value = df.Int_IdMoneda.ToString(),
                    Selected = false

                };

            });

            ViewBag.items = items;

        }

        public void divisasCompra_venta()
        {

            List<moneda> lst = null;
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            using (Models.MasterExchangeEntities dr = new Models.MasterExchangeEntities())
            {
                lst = (
                     from d in dr.TaxaCompras
                     join db in dr.Tb_TaxSuc on d.Lng_IdTaxa equals db.Lng_IdTaxSuc
                     where db.Lng_IdSucursal == sucursalid
                     orderby d.Int_IdMoneda
                     select new moneda
                     {
                         Int_IdMoneda = d.Int_IdMoneda,
                         Moneda = d.Moneda

                     }).ToList();
            }


            List<SelectListItem> items = lst.ConvertAll(df =>
            {
                return new SelectListItem()
                {
                    Text = df.Moneda.ToString(),
                    Value = df.Int_IdMoneda.ToString(),
                    Selected = false

                };

            });

            ViewBag.items = items;

        }

        public void comboventa()
        {
            List<moneda> mls = null;
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);

            using (Models.MasterExchangeEntities dr = new Models.MasterExchangeEntities())
            {
                mls = (from d in dr.TaxaVentas
                       join db in dr.Tb_TaxSuc on d.Lng_IdTaxa equals db.Lng_IdTaxSuc
                       where db.Lng_IdSucursal == sucursalid
                       select new moneda
                       {
                           Int_IdMoneda = d.Int_IdMoneda,
                           Moneda = d.Moneda

                       }).ToList();
            }

            List<SelectListItem> venta = mls.ConvertAll(df =>
            {
                return new SelectListItem()
                {
                    Text = df.Moneda.ToString(),
                    Value = df.Int_IdMoneda.ToString(),

                };
            });

            ViewBag.venta = venta;

        }

        public JsonResult usdcompra()
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            List<Elementos> usd = new List<Elementos>();
            using (MasterExchangeEntities fg = new MasterExchangeEntities())
            {
                usd = (from dolar in fg.TaxaCompras
                       join db in fg.Tb_TaxSuc on dolar.Lng_IdTaxa equals db.Lng_IdTaxSuc
                       where dolar.IdTaxa == 1 && db.Lng_IdSucursal == sucursalid
                       select new Elementos
                       {
                           value = dolar.IdTaxa,
                           Text = dolar.Valor

                       }).ToList();

            }
            return Json(usd, JsonRequestBehavior.AllowGet);
        }

        public JsonResult usdventa()
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);

            List<Elementos> usd = new List<Elementos>();
            using (MasterExchangeEntities fg = new MasterExchangeEntities())
            {
                usd = (from dolar in fg.TaxaVentas
                       join db in fg.Tb_TaxSuc on dolar.Lng_IdTaxa equals db.Lng_IdTaxSuc
                       where dolar.IdTaxa == 1 && db.Lng_IdSucursal == sucursalid
                       select new Elementos
                       {
                           value = dolar.IdTaxa,
                           Text = dolar.Valor

                       }).ToList();

            }
            return Json(usd, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Tcomprar()
        {
            int sucursalid = Convert.ToInt32(Session["idsucursal"]);

            IEnumerable<Tcompras> lst1 = new List<Tcompras>();


            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                lst1 = (from d in dr.TaxaCompras
                        where d.Lng_IdSucursal == sucursalid
                        orderby d.Int_IdMoneda
                        select new Tcompras
                        {
                            Moneda = d.Moneda,
                            Valor = d.Valor

                        }).ToList();
            }





            return View(lst1);


        }



        public ActionResult Tventas()
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);

            IEnumerable<Tventas> lst1 = new List<Tventas>();


            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                lst1 = (from d in dr.TaxaVentas

                        where d.Lng_IdSucursal == sucursalid
                        orderby d.Int_IdMoneda
                        select new Tventas
                        {
                            Moneda = d.Moneda,
                            Valor = d.Valor
                        }).ToList();
            }

            return View(lst1);
        }

        public ActionResult Comprar()
        {
            modulodivisas();

            return PartialView();
        }


        public ActionResult Index(string idregistro)
        {

            ViewBag.id = idregistro;

            modulodivisas();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Lng_IdRegistro,Int_IdTipoTran,Int_IdMoneda,Dbl_MontoRecibir,Dbl_MontoPagar,Dbl_TipoCambio,Dbl_TipoCambioEsp,Bol_Especial,Dbl_Entregar,Dbl_Cambio,Int_IdTpv,Fec_Fecha,Lng_IdCliente, Lng_IdRegCli, Lng_IdSucursal")] Tb_Registros tb_Registros, Tb_RegCli Tb_RegCli, string idcliente)
        {


            if (ModelState.IsValid)
            {
                db.Tb_Registros.Add(tb_Registros);

                await db.SaveChangesAsync();
                Tb_RegCli.Lng_IdRegistro = tb_Registros.Lng_IdRegistro;
                int idregistro = tb_Registros.Lng_IdRegistro;
                int turno = Convert.ToInt32(Session["turno"]);


                SqlDataAdapter obja = new SqlDataAdapter("insert into Tb_RegCli(Lng_IdRegistro , Lng_IdCliente) values( " + Tb_RegCli.Lng_IdRegistro + " , " + idcliente + " )", con);
                DataSet a = new DataSet();
                obja.Fill(a);
                a.Reset();

                SqlDataAdapter otr = new SqlDataAdapter("insert into Tb_RegUsu(Int_IdUsuario , Lng_IdRegistro, Int_Turno) values( " + User.Identity.Name + " , " + Tb_RegCli.Lng_IdRegistro + ", "+ turno + " )", con);
                DataSet b = new DataSet();
                otr.Fill(b);
                b.Reset();

                SqlDataAdapter historial = new SqlDataAdapter("UPDATE [dbo].[Tb_Registros] SET [Bol_Cancelar] = 0 where Lng_IdRegistro = " + tb_Registros.Lng_IdRegistro + "", con);
                DataSet h = new DataSet();
                historial.Fill(h);
                h.Reset();

                string idreg = Convert.ToString(idregistro);

                return RedirectToAction("Index", new { idregistro = idreg });
            }

            modulodivisas();
            comboventa();

            return View();
        }

        public ActionResult Venta(string idregistro, string idventa)
        {
            divisasCompra_venta();
            comboventa();

            ViewBag.id = idregistro;
            ViewBag.idventa = idventa;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Venta([Bind(Include = "Lng_IdRegistro,Int_IdTipoTran,Int_IdMoneda,int_idmonventa,Dbl_MontoRecibir,Dbl_MontoPagar,Dbl_TipoCambio,Dbl_TipoCambioven,Dbl_TipoCambioEsp,Bol_Especial,Dbl_Entregar,Dbl_Cambio,Int_IdTpv,Fec_Fecha,Lng_IdCliente, Lng_IdRegCli, Bol_multimoneda, Lng_IdSucursal")] Tb_Registros tb_Registros, Tb_RegCli Tb_RegCli, string idcliente)
        {
            divisasCompra_venta();

            if (ModelState.IsValid)
            {
                int turno = Convert.ToInt32(Session["turno"]);

                db.Tb_Registros.Add(tb_Registros);

                await db.SaveChangesAsync();
                Tb_RegCli.Lng_IdRegistro = tb_Registros.Lng_IdRegistro;
                int idregistro = tb_Registros.Lng_IdRegistro;

                

                SqlDataAdapter obja = new SqlDataAdapter("insert into Tb_RegCli(Lng_IdRegistro , Lng_IdCliente) values( " + Tb_RegCli.Lng_IdRegistro + " , " + idcliente + " )", con);
                DataSet a = new DataSet();
                obja.Fill(a);
                a.Reset();


                SqlDataAdapter otr = new SqlDataAdapter("insert into Tb_RegUsu(Int_IdUsuario , Lng_IdRegistro , Int_Turno) values( " + User.Identity.Name + " , " + Tb_RegCli.Lng_IdRegistro + ", "+turno+" )", con);
                DataSet b = new DataSet();
                otr.Fill(b);
                b.Reset();

                SqlDataAdapter historial = new SqlDataAdapter("UPDATE [dbo].[Tb_Registros] SET [Bol_Cancelar] = 0 where Lng_IdRegistro = " + tb_Registros.Lng_IdRegistro + "", con);
                DataSet h = new DataSet();
                historial.Fill(h);
                h.Reset();

                string idreg = Convert.ToString(idregistro);
                string idventa = "0";

                return RedirectToAction("Venta", new { idregistro = idreg, idventa });


            }

            divisasCompra_venta();
            comboventa();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clientes(string nombre, string numcliente, string dirreccion, string identificacion, HttpPostedFileBase archivo, string nombrearchivo, string telefono, string mail)
        {
            using (Models.MasterExchangeEntities mir1 = new Models.MasterExchangeEntities())
            {


                var userv = mir1.Tb_Clientes.FirstOrDefault(e => e.Txt_Identificacion == identificacion);

                if (userv == null)
                {

                    if (archivo != null && archivo.ContentLength > 0)
                    {
                        byte[] temparchivo = new byte[archivo.ContentLength];
                        archivo.InputStream.Read(temparchivo, 0, archivo.ContentLength);

                        var ut = new Tb_Clientes()
                        {

                            Txt_Cliente = nombre,
                            Txt_NumCliente = numcliente,
                            Txt_Direccion = dirreccion,
                            Txt_Telefono = telefono,
                            Txt_Email = mail,
                            Txt_Identificacion = identificacion,
                            Fec_Alta = DateTime.Now,
                            File_Nombre = nombrearchivo,
                            Doc_cliente = temparchivo

                        };

                        mir1.Tb_Clientes.Add(ut);
                        mir1.SaveChanges();
                    }


                }
                else
                {
                    ViewBag.mensaje = "hola";

                }
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public JsonResult returnticket(int registro)
        {
            
            List<registrosall> ticket = new List<registrosall>();
            using (MasterExchangeEntities db = new MasterExchangeEntities())
            {
                ticket = (from c in db.Tb_Registros
                          join ca in db.Ct_Moneda on c.Int_IdMoneda equals ca.Int_IdMoneda
                          where c.Lng_IdRegistro == registro
                          select new registrosall() 
                          {
                              Lng_IdRegistro = c.Lng_IdRegistro,
                              Moneda = ca.Txt_Moneda,
                              Dbl_MontoRecibir = c.Dbl_MontoRecibir,
                              Dbl_TipoCambio = c.Dbl_TipoCambio,
                              Dbl_MontoPagar = c.Dbl_MontoPagar,
                              Fec_Fecha =  c.Fec_Fecha
                             

                          }).ToList();
                
            }
            
                

            return Json(ticket, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult returnticketventa(int alt)
        {
            

            List<registrosall> ticket = new List<registrosall>();
            using (MasterExchangeEntities db = new MasterExchangeEntities())
            {
                ticket = (from c in db.Tb_Registros
                        
                          join cb in db.Ct_Moneda on c.Int_IdMonVenta equals cb.Int_IdMoneda
                          where c.Lng_IdRegistro == alt
                          select new registrosall
                          {
                              Lng_IdRegistro = c.Lng_IdRegistro,
                              Moneda = cb.Txt_Moneda,
                              Monedaventa = cb.Txt_Moneda,
                              Int_IdMoneda = c.Int_IdMoneda,
                              Dbl_MontoRecibir = c.Dbl_MontoRecibir,
                              Dbl_TipoCambio = c.Dbl_TipoCambio,
                              Dbl_MontoPagar = c.Dbl_MontoPagar,
                              Fec_Fecha = c.Fec_Fecha,
                              Int_IdMonVenta = cb.Int_IdMoneda,
                              Dbl_TipoCambioven = c.Dbl_TipoCambioven,
                              Dbl_Entregar = c.Dbl_Entregar
                          }).ToList();

            }
            return Json(ticket, JsonRequestBehavior.AllowGet);
        }



        public ActionResult cancelaciones()
        {
            int sucursalid = Convert.ToInt32(Session["idsucursal"]);
            string a = User.Identity.Name;
            int iduser = Convert.ToInt32(a);

            bool t = false;

            DateTime fecha = Convert.ToDateTime(Session["fecha"]);



            List<registrosall> lst1 = new List<registrosall>();
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {

                lst1 = (from d in dr.Tb_Registros
                        join da in dr.Ct_Moneda on d.Int_IdMoneda equals+ da.Int_IdMoneda into df
                        from da in df.DefaultIfEmpty()
                        join db in dr.Tb_RegUsu on d.Lng_IdRegistro equals+ db.Lng_IdRegistro
                        join dc in dr.Ct_TipoTran on d.Int_IdTipoTran equals+ dc.Int_IdTipoTran
                        
                        where db.Int_IdUsuario == iduser && d.Bol_Cancelar == t && d.Fec_Fecha > fecha
                        select new registrosall
                        {
                           Lng_IdRegistro = d.Lng_IdRegistro,
                           Txt_TipoTran = dc.Txt_TipoTran,
                           Int_IdMoneda = da.Int_IdMoneda,
                           Moneda = da.Txt_Moneda,
                           Dbl_TipoCambio = d.Dbl_TipoCambio,
                           Dbl_Cambio = d.Dbl_Cambio,
                           Int_IdMonVenta = da.Int_IdMoneda,
                           Monedaven = da.Txt_Moneda, 
                           Dbl_TipoCambioven = d.Dbl_TipoCambioven,
                           Dbl_Entregar = d.Dbl_Entregar,
                           Dbl_MontoPagar = d.Dbl_MontoPagar,
                           Dbl_MontoRecibir = d.Dbl_MontoRecibir,
                           Fec_Fecha = d.Fec_Fecha
                           
                        }).ToList();
            }
            return View(lst1);
        }

        public async Task<ActionResult> actualizar(int? registro)
        {
            if (registro == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tb_Registros tb_Registros = await db.Tb_Registros.FindAsync(registro);

            if (tb_Registros == null)
            {
                return HttpNotFound();
            }

            return View(tb_Registros);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> actualizar([Bind(Include = "Lng_IdRegistro,Int_IdTipoTran,Int_IdMoneda,Int_IdMonVenta,Dbl_MontoRecibir,Dbl_MontoPagar,Dbl_TipoCambio,Dbl_TipoCambioven,Dbl_TipoCambioEsp,Bol_Especial,Dbl_Entregar,Dbl_Cambio,Int_IdTpv,Fec_Fecha,Bol_multimoneda")] Tb_Registros tb_Registros, string motivo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Registros).State = EntityState.Modified;
                await db.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return View(tb_Registros);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult cancelar(int id, string motivo)
        {

            SqlDataAdapter historial = new SqlDataAdapter("UPDATE [dbo].[Tb_Registros] SET [Bol_Cancelar] = 1 where Lng_IdRegistro = " + id+ "", con);
            DataSet h = new DataSet();
            historial.Fill(h);
            h.Reset();

            SqlDataAdapter obja = new SqlDataAdapter("INSERT INTO [dbo].[Tb_Cancelaciones] ([Txt_Motivo] ,[Lng_IdRegistro] ,[Lng_IdUsuario],[Fec_Cancelacion]) VALUES('" + motivo + "' ," + id + "," + User.Identity.Name + ",GETDATE())", con);
            DataSet a = new DataSet();
            obja.Fill(a);
            a.Reset();

            return View();
        }


    }
}