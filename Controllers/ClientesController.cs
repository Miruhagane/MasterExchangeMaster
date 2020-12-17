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
using Microsoft.VisualBasic.ApplicationServices;
using System.IO;
using System.Data.Linq.SqlClient;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class ClientesController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();

        public void sucursales()
        {
            bool activo = false;


            List<Sucursal> lst = null;
            using (MasterExchangeEntities sc = new MasterExchangeEntities())
            {
                lst = (
                       from s in sc.Tb_Sucursal
                       where s.Bol_Activo == activo
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

        // GET: Clientes
        public ActionResult Index()
        {

            sucursales();

            return View();
  
        }

        public ActionResult PDF(int cliente)
        {
           
            var clientearray = db.Tb_Clientes.FirstOrDefault(e => e.Lng_IdCliente == cliente);

            byte[] archivo = clientearray.Doc_cliente;
            return File(archivo, "application/pdf");
        }

        public JsonResult litaclientes(string datos)
        { 
            List<Clientes> lsdt;

            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                lsdt = (from a in dr.Tb_Clientes
                        where a.Txt_Cliente.Contains(datos) || a.Txt_Direccion.Contains(datos) || a.Txt_Identificacion.Contains(datos) || a.Txt_Email.Contains(datos) || a.Txt_Telefono.Contains(datos)
                        select new Clientes
                        {
                           Lng_IdCliente = a.Lng_IdCliente,
                           Txt_Cliente = a.Txt_Cliente,
                           Txt_Direccion = a.Txt_Direccion,
                           Txt_Identificacion = a.Txt_Identificacion,
                           Txt_Telefono = a.Txt_Telefono,
                           Txt_Email = a.Txt_Email
                        }).ToList();

            }

                return Json(lsdt, JsonRequestBehavior.AllowGet);
        }


        // GET: Clientes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Clientes tb_Clientes = await db.Tb_Clientes.FindAsync(id);
            if (tb_Clientes == null)
            {
                return HttpNotFound();
            }
            return View(tb_Clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Lng_IdCliente,Txt_NumCliente,Txt_Cliente,Txt_Direccion,Txt_Identificacion,Txt_Telefono,Txt_Email,Fec_Alta,File_Nombre,Doc_cliente")] Tb_Clientes tb_Clientes)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Clientes.Add(tb_Clientes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tb_Clientes);
        }

        // GET: Clientes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Clientes tb_Clientes = await db.Tb_Clientes.FindAsync(id);
            if (tb_Clientes == null)
            {
                return HttpNotFound();
            }
            return View(tb_Clientes);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Lng_IdCliente,Txt_NumCliente,Txt_Cliente,Txt_Direccion,Txt_Identificacion,Txt_Telefono,Txt_Email,Fec_Alta,File_Nombre,Doc_cliente")] Tb_Clientes tb_Clientes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Clientes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tb_Clientes);
        }

        // GET: Clientes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Clientes tb_Clientes = await db.Tb_Clientes.FindAsync(id);
            if (tb_Clientes == null)
            {
                return HttpNotFound();
            }
            return View(tb_Clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tb_Clientes tb_Clientes = await db.Tb_Clientes.FindAsync(id);
            db.Tb_Clientes.Remove(tb_Clientes);
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
