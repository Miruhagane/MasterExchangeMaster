﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using EntityState = System.Data.Entity.EntityState;
using WebApplication2.Models.ViewModels;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Data.EntityClient;

namespace WebApplication2.Controllers
{
    public class EntradaCajaController : Controller
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

        public ActionResult invet()
        {
            bool a = true;
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);

            var idlng = db.Tb_EntradaSuc.Max(e => (int?)e.Lng_IdEntrada) ?? 0;
           
           
            IEnumerable<inventario> querty = new List<inventario>();
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                querty = (from c in dr.Tb_EntradaSuc
                          join ca in dr.Tb_EntEmp on c.Lng_IdEntrada equals ca.Lng_IdEntrada
                          join cb in dr.Ct_Moneda on c.Int_IdMoneda equals cb.Int_IdMoneda
                          where c.Int_Sucursal == sucursalid && c.Bol_Activo == a && c.Lng_IdEntrada == idlng
                          select new inventario()
                          { 
                              Int_IdMoneda = cb.Int_IdMoneda,
                              Dbl_SaldoEntrada = c.Dbl_SaldoEntrada,
                              Txt_Moneda = cb.Txt_Moneda
                          }).ToList();
            }

            return View(querty);
        }

        public ActionResult final()
        {
           
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);

            var idlng = db.Tb_SalidaSuc.Max(e => (int?)e.Lng_IdSalida) ?? 0;
            

            IEnumerable<inventario> querty = new List<inventario>();
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                querty = (from c in dr.Tb_SalidaSuc
                          join ca in dr.Tb_EntEmp on c.Lng_IdSalida equals ca.Lng_IdSalida
                          join cb in dr.Ct_Moneda on c.Int_IdMoneda equals cb.Int_IdMoneda
                          where c.int_IdSucursal == sucursalid  && c.Lng_IdSalida == idlng
                          select new inventario()
                          {
                              Int_IdMoneda = cb.Int_IdMoneda,
                              Dbl_SaldoSalido = c.Dbl_SaldoSalida,
                              Txt_Moneda = cb.Txt_Moneda
                          }).ToList();
            }
            return View(querty);
        }


        // GET: EntradaCaja
        public ActionResult Index()
        {
            modulodivisas();
            return View();

        }

        // POST: EntradaCaja/Index
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Lng_IdEntrada,Dbl_SaldoEntrada,Fec_Ini,Fec_Fin,Int_Sucursal,Int_IdMoneda,Bol_Activo")] Tb_EntradaSuc tb_EntradaSuc, Tb_EntEmp tb_EntEmp)
        {
            modulodivisas();
            if (ModelState.IsValid)
            {
                db.Tb_EntradaSuc.Add(tb_EntradaSuc);
                await db.SaveChangesAsync();

                tb_EntEmp.Lng_IdEntrada = tb_EntradaSuc.Lng_IdEntrada;

                Session["identrada"] = tb_EntradaSuc.Lng_IdEntrada;

                SqlDataAdapter caja = new SqlDataAdapter("INSERT INTO [dbo].[Tb_EntEmp] ([Lng_IdEntrada], [Int_IdUsuario], [Int_IdTurno], [Fec_Ini], [Fec_Fin]) VALUES (" + tb_EntEmp.Lng_IdEntrada + " ," + User.Identity.Name + " , 1 , GETDATE() , ' ')", con);
                DataSet a = new DataSet();
                caja.Fill(a);
                a.Reset();

                return RedirectToAction("Index");
            }
            modulodivisas();
            return View(tb_EntradaSuc);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Salida([Bind(Include = "Lng_Salida,Dbl_SaldoSalida,Fec_Fin,Int_IdMoneda,int_IdSucursal")] Tb_SalidaSuc tb_SalidaSuc)
        {
            if (ModelState.IsValid)
            {
                db.Tb_SalidaSuc.Add(tb_SalidaSuc);
                await db.SaveChangesAsync();

                SqlDataAdapter caja = new SqlDataAdapter("UPDATE [dbo].[Tb_EntEmp] SET Fec_Fin = GETDATE() WHERE Lng_IdEntEmp = " + Session["identrada"] + "", con);
                DataSet a = new DataSet();
                caja.Fill(a);
                a.Reset();

                return RedirectToAction("Index");
            }

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
