using Microsoft.EntityFrameworkCore;
using Projeto.Livraria.Entidades;

namespace Projeto.Livraria.Dados.Source
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options)
        {
            
        }

        public DbSet<Livro> Livro { get; set; }
    }
}
