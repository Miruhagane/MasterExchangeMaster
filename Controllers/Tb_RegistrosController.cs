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
using EntityState = System.Data.Entity.EntityState;

namespace WebApplication2.Controllers
{
    public class Tb_RegistrosController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();

        // GET: Tb_Registros
        public async Task<ActionResult> Index()
        {
            return View(await db.Tb_Registros.ToListAsync());
        }

        // GET: Tb_Registros/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Registros tb_Registros = await db.Tb_Registros.FindAsync(id);
            if (tb_Registros == null)
            {
                return HttpNotFound();
            }
            return View(tb_Registros);
        }

        // GET: Tb_Registros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tb_Registros/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Lng_IdRegistro,Int_IdTipoTran,Int_IdMoneda,Int_IdMonVenta,Dbl_MontoRecibir,Dbl_MontoPagar,Dbl_TipoCambio,Dbl_TipoCambioven,Dbl_TipoCambioEsp,Bol_Especial,Dbl_Entregar,Dbl_Cambio,Int_IdTpv,Fec_Fecha,Bol_multimoneda")] Tb_Registros tb_Registros)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Registros.Add(tb_Registros);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tb_Registros);
        }

        // GET: Tb_Registros/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Registros tb_Registros = await db.Tb_Registros.FindAsync(id);
            if (tb_Registros == null)
            {
                return HttpNotFound();
            }
            return View(tb_Registros);
        }

        // POST: Tb_Registros/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Lng_IdRegistro,Int_IdTipoTran,Int_IdMoneda,Int_IdMonVenta,Dbl_MontoRecibir,Dbl_MontoPagar,Dbl_TipoCambio,Dbl_TipoCambioven,Dbl_TipoCambioEsp,Bol_Especial,Dbl_Entregar,Dbl_Cambio,Int_IdTpv,Fec_Fecha,Bol_multimoneda")] Tb_Registros tb_Registros)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Registros).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tb_Registros);
        }

        // GET: Tb_Registros/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Registros tb_Registros = await db.Tb_Registros.FindAsync(id);
            if (tb_Registros == null)
            {
                return HttpNotFound();
            }
            return View(tb_Registros);
        }

        // POST: Tb_Registros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tb_Registros tb_Registros = await db.Tb_Registros.FindAsync(id);
            db.Tb_Registros.Remove(tb_Registros);
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
