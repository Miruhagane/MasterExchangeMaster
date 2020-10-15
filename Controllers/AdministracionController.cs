using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class AdministracionController : Controller
    {
        // GET: ventasuser
        public ActionResult Index()
        {
            return View();
        }

        //GET: frmtazas
        public ActionResult Taxacompras()
        {
            return View();

        }

        //GET:TazasVentas
        public ActionResult TazasVentas()
        {
            return View();
        }
     
    }
}
