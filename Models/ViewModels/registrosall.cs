using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public Nullable<int> Int_IdMonVenta { get; set; }
        public Nullable<decimal> Dbl_MontoRecibir { get; set; }
        public Nullable<decimal> Dbl_MontoPagar { get; set; }
        public Nullable<decimal> Dbl_TipoCambio { get; set; }
        public Nullable<decimal> Dbl_TipoCambioven { get; set; }
        public Nullable<decimal> Dbl_TipoCambioEsp { get; set; }
        public Nullable<bool> Bol_Especial { get; set; }
        public Nullable<decimal> Dbl_Entregar { get; set; }
        public Nullable<decimal> Dbl_Cambio { get; set; }
        public Nullable<int> Int_IdTpv { get; set; }

        public System.DateTime Fec_Fecha { get; set; }
        public Nullable<bool> Bol_multimoneda { get; set; }


        //obtener moneda compra
        public Nullable<int> IdTaxa { get; set; }
        public string Moneda { get; set; }
        public string Monedaven { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<System.DateTime> Dia { get; set; }
        public int Lng_IdTaxa { get; set; }

        //obtener moneda venta
        public Nullable<int> IdTaxaventa { get; set; }
        public string Monedaventa { get; set; }
        public Nullable<decimal> Valorventa { get; set; }
        public int Int_IdMonedaventa { get; set; }
        public Nullable<int> Lng_IdSucursal { get; set; }

        //tipos de transaccion
     
        public string Txt_TipoTran { get; set; }


    }
}