using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class arqueoController : Controller
    {
        // GET: arqueo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult pruebas()
        {
            return View();
        }

        public ActionResult cerrararqueo()
        {
            return View();
        }
    }
}