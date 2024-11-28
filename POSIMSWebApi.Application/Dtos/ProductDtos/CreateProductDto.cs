using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Dtos.ProductDtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public int DaysTillExpiration { get; set; }
        public int  Price { get; set; }
        public int ProductCategoryId { get; set; }
    }

    public class CreateProductSales
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
    }
}
