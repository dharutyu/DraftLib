using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using DraftLib.Core;

namespace DraftLib.DAL
{

    internal class TestDbContext : DbContext
    {
        public TestDbContext()
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            TypeProvider<EntityBase>.ChildTypes
                .ToList()
                .ForEach(t =>
                {
                    dynamic config = Activator.CreateInstance(t);
                    modelBuilder.Configurations.Add(config);
                });
        }
    }
}