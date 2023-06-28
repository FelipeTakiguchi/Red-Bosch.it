using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Model;

public partial class RedBoschContext : DbContext
{
    public RedBoschContext()
    {
    }

    public RedBoschContext(DbContextOptions<RedBoschContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<Forum> Forums { get; set; }

    public virtual DbSet<ImageDatum> ImageData { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Permissao> Permissaos { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioCargo> UsuarioCargos { get; set; }

    public virtual DbSet<UsuarioForum> UsuarioForums { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CT-C-0013L\\SQLEXPRESS;Initial Catalog=RedBosch;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cargo__3214EC074B830BAE");

            entity.ToTable("Cargo");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPermissaoNavigation).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.IdPermissao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cargo__IdPermiss__35BCFE0A");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cargo__IdUsuario__36B12243");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comentar__3214EC07DBD43C22");

            entity.ToTable("Comentario");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Conteudo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DataPublicacao).HasColumnType("date");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comentari__IdPos__47DBAE45");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comentari__IdUsu__46E78A0C");
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forum__3214EC07D5240D62");

            entity.ToTable("Forum");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Forums)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Forum__IdUsuario__2D27B809");

            entity.HasOne(d => d.LocationNavigation).WithMany(p => p.Forums)
                .HasForeignKey(d => d.Location)
                .HasConstraintName("FK__Forum__Location__2C3393D0");
        });

        modelBuilder.Entity<ImageDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ImageDat__3214EC2762CE23EC");

            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3214EC2713824653");

            entity.ToTable("Location");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.HasOne(d => d.PhotoNavigation).WithMany(p => p.Locations)
                .HasForeignKey(d => d.Photo)
                .HasConstraintName("FK__Location__Photo__267ABA7A");
        });

        modelBuilder.Entity<Permissao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissa__3214EC07F78D8686");

            entity.ToTable("Permissao");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Post__3214EC0776029817");

            entity.ToTable("Post");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Conteudo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DataPublicacao).HasColumnType("date");

            entity.HasOne(d => d.IdForumNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdForum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__IdForum__403A8C7D");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__IdUsuario__3F466844");

            entity.HasOne(d => d.LocationNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Location)
                .HasConstraintName("FK__Post__Location__3E52440B");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07C55F74FC");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DataNascimento)
                .HasColumnType("date")
                .HasColumnName("Data_Nascimento");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Salt)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(150);

            entity.HasOne(d => d.LocationNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Location)
                .HasConstraintName("FK__Usuario__Locatio__29572725");
        });

        modelBuilder.Entity<UsuarioCargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsuarioC__3214EC07DEC38D97");

            entity.ToTable("UsuarioCargo");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.UsuarioCargos)
                .HasForeignKey(d => d.IdCargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioCa__IdCar__398D8EEE");

            entity.HasOne(d => d.IdForumNavigation).WithMany(p => p.UsuarioCargos)
                .HasForeignKey(d => d.IdForum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioCa__IdFor__3B75D760");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioCargos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioCa__IdUsu__3A81B327");
        });

        modelBuilder.Entity<UsuarioForum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsuarioF__3214EC07CF8D1116");

            entity.ToTable("UsuarioForum");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdForumNavigation).WithMany(p => p.UsuarioForums)
                .HasForeignKey(d => d.IdForum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioFo__IdFor__30F848ED");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioForums)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioFo__IdUsu__300424B4");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vote__3214EC072AB288A4");

            entity.ToTable("Vote");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Votes)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vote__IdPost__440B1D61");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Votes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vote__IdUsuario__4316F928");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
