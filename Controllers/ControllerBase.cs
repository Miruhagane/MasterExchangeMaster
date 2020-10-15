using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class ControllerBase : Controller
    {
        // GET: ControllerBase
        public ActionResult Index()
        {
            return View();
        }

        protected override void ExecuteCore()
        {
            ViewBag.usuario = ((usuario)Session["usuario"]).nombre;
        }
    }
}