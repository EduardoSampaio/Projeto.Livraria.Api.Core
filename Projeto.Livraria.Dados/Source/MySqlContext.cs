using Microsoft.EntityFrameworkCore;
using Projeto.Livraria.Entidades;

namespace Projeto.Livraria.Dados.Source
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {

        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Livro> Livro { get; set; }
    }
}
