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

namespace WebApplication2.Controllers
{
    public class Tb_EntradaSucController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();

        // GET: Tb_EntradaSuc
        public async Task<ActionResult> Index()
        {
            return View(await db.Tb_EntradaSuc.ToListAsync());
        }

        // GET: Tb_EntradaSuc/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_EntradaSuc tb_EntradaSuc = await db.Tb_EntradaSuc.FindAsync(id);
            if (tb_EntradaSuc == null)
            {
                return HttpNotFound();
            }
            return View(tb_EntradaSuc);
        }

        // GET: Tb_EntradaSuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tb_EntradaSuc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Lng_IdEntrada,Dbl_SaldoEntrada,Fec_Ini,Fec_Fin,Int_Sucursal,Int_IdMoneda,Bol_Activo")] Tb_EntradaSuc tb_EntradaSuc)
        {
            if (ModelState.IsValid)
            {
                db.Tb_EntradaSuc.Add(tb_EntradaSuc);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tb_EntradaSuc);
        }

        // GET: Tb_EntradaSuc/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_EntradaSuc tb_EntradaSuc = await db.Tb_EntradaSuc.FindAsync(id);
            if (tb_EntradaSuc == null)
            {
                return HttpNotFound();
            }
            return View(tb_EntradaSuc);
        }

        // POST: Tb_EntradaSuc/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Lng_IdEntrada,Dbl_SaldoEntrada,Fec_Ini,Fec_Fin,Int_Sucursal,Int_IdMoneda,Bol_Activo")] Tb_EntradaSuc tb_EntradaSuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_EntradaSuc).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tb_EntradaSuc);
        }

        // GET: Tb_EntradaSuc/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_EntradaSuc tb_EntradaSuc = await db.Tb_EntradaSuc.FindAsync(id);
            if (tb_EntradaSuc == null)
            {
                return HttpNotFound();
            }
            return View(tb_EntradaSuc);
        }

        // POST: Tb_EntradaSuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tb_EntradaSuc tb_EntradaSuc = await db.Tb_EntradaSuc.FindAsync(id);
            db.Tb_EntradaSuc.Remove(tb_EntradaSuc);
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
