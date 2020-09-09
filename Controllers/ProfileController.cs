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
            List<Elementos1> comp = new List<Elementos1>();
            using (Models.MasterExchangeEntities dbm = new Models.MasterExchangeEntities())
            {
                comp = (from x in dbm.TaxaVentas
                        where x.Int_IdMoneda == idmoneda
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
            
            List<Elementos> lst = new List<Elementos>();
           using (Models.MasterExchangeEntities dk = new Models.MasterExchangeEntities())
            {
                lst = (from d in dk.TaxaCompras
                       where d.Int_IdMoneda == idmoneda
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

            List<Elementos> lst = new List<Elementos>();
            using (Models.MasterExchangeEntities dk = new Models.MasterExchangeEntities())
            {
                lst = (from d in dk.Taxacompcombs
                       where d.Int_IdMoneda == idmoneda
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
            List<Elementos> lst = new List<Elementos>();
            using (Models.MasterExchangeEntities dk = new Models.MasterExchangeEntities())
            {
                lst = (from d in dk.TaxaVentas
                       where d.Int_IdMoneda == idmoneda
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
        public JsonResult buscar(string numero)
        {
            List<listaclientes> ward;

            using (Models.MasterExchangeEntities db = new Models.MasterExchangeEntities())
            {
                ward = (from c in db.Tb_Clientes
                        where c.Txt_Identificacion == numero || c.Txt_Email == numero || c.Txt_Telefono == numero
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

            using (Models.MasterExchangeEntities dr = new Models.MasterExchangeEntities())
            {
                lst = (
                     from d in dr.TaxaCompras
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

            using (Models.MasterExchangeEntities dr = new Models.MasterExchangeEntities())
            {
                lst = (
                     from d in dr.Taxacompcombs
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

            using (Models.MasterExchangeEntities dr = new Models.MasterExchangeEntities())
            {
                mls = (
                     from d in dr.TaxaVentas
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
            List<Elementos> usd = new List<Elementos>();
            using (MasterExchangeEntities fg = new MasterExchangeEntities())
            {
                usd = (from dolar in fg.TaxaCompras
                       where dolar.IdTaxa == 1
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
            List<Elementos> usd = new List<Elementos>();
            using (MasterExchangeEntities fg = new MasterExchangeEntities())
            {
                usd = (from dolar in fg.TaxaVentas
                       where dolar.IdTaxa == 1
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
            List<Tcompras> lst1;
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                lst1 = (from d in dr.TaxaCompras
                        select new Tcompras
                        {
                            IdTaxa = d.IdTaxa,
                            Moneda = d.Moneda,
                            Valor = d.Valor,
                            Dia = d.Dia

                        }).ToList();
            }

            return View(lst1);
        }

        public ActionResult Tventas()
        {
            List<Tventas> lst1;
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                lst1 = (from d in dr.TaxaVentas
                        select new Tventas
                        {
                            IdTaxa = d.IdTaxa,
                            Moneda = d.Moneda,
                            Valor = d.Valor,
                            Dia = d.Dia

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
        public async Task<ActionResult> Index([Bind(Include = "Lng_IdRegistro,Int_IdTipoTran,Int_IdMoneda,Dbl_MontoRecibir,Dbl_MontoPagar,Dbl_TipoCambio,Dbl_TipoCambioEsp,Bol_Especial,Dbl_Entregar,Dbl_Cambio,Int_IdTpv,Fec_Fecha,Lng_IdCliente, Lng_IdRegCli")] Tb_Registros tb_Registros, Tb_RegCli Tb_RegCli, string idcliente)
        {
            

            if (ModelState.IsValid)
            {
                db.Tb_Registros.Add(tb_Registros);

                await db.SaveChangesAsync();
                Tb_RegCli.Lng_IdRegistro = tb_Registros.Lng_IdRegistro;
                int idregistro = tb_Registros.Lng_IdRegistro;
               

                SqlDataAdapter obja = new SqlDataAdapter("insert into Tb_RegCli(Lng_IdRegistro , Lng_IdCliente) values( "+Tb_RegCli.Lng_IdRegistro +" , "+ idcliente +" )", con);
                DataSet a = new DataSet();
                obja.Fill(a);
                a.Reset();

                SqlDataAdapter otr = new SqlDataAdapter("insert into Tb_RegUsu(Int_IdUsuario , Lng_IdRegistro) values( " +User.Identity.Name+ " , " +Tb_RegCli.Lng_IdRegistro+ " )", con);
                DataSet b = new DataSet();
                otr.Fill(b);
                b.Reset();

                string idreg = Convert.ToString(idregistro); 

                return RedirectToAction("Index", new { idregistro = idreg });
            }

            modulodivisas();
            comboventa();

            return View();
        }

        public ActionResult Venta(string idregistro)
        {
            divisasCompra_venta();
            comboventa();

            ViewBag.id = idregistro;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Venta([Bind(Include = "Lng_IdRegistro,Int_IdTipoTran,Int_IdMoneda,int_idmonventa,Dbl_MontoRecibir,Dbl_MontoPagar,Dbl_TipoCambio,Dbl_TipoCambioven,Dbl_TipoCambioEsp,Bol_Especial,Dbl_Entregar,Dbl_Cambio,Int_IdTpv,Fec_Fecha,Lng_IdCliente, Lng_IdRegCli")] Tb_Registros tb_Registros, Tb_RegCli Tb_RegCli, string idcliente)
        {
            divisasCompra_venta();

            if (ModelState.IsValid)
            {


                db.Tb_Registros.Add(tb_Registros);

                await db.SaveChangesAsync();
                Tb_RegCli.Lng_IdRegistro = tb_Registros.Lng_IdRegistro;
                int idregistro = tb_Registros.Lng_IdRegistro;

                SqlDataAdapter obja = new SqlDataAdapter("insert into Tb_RegCli(Lng_IdRegistro , Lng_IdCliente) values( " + Tb_RegCli.Lng_IdRegistro + " , " + idcliente + " )", con);
                DataSet a = new DataSet();
                obja.Fill(a);
                a.Reset();


                SqlDataAdapter otr = new SqlDataAdapter("insert into Tb_RegUsu(Int_IdUsuario , Lng_IdRegistro) values( " + User.Identity.Name + " , " + Tb_RegCli.Lng_IdRegistro + " )", con);
                DataSet b = new DataSet();
                otr.Fill(b);
                b.Reset();

                string idreg = Convert.ToString(idregistro);

                return RedirectToAction("Multimoneda", new { idregistro = idreg });


            }

            divisasCompra_venta();
            comboventa();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clientes(string nombre, string numcliente, string dirreccion, string identificacion, string telefono, string mail)
        {
            
            using (Models.MasterExchangeEntities mir1 = new Models.MasterExchangeEntities())
            {

                var userv = mir1.Tb_Clientes.FirstOrDefault(e => e.Txt_Identificacion == identificacion || e.Txt_Telefono == telefono);

                if (userv == null)
                {
                    var ut = new Tb_Clientes()
                    {
                        Txt_Cliente = nombre,
                        Txt_NumCliente = numcliente,
                        Txt_Direccion = dirreccion,
                        Txt_Telefono = telefono,
                        Txt_Email = mail,
                        Txt_Identificacion = identificacion,
                        Fec_Alta = DateTime.Now
                    };

                    mir1.Tb_Clientes.Add(ut);
                    mir1.SaveChanges();
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
                          join ca in db.TaxaCompras on c.Int_IdMoneda equals+
                          ca.Int_IdMoneda
                          
                          where c.Lng_IdRegistro == registro
                          select new registrosall() 
                          {
                              Lng_IdRegistro = c.Lng_IdRegistro,
                              Moneda = ca.Moneda,
                              Int_IdMoneda = c.Int_IdMoneda, 
                              Dbl_MontoRecibir = c.Dbl_MontoRecibir,
                              Dbl_TipoCambio = c.Dbl_TipoCambio,
                              Dbl_MontoPagar = c.Dbl_MontoPagar,
                              Fec_Fecha =  c.Fec_Fecha,
                             

                          }).ToList();
                
            }
            
                

            return Json(ticket, JsonRequestBehavior.AllowGet);
        }

    }
}