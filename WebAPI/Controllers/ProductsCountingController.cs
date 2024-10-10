using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsCountingController : ControllerBase
    {

        private IProductsCountingService _productsCountingService;
        private IProductService _productService;


        public ProductsCountingController(IProductService productService, IProductsCountingService productsCountingService)
        {
            _productsCountingService = productsCountingService;
            _productService = productService;
        }


        [HttpGet("GetDictionaryProductsCountingByDateAndCategory")]
        public async Task<ActionResult> GetDictionaryProductsCountingByDateAndCategory(DateTime date, int categoryId)
        {
            try
            {               
                var result = await _productsCountingService.GetDictionaryProductsCountingByDateAndCategoryAsync(date,categoryId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet("GetProductsCountingByDateAndCategory")]
        public async Task<ActionResult> GetProductsCountingByDateAndCategory(DateTime date, int categoryId)
        {
            try
            {               
                var result =await _productsCountingService.GetProductsCountingByDateAndCategoryAsync(date,categoryId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet("GetAddedProductsCountingByDate")]
        public async Task<ActionResult> GetAddedProductsCountingByDate(DateTime date)
        {
            try
            {
                List<ProductsCounting> productsCountings = await _productsCountingService.GetProductsCountingByDateAsync(date);
                List<ProductsCountingDto> getAddedProducts = new();

                for (int i = 0; i < productsCountings.Count; i++)
                {
                    ProductsCountingDto getAddedProduct = new();
                    getAddedProduct.ProductName =  _productService.GetByIdAsync(productsCountings[i].ProductId).Result.Name;
                    getAddedProduct.ProductId = productsCountings[i].ProductId;
                    getAddedProduct.Quantity = productsCountings[i].Quantity;
                    getAddedProduct.Id = productsCountings[i].Id;
                    getAddedProduct.Date = productsCountings[i].Date;

                    getAddedProducts.Add(getAddedProduct);

                }
                return Ok(getAddedProducts);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetNotAddedProductsCountingByDate")]
        public async Task<ActionResult> GetNotAddedProductsCountingByDate(DateTime date, int categoryId)
        {
            try
            {
                List<ProductsCountingDto> productsCountings = await _productsCountingService.GetProductsCountingByDateAndCategoryAsync(date, categoryId);
                List<int> AddedProductsIds = new();

                for (int i = 0; i < productsCountings.Count; i++)
                {
                    if (!AddedProductsIds.Contains(productsCountings[i].ProductId))
                    {
                        AddedProductsIds.Add(productsCountings[i].ProductId);
                    }
                }

                List<Product> products = await _productService.GetAllByCategoryIdAsync(categoryId);

                List<ProductNotAddedDto> getNotAddedProducts = new();

                for (int i = 0; i < products.Count; i++)
                {
                    if (!AddedProductsIds.Contains(products[i].Id))
                    {
                        ProductNotAddedDto getNotAddedProduct = new();
                        getNotAddedProduct.Id = products[i].Id;
                        getNotAddedProduct.Name = products[i].Name;
                        getNotAddedProducts.Add(getNotAddedProduct);

                    }
                }

                return Ok(getNotAddedProducts);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddProductsCounting")]
        public async Task<ActionResult> AddProductsCounting(ProductsCounting productsCounting)
        {
            if (productsCounting == null || productsCounting.Quantity < 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                if (await _productsCountingService.IsExistAsync(productsCounting.ProductId ,productsCounting.Date))
                {
                    return BadRequest(Messages.OncePerDay);
                }

               await _productsCountingService.AddAsync(productsCounting);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteProductsCountingById")]
        public async Task<ActionResult> DeleteProductsCountingById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
               await _productsCountingService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateProductsCounting")]
        public async Task<ActionResult> UpdateProductsCounting(ProductsCounting productsCounting)
        {
            if (productsCounting == null || productsCounting.Quantity < 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                if (productsCounting.Quantity == 0)
                {
                   await _productsCountingService.DeleteByIdAsync(productsCounting.Id);
                }
                else
                {
                   await _productsCountingService.UpdateAsync(productsCounting);
                }
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

    }
}