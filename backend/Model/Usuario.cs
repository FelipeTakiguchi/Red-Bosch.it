using System;
using System.Collections.Generic;

namespace backend.Model;

public partial class Usuario
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public byte[] Senha { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public DateTime DataNascimento { get; set; }

    public int? Location { get; set; }

    public virtual ICollection<Cargo> Cargos { get; set; } = new List<Cargo>();

    public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();

    public virtual ICollection<Forum> Forums { get; set; } = new List<Forum>();

    public virtual Location? LocationNavigation { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<UsuarioCargo> UsuarioCargos { get; set; } = new List<UsuarioCargo>();

    public virtual ICollection<UsuarioForum> UsuarioForums { get; set; } = new List<UsuarioForum>();

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
}
