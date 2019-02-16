using Microsoft.EntityFrameworkCore;
using Projeto.Livraria.Dados.Interfaces;
using Projeto.Livraria.Dados.Source;
using Projeto.Livraria.Entidades;
using System.Linq;

namespace Projeto.Livraria.Dados.Repositorios
{
    public class UsuarioRepositorio : Repositorio<Usuario, int>, IUsuarioRepositorio
    {
        public UsuarioRepositorio(MySqlContext db) : base(db)
        {
        }

        public Usuario Login(Usuario usuario) => Db.Usuario.FirstOrDefault(x => x.Email.Equals(usuario.Email) && x.Senha.Equals(usuario.Senha));

        public override IQueryable<Usuario> GetAll()
        {
            return DbSet.AsQueryable();
        }
    }
}
