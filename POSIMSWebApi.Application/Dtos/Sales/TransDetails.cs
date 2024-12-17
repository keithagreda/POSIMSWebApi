namespace POSIMSWebApi.Application.Dtos.Sales
{
    public class TransDetails
    {
        public string TransNum { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int StorageLocationId { get; set; }
    }

}
