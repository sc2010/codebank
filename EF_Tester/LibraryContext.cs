using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
// using MySQL.Data.EntityFrameworkCore.Extensions;

namespace EF_Tester
{
    public class LibraryContext : DbContext
    {

        public DbSet<Book> Book { get; set; }

        public DbSet<Publisher> Publisher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseMySQL("server=localhost;port=3306;database=devel_codebank;uid=root;password=123456");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ISBN);
                entity.Property(e => e.Title).IsRequired();
                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books);
            });
        }
    }
}
