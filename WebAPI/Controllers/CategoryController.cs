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
        public ActionResult GetCategory()
        {
            try
            {
               
                var result = _categoryService.GetAll();
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpPost("AddCategory")]
        public ActionResult AddCategory(Category Category)
        {
            try
            {
                if (Category == null)
                {
                    return BadRequest("There is no data!");
                }
                _categoryService.Add(Category);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpDelete("DeleteCategory")]
        public ActionResult DeleteCategory(int  id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
                _categoryService.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpPut("UpdateCategory")]
        public ActionResult UpdateCategory(Category Category)
        {
            try
            {
                if (Category == null)
                {
                    return BadRequest("There is no data!");
                }
                _categoryService.Update(Category);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }
    }
}