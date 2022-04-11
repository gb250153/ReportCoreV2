using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ReportCoreV2.ASGEfModel
{
    public partial class ASGContext : DbContext
    {
        public ASGContext()
        {
        }

        public ASGContext(DbContextOptions<ASGContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AsggeneralInfo> AsggeneralInfos { get; set; }
        public virtual DbSet<BusinessKpi> BusinessKpis { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ExternalDBConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AI_KS");

            modelBuilder.Entity<AsggeneralInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ASGGeneralInfo");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.NumberOfLogs).HasMaxLength(50);

                entity.Property(e => e.ProcessRate).HasMaxLength(50);

                entity.Property(e => e.ProjectName).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<BusinessKpi>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("BusinessKPI");

                entity.Property(e => e.LoyaltyNum).HasMaxLength(50);

                entity.Property(e => e.Mainbusiness).HasMaxLength(50);

                entity.Property(e => e.Tender).HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
