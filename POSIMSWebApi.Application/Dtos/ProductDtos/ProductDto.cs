using POSIMSWebApi.Application.Dtos.ProductCategory;

namespace POSIMSWebApi.Application.Dtos.ProductDtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProdCode { get; set; }
        public int DaysTillExpiration { get; set; }
    }

    public class ProductV1Dto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ProdCode { get; set; }
        public int DaysTillExpiration { get; set; }
        public List<ProductCategoryDto> ProductCategoriesDto { get; set; } = new List<ProductCategoryDto>();
    }
}
