using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class registrosall
    {
        // modelo de registro
        public int Lng_IdRegistro { get; set; }
        public Nullable<int> Int_IdTipoTran { get; set; }
        public Nullable<int> Int_IdMoneda { get; set; }
        public Nullable<decimal> Dbl_MontoRecibir { get; set; }
        public Nullable<decimal> Dbl_MontoPagar { get; set; }

        public Nullable<decimal> Dbl_TipoCambio { get; set; }
        public Nullable<decimal> Dbl_TipoCambioEsp { get; set; }
        public Nullable<bool> Bol_Especial { get; set; }
        public Nullable<decimal> Dbl_Entregar { get; set; }
        public decimal Dbl_Cambio { get; set; }
        public Nullable<int> Int_IdTpv { get; set; }
        public Nullable<System.DateTime> Fec_Fecha { get; set; }
        public Nullable<int> int_idmonventa { get; set; }
        public Nullable<decimal> Dbl_TipoCambioven { get; set; }


        //obtener moneda 
        public Nullable<int> IdTaxa { get; set; }
        public string Moneda { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<System.DateTime> Dia { get; set; }
        public int Lng_IdTaxa { get; set; }

      

    }
}