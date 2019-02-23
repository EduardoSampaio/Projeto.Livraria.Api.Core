using System;

namespace Projeto.Livraria.Entidades
{
    public class Livro
    {
        public Livro(int id, string nome, string autor, int ano)
        {
            Id = id;
            Nome = nome;
            Autor = autor;
            Ano = ano;
        }

        protected Livro()
        {
        }

        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Autor { get; set; }
        public virtual int Ano { get; set; }

        public override bool Equals(object obj)
        {
            var livro = obj as Livro;
            return livro != null &&
                   Id == livro.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override string ToString()
        {
            return $"Id: {Id} Nome: {Nome} Autor: {Autor} Ano: {Ano}";
        }
    }
}