using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace WPFSQLite;

public partial class WpfContext : DbContext
{
    public WpfContext()
    {
    }

    public WpfContext(DbContextOptions<WpfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryUser> CategoryUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite($"Data Source={"D:\\WPFSQLite\\WPFSQLite\\wpf.db"}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.NameCategory).HasColumnType("VARCHAR (255)");
        });

        modelBuilder.Entity<CategoryUser>(entity =>
        {
            entity.ToTable("CategoryUser");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cash).HasColumnType("REAL (10, 2)");
            entity.Property(e => e.CategoryId)
                .HasColumnType("INT")
                .HasColumnName("CategoryID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP");
            entity.Property(e => e.UserId)
                .HasColumnType("INT")
                .HasColumnName("UserID");

            entity.HasOne(d => d.Category).WithMany(p => p.CategoryUsers).HasForeignKey(d => d.CategoryId);

            entity.HasOne(d => d.User).WithMany(p => p.CategoryUsers).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FullName).HasColumnType("VARCHAR (255)");
            entity.Property(e => e.Salary).HasColumnType("REAL (10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
