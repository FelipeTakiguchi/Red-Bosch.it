using System;
using System.Collections.Generic;
using backend.Model;
using Microsoft.EntityFrameworkCore;

namespace backend.Model;

public partial class FullExampleContext : DbContext
{
    public FullExampleContext()
    {
    }

    public FullExampleContext(DbContextOptions<FullExampleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ImageDatum> ImageData { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CT-C-0013L\\SQLEXPRESS;Initial Catalog=FullExample;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ImageDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ImageDat__3214EC27BBD54E46");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC276FECDC96");

            entity.ToTable("Location");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.PhotoNavigation).WithMany(p => p.Locations)
                .HasForeignKey(d => d.Photo)
                .HasConstraintName("FK__Location__Photo__267ABA7A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
