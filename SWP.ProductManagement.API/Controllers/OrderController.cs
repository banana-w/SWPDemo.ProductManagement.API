using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.ProductManagement.Repository.DTO.Order;
using SWP.ProductManagement.Repository.Models;
using SWP.ProductManagement.Repository.UnitOfWork;

namespace SWP.ProductManagement.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetOrders([FromQuery] int page, [FromQuery] int size)
        {
            var orders = _unitOfWork.OrderRepository.Get(pageIndex: page, pageSize: size);
            return Ok(orders);
        }
        [HttpPost]
        public IActionResult AddOrder([FromBody] CreateOrderDto order)
        {
            var addOrder = new Order
            {
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
                TotalAmount = order.TotalAmount,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetail
                {
                    ProductId = od.ProductId,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                }).ToList()
            };
            _unitOfWork.OrderRepository.Insert(addOrder);
            _unitOfWork.Save();
            return Ok(order);
        }
        [HttpPut]
        public IActionResult UpdateOrder([FromQuery] int id, [FromBody] OrderDTO order)
        {
            var updateOrder = _unitOfWork.OrderRepository.GetByID(id);
            if (updateOrder == null)
            {
                return NotFound();
            }
            updateOrder.OrderDate = order.OrderDate;
            updateOrder.CustomerId = order.CustomerId;
            updateOrder.TotalAmount = order.TotalAmount;
            _unitOfWork.OrderRepository.Update(updateOrder);
            _unitOfWork.Save();
            return Ok(order);
        }
        [HttpDelete]
        public IActionResult DeleteOrder([FromQuery] int id)
        {
            var deleteOrder = _unitOfWork.OrderRepository.GetByID(id);
            if (deleteOrder == null)
            {
                return NotFound();
            }
            _unitOfWork.OrderRepository.Delete(id);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
