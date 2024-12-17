namespace POSIMSWebApi.Application.Dtos.Sales
{
    public class CreateOrEditSalesV1Dto
    {
        public Guid? SalesHeaderId { get; set; }
        public Guid? CustomerId { get; set; }
        public List<CreateSalesDetailV1Dto> CreateSalesDetailV1Dto { get; set; } = new List<CreateSalesDetailV1Dto>();
    }

}
