using Projeto.Livraria.Dados.Interfaces;
using Projeto.Livraria.Dados.Source;
using Projeto.Livraria.Entidades;

namespace Projeto.Livraria.Dados.Repositorios
{
    public class PerfilRepositorio : Repositorio<Perfil, int> , IPerfilRepositorio
    {
        public PerfilRepositorio(MySqlContext db) : base(db)
        {
        }
    
    }
}
