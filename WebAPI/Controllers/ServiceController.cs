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
    public class ServiceController : ControllerBase
    {
        private IMarketService _marketService;
        private IMarketContractService _marketContractService;
        private IServiceListService _serviceListService;
        private IServiceListDetailService _serviceListDetailService;

        public ServiceController(IMarketService marketService, IMarketContractService marketContractService, IServiceListService serviceListService, IServiceListDetailService serviceListDetailService)
        {
            _marketService = marketService;
            _serviceListService = serviceListService;
            _serviceListDetailService = serviceListDetailService;
            _marketContractService = marketContractService;
        }

        [HttpGet("GetByDateServiceList")]
        public async Task<ActionResult> GetByDateServiceList(DateTime date)
        {
            if (date.Date > DateTime.Now.Date)
            {
                return BadRequest(Messages.WrongDate);
            }
            try
            {
                var result = await _serviceListService.GetByDateAsync(date.Date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddServiceListAndListDetail")]
        public async Task<ActionResult> AddService(List<ServiceListDetailDto> serviceListDetailDto, int userId)
        {

            if (serviceListDetailDto.IsNullOrEmpty())
            {
                return BadRequest(Messages.ListEmpty);
            }

            try
            {
                int id = serviceListDetailDto[0].ServiceListId;
                bool IsNewList = false;
                if (id == 0)
                {
                    id = await _serviceListService.AddAsync(new ServiceList { Id = 0, UserId = userId, Date = DateTime.Now });
                    IsNewList = true;
                }

                for (int i = 0; i < serviceListDetailDto.Count; i++)
                {
                    ServiceListDetail serviceListDetail = new ServiceListDetail();

                    if (IsNewList)
                    {
                        serviceListDetail.ServiceListId = id;
                    }
                    else
                    {
                        serviceListDetail.ServiceListId = serviceListDetailDto[i].ServiceListId;
                    }


                    serviceListDetail.MarketContractId = await _marketContractService.GetIdByMarketIdAsync(serviceListDetailDto[i].MarketId);

                    if (await _serviceListDetailService.IsExistAsync(serviceListDetail.ServiceListId, serviceListDetail.MarketContractId))
                    {
                        return Conflict(Messages.Conflict);
                    }

                    serviceListDetail.Price = await _marketContractService.GetPriceByIdAsync(serviceListDetail.MarketContractId);
                    serviceListDetail.Quantity = serviceListDetailDto[i].Quantity;
                  await  _serviceListDetailService.AddAsync(serviceListDetail);
                }
                return Ok(id);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);

            }
        }

        [HttpGet("GetAddedMarketByServiceListId")]
        public async Task<ActionResult> GetAddedMarketByServiceListId(int listId)
        {
            try
            {
                List<ServiceListDetail> serviceListDetail = await _serviceListDetailService.GetByListIdAsync(listId);

                List<GetAddedServiceListDetailDto> List = new List<GetAddedServiceListDetailDto>();

                for (int i = 0; i < serviceListDetail.Count; i++)
                {
                    GetAddedServiceListDetailDto addedServiceListDetailDto = new();
                    addedServiceListDetailDto.Id = serviceListDetail[i].Id;
                    addedServiceListDetailDto.ServiceListId = serviceListDetail[i].ServiceListId;
                    addedServiceListDetailDto.Quantity = serviceListDetail[i].Quantity;

                    addedServiceListDetailDto.MarketId = await _marketContractService.GetMarketIdByIdAsync(serviceListDetail[i].MarketContractId);

                    addedServiceListDetailDto.MarketName = await _marketService.GetNameByIdAsync(addedServiceListDetailDto.MarketId);

                    List.Add(addedServiceListDetailDto);
                }

                return Ok(List);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetMarketByServiceListId")]
        public async Task<ActionResult> GetMarketByServiceListId(int listId)
        {


            try
            {
                List<Market> allMarkets = await _marketService.GetAllActiveAsync();

                if (listId == 0)
                {
                    return Ok(allMarkets);
                }

                List<ServiceListDetail> serviceListDetail = await _serviceListDetailService.GetByListIdAsync(listId);
                List<int> marketIds = serviceListDetail.Select( detail =>  _marketContractService.GetMarketIdByIdAsync(detail.MarketContractId).Result).ToList();

                // Using LINQ to filter markets
                List<Market> filteredMarkets = allMarkets.Where(m => !marketIds.Contains(m.Id)).ToList();

                return Ok(filteredMarkets);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteServiceListDetail")]
        public async Task<ActionResult> DeleteServiceListDetail(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
               await _serviceListDetailService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPut("UpdateServiceListDetail")]
        public async Task<ActionResult> UpdateServiceListDetail(ServiceListDetailDto serviceListDetailDto)
        {
            if (serviceListDetailDto == null)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {    
                ServiceListDetail serviceListDetail = new();
                serviceListDetail.ServiceListId = serviceListDetailDto.ServiceListId;                
                serviceListDetail.MarketContractId = await _marketContractService.GetIdByMarketIdAsync(serviceListDetailDto.MarketId);

                if (!await _serviceListDetailService.IsExistAsync(serviceListDetail.ServiceListId, serviceListDetail.MarketContractId))
                {
                    return Conflict(Messages.WrongInput);
                }

                serviceListDetail.Quantity = serviceListDetailDto.Quantity;
                serviceListDetail.Price = await _marketContractService.GetPriceByIdAsync(serviceListDetail.MarketContractId);
                serviceListDetail.Id = _serviceListDetailService.GetIdByServiceListIdAndMarketContractIdAsync(serviceListDetail.ServiceListId, serviceListDetail.MarketContractId).Result;
               await _serviceListDetailService.UpdateAsync(serviceListDetail);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        public class DataForDelete
        {

            public int ServiceListId { get; set; }
            public int MarketId { get; set; }
        }




    }
}
