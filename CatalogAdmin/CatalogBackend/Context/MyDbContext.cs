using System;
using System.Collections.Generic;
using CatalogBackend.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogBackend.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Orar> Orars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-410LQCF;Database=Orar;User Id=admin;Password=123;Trusted_Connection=True;Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Orar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orar__3213E83F5593F8F1");

            entity.ToTable("orar");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Class)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("class");
            entity.Property(e => e.Classroom)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("classroom");
            entity.Property(e => e.DayOffWeek)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("dayOFfWeek");
            entity.Property(e => e.Grp)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("grp");
            entity.Property(e => e.Hours)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("hours");
            entity.Property(e => e.Module)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("module");
            entity.Property(e => e.Teacher)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("teacher");
            entity.Property(e => e.Type)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.Week)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("week");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
