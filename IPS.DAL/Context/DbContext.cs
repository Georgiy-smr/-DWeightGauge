using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace IPS.DAL.Context
{
    public class DBContext : DbContext
    {
        public DbSet<Cargo> Cargoes { get; set; }
        public DbSet<IPS> iPs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IPS2Cargo>().HasKey(k => new { k.IPSId, k.CargoId });

            modelBuilder.Entity<IPS2Cargo>()
                .HasOne(x => x.Cargo)
                .WithMany(x => x.IPS2Cargoes)
                .HasForeignKey(x => x.CargoId); 
            modelBuilder.Entity<IPS2Cargo>()
               .HasOne(x => x.IPS)
               .WithMany(x => x.IPS2Cargoes)
               .HasForeignKey(x => x.IPSId);
            //base.OnModelCreating(modelBuilder);
        }
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

    }
}
