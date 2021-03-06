﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models.ViewModels
{
    public class users
    {

        public int Int_Idusuario { get; set; }
        public string Txt_Nombre { get; set; }
        public string Txt_Apellido { get; set; }
        public string Txt_NomCorto { get; set; }
        public string Txt_Password { get; set; }
        public int Bol_Activo { get; set; }
        public Nullable<int> Int_IdArea { get; set; }
        public Nullable<int> Int_IdSubarea { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime Fec_Alta { get; set; }
        public Nullable<System.DateTime> Fec_Baja { get; set; }
        public string Int_Ext { get; set; }
        public string Num_Telefono_1 { get; set; }
        public Nullable<bool> Bol_Bloqueado { get; set; }
        public string Num_Telefono_2 { get; set; }
        public string Txt_email { get; set; }
        public Nullable<bool> Bol_Promotor { get; set; }
        public Nullable<int> Int_IdEmpresa { get; set; }
        public string Txt_Password2 { get; set; }
        public string Txt_Password3 { get; set; }
        public Nullable<int> Int_Idempleado { get; set; }
        public Nullable<int> Int_IdPlaza { get; set; }
        public Nullable<int> Lng_IdSucursal { get; set; }
        public Nullable<int> Int_IdDepartamentos { get; set; }
        public byte[] Img_Foto { get; set; }


        public string txt_sucursal { get; set; }

    }
}