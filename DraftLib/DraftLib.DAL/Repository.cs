using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DraftLib.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : EntityBase
    {
        protected DbContext context;
        protected IDbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual TEntity FindById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void InsertGraph(TEntity entity)
        {
            entity.Created = DateTime.Now;
            entity.Modified = DateTime.Now;
            context.Entry(entity).State = EntityState.Added;
            dbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            entity.Modified = DateTime.Now;
            context.Entry(entity).State = EntityState.Modified;
            dbSet.Attach(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = dbSet.Find(id);
            context.Entry(entity).State = EntityState.Deleted;
            
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            dbSet.Attach(entity);
            dbSet.Remove(entity);
        }

        public virtual void Insert(TEntity entity)
        {
            entity.Created = DateTime.Now;
            entity.Modified = DateTime.Now;
            context.Entry(entity).State = EntityState.Added;
            dbSet.Attach(entity);
        }

        public virtual RepositoryQuery<TEntity> Query()
        {
            var repositoryGetFluentHelper =
                new RepositoryQuery<TEntity>(this);

            return repositoryGetFluentHelper;
        }

        internal IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>>
                includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = dbSet;
            
            includeProperties?.ForEach(i => { query = query.Include(i); });

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);
         
            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query;
        }
    }
}