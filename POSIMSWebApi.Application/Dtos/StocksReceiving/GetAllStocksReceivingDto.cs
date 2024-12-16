namespace POSIMSWebApi.Application.Dtos.StocksReceiving
{
    public class GetAllStocksReceivingDto
    {
        public Guid Id { get; set; }
        public int ProductId { get; set; }
        public string TransNum { get; set; }
        public string ProductName { get; set; }
        public string StorageLocation { get; set; }
        public decimal Quantity { get; set; }
        public int StorageLocationId { get; set;}
        public string DateReceived { get; set; }
    }
}
