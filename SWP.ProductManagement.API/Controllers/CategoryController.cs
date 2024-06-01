using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWP.ProductManagement.Repository.DTO.Category;
using SWP.ProductManagement.Repository.Models;
using SWP.ProductManagement.Repository.UnitOfWork;

namespace SWP.ProductManagement.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult GetCategories([FromQuery] int page, [FromQuery] int size)
        {
            var categories = _unitOfWork.CategoryRepository.Get(pageIndex: page, pageSize: size);
            return Ok(categories);
        }
        [HttpPost]
        public IActionResult AddCategory([FromBody] CategoryDTO category)
        {
            if (category.CategoryName == null)
            {
                return BadRequest("Category data is null");
            }
            var addCategory = new Category
            {
                CategoryName = category.CategoryName,
                Description = category.Description
            };
            _unitOfWork.CategoryRepository.Insert(addCategory);
            _unitOfWork.Save();
            return Ok(category);
        }
        [HttpPut]
        public IActionResult UpdateCategory([FromQuery] int id, [FromBody] CategoryDTO category)
        {   
            var updateCategory = _unitOfWork.CategoryRepository.GetByID(id);
            if(updateCategory == null)
            {
                return NotFound();
            }
            updateCategory.CategoryName = category.CategoryName;
            updateCategory.Description = category.Description;
            _unitOfWork.CategoryRepository.Update(updateCategory);
            _unitOfWork.Save();
            return Ok(category);
        }
        [HttpDelete]
        public IActionResult DeleteCategory([FromQuery] int id)
        {   
            var deleteCategory = _unitOfWork.CategoryRepository.GetByID(id);
            if (deleteCategory == null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryRepository.Delete(id);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
