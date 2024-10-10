using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionListController : ControllerBase
    {

        private IProductionListService _productionListService;
        private IProductionListDetailService _productionListDetailService;
        private IProductService _productService;

        public ProductionListController(IProductService productService ,IProductionListService productionListService, IProductionListDetailService productionListDetailService)
        {
            _productService = productService;
            _productionListService = productionListService;
            _productionListDetailService = productionListDetailService;

        }

        [HttpGet("GetAddedProductsByDateAndCategoryId")]
        public async Task<ActionResult> GetAddedProductsByDateAndCategoryId(DateTime date, int categoryId)
        {
            if (categoryId == 0 || date.Date > DateTime.Now.Date)
            {
                return BadRequest(Messages.WrongInput);
            }
            var listId = await _productionListService.GetByDateAndCategoryIdAsync(date, categoryId);
            var productsList = await _productionListDetailService.GetProductsByListIdAsync(listId);
            return Ok(productsList);
        }

        [HttpGet("GetNotAddedProductsByListAndCategoryId")]
        public async Task<ActionResult> GetNotAddedProductsByListAndCategoryId(DateTime date, int categoryId)
        {
            if(categoryId == 0 || date.Date >  DateTime.Now.Date) {
                return BadRequest(Messages.WrongInput);
            }

            var listId = await _productionListService.GetByDateAndCategoryIdAsync(date,categoryId);
         

            if(listId == 0)
            {
                var productList =await _productService.GetAllByCategoryIdAsync(categoryId);
                return Ok(productList);
            }
            var productListNotAdded = await _productService.GetNotAddedProductsByListAndCategoryIdAsync(listId,categoryId);
            return Ok(productListNotAdded);

        }

        [HttpPost("AddProductionListAndDetail")]
        public async Task<ActionResult> AddProductionDetailList(List<ProductionListDetail> productionListDetail, int userId, int categoryId, DateTime date)
        {

            if (productionListDetail == null || productionListDetail.Count == 0)
            {
                return BadRequest("Product list is null or empty.");
            }

            var listId = await _productionListService.GetByDateAndCategoryIdAsync(DateTime.Now, categoryId);

            if (listId == 0)
            {
                listId = await _productionListService.AddAsync(new ProductionList { Id = listId, UserId = userId, Date = date , CategoryId= categoryId});
                
            }

            for (int i = 0; i < productionListDetail.Count; i++)
            {  
                    productionListDetail[i].ProductionListId = listId;
              
                    if ( await _productionListDetailService.IsExistAsync(productionListDetail[i].ProductId, productionListDetail[i].ProductionListId))
                    {
                        return Conflict("A product already exist in the list.");
                    }
               
               productionListDetail[i].Price =await _productService.GetPriceByIdAsync(productionListDetail[i].ProductId);
            }

          await  _productionListDetailService.AddListAsync(productionListDetail);
            return Ok();
        }

        [HttpDelete("DeleteProductionListDetail")]
        public async Task<ActionResult> DeleteDoughFactoryListDetail(int id)
        {
            await _productionListDetailService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPut("UpdateProductionListDetail")]
        public async Task<ActionResult> UpdateDoughFactoryListDetail(ProductionListDetail productionListDetail)
        {
          await  _productionListDetailService.UpdateAsync(productionListDetail);
            return Ok();
        }

    }

}

