using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using LinksShortening.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LinksShortening.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Link> Links => Set<Link>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>(LinkConfig);
        }

        private void LinkConfig(EntityTypeBuilder<Link> link)
        {
            link.ToTable("Links");

            link.Property(l => l.Id).HasColumnName("Id").HasColumnType("INT");
            link.HasKey(l => l.Id);

            link.Property(l => l.LongURL).HasColumnName("LongURL").HasColumnType("TEXT");
            link.Property(l => l.CreationDate).HasColumnName("CreationDate").HasColumnType("DATE");
            link.Property(l => l.ShortURL).HasColumnName("ShortURL").HasColumnType("TEXT");
            link.Property(l => l.Jumps).HasColumnName("Jumps").HasColumnType("INT");
        }
    }
}
