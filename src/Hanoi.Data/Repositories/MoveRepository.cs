using System;
using System.Linq;
using System.Threading.Tasks;
using Hanoi.Data.Contexts;
using HanoiTower.Domain.Entities;
using HanoiTower.Domain.Interfaces.Repositories;

namespace Hanoi.Data.Repositories
{
    public class MoveRepository : RepositoryBase<Move>, IMoveRepository
    {
        public MoveRepository(HanoiDataContext context) : base(context)
        {
        }

        public Move GetLastMoveByHanoi(Guid hanoiId)
        {
            var hanoi = Context.Hanois.FirstOrDefault(x => x.Id == hanoiId);
            return hanoi?.Moves.OrderByDescending(x => x.Step).FirstOrDefault();
        }
    }
}