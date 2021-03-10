using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class arqueotesoreria
    {
        public int idregistro { get; set; }
        public string moneda { get; set; }
        public Nullable<decimal> monto { get; set; }
        public Nullable<decimal> montosuc { get; set; }
        public Nullable<bool> Bol_Ok { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }

        public string nusuario { get; set; }

        public string pusuario { get; set; }

        public Nullable<int> idmoneda { get; set; }

        public Nullable<int> turno { get; set; }

        public string nombre {get; set;}
        public string apellido { get; set; }


    }
}