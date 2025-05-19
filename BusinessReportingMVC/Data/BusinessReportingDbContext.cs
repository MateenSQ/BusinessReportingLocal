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
        => optionsBuilder.UseInMemoryDatabase("InMemoryDB");
        //=> optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdminAndResource>(entity =>
        {
            entity.HasKey(e => e.AdminAndResourcesId);
        });

        modelBuilder.Entity<BusinessDevelopment>(entity =>
        {
            entity.HasOne(d => d.BusinessDevelopmentNotes).WithMany(p => p.BusinessDevelopments)
                .HasForeignKey(d => d.BusinessDevelopmentNotesId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.BusinessDevelopmentValue).WithMany(p => p.BusinessDevelopments)
                .HasForeignKey(d => d.BusinessDevelopmentValueId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<BusinessDevelopmentNote>(entity =>
        {
            entity.HasKey(e => e.BusinessDevelopmentNotesId);
        });

        modelBuilder.Entity<BusinessDevelopmentValue>(entity =>
        {
            entity.HasKey(e => e.BusinessDevelopmentValueId);
        });

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.ClaimId);
        });

        modelBuilder.Entity<Financial>(entity =>
        {
            entity.HasKey(e => e.FinancialsId);

            entity.HasOne(d => d.FinancialsActual).WithMany(p => p.Financials)
                .HasForeignKey(d => d.FinancialsActualId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.FinancialsDeviation).WithMany(p => p.Financials)
                .HasForeignKey(d => d.FinancialsDeviationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<FinancialsActual>(entity =>
        {
            entity.HasKey(e => e.FinancialsActualId);
        });

        modelBuilder.Entity<FinancialsDeviation>(entity =>
        {
            entity.HasKey(e => e.FinancialsDeviationId);
        });

        modelBuilder.Entity<KeyHighlight>(entity =>
        {
            entity.HasKey(e => e.KeyHighlightsId);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId);
        });

        modelBuilder.Entity<ProjectIndividual>(entity =>
        {
            entity.HasKey(e => e.ProjectIndividualId);

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectIndividuals)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId);

            entity.HasOne(d => d.AdminAndResources).WithMany(p => p.Reports)
                .HasForeignKey(d => d.AdminAndResourcesId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.BusinessDevelopment).WithMany(p => p.Reports)
                .HasForeignKey(d => d.BusinessDevelopmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.CreatedByUser).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Financials).WithMany(p => p.Reports)
                .HasForeignKey(d => d.FinancialsId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.KeyHighlights).WithMany(p => p.Reports)
                .HasForeignKey(d => d.KeyHighlightsId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Project).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Strategy).WithMany(p => p.Reports)
                .HasForeignKey(d => d.StrategyId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Strategy>(entity =>
        {
            entity.HasKey(e => e.StrategyId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
        });

        modelBuilder.Entity<UserClaim>(entity =>
        {
            entity.HasKey(e => e.UserClaimsId);

            entity.HasOne(d => d.Claim).WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.ClaimId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.User).WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
