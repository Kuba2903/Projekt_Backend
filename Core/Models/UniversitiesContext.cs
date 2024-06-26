﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Core.Models;

public partial class UniversitiesContext : DbContext
{
    public UniversitiesContext()
    {
    }

    public UniversitiesContext(DbContextOptions<UniversitiesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<RankingCriterion> RankingCriteria { get; set; }

    public virtual DbSet<RankingSystem> RankingSystems { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<UniversityRankingYear> UniversityRankingYears { get; set; }

    public virtual DbSet<UniversityYear> UniversityYears { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=universities;uid=root;pwd=pass", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.4.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("country");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryName)
                .HasMaxLength(100)
                .HasColumnName("country_name");
        });

        modelBuilder.Entity<RankingCriterion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ranking_criteria");

            entity.HasIndex(e => e.RankingSystemId, "fk_rc_rs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CriteriaName)
                .HasMaxLength(200)
                .HasColumnName("criteria_name");
            entity.Property(e => e.RankingSystemId).HasColumnName("ranking_system_id");

            entity.HasOne(d => d.RankingSystem).WithMany(p => p.RankingCriteria)
                .HasForeignKey(d => d.RankingSystemId)
                .HasConstraintName("fk_rc_rs");
        });

        modelBuilder.Entity<RankingSystem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ranking_system");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SystemName)
                .HasMaxLength(100)
                .HasColumnName("system_name");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("university");

            entity.HasIndex(e => e.CountryId, "fk_uni_cnt");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.UniversityName)
                .HasMaxLength(200)
                .HasColumnName("university_name");

            entity.HasOne(d => d.Country).WithMany(p => p.Universities)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("fk_uni_cnt");
        });

        modelBuilder.Entity<UniversityRankingYear>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("university_ranking_year");

            entity.HasIndex(e => e.RankingCriteriaId, "fk_ury_rc");

            entity.HasIndex(e => e.UniversityId, "fk_ury_uni");

            entity.Property(e => e.RankingCriteriaId).HasColumnName("ranking_criteria_id");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.RankingCriteria).WithMany()
                .HasForeignKey(d => d.RankingCriteriaId)
                .HasConstraintName("fk_ury_rc");

            entity.HasOne(d => d.University).WithMany()
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("fk_ury_uni");
        });

        modelBuilder.Entity<UniversityYear>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("university_year");

            entity.HasIndex(e => e.UniversityId, "fk_uy_uni");

            entity.Property(e => e.NumStudents).HasColumnName("num_students");
            entity.Property(e => e.PctFemaleStudents).HasColumnName("pct_female_students");
            entity.Property(e => e.PctInternationalStudents).HasColumnName("pct_international_students");
            entity.Property(e => e.StudentStaffRatio)
                .HasPrecision(6, 2)
                .HasColumnName("student_staff_ratio");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.University).WithMany()
                .HasForeignKey(d => d.UniversityId)
                .HasConstraintName("fk_uy_uni");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
