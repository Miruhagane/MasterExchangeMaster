using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class listaclientes
    {
        public int Lng_IdCliente { get; set; }
        public string Txt_NumCliente { get; set; }
        public string Txt_Cliente { get; set; }
        public string Txt_Direccion { get; set; }
        public string Txt_Identificacion { get; set; }
        public string Txt_Telefono { get; set; }
        public string Txt_Email { get; set; }
        public Nullable<System.DateTime> Fec_Alta { get; set; }
    }
}