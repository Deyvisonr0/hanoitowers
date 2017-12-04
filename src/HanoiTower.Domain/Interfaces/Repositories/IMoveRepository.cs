using System;
using System.Threading.Tasks;
using HanoiTower.Domain.Entities;

namespace HanoiTower.Domain.Interfaces.Repositories
{
    public interface IMoveRepository : IRepositoryBase<Move>
    {
        Move GetLastMoveByHanoi(Guid hanoiId);
    }
}