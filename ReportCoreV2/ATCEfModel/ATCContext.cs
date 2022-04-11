using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ReportCoreV2.ATCEfModel
{
    public partial class ATCContext : DbContext
    {
        public ATCContext()
        {
        }

        public ATCContext(DbContextOptions<ATCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DurationResult> DurationResults { get; set; }
        public virtual DbSet<TblSysCycleDuration> TblSysCycleDurations { get; set; }
        public virtual DbSet<TblSysExecutionLog> TblSysExecutionLogs { get; set; }
        public virtual DbSet<TblSysExecutionLogArchive> TblSysExecutionLogArchives { get; set; }
        public virtual DbSet<TblSysScenario> TblSysScenarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=ATCDBConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<DurationResult>(entity =>
            {
                entity.ToTable("DurationResult");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ActionDuration)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.ActionNumber)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.ScenarioDuration)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.ScenarioNumber)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.SetContent)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.SetDuration)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.SetTestRun)
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TblSysCycleDuration>(entity =>
            {
                entity.HasKey(e => e.Pk);

                entity.ToTable("Tbl_Sys_CycleDuration");

                entity.Property(e => e.Pk).HasColumnName("pk");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ActionDuration).HasColumnType("numeric(18, 5)");

                entity.Property(e => e.ActionFailReason).HasDefaultValueSql("('')");

                entity.Property(e => e.ActionStatus).HasMaxLength(50);

                entity.Property(e => e.Scenario)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.TestCycle)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TestCycleTimeStamp)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.TestSet)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TestSetExecutionId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblSysExecutionLog>(entity =>
            {
                entity.HasKey(e => e.RequestNum)
                    .HasName("PK_Tbl_sys_Execution_Log_RequestNum");

                entity.ToTable("Tbl_sys_Execution_Log");

                entity.Property(e => e.RequestNum).ValueGeneratedNever();

                entity.Property(e => e.ActionAvgDurationTime)
                    .HasColumnName("Action_avg_Duration_Time")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ActionCounter)
                    .HasColumnName("Action_Counter")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ActionCounterExecuted)
                    .HasColumnName("Action_Counter_executed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ActionCounterExecutedFError)
                    .HasColumnName("Action_Counter_executed_F_ERROR")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ActionCounterExecutedFail)
                    .HasColumnName("Action_Counter_executed_FAIL")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ActionCounterExecutedPass)
                    .HasColumnName("Action_Counter_executed_Pass")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ActionCounterExecutedTError)
                    .HasColumnName("Action_Counter_executed_T_ERROR")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ActualEndTime).HasColumnType("datetime");

                entity.Property(e => e.ActualStartTime).HasColumnType("datetime");

                entity.Property(e => e.AppType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateReq).HasColumnType("datetime");

                entity.Property(e => e.DependenceReq).HasColumnName("dependenceReq");

                entity.Property(e => e.Env)
                    .HasColumnName("env")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.IsSchedule)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Process)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Project)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RequestBy).HasMaxLength(50);

                entity.Property(e => e.RunOnHost)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ScenarioAvgDurationTime)
                    .HasColumnName("Scenario_avg_Duration_Time")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounter)
                    .HasColumnName("Scenario_Counter")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounterExecuted)
                    .HasColumnName("Scenario_Counter_executed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounterExecutedFError)
                    .HasColumnName("Scenario_Counter_executed_F_ERROR")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounterExecutedFail)
                    .HasColumnName("Scenario_Counter_executed_FAIL")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounterExecutedPass)
                    .HasColumnName("Scenario_Counter_executed_Pass")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounterExecutedTError)
                    .HasColumnName("Scenario_Counter_executed_T_ERROR")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScheduleSetTime).HasColumnType("datetime");

                entity.Property(e => e.SendMailOnStart)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TestSetExecutionId)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TestSetName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TotalDurationTime)
                    .HasColumnName("Total_Duration_Time")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblSysExecutionLogArchive>(entity =>
            {
                entity.HasKey(e => e.RequestNum)
                    .HasName("PK_Tbl_sys_Execution_Log_Archive_RequestNum");

                entity.ToTable("Tbl_sys_Execution_Log_Archive");

                entity.Property(e => e.RequestNum).ValueGeneratedNever();

                entity.Property(e => e.ActionAvgDurationTime)
                    .HasColumnName("Action_avg_Duration_Time")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ActionCounter)
                    .HasColumnName("Action_Counter")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ActionCounterExecuted)
                    .HasColumnName("Action_Counter_executed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ActionCounterExecutedFError).HasColumnName("Action_Counter_executed_F_ERROR");

                entity.Property(e => e.ActionCounterExecutedFail).HasColumnName("Action_Counter_executed_FAIL");

                entity.Property(e => e.ActionCounterExecutedPass).HasColumnName("Action_Counter_executed_Pass");

                entity.Property(e => e.ActionCounterExecutedTError).HasColumnName("Action_Counter_executed_T_ERROR");

                entity.Property(e => e.ActualEndTime).HasColumnType("datetime");

                entity.Property(e => e.ActualStartTime).HasColumnType("datetime");

                entity.Property(e => e.AppType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateReq).HasColumnType("datetime");

                entity.Property(e => e.DependenceReq).HasColumnName("dependenceReq");

                entity.Property(e => e.Env)
                    .HasColumnName("env")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.IsSchedule)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Process)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Project)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RequestBy).HasMaxLength(50);

                entity.Property(e => e.RunOnHost)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ScenarioAvgDurationTime)
                    .HasColumnName("Scenario_avg_Duration_Time")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounter)
                    .HasColumnName("Scenario_Counter")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounterExecuted)
                    .HasColumnName("Scenario_Counter_executed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounterExecutedFError)
                    .HasColumnName("Scenario_Counter_executed_F_ERROR")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounterExecutedFail)
                    .HasColumnName("Scenario_Counter_executed_FAIL")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounterExecutedPass)
                    .HasColumnName("Scenario_Counter_executed_Pass")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScenarioCounterExecutedTError)
                    .HasColumnName("Scenario_Counter_executed_T_ERROR")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ScheduleSetTime).HasColumnType("datetime");

                entity.Property(e => e.SendMailOnStart)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TestSetExecutionId)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TestSetName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.TotalDurationTime)
                    .HasColumnName("Total_Duration_Time")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblSysScenario>(entity =>
            {
                entity.ToTable("Tbl_sys_Scenarios");

                entity.HasIndex(e => new { e.ProjectOwner, e.ScenarioName }, "Scenario_Project_Name_Restriction")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AbortOnError).HasMaxLength(10);

                entity.Property(e => e.AppLink).HasMaxLength(10);

                entity.Property(e => e.Ddt)
                    .HasMaxLength(50)
                    .HasColumnName("DDT");

                entity.Property(e => e.FsdVersion)
                    .HasMaxLength(50)
                    .HasColumnName("Fsd_version");

                entity.Property(e => e.Guid).HasColumnName("GUID");

                entity.Property(e => e.Led).HasColumnName("LED");

                entity.Property(e => e.Ler).HasColumnName("LER");

                entity.Property(e => e.Les).HasColumnName("LES");

                entity.Property(e => e.Lock).HasDefaultValueSql("(N'NA')");

                entity.Property(e => e.MarketPlace).HasMaxLength(50);

                entity.Property(e => e.ParentRootFolder).HasMaxLength(50);

                entity.Property(e => e.Product).HasMaxLength(50);

                entity.Property(e => e.ProjectOwner)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.QcId).HasColumnName("Qc_id");

                entity.Property(e => e.RecordMovie).HasMaxLength(50);

                entity.Property(e => e.RootFolder).HasMaxLength(50);

                entity.Property(e => e.ScenarioCreateBy).HasMaxLength(50);

                entity.Property(e => e.ScenarioCreationDate).HasColumnType("datetime");

                entity.Property(e => e.ScenarioDesc).IsRequired();

                entity.Property(e => e.ScenarioLastUpdate).HasColumnType("datetime");

                entity.Property(e => e.ScenarioName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.ScenarioStatus).HasMaxLength(50);

                entity.Property(e => e.ScenarioTableName).HasMaxLength(200);

                entity.Property(e => e.ScenarioTemplateInstanceKeyWord).HasColumnName("Scenario_template_Instance_KeyWord");

                entity.Property(e => e.ScenarioTemplateLink).HasColumnName("Scenario_template_link");

                entity.Property(e => e.ScenarioTemplatePath).HasColumnName("Scenario_template_Path");

                entity.Property(e => e.ScenarioType).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
