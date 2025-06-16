using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Arna_Project_Track.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-HUJ9RC4\\MSSQLSERVER02;Database=ArnaProjectTrack;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Projects__3214EC075E5DA590");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Priority).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
