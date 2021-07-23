using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TenDotAPI.Models
{
    public partial class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext()
        {
        }

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApprovalMaster> ApprovalMaster { get; set; }
        public virtual DbSet<UserRecord> UserRecord { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=MUM02L9612\\SQLEXPRESS;Database=EmployeeDB;User ID=sa;Password=Password123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApprovalMaster>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Approver)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LowerAmtLimit).HasColumnName("lowerAmtLimit");

                entity.Property(e => e.TenDotHirarchy)
                    .HasColumnName("tenDotHirarchy")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRecord>(entity =>
            {
                entity.HasKey(e => e.ContactCode)
                    .HasName("PK__UserReco__8D7BE9DDEFD365DE");

                entity.Property(e => e.ContactCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TenDotHirarchy)
                    .HasColumnName("tenDotHirarchy")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
