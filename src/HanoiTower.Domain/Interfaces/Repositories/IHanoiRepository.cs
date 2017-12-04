using System.Collections.Generic;
using System.Linq;
using HanoiTower.Domain.Entities;

namespace HanoiTower.Domain.Interfaces.Repositories
{
    public interface IHanoiRepository : IRepositoryBase<Hanoi>
    {
        IQueryable<Hanoi> GetAllFinishedHanois();
    }
}