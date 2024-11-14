using POSIMSWebApi.Application.Dtos.ProductDtos;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface IProductService
    {
        Task<IList<ProductWithCategDto>> GetAllProductsWithCategory();
        Task<string> CreateProduct(CreateProductDto input);
    }
}