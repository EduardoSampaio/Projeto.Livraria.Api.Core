using System;

namespace Projeto.Livraria.Entidades
{
    public class Usuario
    {
        protected Usuario()
        {

        }

        public Usuario(int id, string nome, string email, string senha, int perfil)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Senha = senha;
            PerfilId = perfil;
        }

        public virtual int Id { get; private set; }
        public virtual string Nome { get; private set; }
        public virtual string Email { get; private set; }
        public virtual string Senha { get; private set; }
        public virtual int PerfilId { get; private set; }
        public virtual Perfil Perfil { get; private set; }

        public void TrocarSenha(string senha) => Senha = senha;

        public void TrocarPerfil(int perfilId) => PerfilId = perfilId;

        public override bool Equals(object obj)
        {
            var usuario = obj as Usuario;
            return usuario != null &&
                   Id == usuario.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return $"[Id: {Id} Nome: {Nome} Email: {Email} Senha: {Senha}]";
        }
    }
}
