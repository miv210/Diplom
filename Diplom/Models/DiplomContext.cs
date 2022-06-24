using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Diplom.Models
{
    public partial class DiplomContext : DbContext
    {
        private static DiplomContext context;
        public DiplomContext()
        {
        }

        public DiplomContext(DbContextOptions<DiplomContext> options)
            : base(options)
        {
        }
        public static DiplomContext GetContext()
        {
            if (context == null)
                context = new DiplomContext();
            return context;
        }
        public static DiplomContext CloseContext()
        {
            return context = null;
        }

        public virtual DbSet<Bid> Bids { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Contract> Contracts { get; set; } = null!;
        public virtual DbSet<Operator> Operators { get; set; } = null!;
        public virtual DbSet<Passport> Passports { get; set; } = null!;
        public virtual DbSet<StatusBid> StatusBids { get; set; } = null!;
        public virtual DbSet<StatusContract> StatusContracts { get; set; } = null!;
        public virtual DbSet<TypeBid> TypeBids { get; set; } = null!;
        public virtual DbSet<TypeContract> TypeContracts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=MIV\\SQLEXPRESS;Database=Diplom;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bid>(entity =>
            {
                entity.ToTable("Bid");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateBid)
                    .HasColumnType("date")
                    .HasColumnName("Date_bid");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.IdClienta).HasColumnName("id_clienta");

                entity.Property(e => e.IdOperatora).HasColumnName("id_operatora");

                entity.Property(e => e.StatusBid).HasColumnName("Status_bid");

                entity.Property(e => e.TypeBid).HasColumnName("Type_bid");

                entity.HasOne(d => d.IdClientaNavigation)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.IdClienta)
                    .HasConstraintName("FK_Bid_Client");

                entity.HasOne(d => d.IdOperatoraNavigation)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.IdOperatora)
                    .HasConstraintName("FK_Bid_Operator");

                entity.HasOne(d => d.StatusB)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.StatusBid)
                    .HasConstraintName("FK_Bid_Status_bid");

                entity.HasOne(d => d.TypeB)
                    .WithMany(p => p.Bids)
                    .HasForeignKey(d => d.TypeBid)
                    .HasConstraintName("FK_Bid_Type_bid1");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.IdPassporta).HasColumnName("id_passporta");

                entity.Property(e => e.Login).HasMaxLength(50);

                entity.Property(e => e.NContract).HasColumnName("N_contract");

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PersonalAccount).HasColumnName("Personal_account");

                entity.Property(e => e.TelethonNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Telethon_number");

                entity.HasOne(d => d.IdPassportaNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.IdPassporta)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Client_Passport");

                entity.HasOne(d => d.NContractNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.NContract)
                    .HasConstraintName("FK_Client_Contract");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.ToTable("Contract");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ContractAmount)
                    .HasMaxLength(50)
                    .HasColumnName("Contract_amount");

                entity.Property(e => e.DateOfSigning)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_signing");

                entity.Property(e => e.IdClienta).HasColumnName("id_clienta");

                entity.Property(e => e.TypeContract).HasColumnName("Type_contract");

                entity.HasOne(d => d.IdClientaNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.IdClienta)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Contract_Client");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Contract_Status_contract");

                entity.HasOne(d => d.TypeContractNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.TypeContract)
                    .HasConstraintName("FK_Contract_Type_contract");
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.ToTable("Operator");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Patronymic).HasMaxLength(255);

                entity.Property(e => e.Surname).HasMaxLength(255);
            });

            modelBuilder.Entity<Passport>(entity =>
            {
                entity.ToTable("Passport");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CodeDepartment)
                    .HasMaxLength(50)
                    .HasColumnName("Code_department");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_birth");

                entity.Property(e => e.DateOfIssue)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_issue");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                entity.Property(e => e.RegistrationAddress)
                    .HasMaxLength(50)
                    .HasColumnName("Registration_address");

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.Property(e => e.WhoIssued)
                    .HasMaxLength(50)
                    .HasColumnName("Who_issued");
            });

            modelBuilder.Entity<StatusBid>(entity =>
            {
                entity.ToTable("Status_bid");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.NameStatusa)
                    .HasMaxLength(50)
                    .HasColumnName("Name_statusa");
            });

            modelBuilder.Entity<StatusContract>(entity =>
            {
                entity.ToTable("Status_contract");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TypeBid>(entity =>
            {
                entity.ToTable("Type_bid");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.NameType)
                    .HasMaxLength(50)
                    .HasColumnName("Name_type");
            });

            modelBuilder.Entity<TypeContract>(entity =>
            {
                entity.ToTable("Type_contract");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NameType)
                    .HasMaxLength(50)
                    .HasColumnName("Name_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
