using Microsoft.VisualBasic.ApplicationServices;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Ajax.Utilities;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Data;
using System;
using WebApplication2.Models.ViewModels;
using System.Collections.Generic;

namespace WebApplication2.Controllers
{
  
    public class HomeController : Controller
    {
        string con = System.Configuration.ConfigurationManager.ConnectionStrings["server2"].ConnectionString;

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public void sucursales()
        {

            List<Sucursal> lst = null;
            using (Models.MasterExchangeEntities dr = new Models.MasterExchangeEntities())
            {
                lst = (
                     from d in dr.Tb_Sucursal
                     orderby d.Lng_IdSucursal
                     select new Sucursal
                     {
                         Lng_IdSucursal = d.Lng_IdSucursal,
                         Txt_Sucursal = d.Txt_Sucursal

                     }).ToList();
            }


            List<SelectListItem> items = lst.ConvertAll(df =>
            {
                return new SelectListItem()
                {
                    Text = df.Txt_Sucursal.ToString(),
                    Value = df.Lng_IdSucursal.ToString(),
                    Selected = false

                };

            });

            ViewBag.items = items;

        }

        private class Elementos1
        {
            public Nullable<int> value { get; set; }
            public Nullable<decimal> Text { get; set; }
        }

        public ActionResult Index(string Message)
        {
            sucursales();
            ViewBag.Message = Message;

        
            return View();
        }

       [HttpPost]
        public JsonResult datosusuario(string usuario, string Pass)
        {
            List<users> querty;
            using (MasterExchangeEntities da = new MasterExchangeEntities())
            {

                querty = (from a in da.Tb_Usuarios
                          join ab in da.Tb_Sucursal on a.Lng_IdSucursal equals ab.Lng_IdSucursal
                          where a.Txt_NomCorto == usuario && a.Txt_Password == Pass
                          select new users
                          {
                              Int_Idusuario = a.Int_Idusuario,
                              Txt_Nombre = a.Txt_Nombre,
                              Txt_Apellido = a.Txt_Apellido,
                              Lng_IdSucursal = ab.Lng_IdSucursal,
                              txt_sucursal = ab.Txt_Sucursal,
                              Int_IdArea = a.Int_IdArea
                              

                          }).ToList();

            }

            return Json(querty, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult actualizarsucursal(int idusuario , int idsucursal)
        {
            string msn = "";

            try
            {
                SqlDataAdapter caja = new SqlDataAdapter("update Tb_Usuarios set Lng_IdSucursal = "+idsucursal+" where Int_Idusuario = "+idusuario+"", con);
                DataSet a = new DataSet();
                caja.Fill(a);

                msn = "1";

                return Json(msn, JsonRequestBehavior.AllowGet);
            }

            catch (Exception err)
            {
                string error = Convert.ToString(err);
                msn = "2";
                return Json(msn, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult Index(string usuario, string Pass, string turno, DateTime fecha)
        {
         
           
            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(Pass))
            {

                       //validacion de usuario
               MasterExchangeEntities db = new MasterExchangeEntities();


           
                var userv = db.Tb_Usuarios.FirstOrDefault(e => e.Txt_NomCorto == usuario && e.Txt_Password == Pass && e.Bol_Activo == 0);

                

                Session["fecha"] = fecha;
                Session["turno"] = turno;

                DateTime fecha1 = Convert.ToDateTime(fecha);
                DateTime fecha2 = fecha1.AddHours(23);

                if (userv != null)
                {




                    // sesion de usuario 
                    Session["idSucursal"] = userv.Lng_IdSucursal;
                    //obtener los valores del usuario
                    string id = userv.Int_Idusuario.ToString();
                    Session["idusers"] = userv.Int_Idusuario; 
                    int? tipo = userv.Int_IdArea;
                    int? Sucursal = userv.Lng_IdSucursal;
                    string suc = userv.Lng_IdSucursal.ToString();
                    

                    //obtener el area del usuario
                    var area = db.Ct_Areas.FirstOrDefault(a => a.Int_IdArea == tipo);
                    string areausuario = area.Txt_Area;

                    //obtener sucursal del usuario

                    var Idsucursal = db.Tb_Sucursal.FirstOrDefault(s => s.Lng_IdSucursal == Sucursal);
                    string Sucursalusuario = Idsucursal.Txt_Sucursal;
                    string direccion = Idsucursal.Txt_Direccion;

                    //obtener plaza de la sucursal
                    int? idplazasucursal = Idsucursal.Int_IdPlaza;
                    var idplaza = db.Ct_Plazas.FirstOrDefault(p => p.Int_IdPlazas == idplazasucursal);
                    string plazadelusuario = idplaza.Txt_Plazas;
                    //guardar valores de sesion del usuario
                    Session["usuario"] = userv.Txt_Nombre;
                    Session["apellido"] = userv.Txt_Apellido;
                    Session["cortoName"] = userv.Txt_NomCorto;
                    Session["area"] = areausuario;
                    Session["intareadeusurio"] = tipo;
                    Session["sucursaledelusuario"] = Sucursalusuario;
                    Session["direccion"] = direccion;
                    Session["plazadelusuaio"] = plazadelusuario;

                    FormsAuthentication.SetAuthCookie(id, true);

                    

                    bool activo = false;
                    bool status = true;

                    int idturno = Convert.ToInt32(turno);
                    var dbturno = db.Tb_Arqueo.FirstOrDefault(c => c.Int_IdUsuario == userv.Int_Idusuario && c.Int_IdStatus == 1 && c.Int_IdSucursal == userv.Lng_IdSucursal && c.Bol_Congelar == activo);

                    var userval = (from c in db.Tb_EntradaSuc
                                   join ca in db.Tb_EntEmp on c.Lng_IdEntrada equals ca.Lng_IdEntrada
                                   where c.Int_Sucursal == Sucursal && c.Bol_Activo == status && ca.Int_IdTurno == idturno && ca.Int_IdUsuario == userv.Int_Idusuario && c.Fec_Ini > fecha1 && c.Fec_Ini < fecha2
                                   select c
                                   ).FirstOrDefault();


                        var denominacion = db.Ct_Denominaciones.Max(c => c.Int_IdDenominacion);
                        int iddeno = Convert.ToInt32(denominacion);

                    if (userv.Int_IdArea == 1)
                    {
                        return RedirectToAction("Index", "Profile");
                    }

                

                    else if (dbturno == null)
                    {
                        int valor = 1;

                        for (int i = 0; i < iddeno; i++)
                        {
                            SqlDataAdapter caja = new SqlDataAdapter("INSERT INTO [dbo].[Tb_Arqueo] ([Int_IdDenomicacion],[Dbl_Cantidad] ,[Fec_Cierre] ,[Int_IdMoneda] ,[Int_IdStatus],[Int_IdTurno],[Int_IdSucursal],[Int_IdUsuario], [Bol_Congelar])  VALUES(" + valor + ",0, GETDATE(),7,1," + turno + "," + userv.Lng_IdSucursal + "," + userv.Int_Idusuario + ", 0)", con);
                            DataSet a = new DataSet();
                            caja.Fill(a);
                            a.Reset();

                            valor = valor + 1;
                        }
                        valor = 1;

                        for (int i = 0; i < 6; i++)
                        {
                            SqlDataAdapter arqdiv = new SqlDataAdapter("INSERT INTO [dbo].[Tb_ArqueoMoneda] ([Int_IdMoneda] ,[Dbl_Valor] ,[Fec_Cierre] ,[Int_IdStatus] ,[Int_IdTurno] ,[Int_IdSucursal] ,[Int_IdUsuario] ,[Bol_Congelar])VALUES (" + valor + ",0,GETDATE(),1," + turno + "," + userv.Lng_IdSucursal + "," + userv.Int_Idusuario + ",0)", con);
                            DataSet a = new DataSet();
                            arqdiv.Fill(a);
                            a.Reset();

                            valor = valor + 1;
                        }


                        if (userval == null)
                        {

                            return RedirectToAction("Index", "EntradaCaja");


                        }
                        else
                        {
                            return RedirectToAction("Index", "EntradaCaja");
                        }



                    }

                    else if (userval == null)
                    {
                        return RedirectToAction("Index", "EntradaCaja");
                    }
                    else if (userval.Bol_Activo == status)
                    {
                        return RedirectToAction("Index", "Profile");
                    }



                    return RedirectToAction("Index", new { Message = "error" });



                }

                else

                {

                    return RedirectToAction("Index", new { Message = "usuario no encontrado" });

                }
            }
            else
            {
                return RedirectToAction("Index", new { Message = "llena los datos" });
                

            }

        }

       

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}