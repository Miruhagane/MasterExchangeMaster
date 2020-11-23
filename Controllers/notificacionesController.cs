using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using WebApplication2.Models.ViewModels;
using System.Web.Mvc;
using WebApplication2.Models;
using EntityState = System.Data.Entity.EntityState;

namespace WebApplication2.Controllers
{
    public class notificacionesController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();

        // GET: notificaciones
        public async Task<ActionResult> Index()
        {
            return View(await db.Tb_SalidaSuc.ToListAsync());
        }

        public JsonResult notsalidas()
        {
            bool a = true;
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            int turno = Convert.ToInt32(Session["turno"]);
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
                          where c.int_IdSucursal == sucursalid && c.Bol_Activo == a && c.Fec_Fin > f2 && c.Fec_Fin <= f1
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

        // GET: notificaciones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_SalidaSuc tb_SalidaSuc = await db.Tb_SalidaSuc.FindAsync(id);
            if (tb_SalidaSuc == null)
            {
                return HttpNotFound();
            }
            return View(tb_SalidaSuc);
        }

        // GET: notificaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: notificaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Lng_IdSalida,Dbl_SaldoSalida,Fec_Fin,Int_IdMoneda,int_IdSucursal,Bol_Activo,Int_Idusuario,Int_IdTurno,Int_estatus,Txt_Motivo")] Tb_SalidaSuc tb_SalidaSuc)
        {
            if (ModelState.IsValid)
            {
                db.Tb_SalidaSuc.Add(tb_SalidaSuc);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tb_SalidaSuc);
        }

        // GET: notificaciones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_SalidaSuc tb_SalidaSuc = await db.Tb_SalidaSuc.FindAsync(id);
            if (tb_SalidaSuc == null)
            {
                return HttpNotFound();
            }
            return View(tb_SalidaSuc);
        }

        // POST: notificaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Lng_IdSalida,Dbl_SaldoSalida,Fec_Fin,Int_IdMoneda,int_IdSucursal,Bol_Activo,Int_Idusuario,Int_IdTurno,Int_estatus,Txt_Motivo")] Tb_SalidaSuc tb_SalidaSuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_SalidaSuc).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tb_SalidaSuc);
        }

        // GET: notificaciones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_SalidaSuc tb_SalidaSuc = await db.Tb_SalidaSuc.FindAsync(id);
            if (tb_SalidaSuc == null)
            {
                return HttpNotFound();
            }
            return View(tb_SalidaSuc);
        }

        // POST: notificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tb_SalidaSuc tb_SalidaSuc = await db.Tb_SalidaSuc.FindAsync(id);
            db.Tb_SalidaSuc.Remove(tb_SalidaSuc);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
