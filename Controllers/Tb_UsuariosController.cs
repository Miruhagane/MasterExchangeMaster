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
using System.Web.Helpers;

namespace WebApplication2.Controllers
{
    public class Tb_UsuariosController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();

        // GET: Tb_Usuarios
        public async Task<ActionResult> Index()
        {
            return View(await db.Tb_Usuarios.ToListAsync());
        }

        // GET: Tb_Usuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Usuarios tb_Usuarios = await db.Tb_Usuarios.FindAsync(id);
            if (tb_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tb_Usuarios);
        }

        // GET: Tb_Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tb_Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Int_Idusuario,Txt_Nombre,Txt_Apellido,Txt_NomCorto,Txt_Password,Bol_Activo,Int_IdArea,Int_IdSubarea,Fec_Alta,Fec_Baja,Int_Ext,Num_Telefono_1,Bol_Bloqueado,Num_Telefono_2,Txt_email,Bol_Promotor,Int_IdEmpresa,Txt_Password2,Txt_Password3,Int_Idempleado,Int_IdPlaza,Lng_IdSucursal,Int_IdDepartamentos,Img_Foto")] Tb_Usuarios tb_Usuarios)
        {
            HttpPostedFileBase fileBase = Request.Files[0];
            WebImage image = new WebImage(fileBase.InputStream);
            tb_Usuarios.Img_Foto = image.GetBytes();

            if (ModelState.IsValid)
            {
                db.Tb_Usuarios.Add(tb_Usuarios);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tb_Usuarios);
        }

        // GET: Tb_Usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Usuarios tb_Usuarios = await db.Tb_Usuarios.FindAsync(id);
            if (tb_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tb_Usuarios);
        }

        // POST: Tb_Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Int_Idusuario,Txt_Nombre,Txt_Apellido,Txt_NomCorto,Txt_Password,Bol_Activo,Int_IdArea,Int_IdSubarea,Fec_Alta,Fec_Baja,Int_Ext,Num_Telefono_1,Bol_Bloqueado,Num_Telefono_2,Txt_email,Bol_Promotor,Int_IdEmpresa,Txt_Password2,Txt_Password3,Int_Idempleado,Int_IdPlaza,Lng_IdSucursal,Int_IdDepartamentos,Img_Foto")] Tb_Usuarios tb_Usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Usuarios).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tb_Usuarios);
        }

        // GET: Tb_Usuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Usuarios tb_Usuarios = await db.Tb_Usuarios.FindAsync(id);
            if (tb_Usuarios == null)
            {
                return HttpNotFound();
            }
            return View(tb_Usuarios);
        }

        // POST: Tb_Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tb_Usuarios tb_Usuarios = await db.Tb_Usuarios.FindAsync(id);
            db.Tb_Usuarios.Remove(tb_Usuarios);
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
