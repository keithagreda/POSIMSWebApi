using Domain.ApiResponse;
using LanguageExt.Common;
using POSIMSWebApi.Application.Dtos.ProductDtos;

namespace POSIMSWebApi.Application.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<IList<ProductWithCategDto>>> GetAllProductsWithCategory();
        Task<ApiResponse<string>> CreateProduct(CreateProductDto input);
    }
}