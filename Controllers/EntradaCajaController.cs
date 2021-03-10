using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using EntityState = System.Data.Entity.EntityState;
using WebApplication2.Models.ViewModels;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class EntradaCajaController : Controller
    {

        private MasterExchangeEntities db = new MasterExchangeEntities();
        string con = System.Configuration.ConfigurationManager.ConnectionStrings["server2"].ConnectionString;

        public void modulodivisas()
        {

            List<moneda> lst = null;

            using (Models.MasterExchangeEntities dr = new Models.MasterExchangeEntities())
            {
                lst = (
                     from d in dr.Ct_Moneda
                     select new moneda
                     {
                         Int_IdMoneda = d.Int_IdMoneda,
                         Moneda = d.Txt_Moneda

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

        public ActionResult invet()
        {
            bool a = true;
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            int turno = Convert.ToInt32(Session["turno"]);
            int usuario = Convert.ToInt32(User.Identity.Name);
            DateTime fecha = Convert.ToDateTime(Session["fecha"]);


            List<inventario> querty = new List<inventario>();
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                querty = (from c in dr.Tb_EntradaSuc
                          join ca in dr.Tb_EntEmp on c.Lng_IdEntrada equals ca.Lng_IdEntrada
                          join cb in dr.Ct_Moneda on c.Int_IdMoneda equals cb.Int_IdMoneda
                          where c.Int_Sucursal == sucursalid && c.Bol_Activo == a && ca.Int_IdTurno == turno && ca.Int_IdUsuario == usuario && c.Fec_Ini > fecha
                          select new inventario
                          {
                              Dbl_SaldoEntrada = c.Dbl_SaldoEntrada,
                              Txt_Moneda = cb.Txt_Moneda
                          }).ToList();
            }


            return View(querty);
        }

        public ActionResult dotaciones()
        {
            modulodivisas();
            return View();
        }

        
        public JsonResult estatus()
        {
           
            bool a = true;
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            int turno = Convert.ToInt32(Session["turno"]);
            int usuario = Convert.ToInt32(User.Identity.Name);

            DateTime fecha = Convert.ToDateTime(Session["fecha"]);

            List<inventario> querty;
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                querty = (from c in dr.Tb_EntradaSuc
                          join ca in dr.Tb_EntEmp on c.Lng_IdEntrada equals ca.Lng_IdEntrada
                          join cb in dr.Ct_Moneda on c.Int_IdMoneda equals cb.Int_IdMoneda
                          join cc in dr.Tb_Sucursal on c.Int_Sucursal equals cc.Lng_IdSucursal
                          join cd in dr.Tb_Usuarios on ca.Int_IdUsuario equals cd.Int_Idusuario
                          where c.Int_Sucursal == sucursalid && c.Bol_Activo == a && ca.Int_IdTurno == turno && ca.Int_IdUsuario == usuario && c.Fec_Ini > fecha && ca.Fec_Ini > fecha
                          select new inventario
                          {
                              Lng_IdEntrada = c.Lng_IdEntrada,
                              Dbl_SaldoEntrada = c.Dbl_SaldoEntrada,
                              Txt_Moneda = cb.Txt_Moneda,
                              Int_Estatus = c.Int_Estatus,
                              Txt_sucursal = cc.Txt_Sucursal,
                              n_usuario = cd.Txt_Nombre,
                              Fec_Ini = c.Fec_Ini,
                              Txt_Motivo = c.Txt_Motivo

                          }).ToList();
            }


            return Json(querty, JsonRequestBehavior.AllowGet);


        }

        public JsonResult admins(DateTime fec)
        {

             DateTime fecha = fec;
                DateTime fec1 = fecha.AddHours(23);



                List<inventario> querty;

                using (MasterExchangeEntities dc = new MasterExchangeEntities())
                {
                    querty = (from c in dc.Tb_EntradaSuc
                              join ca in dc.Tb_EntEmp on c.Lng_IdEntrada equals ca.Lng_IdEntrada
                              join cb in dc.Ct_Moneda on c.Int_IdMoneda equals cb.Int_IdMoneda
                              join cc in dc.Tb_Sucursal on c.Int_Sucursal equals cc.Lng_IdSucursal
                              join cd in dc.Tb_Usuarios on ca.Int_IdUsuario equals cd.Int_Idusuario
                              where c.Fec_Ini > fecha && c.Fec_Ini < fec1 && c.Int_Estatus >= 2
                              select new inventario
                              {
                                  Lng_IdEntrada = c.Lng_IdEntrada,
                                  Dbl_SaldoEntrada = c.Dbl_SaldoEntrada,
                                  Txt_Moneda = cb.Txt_Moneda,
                                  Int_Estatus = c.Int_Estatus,
                                  Txt_sucursal = cc.Txt_Sucursal,
                                  n_usuario = cd.Txt_Nombre,
                                  Fec_Ini = c.Fec_Ini,
                                  Txt_Motivo = c.Txt_Motivo
                              }).ToList();


                }

                return Json(querty, JsonRequestBehavior.AllowGet);
            
            
            
          
        }




        // GET: EntradaCaja
        public ActionResult Index()
        {
            modulodivisas();
            return View();

        }

        // POST: EntradaCaja/Index
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Lng_IdEntrada,Dbl_SaldoEntrada,Fec_Ini,Fec_Fin,Int_Sucursal,Int_IdMoneda,Bol_Activo,Int_Estatus,Txt_Motivo")] Tb_EntradaSuc tb_EntradaSuc, Tb_EntEmp tb_EntEmp)
        {
            int idturno = Convert.ToInt32(Session["turno"]);
            modulodivisas();
            if (ModelState.IsValid)
            {
                db.Tb_EntradaSuc.Add(tb_EntradaSuc);
                await db.SaveChangesAsync();

                tb_EntEmp.Lng_IdEntrada = tb_EntradaSuc.Lng_IdEntrada;

                

                SqlDataAdapter caja = new SqlDataAdapter("INSERT INTO [dbo].[Tb_EntEmp] ([Lng_IdEntrada], [Int_IdUsuario], [Int_IdTurno], [Fec_Ini], [Fec_Fin]) VALUES (" + tb_EntEmp.Lng_IdEntrada + " ," + User.Identity.Name + " , "+ idturno + ", GETDATE() , ' ')", con);
                DataSet a = new DataSet();
                caja.Fill(a);
                a.Reset();

                SqlDataAdapter historial = new SqlDataAdapter("INSERT INTO [dbo].[Tb_HistorialEntySal] ([Lng_IdEntrada], [Lng_IdSalida], [Int_Sucursal],[Int_Estatus], [Fec_Creacion], [Int_IdUsuario], [Int_IdTurno]) VALUES (" + tb_EntradaSuc.Lng_IdEntrada + ",' ' , " + tb_EntradaSuc.Int_Sucursal + ", " + tb_EntradaSuc.Int_Estatus + " ,GETDATE()," + User.Identity.Name + " , " + idturno + ")", con);
                DataSet h = new DataSet();
                historial.Fill(h);
                h.Reset();

                return RedirectToAction("Index", "Profile");

            }
            modulodivisas();
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult actualizar()
        {
            modulodivisas();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> actualizar([Bind(Include = "Lng_IdEntrada,Dbl_SaldoEntrada,Fec_Ini,Fec_Fin,Int_Sucursal,Int_IdMoneda,Bol_Activo, Int_Estatus")] Tb_EntradaSuc tb_EntradaSuc, Tb_EntEmp tb_EntEmp)
        {
            int idturno = Convert.ToInt32(Session["turno"]);
            modulodivisas();
            if (ModelState.IsValid)
            {
                db.Tb_EntradaSuc.Add(tb_EntradaSuc);
                await db.SaveChangesAsync();

                tb_EntEmp.Lng_IdEntrada = tb_EntradaSuc.Lng_IdEntrada;



                SqlDataAdapter caja = new SqlDataAdapter("INSERT INTO [dbo].[Tb_EntEmp] ([Lng_IdEntrada], [Int_IdUsuario], [Int_IdTurno], [Fec_Ini], [Fec_Fin]) VALUES (" + tb_EntEmp.Lng_IdEntrada + " ," + User.Identity.Name + " , " + idturno + ", GETDATE() , ' ')", con);
                DataSet a = new DataSet();
                caja.Fill(a);
                a.Reset();

                SqlDataAdapter historial = new SqlDataAdapter("INSERT INTO [dbo].[Tb_HistorialEntySal] ([Lng_IdEntrada], [Lng_IdSalida], [Int_Sucursal],[Int_Estatus], [Fec_Creacion], [Int_IdUsuario], [Int_IdTurno]) VALUES (" + tb_EntradaSuc.Lng_IdEntrada + ", ' ' ,  " + tb_EntradaSuc.Int_Sucursal + ", "+tb_EntradaSuc.Int_Estatus+" , GETDATE()," + User.Identity.Name + " , " + idturno + ")", con);
                DataSet h = new DataSet();
                historial.Fill(h);
                h.Reset();

                return RedirectToAction("actualizar");
            }
            modulodivisas();

            return RedirectToAction("actualizar");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult status( int idregistro, string mtc, int estatus)
        {
            int idturno = Convert.ToInt32(Session["turno"]); 
            int sucursal = Convert.ToInt32(Session["idSucursal"]);


            SqlDataAdapter caja = new SqlDataAdapter("UPDATE [dbo].[Tb_EntradaSuc] SET [Int_Estatus] = "+ estatus + ", [Txt_Motivo] = ' "+mtc+" ' where  Lng_IdEntrada = " + idregistro + " ", con);
            DataSet a = new DataSet();
            caja.Fill(a);
            a.Reset();

            SqlDataAdapter historial = new SqlDataAdapter("INSERT INTO [dbo].[Tb_HistorialEntySal] ([Lng_IdEntrada], [Lng_IdSalida], [Int_Sucursal],[Int_Estatus], [Fec_Creacion], [Int_IdUsuario], [Int_IdTurno]) VALUES (" + idregistro + ",' ' ,  " + sucursal + ", " + estatus + " ,GETDATE()," + User.Identity.Name + " , " + idturno + ")", con);
            DataSet h = new DataSet();
            historial.Fill(h);
            h.Reset();


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
