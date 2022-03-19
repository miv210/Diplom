using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Diplom.Models
{
    public partial class TestBdContext : DbContext
    {
        public TestBdContext()
        {
        }

        public TestBdContext(DbContextOptions<TestBdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Dogovor> Dogovors { get; set; } = null!;
        public virtual DbSet<Pasport> Pasports { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MIV\\SQLEXPRESS;Database=TestBd;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adres).HasMaxLength(150);

                entity.Property(e => e.Fio)
                    .HasMaxLength(150)
                    .HasColumnName("FIO");

                entity.Property(e => e.LicevoiChet).HasColumnName("Licevoi_chet");

                entity.Property(e => e.NDogovora).HasColumnName("N_dogovora");

                entity.Property(e => e.Telethon).HasMaxLength(150);

                entity.HasOne(d => d.Pasport)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.PasportId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Clients_Pasports");
            });

            modelBuilder.Entity<Dogovor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataPodpisanie)
                    .HasMaxLength(50)
                    .HasColumnName("Data_podpisanie");

                entity.Property(e => e.IdClient).HasColumnName("Id_client");

                entity.Property(e => e.Sostoynie).HasMaxLength(50);

                entity.Property(e => e.SumDogovora).HasColumnName("Sum_dogovora");

                entity.Property(e => e.TypeDogovora)
                    .HasMaxLength(30)
                    .HasColumnName("Type_dogovora");

                entity.HasOne(d => d.IdClientNavigation)
                    .WithMany(p => p.Dogovors)
                    .HasForeignKey(d => d.IdClient)
                    .HasConstraintName("FK_Dogovors_Clients");
            });

            modelBuilder.Entity<Pasport>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdresRegistarci)
                    .HasMaxLength(150)
                    .HasColumnName("Adres_registarci");

                entity.Property(e => e.DataRojdeniy)
                    .HasMaxLength(30)
                    .HasColumnName("Data_rojdeniy");

                entity.Property(e => e.DataVidachi)
                    .HasMaxLength(30)
                    .HasColumnName("Data_vidachi");

                entity.Property(e => e.KemVidan)
                    .HasMaxLength(150)
                    .HasColumnName("Kem_vidan");

                entity.Property(e => e.KodPodrazdeleniy)
                    .HasMaxLength(30)
                    .HasColumnName("Kod_podrazdeleniy");

                entity.Property(e => e.MestoRojdeniy)
                    .HasMaxLength(150)
                    .HasColumnName("Mesto_rojdeniy");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .HasMaxLength(80)
                    .HasColumnName("login_");

                entity.Property(e => e.Password)
                    .HasMaxLength(80)
                    .HasColumnName("password_");

                entity.Property(e => e.Role)
                    .HasMaxLength(80)
                    .HasColumnName("role_");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
