using System;
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
                     from d in dr.Taxacompcombs
                     select new moneda
                     {
                         Int_IdMoneda = d.Int_IdMoneda,
                         Moneda = d.Moneda

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

       
        public JsonResult inventario()
        {
            var ds = db.Database.ExecuteSqlCommand("select Tb_EntradaSuc.Int_IdMoneda  as IdTaxa, Ct_Moneda.Txt_Moneda as Moneda, Tb_EntradaSuc.Dbl_SaldoEntrada  as 'Valor' from dbo.Tb_EntradaSuc INNER JOIN dbo.Ct_Moneda ON Tb_EntradaSuc.Int_IdMoneda  = Ct_Moneda.Int_IdMoneda  inner join dbo.Tb_EntEmp On Tb_EntradaSuc.Lng_IdEntrada = Tb_EntEmp.Lng_IdEntrada Where(Tb_EntradaSuc.Bol_Activo = 1) and(Tb_EntradaSuc.Int_IdMoneda IN(1, 2, 3, 4, 5, 6, 7)) and Tb_EntradaSuc.Int_Sucursal = @sucursal", new SqlParameter("@sucursal", Session["idSucursal"]));
            var jsonresult1 = JsonConvert.SerializeObject(ds);     
            return Json(jsonresult1, JsonRequestBehavior.AllowGet);  
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

                SqlDataAdapter caja = new SqlDataAdapter("INSERT INTO [dbo].[Tb_EntEmp] ([Lng_IdEntrada], [Int_IdUsuario], [Int_IdTurno]) VALUES (" + tb_EntEmp.Lng_IdEntrada + " ," + User.Identity.Name + " , 1 )", con);
                DataSet a = new DataSet();
                caja.Fill(a);
                a.Reset();

                return RedirectToAction("Index");
            }
            modulodivisas();
            return View(tb_EntradaSuc);
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
