using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Dtos.ProductDtos
{
    public class ProductWithCategDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public ICollection<string> ProductCategories { get; set; } = new List<string>();
    }
}
