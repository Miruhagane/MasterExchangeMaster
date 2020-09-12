using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class inventario
    {
        public int Lng_IdEntrada { get; set; }
        public decimal Dbl_SaldoEntrada { get; set; }
        public DateTime Fec_Ini { get; set; }
        public DateTime Fec_Fin { get; set; }
        public int Int_Sucursal { get; set; }
        public int Int_IdMoneda { get; set; }
        public bool Bol_Activo { get; set; }

        public int Lng_IdEntEmp { get; set; }
        
        public int Int_IdUsuario { get; set; }
        public int Int_IdTurno { get; set; }

        public string Txt_Moneda { get; set; }
    }
}