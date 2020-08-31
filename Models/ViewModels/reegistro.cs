using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class registro
    {

        public int Lng_IdRegistro { get; set; }
        public Nullable<int> Int_IdTipoTran { get; set; }
        public Nullable<int> Int_IdMoneda { get; set; }
        public Nullable<decimal> Dbl_MontoRecibir { get; set; }
        public Nullable<decimal> Dbl_MontoPagar { get; set; }

        public Nullable<decimal> Dbl_TipoCambio { get; set; }
        public Nullable<decimal> Dbl_TipoCambioEsp { get; set; }
        public Nullable<bool> Bol_Especial { get; set; }
        public Nullable<decimal> Dbl_Entregar { get; set; }
        public Nullable<decimal> Dbl_Cambio { get; set; }
        public Nullable<int> Int_IdTpv { get; set; }
        public Nullable<System.DateTime> Fec_Fecha { get; set; }
        public Nullable<int> int_idmonventa { get; set; }
        public Nullable<decimal> Dbl_TipoCambioven { get; set; }


    }
}