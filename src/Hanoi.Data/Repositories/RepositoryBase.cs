using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Hanoi.Data.Contexts;
using HanoiTower.Domain.Interfaces.Repositories;
using HanoiTower.Shared.Entities;

namespace Hanoi.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity: Entity
    {
        protected readonly HanoiDataContext Context;

        public RepositoryBase(HanoiDataContext context)
        {
            Context = context;
        }

        public TEntity Add(TEntity obj)
        {
            return Context.Set<TEntity>().Add(obj);
        }

        public TEntity NoTransactionAdd(TEntity obj)
        {
            var robj = Context.Set<TEntity>().Add(obj);
            Context.SaveChanges();
            return robj;
        }

        public TEntity GetByGuid(Guid id)
        {
            return Context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public Task<TEntity> GetByGuidAsync(Guid id)
        {
            return Context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(TEntity obj)
        {
            Context.Entry(obj).State = EntityState.Modified;
        }

        public void NoTransactionUpdate(TEntity obj)
        {
            Context.Entry(obj).State = EntityState.Modified;
            Context.SaveChanges();
        }


        public void Remove(TEntity obj)
        {
            var robj= Context.Set<TEntity>().FirstOrDefault(t => t.Id == obj.Id);

            if (robj != null) Context.Set<TEntity>().Remove(robj);
        }
        
    }
}