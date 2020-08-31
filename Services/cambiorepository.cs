using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Controllers;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class cambiorepository
    {
        public List<Tb_Clientes> obtener_cliente()
        {
            using (var db = new MasterExchangeEntities())
            {
                return db.Tb_Clientes.ToList();
            }
        }
    }
}