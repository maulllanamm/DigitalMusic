using DigitalMusic.Domain.Common;

namespace DigitalMusic.Application.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Entity> GetBaseRepository<Entity>() where Entity : class, IBaseEntity;
        IBaseGuidRepository<Entity> GetBaseGuidRepository<Entity>() where Entity : class, IBaseGuidEntity;
        void BeginTransaction();
        void Commit();
        void Rollback();
        int SaveChanges();
        void Dispose();
    }
}
