using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaleBreadReceivedFromMarketController : ControllerBase
    {

        private IStaleBreadReceivedFromMarketService _staleBreadReceivedFromMarketService;

        private IMarketContractService _marketContractService;
        private IServiceListDetailService _serviceListDetailService;
        private IServiceListService _serviceListService;
        private IMarketService _marketService;

        public StaleBreadReceivedFromMarketController(IMarketService marketService, IMarketContractService marketContractService, IServiceListService serviceListService, IServiceListDetailService serviceListDetailService, IStaleBreadReceivedFromMarketService staleBreadReceivedFromMarketService)
        {
            _staleBreadReceivedFromMarketService = staleBreadReceivedFromMarketService;

            _serviceListService = serviceListService;
            _serviceListDetailService = serviceListDetailService;
            _marketContractService = marketContractService;
            _marketService = marketService;
        }

        [HttpGet("GetStaleBreadReceivedFromMarketByDate")]
        public async Task<ActionResult> GetStaleBreadReceivedFromMarketByDate(DateTime date)
        {
            try
            {
                List<StaleBreadReceivedFromMarket> staleBreadReceivedFromMarket = await _staleBreadReceivedFromMarketService.GetByDateAsync(date);
                List<StaleBreadReceivedFromMarketDto> staleBreadReceivedFromMarketDto = staleBreadReceivedFromMarket
                            .Select( item => new StaleBreadReceivedFromMarketDto
                            {
                                id = item.Id,
                                MarketId = item.MarketId,
                                MarketName =  _marketService.GetNameByIdAsync(item.MarketId).Result,
                                Quantity = item.Quantity
                            })
                                .ToList();


                return Ok(staleBreadReceivedFromMarketDto);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("GetNoBreadReceivedMarketListByDate")]
        public async Task<ActionResult> GetNoMoneyReceivedMarketListByDate(DateTime date)
        {
            try
            {
                List<int> MarketIds = new List<int>();
                List<ServiceList> serviceList = await _serviceListService.GetByDateAsync(date);

                for (int i = 0; i < serviceList.Count; i++)
                {
                    List<ServiceListDetail> serviceListDetail = await _serviceListDetailService.GetByListIdAsync(serviceList[i].Id);

                    for (int j = 0; j < serviceListDetail.Count; j++)
                    {

                        var newMarketId = await _marketContractService.GetMarketIdByIdAsync(serviceListDetail[j].MarketContractId);
                        if (!MarketIds.Contains(newMarketId))
                        {
                            MarketIds.Add(newMarketId);
                        }
                    }
                }

                List<StaleBreadReceivedFromMarket> staleBreadReceivedFromMarket = await _staleBreadReceivedFromMarketService.GetByDateAsync(date);

                List<int> filteredMarkets = MarketIds.Except(staleBreadReceivedFromMarket.Select(s => s.MarketId)).ToList();

                List<NoStaleBreadReceivedFromMarketDto> noStaleBreadReceivedFromMarketDto = new();

                for (int i = 0; i < filteredMarkets.Count; i++)
                {
                    NoStaleBreadReceivedFromMarketDto s = new();
                    s.MarketId = filteredMarkets[i];
                    s.MarketName = await _marketService.GetNameByIdAsync(filteredMarkets[i]);
                    noStaleBreadReceivedFromMarketDto.Add(s);
                }

               return Ok(noStaleBreadReceivedFromMarketDto);
               
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetStaleBreadReceivedFromMarketByMarketId")]
        public async Task<ActionResult> GetStaleBreadReceivedFromMarket(int marketId, DateTime date)
        {
            try
            {
                var result = await _staleBreadReceivedFromMarketService.GetByMarketIdAsync(marketId, date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetByIdStaleBreadReceivedFromMarket")]
        public async Task<ActionResult> GetByIdStaleBreadReceivedFromMarket(int id)
        {

            try
            {
                var result = await _staleBreadReceivedFromMarketService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("AddStaleBreadReceivedFromMarket")]
        public async Task<ActionResult> AddStaleBreadReceivedFromMarket(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {
            if (staleBreadReceivedFromMarket == null || staleBreadReceivedFromMarket.Quantity < 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            
            if (await _staleBreadReceivedFromMarketService.IsExistAsync(staleBreadReceivedFromMarket.MarketId,staleBreadReceivedFromMarket.Date))
            {
                return BadRequest(Messages.Conflict);
            }

            try
            {
               await _staleBreadReceivedFromMarketService.AddAsync(staleBreadReceivedFromMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteStaleBreadReceivedFromMarketById")]
        public async Task<ActionResult> DeleteStaleBreadReceivedFromMarket(int id)
        {

            try
            {
               await _staleBreadReceivedFromMarketService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteByDateAndMarketId")]
        public async Task<ActionResult> DeleteByDateAndMarketId(DataForDeleteForStaleBreadReceivedFromMarket dataForDeleteForStaleBreadReceivedFromMarket)
        {

            try
            {
                await _staleBreadReceivedFromMarketService.DeleteByDateAndMarketIdAsync(dataForDeleteForStaleBreadReceivedFromMarket.Date, dataForDeleteForStaleBreadReceivedFromMarket.MarketId);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("UpdateStaleBreadReceivedFromMarket")]
        public async Task<ActionResult> UpdateStaleBreadReceivedFromMarket(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {


            try
            {
              await  _staleBreadReceivedFromMarketService.UpdateAsync(staleBreadReceivedFromMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        public class DataForDeleteForStaleBreadReceivedFromMarket
        {
            public DateTime Date { get; set; }
            public int MarketId { get; set; }
        }

        public class StaleBreadReceivedFromMarketDto
        {
            public int id { get; set; }
            public int MarketId { get; set; }
            public string MarketName { get; set; }
            public int Quantity { get; set; }
        }
        class NoStaleBreadReceivedFromMarketDto

        {
            public int MarketId { get; set; }
            public string MarketName { get; set; }
           
        }
    }
}