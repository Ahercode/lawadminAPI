using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lawadmin.WebAPI.Entities;

public partial class LawAdminDB_Context : DbContext
{
    public LawAdminDB_Context()
    {
    }

    public LawAdminDB_Context(DbContextOptions<LawAdminDB_Context> options)
        : base(options)
    {
    }

    public virtual DbSet<CaseMonth> CaseMonths { get; set; }

    public virtual DbSet<CaseYear> CaseYears { get; set; }

    public virtual DbSet<Court> Courts { get; set; }

    public virtual DbSet<CourtCase> CourtCases { get; set; }

    public virtual DbSet<CourtCaseQuorom> CourtCaseQuoroms { get; set; }

    public virtual DbSet<Quorom> Quoroms { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CaseMonth>(entity =>
        {
            entity.Property(e => e.Name).IsFixedLength();
            entity.Property(e => e.Status).IsFixedLength();

            entity.HasOne(d => d.CaseYear).WithMany(p => p.CaseMonths).HasConstraintName("FK_CaseMonth_CaseYear");
        });

        modelBuilder.Entity<CaseYear>(entity =>
        {
            entity.Property(e => e.Name).IsFixedLength();
            entity.Property(e => e.Status).IsFixedLength();
        });

        modelBuilder.Entity<Court>(entity =>
        {
            entity.Property(e => e.Status).IsFixedLength();
        });

        modelBuilder.Entity<CourtCase>(entity =>
        {
            entity.Property(e => e.Status).IsFixedLength();

            entity.HasOne(d => d.CaseMonth).WithMany(p => p.CourtCases).HasConstraintName("FK_CourtCase_CaseMonth");

            entity.HasOne(d => d.Court).WithMany(p => p.CourtCases).HasConstraintName("FK_CourtCase_Court");
        });

        modelBuilder.Entity<CourtCaseQuorom>(entity =>
        {
            entity.HasOne(d => d.CourtCase).WithMany(p => p.CourtCaseQuoroms).HasConstraintName("FK_CourtCaseQuorom_CourtCase");

            entity.HasOne(d => d.Quorom).WithMany(p => p.CourtCaseQuoroms).HasConstraintName("FK_CourtCaseQuorom_Quorom");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
