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
using EntityState = System.Data.Entity.EntityState;
using System.Data.SqlClient;

namespace WebApplication2.Controllers
{
    public class SalidaSucController : Controller
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
        // GET: SalidaSuc
        public ActionResult Index()
        {
            modulodivisas();
            return View();
        }


        public ActionResult tsalida()
        {
            bool a = true;
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            int turno = Convert.ToInt32(Session["turno"]);
            int usuario = Convert.ToInt32(User.Identity.Name);
            List<inventario> querty = new List<inventario>();
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                querty = (from c in dr.Tb_SalidaSuc
                          join cb in dr.Ct_Moneda on c.Int_IdMoneda equals cb.Int_IdMoneda
                          where c.int_IdSucursal == sucursalid && c.Int_IdTurno == turno && c.Int_Idusuario == usuario && c.Bol_Activo == a
                          select new inventario()
                          {
                              Int_IdMoneda = cb.Int_IdMoneda,
                             Dbl_SaldoSalida = c.Dbl_SaldoSalida,
                              Txt_Moneda = cb.Txt_Moneda
                          }).ToList();
            }

            return View(querty);
        }


        // POST: SalidaSuc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Lng_IdSalida,Dbl_SaldoSalida,Fec_Fin,Int_IdMoneda,int_IdSucursal,Int_Idusuario, Int_IdTurno")] Tb_SalidaSuc tb_SalidaSuc)
        {
            int idturno = Convert.ToInt32(Session["turno"]);
            modulodivisas();
            if (ModelState.IsValid)
            {
                db.Tb_SalidaSuc.Add(tb_SalidaSuc);
                int id = tb_SalidaSuc.Lng_IdSalida;
                await db.SaveChangesAsync();


                SqlDataAdapter historial = new SqlDataAdapter("INSERT INTO [dbo].[Tb_HistorialEntySal] ([Lng_IdEntrada], [Lng_IdSalida], [Dbl_Monto], [Int_IdMoneda], [Int_Sucursal],[Int_Estatus], [Fec_Creacion], [Int_IdUsuario], [Int_IdTurno]) VALUES ('', "+tb_SalidaSuc.Lng_IdSalida+" ," +tb_SalidaSuc.Dbl_SaldoSalida + " , " + tb_SalidaSuc.Int_IdMoneda + ", " + tb_SalidaSuc.int_IdSucursal + ", 1 ,GETDATE()," + User.Identity.Name + " , " + idturno + ")", con);
                DataSet h = new DataSet();
                historial.Fill(h);
                h.Reset();

                return RedirectToAction("Index");
            }

            return View(tb_SalidaSuc);
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
