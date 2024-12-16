using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Dtos.StorageLocation
{
    public class CreateOrEditStorageLocationDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class GetStorageLocationForDropDownDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
