namespace POSIMSWebApi.Application.Dtos.Stocks
{
    public class StocksDto
    {
        public int Id { get; set; }
        public string StockNum { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public int ProductId { get; set; }
    }
}
