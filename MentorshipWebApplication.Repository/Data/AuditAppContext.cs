using System;
using System.Collections.Generic;
using MentorshipWebApplication.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MentorshipWebApplication.Repository.Data
{
    public partial class AuditAppContext : DbContext
    {
        public AuditAppContext()
        {
        }

        public AuditAppContext(DbContextOptions<AuditAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Audit> Audits { get; set; } = null!;
        public virtual DbSet<UpdateAudit> UpdateAudits { get; set; } = null!;
        public virtual DbSet<AuditStatus> AuditStatuses { get; set; } = null!;
        public virtual DbSet<BranchDetail> BranchDetails { get; set; } = null!;
        public virtual DbSet<Examiner> Examiners { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=CTSDOTNET805;Initial Catalog=AuditApp;User ID=sa;Password=pass@word1");
                //optionsBuilder.UseSqlServer("Server=tcp:sampledotnetserver.database.windows.net,1433;Initial Catalog=AuditApp;Persist Security Info=False;User ID=dotnet;Password=password@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Audit>(entity =>
            {
                entity.Property(e => e.AuditId).HasColumnName("AuditID");

                entity.Property(e => e.AssociateExaminerId).HasColumnName("AssociateExaminerID");

                //entity.Property(e => e.AssociateExaminerName).HasColumnName("AssociateExaminerName");

                entity.Property(e => e.AuditDate).HasColumnType("datetime");

                entity.Property(e => e.AuditHours).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.AuditStatusId).HasColumnName("AuditStatusID");

                //entity.Property(e => e.AuditStatuses).HasColumnName("AuditStatuses");

                entity.Property(e => e.BranchId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("BranchID");

                entity.Property(e => e.LeadExaminerId).HasColumnName("LeadExaminerID");

                //entity.Property(e => e.LeadExaminerName).HasColumnName("LeadExaminerName");

                entity.HasOne(d => d.AssociateExaminer)
                    .WithMany(p => p.AuditAssociateExaminers)
                    .HasForeignKey(d => d.AssociateExaminerId)
                    .HasConstraintName("FK_Audits_AssociateExaminers");

                entity.HasOne(d => d.AuditStatus)
                    .WithMany(p => p.Audits)
                    .HasForeignKey(d => d.AuditStatusId)
                    .HasConstraintName("FK_Audits_AuditStatuses");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Audits)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Audits_BranchDetails");

                entity.HasOne(d => d.LeadExaminer)
                    .WithMany(p => p.AuditLeadExaminers)
                    .HasForeignKey(d => d.LeadExaminerId)
                    .HasConstraintName("FK_Audits_Examiners");
            });

            modelBuilder.Entity<UpdateAudit>(entity =>
            {
                entity.HasKey(e => e.AuditId);

                entity.Property(e => e.AuditId).HasColumnName("AuditID");

                entity.Property(e => e.AssociateExaminerId).HasColumnName("AssociateExaminerID");

                entity.Property(e => e.AuditDate).HasColumnType("datetime");

                entity.Property(e => e.AuditHours).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.AuditStatusId).HasColumnName("AuditStatusID");

                entity.Property(e => e.BranchId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("BranchID");

                entity.Property(e => e.LeadExaminerId).HasColumnName("LeadExaminerID");
            });

            modelBuilder.Entity<AuditStatus>(entity =>
            {
                entity.Property(e => e.AuditStatusId).HasColumnName("AuditStatusID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BranchDetail>(entity =>
            {
                entity.HasKey(e => e.BranchId);

                entity.Property(e => e.BranchId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("BranchID");

                entity.Property(e => e.Address)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BranchManagerName).HasMaxLength(100);

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Examiner>(entity =>
            {
                entity.Property(e => e.ExaminerId).HasColumnName("ExaminerID");

                entity.Property(e => e.HoursAssigned).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
