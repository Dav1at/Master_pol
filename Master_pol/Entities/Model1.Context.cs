﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Master_pol.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Partner_companyEntities6 : DbContext
    {
        public Partner_companyEntities6()
            : base("name=Partner_companyEntities6")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Discount> Discount { get; set; }
        public virtual DbSet<DiscountResult> DiscountResult { get; set; }
        public virtual DbSet<Material_type> Material_type { get; set; }
        public virtual DbSet<Partners> Partners { get; set; }
        public virtual DbSet<Product_type> Product_type { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Sale_history> Sale_history { get; set; }
        public virtual DbSet<skidki> skidki { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}