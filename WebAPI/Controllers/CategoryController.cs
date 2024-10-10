using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private ICategoryService _categoryService;


        public CategoryController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService; ;
        }

        [HttpGet("GetCategories")]
        public async Task<ActionResult> GetCategory()
        {
            try
            {
               
                var result = await _categoryService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpPost("AddCategory")]
        public async Task<ActionResult> AddCategory(Category Category)
        {
            try
            {
                if (Category == null)
                {
                    return BadRequest("There is no data!");
                }
               await _categoryService.AddAsync(Category);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpDelete("DeleteCategory")]
        public async Task<ActionResult> DeleteCategory(int  id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
               await _categoryService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpPut("UpdateCategory")]
        public async Task<ActionResult> UpdateCategory(Category Category)
        {
            try
            {
                if (Category == null)
                {
                    return BadRequest("There is no data!");
                }
               await _categoryService.UpdateAsync(Category);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }
    }
}