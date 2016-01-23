using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

namespace DraftLib.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        private bool disposed;
        private Hashtable repositories;

        public UnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public UnitOfWork()
        {
            context = new TestDbContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    context.Dispose();

            disposed = true;
        }

        public IRepository<T> Repository<T>() where T : EntityBase
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                        .MakeGenericType(typeof(T)), context);

                repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)repositories[type];
        }
    }
}