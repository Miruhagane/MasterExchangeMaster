﻿using System;
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
using System.Data.SqlClient;

namespace WebApplication2.Controllers
{
    public class sucadminController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();
        string con = System.Configuration.ConfigurationManager.ConnectionStrings["server2"].ConnectionString;

        // GET: sucadmin
        public async Task<ActionResult> Index()
        {
            return View(await db.Tb_Sucursal.ToListAsync());
        }

        public ActionResult insert(string monto_1, string monto_2, string monto_3, string monto_4, string monto_5, string monto_6, string monto_7, string monto_8, string monto_9, string monto_10, string monto_11, string monto_12, string monto_13, string monto_14, string monto_15, string monto_16, string monto_17, string id_1, string id_2, string id_3, string id_4, string id_5, string id_6, string id_7, string id_8, string id_9,  string id_10, string id_11, string id_12, string id_13, string id_14, string id_15, string id_16, string id_17)
        {
            decimal[] tmonto = { Convert.ToDecimal(monto_1), Convert.ToDecimal(monto_2), Convert.ToDecimal(monto_3), Convert.ToDecimal(monto_4), Convert.ToDecimal(monto_5), Convert.ToInt32(monto_6), Convert.ToDecimal(monto_7), Convert.ToDecimal(monto_8), Convert.ToDecimal(monto_9), Convert.ToDecimal(monto_10), Convert.ToDecimal(monto_11), Convert.ToDecimal(monto_12), Convert.ToDecimal(monto_13), Convert.ToDecimal(monto_14), Convert.ToDecimal(monto_15), Convert.ToDecimal(monto_16), Convert.ToDecimal(monto_17) };

            int[] id_suc = { Convert.ToInt32(id_1), Convert.ToInt32(id_2), Convert.ToInt32(id_3), Convert.ToInt32(id_4), Convert.ToInt32(id_5), Convert.ToInt32(id_6), Convert.ToInt32(id_7), Convert.ToInt32(id_8), Convert.ToInt32(id_9), Convert.ToInt32(id_10), Convert.ToInt32(id_11), Convert.ToInt32(id_12), Convert.ToInt32(id_13), Convert.ToInt32(id_14), Convert.ToInt32(id_15), Convert.ToInt32(id_16), Convert.ToInt32(id_17) };

            var grupdot = db.Tb_DotGrupo.Max(x => x.Int_IdGrupo);

            int sumgrup = Convert.ToInt32(grupdot);
            sumgrup = sumgrup + 1;

            int a = 1;
            for (int id = 0; id < 16; id++)
            {

                if (tmonto[id] != 0)
                {
                    SqlDataAdapter caja = new SqlDataAdapter("insert into dbo.Tb_Dotaciones (Lng_IdSucursal, Dbl_Monto, Int_IdMoneda, Fec_Registro, Int_Idusuario,Int_EstatusDot) values ( " + id_suc[id] + "," + tmonto[id] + " , 7 , GETDATE() ," + User.Identity.Name + ", 1 )", con);
                    DataSet insert = new DataSet();
                    caja.Fill(insert);
                    insert.Reset();


                    SqlDataAdapter saldo = new SqlDataAdapter("insert into Tb_SalidaSuc (Dbl_SaldoSalida, Fec_Fin, Int_IdMoneda, int_IdSucursal, Bol_Activo , Int_Idusuario, Int_IdTurno, Int_estatus, Txt_Motivo) values (" + tmonto[id] + ",GETDATE(),7,"+ id_suc[id] + ",1," + User.Identity.Name + ",1,1,'')", con);
                    DataSet sucursal = new DataSet();
                    saldo.Fill(sucursal);
                    sucursal.Reset();

                    var iddotaciones = db.Tb_Dotaciones.Max(x => x.Lng_IdDotacion);
                    var idsalidasuc = db.Tb_SalidaSuc.Max(x => x.Lng_IdSalida);


                    SqlDataAdapter tbpaso = new SqlDataAdapter("insert into [Tb_DotSal] (Lng_IdDotacion , Lng_IdSalida) values (" + iddotaciones + " ," + idsalidasuc + " )", con);
                    DataSet ids = new DataSet();
                    tbpaso.Fill(ids);
                    ids.Reset();

                    SqlDataAdapter tbdot = new SqlDataAdapter("insert into Tb_DotGrupo (Lng_IdDotacion , Int_IdGrupo, Bol_Activo) values ( " + iddotaciones + " ," + sumgrup + ", 0  ) ", con);
                    DataSet iddot = new DataSet();
                    tbdot.Fill(iddot);
                    iddot.Reset();

                 

                }


                a = a + 1;
            }



            return RedirectToAction("indexsalidas", "salidasuc");
        }

        // GET: sucadmin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Sucursal tb_Sucursal = await db.Tb_Sucursal.FindAsync(id);
            if (tb_Sucursal == null)
            {
                return HttpNotFound();
            }
            return View(tb_Sucursal);
        }

        // GET: sucadmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: sucadmin/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Lng_IdSucursal,Int_IdPlaza,Txt_Sucursal,Txt_Direccion")] Tb_Sucursal tb_Sucursal)
        {
            if (ModelState.IsValid)
            {
                db.Tb_Sucursal.Add(tb_Sucursal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(tb_Sucursal);
        }

        // GET: sucadmin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Sucursal tb_Sucursal = await db.Tb_Sucursal.FindAsync(id);
            if (tb_Sucursal == null)
            {
                return HttpNotFound();
            }
            return View(tb_Sucursal);
        }

        // POST: sucadmin/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Lng_IdSucursal,Int_IdPlaza,Txt_Sucursal,Txt_Direccion")] Tb_Sucursal tb_Sucursal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb_Sucursal).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(tb_Sucursal);
        }

        // GET: sucadmin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tb_Sucursal tb_Sucursal = await db.Tb_Sucursal.FindAsync(id);
            if (tb_Sucursal == null)
            {
                return HttpNotFound();
            }
            return View(tb_Sucursal);
        }

        // POST: sucadmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Tb_Sucursal tb_Sucursal = await db.Tb_Sucursal.FindAsync(id);
            db.Tb_Sucursal.Remove(tb_Sucursal);
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
