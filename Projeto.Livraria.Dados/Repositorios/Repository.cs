using Microsoft.EntityFrameworkCore;
using Projeto.Livraria.Dados.Interfaces;
using Projeto.Livraria.Dados.Source;
using System;
using System.Linq;

namespace Projeto.Livraria.Dados.Repositorios
{
    public class Repositorio<TEntity, ID> : IRepositorio<TEntity, ID>
        where TEntity : class where ID : struct
    {
        protected readonly MySqlContext Db;

        protected readonly DbSet<TEntity> DbSet;

        protected Repositorio(MySqlContext db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public virtual void Delete(ID id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual TEntity Find(ID id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }
    }
}