using Hanoi.Data.Contexts;
using HanoiTower.Domain.Interfaces.Repositories;

namespace Hanoi.Data.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HanoiDataContext _context;

        public UnitOfWork(HanoiDataContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
        
    }
}