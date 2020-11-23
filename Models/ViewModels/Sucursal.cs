using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class Sucursal
    {
        public int Lng_IdSucursal { get; set; }
        public Nullable<int> Int_IdPlaza { get; set; }
        public string Txt_Sucursal { get; set; }
        public string Txt_Direccion { get; set; }
    }
}