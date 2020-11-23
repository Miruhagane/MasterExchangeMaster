using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class inventario
    {

        //modelo entradasaldos
        public int Lng_IdEntrada { get; set; }
        public Nullable<decimal> Dbl_SaldoEntrada { get; set; }
        public Nullable<System.DateTime> Fec_Ini { get; set; }
        
        public Nullable<int> Int_Sucursal { get; set; }
        public Nullable<int> Int_IdMoneda { get; set; }
        public Nullable<bool> Bol_Activo { get; set; }
  
        public Nullable<int> Int_Estatus { get; set; }

        public string Txt_Motivo { get; set; }

        //modelo tabla de paso 
        public int Lng_IdEntEmp { get; set; }
       
        public Nullable<int> Int_IdUsuario { get; set; }
        public Nullable<int> Int_IdTurno { get; set; }
        public string Txt_Moneda { get; set; }

      // modelo variable sucrusal
        public string Txt_sucursal { get; set; }


      // modelo 
         public string n_usuario { get; set; }


    }
}