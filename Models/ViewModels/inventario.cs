﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class inventario
    {
        public int Lng_IdEntrada { get; set; }
        public Nullable<decimal> Dbl_SaldoEntrada { get; set; }
        public Nullable<System.DateTime> Fec_Ini { get; set; }
        public Nullable<System.DateTime> Fec_Fin { get; set; }
        public Nullable<int> Int_Sucursal { get; set; }
        public Nullable<int> Int_IdMoneda { get; set; }
        public Nullable<bool> Bol_Activo { get; set; }
        public Nullable<decimal> Dbl_SaldoSalido { get; set; }

        public int Lng_IdEntEmp { get; set; }
        
        public int Int_IdUsuario { get; set; }
        public int Int_IdTurno { get; set; }

        public string Txt_Moneda { get; set; }
    }
}