﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Universities.Infrastructure.Contracts.EntitiesDB;

namespace Universities.Infrastructure.Impl.DbContext
{
    public partial class UniversityDBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public UniversityDBContext()
        {
        }

        public UniversityDBContext(DbContextOptions<UniversityDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Domain> Domains { get; set; }
        public virtual DbSet<University> Universities { get; set; }
        public virtual DbSet<WebPage> WebPages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=074BCN2024\\SQLEXPRESS;Initial Catalog=UniversityDB;User ID=adria;Password=1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain>(entity =>
            {
                entity.Property(e => e.DomainName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.University)
                    .WithMany(p => p.Domains)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_Domains_Domains");
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.Property(e => e.AlphaTwoCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.StateProvince).HasMaxLength(50);
            });

            modelBuilder.Entity<WebPage>(entity =>
            {
                entity.Property(e => e.WebPageName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.University)
                    .WithMany(p => p.WebPages)
                    .HasForeignKey(d => d.UniversityId)
                    .HasConstraintName("FK_WebPages_Universities");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}