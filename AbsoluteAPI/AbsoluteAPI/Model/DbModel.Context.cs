﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AbsoluteAPI.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class absoluteEntities : DbContext
    {
        public absoluteEntities()
            : base("name=absoluteEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<APP_BANNER_V> APP_BANNER_V { get; set; }
        public virtual DbSet<APP_COMPETIZIONI_EVENTI_V> APP_COMPETIZIONI_EVENTI_V { get; set; }
        public virtual DbSet<APP_ELENCO_SQUADRE_V> APP_ELENCO_SQUADRE_V { get; set; }
        public virtual DbSet<APP_INCONTRI_V> APP_INCONTRI_V { get; set; }
        public virtual DbSet<APP_NEWS_V> APP_NEWS_V { get; set; }
        public virtual DbSet<APP_LS_COMPETIZIONI_EVENTI_V> APP_LS_COMPETIZIONI_EVENTI_V { get; set; }
    }
}