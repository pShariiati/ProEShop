using System.Threading.Tasks;
using ProEShop.Entities;

namespace ProEShop.Services.Contracts
{
    public interface IGenericService<TEntity> where TEntity : EntityBase, new()
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(long id);
        Task<TEntity> FindByIdAsync(long id);
        Task<bool> IsExistsByIdAsync(long id);
    }
}