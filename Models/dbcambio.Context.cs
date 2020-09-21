﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MasterExchangeEntities : DbContext
    {
        public MasterExchangeEntities()
            : base("name=MasterExchangeEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ct_Moneda> Ct_Moneda { get; set; }
        public virtual DbSet<Tb_Clientes> Tb_Clientes { get; set; }
        public virtual DbSet<Tb_RegistrosHistorico> Tb_RegistrosHistorico { get; set; }
        public virtual DbSet<Tb_Taxas> Tb_Taxas { get; set; }
        public virtual DbSet<Tb_RegCli> Tb_RegCli { get; set; }
        public virtual DbSet<Ct_Plazas> Ct_Plazas { get; set; }
        public virtual DbSet<Tb_PlazasApli> Tb_PlazasApli { get; set; }
        public virtual DbSet<Tb_RegUsu> Tb_RegUsu { get; set; }
        public virtual DbSet<TaxaCompra> TaxaCompras { get; set; }
        public virtual DbSet<TaxaVenta> TaxaVentas { get; set; }
        public virtual DbSet<Tb_Registros> Tb_Registros { get; set; }
        public virtual DbSet<Ct_Areas> Ct_Areas { get; set; }
        public virtual DbSet<Ct_TipoTran> Ct_TipoTran { get; set; }
        public virtual DbSet<Tb_Sucursal> Tb_Sucursal { get; set; }
        public virtual DbSet<Tb_Usuarios> Tb_Usuarios { get; set; }
        public virtual DbSet<Taxacompcomb> Taxacompcombs { get; set; }
        public virtual DbSet<Tb_TaxSuc> Tb_TaxSuc { get; set; }
        public virtual DbSet<Tb_EntradaSuc> Tb_EntradaSuc { get; set; }
        public virtual DbSet<Tb_EntEmp> Tb_EntEmp { get; set; }
        public virtual DbSet<Ct_Denominaciones> Ct_Denominaciones { get; set; }
        public virtual DbSet<Tb_SalidaSuc> Tb_SalidaSuc { get; set; }
        public virtual DbSet<Tb_Cierre> Tb_Cierre { get; set; }
    
        public virtual ObjectResult<buscar_Result1> buscar(string numero)
        {
            var numeroParameter = numero != null ?
                new ObjectParameter("numero", numero) :
                new ObjectParameter("numero", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<buscar_Result1>("buscar", numeroParameter);
        }
    
        public virtual ObjectResult<Tabla_Compras_Result> Tabla_Compras()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tabla_Compras_Result>("Tabla_Compras");
        }
    
        public virtual ObjectResult<Tb_Clientes> buscar1(string numero)
        {
            var numeroParameter = numero != null ?
                new ObjectParameter("numero", numero) :
                new ObjectParameter("numero", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tb_Clientes>("buscar1", numeroParameter);
        }
    
        public virtual ObjectResult<Tb_Clientes> buscar1(string numero, MergeOption mergeOption)
        {
            var numeroParameter = numero != null ?
                new ObjectParameter("numero", numero) :
                new ObjectParameter("numero", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tb_Clientes>("buscar1", mergeOption, numeroParameter);
        }
    
        public virtual int inven_sucursal(Nullable<int> sucursal)
        {
            var sucursalParameter = sucursal.HasValue ?
                new ObjectParameter("sucursal", sucursal) :
                new ObjectParameter("sucursal", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("inven_sucursal", sucursalParameter);
        }
    }
}
