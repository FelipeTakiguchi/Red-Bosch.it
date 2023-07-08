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

    public virtual DbSet<Permissao> Permissaos { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioCargo> UsuarioCargos { get; set; }

    public virtual DbSet<UsuarioForum> UsuarioForums { get; set; }

    public virtual DbSet<Vote> Votes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=RedBosch;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cargo__3214EC075EC6FFD1");

            entity.ToTable("Cargo");

            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPermissaoNavigation).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.IdPermissao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cargo__IdPermiss__45F365D3");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Cargos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cargo__IdUsuario__46E78A0C");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Comentar__3214EC0768BDBBE5");

            entity.ToTable("Comentario");

            entity.Property(e => e.Conteudo)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DataPublicacao).HasColumnType("date");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comentari__IdPos__5812160E");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comentari__IdUsu__571DF1D5");
        });

        modelBuilder.Entity<Forum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Forum__3214EC073CCE4375");

            entity.ToTable("Forum");

            entity.Property(e => e.Descricao)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Titulo)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Forums)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Forum__IdUsuario__3D5E1FD2");

            entity.HasOne(d => d.Image).WithMany(p => p.Forums)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK__Forum__ImageId__3C69FB99");
        });

        modelBuilder.Entity<ImageDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ImageDat__3214EC27738EE31E");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Photo).IsRequired();
        });

        modelBuilder.Entity<Permissao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permissa__3214EC071AA74CB1");

            entity.ToTable("Permissao");

            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Post__3214EC07368CDEF3");

            entity.ToTable("Post");

            entity.Property(e => e.Conteudo)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DataPublicacao).HasColumnType("date");

            entity.HasOne(d => d.IdForumNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdForum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__IdForum__5070F446");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Post__IdUsuario__4F7CD00D");

            entity.HasOne(d => d.Image).WithMany(p => p.Posts)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK__Post__ImageId__4E88ABD4");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC07F6074434");

            entity.ToTable("Usuario");

            entity.Property(e => e.DataNascimento)
                .HasColumnType("date")
                .HasColumnName("Data_Nascimento");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Salt)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .IsRequired()
                .HasMaxLength(150);

            entity.HasOne(d => d.Image).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK__Usuario__ImageId__398D8EEE");
        });

        modelBuilder.Entity<UsuarioCargo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsuarioC__3214EC07A6856BC8");

            entity.ToTable("UsuarioCargo");

            entity.HasOne(d => d.IdCargoNavigation).WithMany(p => p.UsuarioCargos)
                .HasForeignKey(d => d.IdCargo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioCa__IdCar__49C3F6B7");

            entity.HasOne(d => d.IdForumNavigation).WithMany(p => p.UsuarioCargos)
                .HasForeignKey(d => d.IdForum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioCa__IdFor__4BAC3F29");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioCargos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioCa__IdUsu__4AB81AF0");
        });

        modelBuilder.Entity<UsuarioForum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsuarioF__3214EC07B237F435");

            entity.ToTable("UsuarioForum");

            entity.HasOne(d => d.IdForumNavigation).WithMany(p => p.UsuarioForums)
                .HasForeignKey(d => d.IdForum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioFo__IdFor__412EB0B6");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioForums)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioFo__IdUsu__403A8C7D");
        });

        modelBuilder.Entity<Vote>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vote__3214EC07DC7D093E");

            entity.ToTable("Vote");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Votes)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vote__IdPost__5441852A");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Votes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Vote__IdUsuario__534D60F1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
