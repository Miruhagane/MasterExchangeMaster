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
    public class CajaController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();

        // GET: Caja
        public ActionResult Index()
        {
            //return View(await db.Tb_EntEmp.ToListAsync());
            return View();
        }

        // GET: Caja/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_EntEmp tb_EntEmp = await db.Tb_EntEmp.FindAsync(id);
            if (tb_EntEmp == null)
            {
                return HttpNotFound();
            }
            return View(tb_EntEmp);
        }

        // GET: Caja/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Caja/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Lng_IdEntEmp,Lng_IdEntrada,Int_IdUsuario,Int_IdTurno")] Tb_EntEmp tb_EntEmp)
        {
            if (ModelState.IsValid)
            {
                db.Tb_EntEmp.Add(tb_EntEmp);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tb_EntEmp);
        }

        // GET: Caja/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_EntEmp tb_EntEmp = await db.Tb_EntEmp.FindAsync(id);
            if (tb_EntEmp == null)
            {
                return HttpNotFound();
            }
            return View(tb_EntEmp);
        }

        // POST: Caja/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Lng_IdEntEmp,Lng_IdEntrada,Int_IdUsuario,Int_IdTurno")] Tb_EntEmp tb_EntEmp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_EntEmp).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tb_EntEmp);
        }

        // GET: Caja/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_EntEmp tb_EntEmp = await db.Tb_EntEmp.FindAsync(id);
            if (tb_EntEmp == null)
            {
                return HttpNotFound();
            }
            return View(tb_EntEmp);
        }

        // POST: Caja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tb_EntEmp tb_EntEmp = await db.Tb_EntEmp.FindAsync(id);
            db.Tb_EntEmp.Remove(tb_EntEmp);
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
