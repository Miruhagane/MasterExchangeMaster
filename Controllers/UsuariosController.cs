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
using System.Runtime.Remoting.Messaging;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Security;
using System.Web.UI;
using WebApplication2.Services;
using Microsoft.Ajax.Utilities;
using System.Web.ModelBinding;
using System.Security.Cryptography;
using System.Web.WebPages;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.AspNet.Identity;
using System.Data.Entity.SqlServer;
using System.Globalization;
using Microsoft.VisualBasic;
using System.Data.Entity.Infrastructure;
using System.Buffers.Text;
using EntityState = System.Data.Entity.EntityState;
using System.Web.Helpers;
using System.Web.Razor.Generator;
using System.IO;
using System.Drawing.Imaging;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private MasterExchangeEntities db = new MasterExchangeEntities();

        string con = System.Configuration.ConfigurationManager.ConnectionStrings["server2"].ConnectionString;
        // obtener areas
        public void arear()
        {
            List<Areas> area = null;

            using (MasterExchangeEntities dr = new MasterExchangeEntities())
            {
                area = (from a in dr.Ct_Areas
                        select new Areas
                        {
                            Int_IdArea = a.Int_IdArea,
                            Txt_Area = a.Txt_Area
                        }).ToList();
            }

            List<SelectListItem> areas = area.ConvertAll(df =>
            {
                return new SelectListItem()
                {
                    Text = df.Txt_Area.ToString(),
                    Value = df.Int_IdArea.ToString(),
                    Selected = false
                };
            });

            ViewBag.areas = areas;
        }

        // obtener plazas
        public void Plazas()
        {
            List<Plazas> plazas = null;
            using (MasterExchangeEntities br = new MasterExchangeEntities())
            {
                plazas = (from b in br.Ct_Plazas
                          select new Plazas
                          {
                              Int_IdPlazas = b.Int_IdPlazas,
                              Txt_Plazas = b.Txt_Plazas
                          }).ToList();
            }

            List<SelectListItem> plaza = plazas.ConvertAll(dp =>
            {
                return new SelectListItem()
                {
                    Text = dp.Txt_Plazas.ToString(),
                    Value = dp.Int_IdPlazas.ToString(),
                    Selected = false
                };
            });

            ViewBag.plaza = plaza;
        }



        public ActionResult actualizaruser()
        {

            return View();
        }


        public JsonResult obtusersuc()
        {
            List<users> querty;
            using (MasterExchangeEntities da = new MasterExchangeEntities())
            {

                querty = (from a in da.Tb_Usuarios
                          join ab in da.Tb_Sucursal on a.Lng_IdSucursal equals ab.Lng_IdSucursal
                          where a.Int_IdArea == 2 && a.Bol_Activo == 0
                          select new users
                          {
                              Int_Idusuario = a.Int_Idusuario,
                              Txt_Nombre = a.Txt_Nombre,
                              Txt_Apellido = a.Txt_Apellido,
                              Lng_IdSucursal = ab.Lng_IdSucursal,
                              txt_sucursal = ab.Txt_Sucursal

                          }).ToList();

            }
            return Json(querty, JsonRequestBehavior.AllowGet);
        }



        public JsonResult obtsuc()
        {
            bool activo = false;

            List<Elementos> lst = new List<Elementos>();
            using (MasterExchangeEntities sc = new MasterExchangeEntities())
            {
                lst = (from a in sc.Tb_Sucursal
                       where a.Bol_Activo == activo
                       select new Elementos
                       {
                           value = a.Lng_IdSucursal,
                           Text = a.Txt_Sucursal

                       }).ToList();
            }

            return Json(lst, JsonRequestBehavior.AllowGet);

        }


        // obtener sucursales
        [HttpGet]
        public JsonResult sucursales(int int_plaza)
        {
            bool activo = false;

            List<Elementos> sucursales = new List<Elementos>();
            using (MasterExchangeEntities bd = new MasterExchangeEntities())
            {
                sucursales = (from c in bd.Tb_Sucursal
                              where c.Int_IdPlaza == int_plaza && c.Bol_Activo == activo
                              select new Elementos
                              {
                                  value = c.Lng_IdSucursal,
                                  Text = c.Txt_Sucursal

                              }).ToList();
            }

            return Json(sucursales, JsonRequestBehavior.AllowGet);
        }


        private class Elementos
        {
            public int value { get; set; }
            public string Text { get; set; }
        }

        [HttpGet]
        public ActionResult Perfil(string Usuario)
        {
            if (Usuario != null)
            {
                var tbuser = from tabla in db.Tb_Usuarios select tabla;



                if (!string.IsNullOrEmpty(Usuario))
                {
                    tbuser = tbuser.Where(tabla => tabla.Txt_Nombre.Contains(Usuario) || tabla.Txt_NomCorto.Contains(Usuario) || tabla.Txt_email.Contains(Usuario));

                }

                //retornar valores de usuario pra inner join
                var Userindex = db.Tb_Usuarios.FirstOrDefault(e => e.Txt_Nombre == Usuario || e.Txt_NomCorto == Usuario || e.Txt_email == Usuario);
                int? areausuario = Userindex.Int_IdArea;
                int? Idsucursal = Userindex.Lng_IdSucursal;
                int? idplaza = Userindex.Int_IdPlaza;
                ViewBag.iduser = Userindex.Int_Idusuario;

                //obtener area del usuario
                var Obtarea = db.Ct_Areas.FirstOrDefault(a => a.Int_IdArea == areausuario);
                string areauser = Obtarea.Txt_Area;

                //Obtener plaza del usuario
                var obtplaza = db.Ct_Plazas.FirstOrDefault(b => b.Int_IdPlazas == idplaza);
                string plazauser = obtplaza.Txt_Plazas;

                //obtener sucursal del usuario 
                var obtsucursal = db.Tb_Sucursal.FirstOrDefault(c => c.Lng_IdSucursal == Idsucursal);
                string sucursaluser = obtsucursal.Txt_Sucursal;

                ViewBag.sucursal = sucursaluser;
                ViewBag.area = areauser;
                ViewBag.plaza = plazauser;

                return View(tbuser.ToList());

            }




            return View();

        }

        // Obtener Perfil de Usuario
        public ActionResult Index(string Usuario)
        { if (Usuario != null)
            {
                var tbuser = from tabla in db.Tb_Usuarios select tabla;



                if (!string.IsNullOrEmpty(Usuario))
                {
                    tbuser = tbuser.Where(tabla => tabla.Txt_Nombre.Contains(Usuario) || tabla.Txt_NomCorto.Contains(Usuario) || tabla.Txt_email.Contains(Usuario));

                }

                //retornar valores de usuario pra inner join
                var Userindex = db.Tb_Usuarios.FirstOrDefault(e => e.Txt_Nombre == Usuario || e.Txt_NomCorto == Usuario || e.Txt_email == Usuario);

                if (Userindex != null)
                {
                    int? areausuario = Userindex.Int_IdArea;
                    int? Idsucursal = Userindex.Lng_IdSucursal;
                    int? idplaza = Userindex.Int_IdPlaza;
                    ViewBag.iduser = Userindex.Int_Idusuario;

                    //obtener area del usuario
                    var Obtarea = db.Ct_Areas.FirstOrDefault(a => a.Int_IdArea == areausuario);
                    string areauser = Obtarea.Txt_Area;

                    //Obtener plaza del usuario
                    var obtplaza = db.Ct_Plazas.FirstOrDefault(b => b.Int_IdPlazas == idplaza);
                    string plazauser = obtplaza.Txt_Plazas;

                    //obtener sucursal del usuario 
                    var obtsucursal = db.Tb_Sucursal.FirstOrDefault(c => c.Lng_IdSucursal == Idsucursal);
                    string sucursaluser = obtsucursal.Txt_Sucursal;

                    ViewBag.sucursal = sucursaluser;
                    ViewBag.area = areauser;
                    ViewBag.plaza = plazauser;

                    return View(tbuser.ToList());
                }
                else
                {
                    ViewBag.msn = "2";
                    return View();
                }
            }


            return View();

        }



        public ActionResult convertirimg(string Usuario)
        {
            int id = Convert.ToInt32(Usuario);

            var imge = db.Tb_Usuarios.Where(x => x.Int_Idusuario == id).FirstOrDefault();

            return File(imge.Img_Foto, "image/jpg");
        }


        //Obtiene datos para Formulario de Insert
        public ActionResult Create()
        {
            arear();
            Plazas();

            return View();
        }

        // POST: Usuarios/Create

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

                string nombrecorto = tb_Usuarios.Txt_NomCorto;
                return RedirectToAction("Index", new { Usuario = nombrecorto });
            }

            arear();
            Plazas();


            return View();
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {


            arear();
            Plazas();
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

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Int_Idusuario,Txt_Nombre,Txt_Apellido,Txt_NomCorto,Txt_Password,Bol_Activo,Int_IdArea,Int_IdSubarea,Fec_Alta,Fec_Baja,Int_Ext,Num_Telefono_1,Bol_Bloqueado,Num_Telefono_2,Txt_email,Bol_Promotor,Int_IdEmpresa,Txt_Password2,Txt_Password3,Int_Idempleado,Int_IdPlaza,Int_IdDepartamentos,Img_Foto,Lng_IdSucursal")] Tb_Usuarios tb_Usuarios)
        {
            HttpPostedFileBase fileBase = Request.Files[0];
            //byte[] imgactual = null;

            Tb_Usuarios _Usuarios = new Tb_Usuarios();

            if (fileBase.ContentLength == 0)
            {
                _Usuarios = db.Tb_Usuarios.Find(tb_Usuarios.Int_Idusuario);
                tb_Usuarios.Img_Foto = _Usuarios.Img_Foto;

            }
            else {

                WebImage image = new WebImage(fileBase.InputStream);
                tb_Usuarios.Img_Foto = image.GetBytes();
            }


            if (ModelState.IsValid)
            {
                db.Entry(_Usuarios).State = EntityState.Detached;
                db.Entry(tb_Usuarios).State = EntityState.Modified;
                await db.SaveChangesAsync();


                return RedirectToAction("Index", new { Usuario = tb_Usuarios.Txt_NomCorto });
            }

            return View();

        }

 

        public ActionResult img(int id)
        {

            Tb_Usuarios imge = db.Tb_Usuarios.Find(id);
            byte[] byteimage = imge.Img_Foto;

            MemoryStream memoryStream = new MemoryStream(byteimage);
            Image image = Image.FromStream(memoryStream);
            memoryStream = new MemoryStream();

            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;


            return File(memoryStream, "image/jpg");
        }

        [HttpPost]
        public JsonResult postsuc(string userid, string sucid)
        {
            int a = 1;

            SqlDataAdapter updateuser = new SqlDataAdapter("update Tb_Usuarios set Lng_IdSucursal = "+sucid+" where Int_Idusuario = "+userid+"", con);
            DataSet set = new DataSet();
            updateuser.Fill(set);
            set.Reset();

            return Json(a, JsonRequestBehavior.AllowGet);
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
