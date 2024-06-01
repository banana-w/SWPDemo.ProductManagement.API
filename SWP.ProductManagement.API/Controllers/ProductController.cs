using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.ProductManagement.Repository.DTO.Product;
using SWP.ProductManagement.Repository.Models;
using SWP.ProductManagement.Repository.UnitOfWork;

namespace SWP.ProductManagement.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetProducts([FromQuery] int page, [FromQuery] int size)
        {
            var products = _unitOfWork.ProductRepository.Get(pageIndex: page, pageSize: size);
            return Ok(products);
        }
        [HttpPost]
        public IActionResult AddProduct([FromBody] ProductDTO product)
        {
            if (product.ProductName == null)
            {
                return BadRequest("Product data is null");
            }
            var addProduct = new Product
            {
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            };
            _unitOfWork.ProductRepository.Insert(addProduct);
            _unitOfWork.Save();
            return Ok(product);
        }
        [HttpPut]
        public IActionResult UpdateProduct([FromQuery] int id, [FromBody] ProductDTO product)
        {
            var updateProduct = _unitOfWork.ProductRepository.GetByID(id);
            if (updateProduct == null)
            {
                return NotFound();
            }
            updateProduct.ProductName = product.ProductName;
            updateProduct.CategoryId = product.CategoryId;
            updateProduct.UnitPrice = product.UnitPrice;
            updateProduct.UnitsInStock = product.UnitsInStock;
            _unitOfWork.ProductRepository.Update(updateProduct);
            _unitOfWork.Save();
            return Ok(product);
        }
        [HttpDelete]
        public IActionResult DeleteProduct([FromQuery] int id)
        {
            var deleteProduct = _unitOfWork.ProductRepository.GetByID(id);
            if (deleteProduct == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductRepository.Delete(id);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
