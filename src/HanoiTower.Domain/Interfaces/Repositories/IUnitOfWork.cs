namespace HanoiTower.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}