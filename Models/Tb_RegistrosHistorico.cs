//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tb_RegistrosHistorico
    {
        public int Lng_IdRegistroHistorico { get; set; }
        public Nullable<int> Lng_IdRegistro { get; set; }
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
        public Nullable<System.DateTime> Fec_Dia { get; set; }
        public Nullable<int> Lng_IdCliente { get; set; }
    }
}
