﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SqlObjectWrapper.EntityModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NeuronNetworkDBEntities : DbContext
    {
        public NeuronNetworkDBEntities()
            : base("name=NeuronNetworkDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<StockHistory> StocksHistory { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<PredictedStockHistory> PredictedStockHistories { get; set; }
    }
}
