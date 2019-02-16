using System;
using System.Collections.Generic;

namespace Projeto.Livraria.Entidades
{
    public class Perfil
    {
        public Perfil(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        protected Perfil()
        {

        }

        public virtual int Id { get; private set; }
        public virtual string Nome { get; private set; }
        public virtual IList<Usuario> Usuarios { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is Perfil perfil &&
                   Id == perfil.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return $"Id: { Id }  Nome: { Nome }";
        }
    }
}
