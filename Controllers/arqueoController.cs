using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class arqueoController : Controller
    {
        public MasterExchangeEntities db = new MasterExchangeEntities();

        public void dr()
        {
            int turno = Convert.ToInt32(Session["turno"]);
            int iduser = Convert.ToInt32(User.Identity.Name);
            int sucursalid = Convert.ToInt32(Session["idsucursal"]);

             var vcompras = (from x in db.Tb_Registros
                            join xa in db.Tb_RegUsu on x.Lng_IdRegistro equals xa.Lng_IdRegistro
                            where x.Int_IdTipoTran == 1 && xa.Int_IdUsuario == iduser && xa.Int_Turno == turno && x.Bol_Cancelar == false && x.Bol_multimoneda == false
                            group x by new { x.Dbl_MontoRecibir } into g
                            let suma = (g.Sum(x => x.Dbl_MontoRecibir))
                            select suma
                             ).Sum();

             var vventas = (from x in db.Tb_Registros
                           join xa in db.Tb_RegUsu on x.Lng_IdRegistro equals xa.Lng_IdRegistro
                           where x.Int_IdTipoTran == 2 && xa.Int_IdUsuario == iduser && xa.Int_Turno == turno && x.Bol_Cancelar == false && x.Bol_multimoneda == false
                           group x by new { x.Dbl_MontoRecibir } into g
                           let suma = (g.Sum(x => x.Dbl_MontoRecibir))
                           select suma
                             ).Sum();

            var saldosinincial = (from s in db.Tb_EntradaSuc
                                  join sa in db.Tb_EntEmp on s.Lng_IdEntrada equals sa.Lng_IdEntrada
                                  where s.Int_Sucursal == sucursalid && s.Int_IdMoneda == 7 && s.Int_Estatus == 1 && sa.Int_IdUsuario == iduser && sa.Int_IdTurno == turno
                                  group s by new { s.Dbl_SaldoEntrada } into g
                                  let suma = (g.Sum(n => n.Dbl_SaldoEntrada))
                                  select suma
                                  ).Sum();

            var saldosdotacion = (from s in db.Tb_EntradaSuc
                                  join sa in db.Tb_EntEmp on s.Lng_IdEntrada equals sa.Lng_IdEntrada
                                  where s.Int_Sucursal == sucursalid && s.Int_IdMoneda == 7 && s.Int_Estatus == 1 && sa.Int_IdUsuario == iduser && sa.Int_IdTurno == turno
                                  group s by new { s.Dbl_SaldoEntrada } into g
                                  let suma = (g.Sum(n => n.Dbl_SaldoEntrada))
                                  select suma
                                  ).Sum();


        }

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