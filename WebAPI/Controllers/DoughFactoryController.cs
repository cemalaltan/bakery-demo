using Business.Abstract;
using Business.Constants;
using Castle.Core.Internal;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoughFactoryController : ControllerBase
    {

        private IDoughFactoryListService _doughFactoryListService;
        private IDoughFactoryListDetailService _doughFactoryListDetailService;
        private IDoughFactoryProductService _doughFactoryProductService;

        public DoughFactoryController(IDoughFactoryProductService doughFactoryProductService, IDoughFactoryListService doughFactoryListService, IDoughFactoryListDetailService doughFactoryListDetailService)
        {
            _doughFactoryListService = doughFactoryListService;
            _doughFactoryListDetailService = doughFactoryListDetailService;
            _doughFactoryProductService = doughFactoryProductService;
        }

        [HttpGet("GetByDateDoughFactoryList")]
        public async Task<ActionResult> GetByDateDoughFactoryList(DateTime date)
        {
            if (date.Date > DateTime.Now.Date)
            {
                return BadRequest(Messages.WrongDate);
            }
            try
            {
                var result = await _doughFactoryListService.GetByDateAsync(date.Date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }


        }

        [HttpPost("AddDoughFactoryListAndListDetail")]
        public async Task<ActionResult> AddDoughFactory(List<DoughFactoryListDetail> doughFactoryListDetail, int userId, DateTime date)
        {
            if (userId <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            if (doughFactoryListDetail.IsNullOrEmpty())
            {
                return BadRequest(Messages.ListEmpty);
            }

            try
            {
                int doughFactoryListId = doughFactoryListDetail[0].DoughFactoryListId;
                bool isNewList = doughFactoryListId == 0;

                if (isNewList)
                {
                    doughFactoryListId = await _doughFactoryListService.AddAsync(new DoughFactoryList { UserId = userId, Date = date });
                }

                foreach (var detail in doughFactoryListDetail)
                {
                    if (isNewList)
                    {
                        detail.DoughFactoryListId = doughFactoryListId;
                       await _doughFactoryListDetailService.AddAsync(detail);
                    }
                    else
                    {
                        if (await _doughFactoryListDetailService.IsExistAsync(detail.DoughFactoryProductId, doughFactoryListId))
                        {
                            return Conflict(Messages.Conflict);
                        }
                        else
                        {
                          await  _doughFactoryListDetailService.AddAsync(detail);
                        }
                    }
                }

                return Ok(doughFactoryListId);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetAddedDoughFactoryListDetailByListId")]
        public async Task<ActionResult> GetDoughFactoryListDetail(int doughFactoryListId)
        {

            try
            {

                List<DoughFactoryListDetail> doughFactoryListDetails = await _doughFactoryListDetailService.GetByDoughFactoryListAsync(doughFactoryListId);

                List<GetAddedDoughFactoryListDetailDto> List = new();

                for (int i = 0; i < doughFactoryListDetails.Count; i++)
                {
                    GetAddedDoughFactoryListDetailDto addedDoughFactoryListDetailDto = new();
                    addedDoughFactoryListDetailDto.Id = doughFactoryListDetails[i].Id;

                    addedDoughFactoryListDetailDto.DoughFactoryProductId = doughFactoryListDetails[i].DoughFactoryProductId;
                    addedDoughFactoryListDetailDto.DoughFactoryProductName =  _doughFactoryProductService.GetByIdAsync(doughFactoryListDetails[i].DoughFactoryProductId).Result.Name;

                    addedDoughFactoryListDetailDto.Quantity = doughFactoryListDetails[i].Quantity;
                    addedDoughFactoryListDetailDto.DoughFactoryListId = doughFactoryListDetails[i].DoughFactoryListId;

                    List.Add(addedDoughFactoryListDetailDto);
                }

                return Ok(List);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("GetNotAddedDoughFactoryListDetailByListId")]
        public async Task<ActionResult> GetMarketByServiceListId(int doughFactoryListId)
        {
            try
            {
                List<DoughFactoryProduct> allDoughFactoryProduct = await _doughFactoryProductService.GetAllAsync();

                List<ProductNotAddedDto> getNotAddedDoughFactoryListDetailDto = new();

                if (doughFactoryListId == 0)
                {                 
                    for (int i = 0; i < allDoughFactoryProduct.Count; i++)
                    {
                        ProductNotAddedDto Dto = new();

                        Dto.Id = allDoughFactoryProduct[i].Id;
                        Dto.Name = allDoughFactoryProduct[i].Name;

                        getNotAddedDoughFactoryListDetailDto.Add(Dto);
                    }
                }
                else
                {

                
                List<DoughFactoryListDetail> doughFactoryListDetails = await _doughFactoryListDetailService.GetByDoughFactoryListAsync(doughFactoryListId);

                List<int> addedDoughFactoryProductIds = new List<int>();

                for (int i = 0; i < doughFactoryListDetails.Count; i++)
                {
                    addedDoughFactoryProductIds.Add(doughFactoryListDetails[i].DoughFactoryProductId);
                }
             
                // LINQ kullanarak filtreleme
                List<DoughFactoryProduct> filteredDoughFactoryProducts = allDoughFactoryProduct.Where(m => !addedDoughFactoryProductIds.Contains(m.Id)).ToList();

                for (int i = 0; i < filteredDoughFactoryProducts.Count; i++)
                {
                        ProductNotAddedDto Dto = new();

                        Dto.Id = filteredDoughFactoryProducts[i].Id;
                        Dto.Name = filteredDoughFactoryProducts[i].Name;

                        getNotAddedDoughFactoryListDetailDto.Add(Dto);
                }
                }

                return Ok(getNotAddedDoughFactoryListDetailDto);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteDoughFactoryListDetail")]
        public async Task<ActionResult> DeleteDoughFactoryListDetail(int detailId)
        {
            if (detailId <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                await _doughFactoryListDetailService.DeleteByIdAsync(detailId);
                return Ok();
            } 
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("UpdateDoughFactoryListDetail")]
        public async Task<ActionResult> UpdateDoughFactoryListDetail(DoughFactoryListDetail doughFactoryListDetail)
        {
            if (doughFactoryListDetail == null)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                await _doughFactoryListDetailService.UpdateAsync(doughFactoryListDetail);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        private class GetAddedDoughFactoryListDetailDto
        {
            public int Id { get; set; }
            public int DoughFactoryProductId { get; set; }
            public string DoughFactoryProductName { get; set; }
            public int Quantity { get; set; }
            public int DoughFactoryListId { get; set; }

        }

        public class GetNotAddedDoughFactoryListDetailDto
        {
            public int DoughFactoryProductId { get; set; }            
            public string DoughFactoryProductName { get; set; }
            
        }
    }
}
