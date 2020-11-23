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
using System.Web.Helpers;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

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

        public void sucursales()
        {
            List<Sucursal> lst = null;
            using (MasterExchangeEntities sc = new MasterExchangeEntities())
            {
                lst = (
                       from s in sc.Tb_Sucursal
                       select new Sucursal
                       {

                           Lng_IdSucursal = s.Lng_IdSucursal,
                           Txt_Sucursal = s.Txt_Sucursal

                       }).ToList();
            }

            List<SelectListItem> suc = lst.ConvertAll(df =>
            {
                return new SelectListItem()
                {
                    Text = df.Txt_Sucursal.ToString(),
                    Value = df.Lng_IdSucursal.ToString(),
                    Selected = false
                };
            });

            ViewBag.suc = suc;
        }
        // GET: SalidaSuc
        public ActionResult Index()
        {
            modulodivisas();
            return View();
        }


        public JsonResult dtr()
        {
            bool a = true;
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            int turno = Convert.ToInt32(Session["turno"]);
            int usuario = Convert.ToInt32(User.Identity.Name);

            DateTime fecha = Convert.ToDateTime(Session["fecha"]);


            List<Salidas> querty;
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                querty = (from c in dr.Tb_SalidaSuc
                          join cb in dr.Ct_Moneda on c.Int_IdMoneda equals cb.Int_IdMoneda
                          where c.int_IdSucursal == sucursalid && c.Int_IdTurno == turno && c.Bol_Activo == a 
                          select new Salidas
                          {
                              Int_IdMoneda = cb.Int_IdMoneda,
                              Lng_IdSalida = c.Lng_IdSalida,
                              Dbl_SaldoSalida = c.Dbl_SaldoSalida,
                              Int_estatus = c.Int_estatus,
                              Txt_Moneda = cb.Txt_Moneda
                          }).ToList();
            }

            return Json(querty, JsonRequestBehavior.AllowGet);
        }

        public ActionResult indexsalidas()
        {
            modulodivisas();
            sucursales();
            return View();
        }

        public JsonResult salidas()
        {
            bool a = true;
            int usuario = Convert.ToInt32(User.Identity.Name);

            DateTime fecha = Convert.ToDateTime(Session["fecha"]);

            DateTime f1 = fecha.AddDays(1);
            DateTime f2 = DateTime.Now; ;
            f2 = f2.AddDays(-1);


            List<Salidas> querty;
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                querty = (from c in dr.Tb_SalidaSuc
                          join cb in dr.Ct_Moneda on c.Int_IdMoneda equals cb.Int_IdMoneda
                          join cc in dr.Tb_Sucursal on c.int_IdSucursal equals cc.Lng_IdSucursal
                          join cd in dr.Tb_Usuarios on c.Int_Idusuario equals cd.Int_Idusuario
                          where c.Bol_Activo == a && c.Fec_Fin > f2 && c.Fec_Fin <= f1
                          select new Salidas
                          {
                              Int_IdMoneda = cb.Int_IdMoneda,
                              Lng_IdSalida = c.Lng_IdSalida,
                              Dbl_SaldoSalida = c.Dbl_SaldoSalida,
                              Int_estatus = c.Int_estatus,
                              Txt_Moneda = cb.Txt_Moneda,
                              Txt_sucursal = cc.Txt_Sucursal,
                              username = cd.Txt_Nombre,
                              Txt_Motivo = c.Txt_Motivo
                              
                          }).ToList();
            }

            return Json(querty, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult status(int idregistro, string mtc, int estatus)
        {
            int idturno = Convert.ToInt32(Session["turno"]);
            int sucursal = Convert.ToInt32(Session["idSucursal"]);


            SqlDataAdapter caja = new SqlDataAdapter("UPDATE [dbo].[Tb_SalidaSuc] SET [Int_Estatus] = " + estatus + " ,[Txt_Motivo] = ' " + mtc + " ', [Bol_Activo] = 1 where  Lng_IdSalida = " + idregistro + " ", con);
            DataSet a = new DataSet();
            caja.Fill(a);
            a.Reset();

            SqlDataAdapter historial = new SqlDataAdapter("INSERT INTO [dbo].[Tb_HistorialEntySal] ([Lng_IdEntrada], [Lng_IdSalida], [Int_Sucursal],[Int_Estatus], [Fec_Creacion], [Int_IdUsuario], [Int_IdTurno]) VALUES ( ' ' ," + idregistro + " ,  " + sucursal + ", " + estatus + " ,GETDATE()," + User.Identity.Name + " , " + idturno + ")", con);
            DataSet h = new DataSet();
            historial.Fill(h);
            h.Reset();


            return View();
        }


        // POST: SalidaSuc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Lng_IdSalida,Dbl_SaldoSalida,Fec_Fin,Int_IdMoneda,int_IdSucursal,Int_Idusuario,Int_IdTurno, Int_estatus, Bol_Activo")] Tb_SalidaSuc tb_SalidaSuc)
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            int turno = Convert.ToInt32(Session["turno"]);
            int usuario = Convert.ToInt32(User.Identity.Name);
           

            modulodivisas();
            if (ModelState.IsValid)
            {
                db.Tb_SalidaSuc.Add(tb_SalidaSuc);
                int id = tb_SalidaSuc.Lng_IdSalida;
                await db.SaveChangesAsync();


                SqlDataAdapter historial = new SqlDataAdapter("INSERT INTO [dbo].[Tb_HistorialEntySal] ([Lng_IdEntrada], [Lng_IdSalida], [Int_Sucursal],[Int_Estatus], [Fec_Creacion], [Int_IdUsuario], [Int_IdTurno]) VALUES ('', "+id+", "+ sucursalid + ", "+tb_SalidaSuc.Int_estatus+" , GETDATE(), "+ usuario + ", "+turno+")", con);
                DataSet h = new DataSet();
                historial.Fill(h);
                h.Reset();

                return RedirectToAction("Index");
            }

            return View(tb_SalidaSuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> indexsalidas([Bind(Include = "Lng_IdSalida,Dbl_SaldoSalida,Fec_Fin,Int_IdMoneda,int_IdSucursal,Int_Idusuario,Int_IdTurno, Int_estatus, Bol_Activo")] Tb_SalidaSuc tb_SalidaSuc)
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            int turno = Convert.ToInt32(Session["turno"]);
            int usuario = Convert.ToInt32(User.Identity.Name);


            modulodivisas();
            sucursales();
            if (ModelState.IsValid)
            {
                db.Tb_SalidaSuc.Add(tb_SalidaSuc);
                int id = tb_SalidaSuc.Lng_IdSalida;
                await db.SaveChangesAsync();


                SqlDataAdapter historial = new SqlDataAdapter("INSERT INTO [dbo].[Tb_HistorialEntySal] ([Lng_IdEntrada], [Lng_IdSalida], [Int_Sucursal],[Int_Estatus], [Fec_Creacion], [Int_IdUsuario], [Int_IdTurno]) VALUES ('', " + id + ", " + sucursalid + ", " + tb_SalidaSuc.Int_estatus + " , GETDATE(), " + usuario + ", " + turno + ")", con);
                DataSet h = new DataSet();
                historial.Fill(h);
                h.Reset();

                return RedirectToAction("indexsalidas");
            }

            modulodivisas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult datosvr(int grupo, HttpPostedFileBase archivo)
        {
            byte[] temparchivo = new byte[archivo.ContentLength];
            archivo.InputStream.Read(temparchivo, 0, archivo.ContentLength);

            if (archivo != null && archivo.ContentLength > 0)
            {

                SqlDataAdapter caja = new SqlDataAdapter("insert into Tb_EstatusTesoreria ( Int_IdGrupo, Int_IdEstatusTesoreria, Int_IdUsuario, Fec_Dia) values ( "+grupo+", 1, "+User.Identity.Name+", GETDATE())", con);
                DataSet insert = new DataSet();
                caja.Fill(insert);
                insert.Reset();

                var idmax = db.Tb_EstatusTesoreria.Max(e => e.Lng_IdTesoreria);

                SqlDataAdapter ticket = new SqlDataAdapter("insert into Tb_TicketTesoreria (Lng_IdTesoreria, Img_Archivo, Fec_Dia, Bol_Activo) values ("+idmax+", "+archivo+", GETDATE(), 1 )", con);
                DataSet insertT = new DataSet();
                ticket.Fill(insertT);
                insertT.Reset();
            }
            else
            {
                return RedirectToAction("datosvr", new { error = "2" });
            }

            return View();
        }

        public JsonResult agrupar()
        {
            string JSONresult;
            
            SqlDataAdapter da = new SqlDataAdapter("select c.Txt_Nombre as 'Usuario' ,SUM(a.Dbl_Monto) as total, max(a.Fec_Registro) as fecha, b.Int_IdGrupo as Grupo from Tb_Dotaciones as a inner join Tb_DotGrupo as b on a.Lng_IdDotacion = b.Lng_IdDotacion inner join Tb_Usuarios as c on a.Int_Idusuario = c.Int_Idusuario group by c.Txt_Nombre, b.Int_IdGrupo", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            JSONresult = JsonConvert.SerializeObject(dt);

            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult datosvr(string error)
        {
            ViewBag.error = error;
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
