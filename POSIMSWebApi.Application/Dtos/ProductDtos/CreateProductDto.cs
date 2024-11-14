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
        public int  Price { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
