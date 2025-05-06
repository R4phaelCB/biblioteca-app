using System;
using System.Linq;

namespace BibliotecaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BibliotecaContext())
            {
                // Exemplo: Cadastro de um livro
                var livro = new Livro { Titulo = "Dom Casmurro", Autor = "Machado de Assis" };
                db.Livros.Add(livro);
                db.SaveChanges();

                // Exemplo: Cadastro de um usuário
                var usuario = new Usuario { Nome = "João Silva" };
                db.Usuarios.Add(usuario);
                db.SaveChanges();

                // Exemplo: Empréstimo de um livro
                var emprestimo = new Emprestimo
                {
                    LivroId = livro.LivroId,
                    UsuarioId = usuario.UsuarioId,
                    DataEmprestimo = DateTime.Now
                };
                livro.Disponivel = false;
                db.Emprestimos.Add(emprestimo);
                db.SaveChanges();

                // Exemplo: Devolução de um livro
                emprestimo.DataDevolucao = DateTime.Now;
                livro.Disponivel = true;
                db.SaveChanges();

                // Relatório de livros disponíveis
                var livrosDisponiveis = db.Livros.Where(l => l.Disponivel).ToList();
                Console.WriteLine("Livros disponíveis:");
                foreach (var l in livrosDisponiveis)
                {
                    Console.WriteLine($"- {l.Titulo} por {l.Autor}");
                }

                // Relatório de livros emprestados
                var livrosEmprestados = db.Emprestimos
                    .Where(e => e.DataDevolucao == null)
                    .Select(e => new { e.Livro.Titulo, e.Usuario.Nome, e.DataEmprestimo })
                    .ToList();

                Console.WriteLine("\nLivros emprestados:");
                foreach (var e in livrosEmprestados)
                {
                    Console.WriteLine($"- {e.Titulo} emprestado para {e.Nome} em {e.DataEmprestimo}");
                }
            }
        }
    }
}
