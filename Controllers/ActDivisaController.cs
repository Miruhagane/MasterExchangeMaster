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
    public class ActDivisaController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();

        // GET: ActDivisa
        public async Task<ActionResult> Index()
        {
            return View(await db.TaxaCompras.ToListAsync());
        }

        // GET: ActDivisa/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxaCompra taxaCompra = await db.TaxaCompras.FindAsync(id);
            if (taxaCompra == null)
            {
                return HttpNotFound();
            }
            return View(taxaCompra);
        }

        // GET: ActDivisa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ActDivisa/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdTaxa,Moneda,Valor,Dia,Lng_IdTaxa,Int_IdMoneda")] TaxaCompra taxaCompra)
        {
            if (ModelState.IsValid)
            {
                db.TaxaCompras.Add(taxaCompra);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(taxaCompra);
        }

        // GET: ActDivisa/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxaCompra taxaCompra = await db.TaxaCompras.FindAsync(id);
            if (taxaCompra == null)
            {
                return HttpNotFound();
            }
            return View(taxaCompra);
        }

        // POST: ActDivisa/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
  
        // GET: ActDivisa/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaxaCompra taxaCompra = await db.TaxaCompras.FindAsync(id);
            if (taxaCompra == null)
            {
                return HttpNotFound();
            }
            return View(taxaCompra);
        }

        // POST: ActDivisa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            TaxaCompra taxaCompra = await db.TaxaCompras.FindAsync(id);
            db.TaxaCompras.Remove(taxaCompra);
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
