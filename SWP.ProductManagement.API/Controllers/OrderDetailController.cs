using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.ProductManagement.Repository.UnitOfWork;

namespace SWP.ProductManagement.API.Controllers
{
    [Route("api/orderDetails")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderDetailController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetOrderDetails([FromQuery] int page, [FromQuery] int size)
        {
            var orderDetails = _unitOfWork.OrderDetailRepository.Get(pageIndex: page, pageSize: size);
            return Ok(orderDetails);
        }


    }
}
