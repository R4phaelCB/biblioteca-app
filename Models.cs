using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BibliotecaApp
{
    // Entidade Livro
    public class Livro
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public bool Disponivel { get; set; } = true;
    }

    // Entidade Usuario
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nome { get; set; }
    }

    // Entidade Emprestimo
    public class Emprestimo
    {
        public int EmprestimoId { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime DataEmprestimo { get; set; } = DateTime.Now;
        public DateTime? DataDevolucao { get; set; }
    }

    // Contexto do banco de dados
    public class BibliotecaContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=biblioteca.db");
    }
}
