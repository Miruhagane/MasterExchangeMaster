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
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Configuration;

namespace WebApplication2.Controllers
{
    public class TasasController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();
        string con = ConfigurationManager.ConnectionStrings["server2"].ConnectionString;

        public void plazas()
        {


            List<Sucursal> lst = null;
            using (MasterExchangeEntities sc = new MasterExchangeEntities())
            {
                lst = (
                       from s in sc.Ct_Plazas
                       select new Sucursal
                       {
                           Int_IdPlaza = s.Int_IdPlazas,
                           Txt_Sucursal = s.Txt_Plazas

                       }).ToList();
            }

            List<SelectListItem> suc = lst.ConvertAll(df =>
            {
                return new SelectListItem()
                {
                    Text = df.Txt_Sucursal.ToString(),
                    Value = df.Int_IdPlaza.ToString(),
                    Selected = false
                };
            });

            ViewBag.suc = suc;
        }


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
        // GET: Tasas
        public ActionResult Index()
        {
            plazas();

            return View();
        }

        public ActionResult ventas()
        {
            plazas();

            return View();
        }

        public ActionResult tsucursales()
        {
            sucursales();

            return View();
        }

        public JsonResult comprastr(int idsucursal)
        {
            int id = 1;
            if (idsucursal != 0)
            {
                id = idsucursal;
            }

            string JSONresult;

            SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT a.Int_IdMoneda AS IdTaxa, b.Txt_Moneda AS Moneda, a.dbl_Valor AS Valor, a.Fec_Dia AS Dia, a.Lng_IdTaxa, b.Int_IdMoneda FROM dbo.Tb_Taxas AS a INNER JOIN dbo.Ct_Moneda AS b ON a.Int_IdMoneda = b.Int_IdMoneda inner join dbo.Tb_TaxSuc as c on a.Lng_IdTaxa = c.Lng_IdTaxSuc where a.Int_IdMoneda <> 7 and a.Lng_IdTaxa in (SELECT TOP (7) Lng_IdTaxa from Tb_Taxas as d inner join dbo.Tb_TaxSuc as c on d.Lng_IdTaxa = c.Lng_IdTaxSuc where Bol_Tipo = 0 and c.Lng_IdSucursal = " + id + " order by 1 desc) and a.Bol_Tipo = 0 order by 1", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            JSONresult = JsonConvert.SerializeObject(dt);

            return Json(JSONresult, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ventastr(int idsucursal)
        {
            int id = 1;
            if (idsucursal != 0)
            {
                id = idsucursal;
            }

            string JSONresult;

            SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT a.Int_IdMoneda AS IdTaxa, b.Txt_Moneda AS Moneda, a.dbl_Valor AS Valor, a.Fec_Dia AS Dia, a.Lng_IdTaxa, b.Int_IdMoneda FROM dbo.Tb_Taxas AS a INNER JOIN dbo.Ct_Moneda AS b ON a.Int_IdMoneda = b.Int_IdMoneda inner join dbo.Tb_TaxSuc as c on a.Lng_IdTaxa = c.Lng_IdTaxSuc where a.Int_IdMoneda <> 7 and a.Lng_IdTaxa in (SELECT TOP (7) Lng_IdTaxa from Tb_Taxas as d inner join dbo.Tb_TaxSuc as c on d.Lng_IdTaxa = c.Lng_IdTaxSuc where Bol_Tipo = 1 and c.Lng_IdSucursal = " + id + " order by 1 desc) and a.Bol_Tipo = 1 order by 1", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            JSONresult = JsonConvert.SerializeObject(dt);


            return Json(JSONresult, JsonRequestBehavior.AllowGet);

        }

        public JsonResult checksuc()
        {

            List<Sucursal> lst;
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                lst = (from a in dr.Tb_Sucursal
                       select new Sucursal
                       {
                           Lng_IdSucursal = a.Lng_IdSucursal,
                           Int_IdPlaza = a.Int_IdPlaza,
                           Txt_Sucursal = a.Txt_Sucursal

                       }).ToList();
            }

            return Json(lst, JsonRequestBehavior.AllowGet);
        }



        // GET: Tasas/Create
        public ActionResult Create()
        {
            return View();
        }

        public JsonResult tasacompra(int idsuc, int idmoneda, string valor)
        {
            int k = 1;
            try
            {
                SqlDataAdapter taxas = new SqlDataAdapter("insert into Tb_Taxas (Int_IdMoneda , dbl_Valor, Bol_Tipo, Fec_Dia, Fec_Vencimiento, Int_IdGrupo) values(" + idmoneda + ", " + valor + ", 0, GETDATE(), GETDATE(), 1)", con);
                DataSet a = new DataSet();
                taxas.Fill(a);
                a.Reset();

                SqlDataAdapter taxsuc = new SqlDataAdapter("insert into Tb_TaxSuc (Lng_IdSucursal, Int_IdGrupo) values (" + idsuc + ",1)", con);
                DataSet b = new DataSet();
                taxsuc.Fill(b);
                b.Reset();

                return Json(k, JsonRequestBehavior.AllowGet);

            }
            catch (Exception er)
            {

                string ms = Convert.ToString(er);

                return Json(ms, JsonRequestBehavior.AllowGet);

            }
   
        }

        public JsonResult tasaventa(int idsuc, int idmoneda, string valor)
        {
            int k = 1;
            try
            {
                SqlDataAdapter taxas = new SqlDataAdapter("insert into Tb_Taxas (Int_IdMoneda , dbl_Valor, Bol_Tipo, Fec_Dia, Fec_Vencimiento, Int_IdGrupo) values(" + idmoneda + ", " + valor + ", 1, GETDATE(), GETDATE(), 2)", con);
                DataSet a = new DataSet();
                taxas.Fill(a);
                a.Reset();

                SqlDataAdapter taxsuc = new SqlDataAdapter("insert into Tb_TaxSuc (Lng_IdSucursal, Int_IdGrupo) values (" + idsuc + ",2)", con);
                DataSet b = new DataSet();
                taxsuc.Fill(b);
                b.Reset();

                return Json(k, JsonRequestBehavior.AllowGet);

            }
            catch (Exception er)
            {

                string ms = Convert.ToString(er);

                return Json(ms, JsonRequestBehavior.AllowGet);

            }

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
