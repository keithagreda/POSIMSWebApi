using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IList<Product>> GetAllProductsAsync();
    }
}
