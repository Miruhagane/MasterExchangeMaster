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

        public ActionResult Index(string Message)
        {
            
            ViewBag.Message = Message;

        
            return View();
        }

        [HttpPost]
        public ActionResult Index(string usuario, string Pass)
        {
           
            if (!string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(Pass))
            {
                       //validacion de usuario
               MasterExchangeEntities db = new MasterExchangeEntities();
                var userv = db.Tb_Usuarios.FirstOrDefault(e => e.Txt_NomCorto == usuario && e.Txt_Password == Pass);



                if (userv != null)
                {
                    // sesion de usuario 
                    Session["idSucursal"] = userv.Lng_IdSucursal;
                    //obtener los valores del usuario
                    string id = userv.Int_Idusuario.ToString();
                    int? tipo = userv.Int_IdArea;
                    int? Sucursal = userv.Lng_IdSucursal;
                    string suc = userv.Lng_IdSucursal.ToString();

                    //obtener el area del usuario
                    var area = db.Ct_Areas.FirstOrDefault(a => a.Int_IdArea == tipo);
                    string areausuario = area.Txt_Area;

                    //obtener sucursal del usuario

                    var Idsucursal = db.Tb_Sucursal.FirstOrDefault(s => s.Lng_IdSucursal == Sucursal);
                    string Sucursalusuario = Idsucursal.Txt_Sucursal;

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
                    Session["plazadelusuaio"] = plazadelusuario;

                    FormsAuthentication.SetAuthCookie(id, true);


                    bool activo = false;
                    var dbturno = db.Tb_Cierre.FirstOrDefault(c => c.Int_IdUsuario == userv.Int_Idusuario && c.Int_IdStatus == 1 && c.Int_IdSucursal == userv.Lng_IdSucursal && c.Bol_Congelar == activo);

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
                            SqlDataAdapter caja = new SqlDataAdapter("INSERT INTO [dbo].[Tb_Cierre] ([Int_IdDenomicacion],[Dbl_Cantidad] ,[Fec_Cierre] ,[Int_IdMoneda] ,[Int_IdStatus],[Int_IdTurno],[Int_IdSucursal],[Int_IdUsuario], [Bol_Congelar])  VALUES(" + valor+",0, GETDATE(),7,1,1," + userv.Lng_IdSucursal + "," + userv.Int_Idusuario + ", 0)", con);
                            DataSet a = new DataSet();
                            caja.Fill(a);
                            a.Reset();

                            valor = valor + 1;
                        }
                        

                        return RedirectToAction("Index", "arqueo");

                    }

                    else if (dbturno.Int_IdStatus == 1 && dbturno.Int_IdSucursal != userv.Lng_IdSucursal)
                    {

                        return RedirectToAction("Index", new { Message = "Este usuario no se Puede logear 2 veces en diferentes Sucursales" });
                    }
                  


                    return RedirectToAction("Index", new { Message = "Hubo un error" });




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