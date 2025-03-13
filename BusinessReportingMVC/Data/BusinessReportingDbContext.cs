using System;
using System.Collections.Generic;
using BusinessReportingMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessReportingMVC.Data;

public partial class BusinessReportingDbContext : DbContext
{
    public BusinessReportingDbContext()
    {
    }

    public BusinessReportingDbContext(DbContextOptions<BusinessReportingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminAndResource> AdminAndResources { get; set; }

    public virtual DbSet<BusinessDevelopment> BusinessDevelopments { get; set; }

    public virtual DbSet<BusinessDevelopmentNote> BusinessDevelopmentNotes { get; set; }

    public virtual DbSet<BusinessDevelopmentValue> BusinessDevelopmentValues { get; set; }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<Financial> Financials { get; set; }

    public virtual DbSet<FinancialsActual> FinancialsActuals { get; set; }

    public virtual DbSet<FinancialsDeviation> FinancialsDeviations { get; set; }

    public virtual DbSet<KeyHighlight> KeyHighlights { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectIndividual> ProjectIndividuals { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Strategy> Strategies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserClaim> UserClaims { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminAndResource>(entity =>
        {
            entity.HasKey(e => e.AdminAndResourcesId).HasName("PK__Admin_An__68A45A1D9A056FDD");

            entity.ToTable("Admin_And_Resources");

            entity.Property(e => e.AdminAndResourcesId).HasColumnName("Admin_And_Resources_Id");
            entity.Property(e => e.PointsOfNote).HasColumnName("Points_Of_Note");
        });

        modelBuilder.Entity<BusinessDevelopment>(entity =>
        {
            entity.HasKey(e => e.BusinessDevelopmentId).HasName("PK__Business__6B072B835A8DBE11");

            entity.ToTable("Business_Development");

            entity.Property(e => e.BusinessDevelopmentId).HasColumnName("Business_Development_Id");
            entity.Property(e => e.BusinessDevelopmentNotesId).HasColumnName("Business_Development_Notes_Id");
            entity.Property(e => e.BusinessDevelopmentValueId).HasColumnName("Business_Development_Value_Id");

            entity.HasOne(d => d.BusinessDevelopmentNotes).WithMany(p => p.BusinessDevelopments)
                .HasForeignKey(d => d.BusinessDevelopmentNotesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Business_Development_Notes_Id");

            entity.HasOne(d => d.BusinessDevelopmentValue).WithMany(p => p.BusinessDevelopments)
                .HasForeignKey(d => d.BusinessDevelopmentValueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Business_Development_Value_Id");
        });

        modelBuilder.Entity<BusinessDevelopmentNote>(entity =>
        {
            entity.HasKey(e => e.BusinessDevelopmentNotesId).HasName("PK__Business__183BA5824BDF1F67");

            entity.ToTable("Business_Development_Notes");

            entity.Property(e => e.BusinessDevelopmentNotesId).HasColumnName("Business_Development_Notes_Id");
            entity.Property(e => e.AwarenessNotes).HasColumnName("Awareness_Notes");
            entity.Property(e => e.IntentNotes).HasColumnName("Intent_Notes");
            entity.Property(e => e.Tendered033Notes).HasColumnName("Tendered_0_33_Notes");
            entity.Property(e => e.Tendered3466Notes).HasColumnName("Tendered_34_66_Notes");
            entity.Property(e => e.Tendered67100Notes).HasColumnName("Tendered_67_100_Notes");
        });

        modelBuilder.Entity<BusinessDevelopmentValue>(entity =>
        {
            entity.HasKey(e => e.BusinessDevelopmentValueId).HasName("PK__Business__3F0FE71E88EB7B09");

            entity.ToTable("Business_Development_Value");

            entity.Property(e => e.BusinessDevelopmentValueId).HasColumnName("Business_Development_Value_Id");
            entity.Property(e => e.AwarenessValue)
                .HasColumnType("money")
                .HasColumnName("Awareness_Value");
            entity.Property(e => e.IntentValue)
                .HasColumnType("money")
                .HasColumnName("Intent_Value");
            entity.Property(e => e.Tendered033Value)
                .HasColumnType("money")
                .HasColumnName("Tendered_0_33_Value");
            entity.Property(e => e.Tendered3466Value)
                .HasColumnType("money")
                .HasColumnName("Tendered_34_66_Value");
            entity.Property(e => e.Tendered67100Value)
                .HasColumnType("money")
                .HasColumnName("Tendered_67_100_Value");
        });

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.ClaimId).HasName("PK__Claims__811C4A6D4D13DDD4");

            entity.Property(e => e.ClaimId).HasColumnName("Claim_Id");
            entity.Property(e => e.ClaimName).HasColumnName("Claim_Name");
            entity.Property(e => e.ClaimType).HasColumnName("Claim_Type");
        });

        modelBuilder.Entity<Financial>(entity =>
        {
            entity.HasKey(e => e.FinancialsId).HasName("PK__Financia__08CC3C0CA9ED1B46");

            entity.Property(e => e.FinancialsId).HasColumnName("Financials_Id");
            entity.Property(e => e.FinancialsActualId).HasColumnName("Financials_Actual_Id");
            entity.Property(e => e.FinancialsDeviationId).HasColumnName("Financials_Deviation_Id");

            entity.HasOne(d => d.FinancialsActual).WithMany(p => p.Financials)
                .HasForeignKey(d => d.FinancialsActualId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Financials_Actual_Id");

            entity.HasOne(d => d.FinancialsDeviation).WithMany(p => p.Financials)
                .HasForeignKey(d => d.FinancialsDeviationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Financials_Deviation_Id");
        });

        modelBuilder.Entity<FinancialsActual>(entity =>
        {
            entity.HasKey(e => e.FinancialsActualId).HasName("PK__Financia__E203C6A1AB11E293");

            entity.ToTable("Financials_Actual");

            entity.Property(e => e.FinancialsActualId).HasColumnName("Financials_Actual_Id");
            entity.Property(e => e.CashPositionActual)
                .HasColumnType("money")
                .HasColumnName("Cash_Position_Actual");
            entity.Property(e => e.DirectCostsActual)
                .HasColumnType("money")
                .HasColumnName("Direct_Costs_Actual");
            entity.Property(e => e.GrossProfitActual)
                .HasColumnType("money")
                .HasColumnName("Gross_Profit_Actual");
            entity.Property(e => e.IndirectCostsActual)
                .HasColumnType("money")
                .HasColumnName("Indirect_Costs_Actual");
            entity.Property(e => e.NetProfitActual)
                .HasColumnType("money")
                .HasColumnName("Net_Profit_Actual");
            entity.Property(e => e.ProductionHoursQuarter).HasColumnName("Production_Hours_Quarter");
            entity.Property(e => e.TurnoverActual)
                .HasColumnType("money")
                .HasColumnName("Turnover_Actual");
            entity.Property(e => e.UtilisationQuarter)
                .HasColumnType("money")
                .HasColumnName("Utilisation_Quarter");
            entity.Property(e => e.WipActual)
                .HasColumnType("money")
                .HasColumnName("WIP_Actual");
            entity.Property(e => e.WorkInHandHoursQuarter).HasColumnName("Work_In_Hand_Hours_Quarter");
            entity.Property(e => e.WorkInHandMoneyQuarter)
                .HasColumnType("money")
                .HasColumnName("Work_In_Hand_Money_Quarter");
        });

        modelBuilder.Entity<FinancialsDeviation>(entity =>
        {
            entity.HasKey(e => e.FinancialsDeviationId).HasName("PK__Financia__54D108BB5795E2D6");

            entity.ToTable("Financials_Deviation");

            entity.Property(e => e.FinancialsDeviationId).HasColumnName("Financials_Deviation_Id");
            entity.Property(e => e.CashPositionForecast)
                .HasColumnType("money")
                .HasColumnName("Cash_Position_Forecast");
            entity.Property(e => e.DirectCostsDeviation)
                .HasColumnType("money")
                .HasColumnName("Direct_Costs_Deviation");
            entity.Property(e => e.GrossProfitDeviation)
                .HasColumnType("money")
                .HasColumnName("Gross_Profit_Deviation");
            entity.Property(e => e.IndirectCostsDeviation)
                .HasColumnType("money")
                .HasColumnName("Indirect_Costs_Deviation");
            entity.Property(e => e.NetProfitDeviation)
                .HasColumnType("money")
                .HasColumnName("Net_Profit_Deviation");
            entity.Property(e => e.ProductionHoursDeviation).HasColumnName("Production_Hours_Deviation");
            entity.Property(e => e.TurnoverDeviation)
                .HasColumnType("money")
                .HasColumnName("Turnover_Deviation");
            entity.Property(e => e.UtilisationQuarter)
                .HasColumnType("money")
                .HasColumnName("Utilisation_Quarter");
            entity.Property(e => e.WipDeviation)
                .HasColumnType("money")
                .HasColumnName("WIP_Deviation");
            entity.Property(e => e.WorkInHandHoursQuarter).HasColumnName("Work_In_Hand_Hours_Quarter");
            entity.Property(e => e.WorkInHandMoneyQuarter)
                .HasColumnType("money")
                .HasColumnName("Work_In_Hand_Money_Quarter");
        });

        modelBuilder.Entity<KeyHighlight>(entity =>
        {
            entity.HasKey(e => e.KeyHighlightsId).HasName("PK__Key_High__257931E987896825");

            entity.ToTable("Key_Highlights");

            entity.Property(e => e.KeyHighlightsId).HasColumnName("Key_Highlights_Id");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__1CB92E037A0932F6");

            entity.Property(e => e.ProjectId).HasColumnName("Project_Id");
            entity.Property(e => e.ForecastOverallDeviation)
                .HasColumnType("money")
                .HasColumnName("Forecast_Overall_Deviation");
            entity.Property(e => e.ForecastOverallForecast)
                .HasColumnType("money")
                .HasColumnName("Forecast_Overall_Forecast");
        });

        modelBuilder.Entity<ProjectIndividual>(entity =>
        {
            entity.HasKey(e => e.ProjectIndividualId).HasName("PK__Project___D45D6EACB196AC01");

            entity.ToTable("Project_Individual");

            entity.Property(e => e.ProjectIndividualId).HasColumnName("Project_Individual_Id");
            entity.Property(e => e.Deviation).HasColumnType("money");
            entity.Property(e => e.ForecastProfit)
                .HasColumnType("money")
                .HasColumnName("Forecast_Profit");
            entity.Property(e => e.IsBottom).HasColumnName("Is_Bottom");
            entity.Property(e => e.ProjectCode).HasColumnName("Project_Code");
            entity.Property(e => e.ProjectId).HasColumnName("Project_Id");
            entity.Property(e => e.ProjectName).HasColumnName("Project_Name");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectIndividuals)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Project_Individual_Project_Id");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__30FA9DD1D8F4FD4E");

            entity.Property(e => e.ReportId).HasColumnName("Report_Id");
            entity.Property(e => e.AdminAndResourcesId).HasColumnName("Admin_And_Resources_Id");
            entity.Property(e => e.BusinessDevelopmentId).HasColumnName("Business_Development_Id");
            entity.Property(e => e.CreatedByUserId).HasColumnName("Created_By_User_Id");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.FinancialsId).HasColumnName("Financials_Id");
            entity.Property(e => e.FromDateRange)
                .HasColumnType("datetime")
                .HasColumnName("From_Date_Range");
            entity.Property(e => e.IsDeleted).HasColumnName("Is_Deleted");
            entity.Property(e => e.IsDraft).HasColumnName("Is_Draft");
            entity.Property(e => e.KeyHighlightsId).HasColumnName("Key_Highlights_Id");
            entity.Property(e => e.ProjectId).HasColumnName("Project_Id");
            entity.Property(e => e.ReportName).HasColumnName("Report_Name");
            entity.Property(e => e.StrategyId).HasColumnName("Strategy_Id");
            entity.Property(e => e.ToDateRange)
                .HasColumnType("datetime")
                .HasColumnName("To_Date_Range");

            entity.HasOne(d => d.AdminAndResources).WithMany(p => p.Reports)
                .HasForeignKey(d => d.AdminAndResourcesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Admin_And_Resources");

            entity.HasOne(d => d.BusinessDevelopment).WithMany(p => p.Reports)
                .HasForeignKey(d => d.BusinessDevelopmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Business_Development_Id");

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Created_By_User_Id");

            entity.HasOne(d => d.Financials).WithMany(p => p.Reports)
                .HasForeignKey(d => d.FinancialsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Financials_Id");

            entity.HasOne(d => d.KeyHighlights).WithMany(p => p.Reports)
                .HasForeignKey(d => d.KeyHighlightsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Key_Highlights_Id");

            entity.HasOne(d => d.Project).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Project_Id");

            entity.HasOne(d => d.Strategy).WithMany(p => p.Reports)
                .HasForeignKey(d => d.StrategyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Strategy_Id");
        });

        modelBuilder.Entity<Strategy>(entity =>
        {
            entity.HasKey(e => e.StrategyId).HasName("PK__Strategy__E5DD67A37457D499");

            entity.ToTable("Strategy");

            entity.Property(e => e.StrategyId).HasColumnName("Strategy_Id");
            entity.Property(e => e.BusinessDevelopment).HasColumnName("Business_Development");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__206D9170FEAFF174");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.IsApproved).HasColumnName("Is_Approved");
        });

        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.HasKey(e => e.UserClaimsId).HasName("PK__User_Cla__F98D208FD8673F6A");

            entity.ToTable("User_Claims");

            entity.Property(e => e.UserClaimsId).HasColumnName("User_Claims_Id");
            entity.Property(e => e.ClaimId).HasColumnName("Claim_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Claim).WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.ClaimId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Claims_Id");

            entity.HasOne(d => d.User).WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_User_Claims");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
