using System;
using System.Linq;

namespace Projeto.Livraria.Dados.Interfaces
{
    public interface IRepositorio<TEntity, ID> : IDisposable 
        where TEntity : class where ID : struct
    {

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(ID id);
        TEntity Find(ID id);
        IQueryable<TEntity> GetAll();
        int SaveChanges();
    }
}
