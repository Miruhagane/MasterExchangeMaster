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
    [Authorize]
    public class ValesController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();

        // GET: Vales
        public  ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> vales()
        {
            return View(await db.Tb_Vales.ToListAsync());
        }


        // GET: Vales/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Vales tb_Vales = await db.Tb_Vales.FindAsync(id);
            if (tb_Vales == null)
            {
                return HttpNotFound();
            }
            return View(tb_Vales);
        }

        // GET: Vales/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vales/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Lng_IdVale,Valor,Fec_Cierre,Int_IdTurno,Int_IdSucursal,Int_IdUsuario,Txt_Concepto,Bol_Status")] Tb_Vales tb_Vales)
        {
            if (ModelState.IsValid)
            {
                
                db.Tb_Vales.Add(tb_Vales);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tb_Vales);
        }

        // GET: Vales/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Vales tb_Vales = await db.Tb_Vales.FindAsync(id);
            if (tb_Vales == null)
            {
                return HttpNotFound();
            }
            return View(tb_Vales);
        }

        // POST: Vales/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Lng_IdVale,Valor,Fec_Cierre,Int_IdTurno,Int_IdSucursal,Int_IdUsuario")] Tb_Vales tb_Vales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Vales).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tb_Vales);
        }

        // GET: Vales/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Vales tb_Vales = await db.Tb_Vales.FindAsync(id);
            if (tb_Vales == null)
            {
                return HttpNotFound();
            }
            return View(tb_Vales);
        }

        // POST: Vales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tb_Vales tb_Vales = await db.Tb_Vales.FindAsync(id);
            db.Tb_Vales.Remove(tb_Vales);
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
