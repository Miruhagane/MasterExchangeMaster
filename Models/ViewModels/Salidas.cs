using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class Salidas
    {

        public string Txt_Moneda { get; set; }

        public int Lng_IdSalida { get; set; }
        public Nullable<decimal> Dbl_SaldoSalida { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Fec_Fin { get; set; }
        public Nullable<int> Int_IdMoneda { get; set; }
        public Nullable<int> int_IdSucursal { get; set; }
        public Nullable<bool> Bol_Activo { get; set; }
        public Nullable<int> Int_Idusuario { get; set; }
        public Nullable<int> Int_IdTurno { get; set; }
        public Nullable<int> Int_estatus { get; set; }
        public string Txt_Motivo { get; set; }


        public string Txt_sucursal { get; set; }
        public string username { get; set; }
    }
}