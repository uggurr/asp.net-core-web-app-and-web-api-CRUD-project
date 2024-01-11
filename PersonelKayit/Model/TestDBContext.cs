using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PersonelKayitApi.Model
{
    public partial class TestDBContext : DbContext
    {
        public TestDBContext()
        {
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adress> Adresses { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeEgitim> EmployeeEgitims { get; set; } = null!;
        public virtual DbSet<EmployeeMedyalar> EmployeeMedyalars { get; set; } = null!;
        public virtual DbSet<MedyaKutuphanesi> MedyaKutuphanesis { get; set; } = null!;
        public virtual DbSet<ParamOkullar> ParamOkullars { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adress>(entity =>
            {
                entity.ToTable("Adress");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Tanim)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Bday).HasColumnType("datetime");

                entity.Property(e => e.Deleted)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Explanation)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeeEgitim>(entity =>
            {
                entity.ToTable("EmployeeEgitim");
            });

            modelBuilder.Entity<EmployeeMedyalar>(entity =>
            {
                entity.ToTable("EmployeeMedyalar");
            });

            modelBuilder.Entity<MedyaKutuphanesi>(entity =>
            {
                entity.ToTable("MedyaKutuphanesi");

                entity.Property(e => e.MedyaAdi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MedyaUrl)
                    .IsUnicode(false)
                    .HasColumnName("MedyaURL");
            });

            modelBuilder.Entity<ParamOkullar>(entity =>
            {
                entity.ToTable("ParamOkullar");

                entity.Property(e => e.OkulAdi)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
