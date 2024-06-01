using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.ProductManagement.Repository.DTO.Category
{
    public class CategoryDTO
    {
        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }
    }
}
