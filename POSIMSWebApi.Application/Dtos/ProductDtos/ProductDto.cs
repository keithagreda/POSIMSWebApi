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
}
