using Projeto.Livraria.Dados.Interfaces;
using Projeto.Livraria.Dados.Source;
using Projeto.Livraria.Entidades;

namespace Projeto.Livraria.Dados.Repositorios
{
    public class LivroRepositorio : Repositorio<Livro, int> , ILivroRepositorio
    {
        public LivroRepositorio(MySqlContext db) : base(db)
        {
        }
    }
}
