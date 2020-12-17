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
using System.Data.SqlClient;
using System.Web.Helpers;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebApplication2.Controllers
{


    public class SalidaSucController : Controller
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
        // GET: SalidaSuc
        public ActionResult Index()
        {
            modulodivisas();
            return View();
        }


        public JsonResult dtr()
        {
            bool a = true;
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            int turno = Convert.ToInt32(Session["turno"]);
            int usuario = Convert.ToInt32(User.Identity.Name);

            DateTime fecha = Convert.ToDateTime(Session["fecha"]);
            DateTime f1 = fecha.AddHours(23);
            DateTime f2 = fecha.AddDays(-2);

            List<Salidas> querty;
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                querty = (from c in dr.Tb_SalidaSuc
                          join cb in dr.Ct_Moneda on c.Int_IdMoneda equals cb.Int_IdMoneda
                          where c.int_IdSucursal == sucursalid && c.Int_IdTurno == turno && c.Bol_Activo == a && c.Fec_Fin > f2 && c.Fec_Fin < f1 
                          select new Salidas
                          {
                              Int_IdMoneda = cb.Int_IdMoneda,
                              Lng_IdSalida = c.Lng_IdSalida,
                              Dbl_SaldoSalida = c.Dbl_SaldoSalida,
                              Int_estatus = c.Int_estatus,
                              Txt_Moneda = cb.Txt_Moneda
                          }).ToList();
            }

            return Json(querty, JsonRequestBehavior.AllowGet);
        }

        public ActionResult indexsalidas()
        {
            modulodivisas();
            sucursales();
            return View();
        }

        [HttpPost]
        public JsonResult obthistorial(DateTime fecha1)
        {

            DateTime fecha2 = fecha1.AddHours(23).AddMinutes(59).AddSeconds(59);

            List<Salidas> querty;
            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                querty = (from c in dr.Tb_SalidaSuc
                          join cb in dr.Ct_Moneda on c.Int_IdMoneda equals cb.Int_IdMoneda
                          join cc in dr.Tb_Usuarios on c.Int_Idusuario equals cc.Int_Idusuario
                          join cd in dr.Tb_Sucursal on c.int_IdSucursal equals cd.Lng_IdSucursal
                          where c.Fec_Fin > fecha1 && c.Fec_Fin < fecha2
                          select new Salidas
                          {
                              Int_IdMoneda = cb.Int_IdMoneda,
                              Lng_IdSalida = c.Lng_IdSalida,
                              Dbl_SaldoSalida = c.Dbl_SaldoSalida,
                              Int_estatus = c.Int_estatus,
                              Txt_Moneda = cb.Txt_Moneda,
                              Txt_sucursal = cd.Txt_Sucursal,
                              Txt_Motivo = c.Txt_Motivo,
                              username = cc.Txt_Nombre + cc.Txt_Apellido,
                              fecha = c.Fec_Fin

                          }).ToList();
            }


            return Json(querty, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult status(int idregistro, string mtc, int estatus)
        {
            int idturno = Convert.ToInt32(Session["turno"]);
            int sucursal = Convert.ToInt32(Session["idSucursal"]);


            SqlDataAdapter caja = new SqlDataAdapter("UPDATE [dbo].[Tb_SalidaSuc] SET [Int_Estatus] = " + estatus + " ,[Txt_Motivo] = ' " + mtc + " ', [Bol_Activo] = 1 where  Lng_IdSalida = " + idregistro + " ", con);
            DataSet a = new DataSet();
            caja.Fill(a);
            a.Reset();

            SqlDataAdapter historial = new SqlDataAdapter("INSERT INTO [dbo].[Tb_HistorialEntySal] ([Lng_IdEntrada], [Lng_IdSalida], [Int_Sucursal],[Int_Estatus], [Fec_Creacion], [Int_IdUsuario], [Int_IdTurno]) VALUES ( ' ' ," + idregistro + " ,  " + sucursal + ", " + estatus + " ,GETDATE()," + User.Identity.Name + " , " + idturno + ")", con);
            DataSet h = new DataSet();
            historial.Fill(h);
            h.Reset();


            return View();
        }


        // POST: SalidaSuc/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([Bind(Include = "Lng_IdSalida,Dbl_SaldoSalida,Fec_Fin,Int_IdMoneda,int_IdSucursal,Int_Idusuario,Int_IdTurno, Int_estatus, Bol_Activo")] Tb_SalidaSuc tb_SalidaSuc)
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            int turno = Convert.ToInt32(Session["turno"]);
            int usuario = Convert.ToInt32(User.Identity.Name);


            modulodivisas();
            if (ModelState.IsValid)
            {
                db.Tb_SalidaSuc.Add(tb_SalidaSuc);
                int id = tb_SalidaSuc.Lng_IdSalida;
                await db.SaveChangesAsync();


                SqlDataAdapter historial = new SqlDataAdapter("INSERT INTO [dbo].[Tb_HistorialEntySal] ([Lng_IdEntrada], [Lng_IdSalida], [Int_Sucursal],[Int_Estatus], [Fec_Creacion], [Int_IdUsuario], [Int_IdTurno]) VALUES ('', " + id + ", " + sucursalid + ", " + tb_SalidaSuc.Int_estatus + " , GETDATE(), " + usuario + ", " + turno + ")", con);
                DataSet h = new DataSet();
                historial.Fill(h);
                h.Reset();

                return RedirectToAction("Index");
            }

            return View(tb_SalidaSuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> indexsalidas([Bind(Include = "Lng_IdSalida,Dbl_SaldoSalida,Fec_Fin,Int_IdMoneda,int_IdSucursal,Int_Idusuario,Int_IdTurno, Int_estatus, Bol_Activo")] Tb_SalidaSuc tb_SalidaSuc)
        {
            int sucursalid = Convert.ToInt32(Session["idSucursal"]);
            int turno = Convert.ToInt32(Session["turno"]);
            int usuario = Convert.ToInt32(User.Identity.Name);


            modulodivisas();
            sucursales();
            if (ModelState.IsValid)
            {
                db.Tb_SalidaSuc.Add(tb_SalidaSuc);
                int id = tb_SalidaSuc.Lng_IdSalida;
                await db.SaveChangesAsync();


                SqlDataAdapter historial = new SqlDataAdapter("INSERT INTO [dbo].[Tb_HistorialEntySal] ([Lng_IdEntrada], [Lng_IdSalida], [Int_Sucursal],[Int_Estatus], [Fec_Creacion], [Int_IdUsuario], [Int_IdTurno]) VALUES ('', " + id + ", " + sucursalid + ", " + tb_SalidaSuc.Int_estatus + " , GETDATE(), " + usuario + ", " + turno + ")", con);
                DataSet h = new DataSet();
                historial.Fill(h);
                h.Reset();

                return RedirectToAction("indexsalidas");
            }

            modulodivisas();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult datosvr(int grupo, HttpPostedFileBase archivo)
        {
            bool a = true;

            if (archivo != null && archivo.ContentLength > 0)
            {
                byte[] temparchivo = new byte[archivo.ContentLength];
                archivo.InputStream.Read(temparchivo, 0, archivo.ContentLength);

                SqlDataAdapter caja = new SqlDataAdapter("insert into Tb_EstatusTesoreria ( Int_IdGrupo, Int_IdEstatusTesoreria, Int_IdUsuario, Fec_Dia) values ( " + grupo + ", 1, " + User.Identity.Name + ", GETDATE())", con);
                DataSet insert = new DataSet();
                caja.Fill(insert);
                insert.Reset();

                var idmax = db.Tb_EstatusTesoreria.Max(e => e.Lng_IdTesoreria);

                using (MasterExchangeEntities df = new MasterExchangeEntities())
                {
                    var content = new Tb_TicketTesoreria()
                    {
                        Lng_IdTesoreria = idmax,
                        Img_Archivo = temparchivo,
                        Fec_Dia = DateTime.Now,
                        Bol_Activo = a

                    };

                    df.Tb_TicketTesoreria.Add(content);
                    df.SaveChanges();
                }

            }
            else
            {
                return RedirectToAction("datosvr", new { error = "2" });
            }

            return View();
        }
        [HttpPost]
        public JsonResult agrupar(DateTime fe1, DateTime fe2)
        {

            string ahora = DateTime.Now.ToString("yyyy'-'MM'-'dd HH:mm:ss");
                

            string JSONresult;

            if (fe1 != null && fe2 != null)
            {

                DateTime f1 = Convert.ToDateTime(fe1);

                DateTime f2 = Convert.ToDateTime(fe2);

                string fecha_ini = f1.ToString("yyyy'-'MM'-'dd HH:mm:ss");

                string fecha_fin = f2.ToString("yyyy'-'MM'-'dd HH:mm:ss");


                SqlDataAdapter da = new SqlDataAdapter("select c.Txt_Nombre as 'Usuario' ,SUM(a.Dbl_Monto) as total, max(a.Fec_Registro) as fecha, b.Int_IdGrupo as Grupo from Tb_Dotaciones as a inner join Tb_DotGrupo as b on a.Lng_IdDotacion = b.Lng_IdDotacion inner join Tb_Usuarios as c on a.Int_Idusuario = c.Int_Idusuario where a.Fec_Registro between '" + fecha_ini + "' and '" + fecha_fin + "' group by c.Txt_Nombre, b.Int_IdGrupo", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                JSONresult = JsonConvert.SerializeObject(dt);

            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select c.Txt_Nombre as 'Usuario' ,SUM(a.Dbl_Monto) as total, max(a.Fec_Registro) as fecha, b.Int_IdGrupo as Grupo from Tb_Dotaciones as a inner join Tb_DotGrupo as b on a.Lng_IdDotacion = b.Lng_IdDotacion inner join Tb_Usuarios as c on a.Int_Idusuario = c.Int_Idusuario where a.Fec_Registro >= '" + ahora + "' group by c.Txt_Nombre, b.Int_IdGrupo", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                JSONresult = JsonConvert.SerializeObject(dt);
            }



            return Json(JSONresult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult datosvr(string error)
        {
            ViewBag.error = error;
            return View();
        }


        public ActionResult ArqueoTesoreria()
        {
            sucursales();

            return View();
        }


  


        [HttpPost]
        public JsonResult atesoria(string sucursal, DateTime fec)
        {
            DateTime fecha = DateTime.Now;
            DateTime f1 = fecha.AddHours(-9).AddMinutes(30).AddSeconds(30);
            DateTime f2 = fecha.AddHours(9).AddMinutes(30).AddSeconds(30);
            

           
            int sucursallid = 0;
         
         
            if (sucursal != "NULL" && sucursal != "" && fec != null)
            {
                sucursallid = Convert.ToInt32(sucursal);
                f1 = fec;
                f2 = fec.AddHours(23);
                DateTime ft1 = f1.AddDays(-1);
                DateTime ft2 = ft1.AddHours(23);


             
                var arqueo = db.Tb_ArqueoTesoria.FirstOrDefault(x => x.Fec_Registro > f1 && x.Fec_Registro < f2 && x.Int_IdSucursal == sucursallid);

                if (arqueo == null)
                {
                    sucursallid = Convert.ToInt32(sucursal);

                 

                    List<arqueotesoreria> querty;
                    bool congelar = true;


                    using (MasterExchangeEntities dr = new MasterExchangeEntities())
                    {
                        querty = (from a in dr.Tb_ArqueoMoneda
                                  join ab in dr.Ct_Moneda on a.Int_IdMoneda equals ab.Int_IdMoneda
                                  where a.Fec_Cierre > ft1 && a.Fec_Cierre < ft2 && a.Bol_Congelar == congelar && a.Int_IdSucursal == sucursallid
                                  select new arqueotesoreria
                                  {

                                      idregistro = a.Lng_IdArqueoMoneda,
                                      moneda = ab.Txt_Moneda,
                                      monto = a.Dbl_Valor,
                                      fecha = a.Fec_Cierre

                                  }).ToList();

                    }

                    return Json(querty, JsonRequestBehavior.AllowGet);
                }

                else {

                    var querty = new List<arqueotesoreria>()
                    {
                        new arqueotesoreria() {
                        idregistro = 0,
                        moneda = "Ya Exite un Arqueo Para Esta Sucursal"
                        }
                    };

                    return Json(querty, JsonRequestBehavior.AllowGet);
                }
               
            }
            else
            {
                List<arqueotesoreria> querty;
                bool congelar = true;


                using (MasterExchangeEntities dr = new MasterExchangeEntities())
                {
                    querty = (from a in dr.Tb_ArqueoMoneda
                              join ab in dr.Ct_Moneda on a.Int_IdMoneda equals ab.Int_IdMoneda
                              where a.Fec_Cierre > f1 && a.Fec_Cierre < f2 && a.Bol_Congelar == congelar && a.Int_IdSucursal == sucursallid
                              select new arqueotesoreria
                              {

                                  idregistro = a.Lng_IdArqueoMoneda,
                                  moneda = ab.Txt_Moneda,
                                  monto = a.Dbl_Valor,
                                  fecha = a.Fec_Cierre

                              }).ToList();

                }

                return Json(querty, JsonRequestBehavior.AllowGet);

            }

            


            
        }


        [HttpPost]
        public ActionResult postarqueo(decimal post1, decimal post2, decimal post3, decimal post4, decimal post5, decimal post6 , decimal cast1, decimal cast2, decimal cast3, decimal cast4, decimal cast5, decimal cast6, int registro1, int registro2, int registro3, int registro4, int registro5, int registro6, int idsucursal, int check1,  int check2, int check3, int check4, int check5, int check6 )
        {
            decimal[] tpost = { post1, post2, post3, post4, post5, post6 };
            decimal[] tcast = { cast1, cast2, cast3, cast4, cast5, cast6 };
            int[] tcheck = { check1, check2, check3, check4 , check5, check6 };
            int[] registros = { registro1, registro2, registro3, registro4, registro5, registro6 };

            var monedas = db.Ct_Moneda.Max(x => x.Int_IdMoneda);

          
            int a = 1;

            for (int id = 0; id < monedas; id++)
            {
                if (tpost[id] != 7)
                {
                    SqlDataAdapter caja = new SqlDataAdapter("insert into Tb_ArqueoTesoria ( Dbl_Monto, Dbl_MontoSuc, Int_IdMoneda, Bol_Ok , Int_IdUsuario, Int_IdSucursal, Fec_Registro, Int_IdEstatusArq, Lng_IdArqueoMoneda) values ( "+ tpost[id]+" , "+tcast[id]+" , "+a+", "+tcheck[id]+" , "+ User.Identity.Name +", "+ idsucursal + " ,GETDATE(), 1, "+ registros[id] + " )", con);
                    DataSet insert = new DataSet();
                    caja.Fill(insert);
                    insert.Reset();
                }
                a = a + 1;
            }

            return View();
        }


        public ActionResult vistaTesoreria()
        {
            sucursales();

            return View();
        }

        public JsonResult ObtVista(string sucursal, DateTime fec)
        {
     
            bool congelar = true;
            DateTime f0 = fec;

            DateTime fecha = fec;
            DateTime f1 = fec.AddHours(22);

            int sucursallid = 0;
            if (sucursal != "NULL" && sucursal != "")
            {
                sucursallid = Convert.ToInt32(sucursal);
            }

            List<arqueotesoreria> querty;

            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {

                querty = (from a in dr.Tb_ArqueoMoneda
                          join b in dr.Tb_ArqueoTesoria on a.Lng_IdArqueoMoneda equals b.Lng_IdArqueoMoneda
                          join c in dr.Ct_Moneda on a.Int_IdMoneda equals c.Int_IdMoneda
                          join d in dr.Tb_Usuarios on b.Int_IdUsuario equals d.Int_Idusuario
                          where b.Fec_Registro > f0  && b.Fec_Registro < f1 && a.Bol_Congelar == congelar && b.Int_IdSucursal == sucursallid
                          select new arqueotesoreria
                          { 
                             moneda = c.Txt_Moneda,
                             monto = b.Dbl_Monto,
                             montosuc = b.Dbl_MontoSuc,
                             Bol_Ok = b.Bol_Ok,
                             fecha = b.Fec_Registro,
                             nusuario = d.Txt_Nombre,
                             pusuario = d.Txt_Apellido 
                          
                          }).ToList();
            }
                return Json(querty, JsonRequestBehavior.AllowGet);
        }


        public JsonResult cancelaciontickets(string id, string mnsn)
        {
            try
            {
                int estatus = 500;
                int idgrupo = Convert.ToInt32(id);

                SqlDataAdapter caja = new SqlDataAdapter("update Tb_DotGrupo set Bol_Activo = 1, Txt_Motivo = '" + mnsn + "' where Int_IdGrupo =  " + idgrupo + " ", con);
                DataSet insert = new DataSet();
                caja.Fill(insert);
                insert.Reset();

                return Json(estatus, JsonRequestBehavior.AllowGet);

            }
            catch (Exception  er)
            {
                string error = Convert.ToString(er);
                   
                return Json(error, JsonRequestBehavior.AllowGet);

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
