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


namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
   
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
                   //obtener los valores del usuario
                    string id = userv.Int_Idusuario.ToString();
                    int? tipo = userv.Int_IdArea;
                    int? Sucursal = userv.Lng_IdSucursal;

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
                    

                 
                     // creacion de cookie de usuario 
                    FormsAuthentication.SetAuthCookie(id, true);
                    
                    return RedirectToAction("Index", "Profile");


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