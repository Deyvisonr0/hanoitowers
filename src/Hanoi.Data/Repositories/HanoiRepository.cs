using System.Linq;
using Hanoi.Data.Contexts;
using HanoiTower.Domain.Enums;
using HanoiTower.Domain.Interfaces.Repositories;

namespace Hanoi.Data.Repositories
{
    public class HanoiRepository : RepositoryBase<HanoiTower.Domain.Entities.Hanoi>, IHanoiRepository
    {
        public HanoiRepository(HanoiDataContext context) : base(context)
        {
        }

        public IQueryable<HanoiTower.Domain.Entities.Hanoi> GetAllFinishedHanois()
        {
            return Context.Hanois.Where(x => x.EStatus == EStatus.Closed);
        }
    }
}