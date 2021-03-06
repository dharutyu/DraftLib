using System.Collections.Generic;

namespace DraftLib.DAL
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        TEntity FindById(object id);
        void InsertGraph(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entity);
        void Insert(TEntity entity);
        RepositoryQuery<TEntity> Query();
    }
}