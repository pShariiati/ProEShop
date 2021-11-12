using System.Collections.Generic;
using System.Threading.Tasks;
using ProEShop.Entities;

namespace ProEShop.Services.Contracts
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<List<Category>> GetAll();
    }
}
