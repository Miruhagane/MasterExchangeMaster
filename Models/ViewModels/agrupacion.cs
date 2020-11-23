using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class agrupacion
    {
        public int id_registro { get; set; }
        public string txt_sucursal { get; set; }
        public Nullable<decimal> monto { get; set; }
        public string moneda { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
        public string usuario { get; set; }
        public Nullable<int> grupo { get; set; }
       
    }
}