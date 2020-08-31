using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class Tventas
    {
        public Nullable<int> IdTaxa { get; set; }
        public string Moneda { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<System.DateTime> Dia { get; set; }
        public int Lng_IdTaxa { get; set; }
        public int Int_IdMoneda { get; set; }
    }
}