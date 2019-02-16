using Projeto.Livraria.Entidades;

namespace Projeto.Livraria.Dados.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario,int>
    {
        Usuario Login(Usuario usuario);
    }
}
