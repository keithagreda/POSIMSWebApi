namespace POSIMSWebApi.Application.Dtos.ProductDtos
{
    public class GetTotalSalesDto
    {
        public int TotalSales { get; set; }
        public int SalesPercentage { get; set; }
        public int[] AllSalesPercentage { get; set; } = [];
    }

    public class PerMonthSalesDto
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public decimal SalesPercentage { get; set; }
        public decimal TotalMonthlySales { get; set; }
    }
}
