using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP.ProductManagement.Repository.DTO.Order
{
    public class OrderDTO
    {
        public DateOnly OrderDate { get; set; }

        public int? CustomerId { get; set; }

        public decimal? TotalAmount { get; set; }
    }
    public class CreateOrderDto
    {
        public DateOnly OrderDate { get; set; }
        public int? CustomerId { get; set; }
        public decimal? TotalAmount { get; set; }
        public List<CreateOrderDetailDto> OrderDetails { get; set; } = new List<CreateOrderDetailDto>();
    }

    public class CreateOrderDetailDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
