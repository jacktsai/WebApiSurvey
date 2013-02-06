using System.Collections.Generic;

namespace WebApi.DAL
{
    public interface IDao<TEntity, TSelectArgs>
        where TEntity : class
        where TSelectArgs : class
    {
        IEnumerable<TEntity> Select(TSelectArgs args);

        void Delete(TEntity entity);

        void Insert(TEntity entity);

        void Update(TEntity entity);
    }
}