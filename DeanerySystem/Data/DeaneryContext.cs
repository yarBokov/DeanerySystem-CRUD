using System;
using System.Collections.Generic;
using DeanerySystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeanerySystem.Data;

public partial class DeaneryContext : DbContext
{
    public DeaneryContext()
    {
    }

    public DeaneryContext(DbContextOptions<DeaneryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Mark> Marks { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    //public virtual DbSet<TeacherSubjectAvg> TeacherSubjectAvgs { get; set; }

    //public virtual DbSet<YearAvg> YearAvgs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Deanery;Username=postgres;Password=Uo987kt");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("groups_pkey");

            entity.ToTable("groups");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("marks_pkey");

            entity.ToTable("marks");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            entity.Property(e => e.Value).HasColumnName("value");

            entity.HasOne(d => d.Student).WithMany(p => p.MarkStudents)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("fk_marks_people1");

            entity.HasOne(d => d.Subject).WithMany(p => p.Marks)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("fk_marks_subjects");

            entity.HasOne(d => d.Teacher).WithMany(p => p.MarkTeachers)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("fk_marks_people2");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("people_pkey");

            entity.ToTable("people");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .HasColumnName("first_name");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.PatherName)
                .HasMaxLength(20)
                .HasColumnName("pather_name");
            entity.Property(e => e.SecondName)
                .HasMaxLength(20)
                .HasColumnName("second_name");
            entity.Property(e => e.Type)
                .HasColumnType("char")
                .HasColumnName("type");

            entity.HasOne(d => d.Group).WithMany(p => p.People)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("fk_people_groups");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("subjects_pkey");

            entity.ToTable("subjects");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        //modelBuilder.Entity<TeacherSubjectAvg>(entity =>
        //{
        //    entity
        //        .HasNoKey()
        //        .ToView("teacher_subject_avg");

        //    entity.Property(e => e.AvgMark).HasColumnName("avg_mark");
        //    entity.Property(e => e.SubjectName)
        //        .HasMaxLength(50)
        //        .HasColumnName("subject_name");
        //    entity.Property(e => e.TeacherName).HasColumnName("teacher_name");
        //});

        //modelBuilder.Entity<YearAvg>(entity =>
        //{
        //    entity
        //        .HasNoKey()
        //        .ToView("year_avg");

        //    entity.Property(e => e.AvgMark).HasColumnName("avg_mark");
        //    entity.Property(e => e.Year).HasColumnName("year");
        //});

        //OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
