using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ReportCoreV2.ExternalEfModel
{
    public partial class ExternalReportsContext : DbContext
    {
        public ExternalReportsContext()
        {
        }

        public ExternalReportsContext(DbContextOptions<ExternalReportsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ManualExecution> ManualExecutions { get; set; }
        public virtual DbSet<ManualProjectsCreatedScenario> ManualProjectsCreatedScenarios { get; set; }
        public virtual DbSet<ManualProjectsExecution> ManualProjectsExecutions { get; set; }
        public virtual DbSet<ManualScenarioCreation> ManualScenarioCreations { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

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

            modelBuilder.Entity<ManualExecution>(entity =>
            {
                entity.ToTable("ManualExecution");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ScenarioExecutionAmount).HasColumnName("Scenario_Execution_Amount");
            });

            modelBuilder.Entity<ManualProjectsCreatedScenario>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Project).IsRequired();
            });

            modelBuilder.Entity<ManualProjectsExecution>(entity =>
            {
                entity.ToTable("ManualProjectsExecution");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Project).IsRequired();
            });

            modelBuilder.Entity<ManualScenarioCreation>(entity =>
            {
                entity.ToTable("ManualScenarioCreation");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.EntryDate).HasColumnType("datetime");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ScenarioCreatedAmount).HasColumnName("Scenario_Created_Amount");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProjectName)
                    .IsRequired()
                    .HasColumnName("Project Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
