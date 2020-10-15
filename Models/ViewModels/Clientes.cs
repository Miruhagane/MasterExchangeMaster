using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class Clientes
    {
        public int Lng_IdCliente { get; set; }
        public string Txt_NumCliente { get; set; }
        public string Txt_Cliente { get; set; }
        public string Txt_Direccion { get; set; }
        public string Txt_Identificacion { get; set; }
        public string Txt_Telefono { get; set; }
        public string Txt_Email { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Fec_Alta { get; set; }

        public string File_Nombre { get; set; }

        public HttpPostedFileBase File { get; set; }
        public byte[] Doc_cliente { get; set; }
    }
}