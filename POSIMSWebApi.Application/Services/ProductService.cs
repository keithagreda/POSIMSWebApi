using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using POSIMSWebApi.Application.Dtos.ProductDtos;
using POSIMSWebApi.Application.Interfaces;

namespace POSIMSWebApi.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<ProductWithCategDto>> GetAllProductsWithCategory()
        {
            var data = _unitOfWork.Product.GetQueryable().Include(e => e.ProductCategories).Select(e => new ProductWithCategDto
            {
                ProductId = e.Id,
                ProductName = e.Name,
                ProductCategories = e.ProductCategories.Select(e => e.Name).ToList()
            }).AsNoTracking().AsSplitQuery();

            return await data.ToListAsync();
        }

        public async Task<string> CreateProduct(CreateProductDto input)
        {
            //data
            //validation
            var query = _unitOfWork.Product.GetQueryable();

            var isExist = await query.AnyAsync(e => e.Name == input.Name);

            if (isExist)
            {
                //if(input.ProductCategoryId != 0)
                //{
                //    var getProductCategories = await _unitOfWork.Product.GetQueryable().Where(e => e.Name == input.Name)
                //    .Include(e => e.ProductCategories)
                //    .Select(e => e.ProductCategories.Select(e => e.Id)).FirstOrDefaultAsync();
                //    if (getProductCategories.Contains(input.ProductCategoryId))
                //    {
                //        return "Invalid Action! Product with this category already exists!";
                //    }

                //    if (!getProductCategories.Contains(input.ProductCategoryId))
                //    {
                //        //save categ
                //        var product = await _unitOfWork.Product.FirstOrDefaultAsync(e => e.Name == input.Name);
                //        var addNewCateg = await _unitOfWork.ProductCategory.FirstOrDefaultAsync(e => e.Id == input.ProductCategoryId);
                //        if (addNewCateg is null) return "Product Creation failed inputted Category doesn't exist!";
                //        product.ProductCategories.Add(addNewCateg);

                //    }
                //}
                
                return "Invalid action! Product name already exists!";
            }
            //getCateg
            var generatedProdCode = GenerateProdCode(input.Name);

            var getExistingCode = await query.Where(e => e.ProdCode.Contains(generatedProdCode)).ToListAsync();

            var prodCode = $"{generatedProdCode}{getExistingCode.Count + 1}";


            ProductCategory? categ = null;
            if(input.ProductCategoryId != 0)
            {
                categ = await _unitOfWork.ProductCategory.FirstOrDefaultAsync(e => e.Id == input.ProductCategoryId);
                if (categ is null) return "Product Creation failed input Category doesn't exist!";
            }
            var newProduct = new Product
            {
                Name = input.Name,
                Price = input.Price,
                DaysTillExpiration = input.DaysTillExpiration,
                ProdCode = prodCode,
                ProductCategories = new List<ProductCategory>() { categ }
            };

            _unitOfWork.Product.Add(newProduct);
            _unitOfWork.Complete();
            _unitOfWork.Dispose();
            return $"Successfully added {input.Name} in the products list!";
        }

        //private async Task<bool> ValidateProductName(string productName)
        //{
        //    var isExist = await _unitOfWork.Product.FirstOrDefaultAsync(e => e.Name == productName);
        //    if(isExist is not null) return true;
        //    return false;
        //}

        private string GenerateProdCode(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                return string.Empty;

            return string.Concat(productName
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(word => char.ToUpper(word[0])));
        }

    }
}
