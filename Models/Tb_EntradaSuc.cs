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
    using System.ComponentModel.DataAnnotations;

    public partial class Tb_EntradaSuc
    {
        public int Lng_IdEntrada { get; set; }
        public Nullable<decimal> Dbl_SaldoEntrada { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Fec_Ini { get; set; }
        public Nullable<int> Int_Sucursal { get; set; }
        public Nullable<int> Int_IdMoneda { get; set; }
        public Nullable<bool> Bol_Activo { get; set; }
        public Nullable<int> Int_Estatus { get; set; }
        public string Txt_Motivo { get; set; }
    }
}
