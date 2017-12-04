using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HanoiTower.Shared.Entities;

namespace HanoiTower.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : Entity
    {
        TEntity Add(TEntity obj);
        TEntity NoTransactionAdd(TEntity obj);
        TEntity GetByGuid(Guid id);
        Task<TEntity> GetByGuidAsync(Guid id);
        void Update(TEntity obj);
        void NoTransactionUpdate(TEntity obj);
        void Remove(TEntity obj);
    }
}