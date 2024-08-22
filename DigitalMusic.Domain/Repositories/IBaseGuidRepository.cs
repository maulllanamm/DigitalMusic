using DigitalMusic.Domain.Common;

namespace DigitalMusic.Application.Repositories
{
    public interface IBaseGuidRepository<TEntity> where TEntity : class, IBaseGuidEntity
    {
        Task<TEntity> Create(TEntity entity);
        Task<int> CreateBulk(List<TEntity> entites);
        Task<bool> Delete(Guid id);
        Task<int> DeleteBulk(List<TEntity> entites);
        Task<bool> SoftDelete(Guid id);
        Task<int> SoftDeleteBulk(List<Guid> entitesId);
        Task<TEntity> Update(TEntity entity);
        Task<int> UpdateBulk(List<TEntity> entites);
        Task<List<TEntity>> GetAll();
        Task<List<TEntity>> GetAll(int page);
        Task<List<TEntity>> GetByListId(List<Guid> listId);
        Task<List<TEntity>> GetByListProperty(string field, string[] values);
        Task<TEntity> GetById(Guid id);
        IEnumerable<TEntity> Filter();
        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);
        Task<int> Count();
    }
}
