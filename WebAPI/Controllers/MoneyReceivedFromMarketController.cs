using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyReceivedFromMarketController : ControllerBase
    {
        private IMarketService _marketService;
        private IMarketContractService _marketContractService;
        private IMoneyReceivedFromMarketService _moneyReceivedFromMarketService;
        private IServiceListDetailService _serviceListDetailService;
        private IServiceListService _serviceListService;
        private IStaleBreadReceivedFromMarketService _staleBreadReceivedFromMarketService;
        private IDebtMarketService _debtMarketService;

        private IMarketEndOfDayService _marketEndOfDayService;


        public MoneyReceivedFromMarketController(IMarketEndOfDayService marketEndOfDayService, IDebtMarketService debtMarketService, IStaleBreadReceivedFromMarketService staleBreadReceivedFromMarketService, IMarketService marketService, IMarketContractService marketContractService, IMoneyReceivedFromMarketService moneyReceivedFromMarketService, IServiceListService serviceListService, IServiceListDetailService serviceListDetailService)
        {
            _marketEndOfDayService = marketEndOfDayService;
            _debtMarketService = debtMarketService;
            _moneyReceivedFromMarketService = moneyReceivedFromMarketService;
            _serviceListDetailService = serviceListDetailService;
            _serviceListService = serviceListService;
            _marketService = marketService;
            _marketContractService = marketContractService;
            _staleBreadReceivedFromMarketService = staleBreadReceivedFromMarketService;
        }



        [HttpGet("GetMoneyReceivedFromMarketByMarketId")]
        public async Task<ActionResult> GetMoneyReceivedFromMarketByMarketId(int marketId, DateTime date)
        {

            try
            {
                //var result = _moneyReceivedFromMarketService.GetByMarketId(marketId);
                var result = await _moneyReceivedFromMarketService.GetByMarketIdAndDateAsync(marketId, date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetMoneyReceivedMarketListByDate")]
        public async Task<ActionResult> GetMoneyReceivedMarketListByDate(DateTime date)
        {
            try
            {
                return Ok(await _marketEndOfDayService.CalculateMarketEndOfDayAsync(date));
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
        [HttpGet("GetMarketsEndOfDayCalculationWithDetail")]
        public async Task<ActionResult> GetMarketsEndOfDayCalculationWithDetail(DateTime date)
        {
            try
            {
                return Ok(await _marketEndOfDayService.MarketsEndOfDayCalculationWithDetailAsync(date));
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }


        [HttpGet("GetNotMoneyReceivedMarketListByDate")]
        public async Task<ActionResult> GetNotMoneyReceivedMarketListByDate(DateTime date)
        {
            try
            {
                List<int> MarketIds = new List<int>();
                List<ServiceList> serviceList =await _serviceListService.GetByDateAsync(date);

                for (int i = 0; i < serviceList.Count; i++)
                {
                    List<ServiceListDetail> serviceListDetail =await _serviceListDetailService.GetByListIdAsync(serviceList[i].Id);

                    for (int j = 0; j < serviceListDetail.Count; j++)
                    {

                        var newMarketId =await _marketContractService.GetMarketIdByIdAsync(serviceListDetail[j].MarketContractId);
                        if (!MarketIds.Contains(newMarketId))
                        {
                            MarketIds.Add(newMarketId);
                        }
                    }
                }

                List<MoneyReceivedFromMarket> moneyReceivedFromMarkets = await _moneyReceivedFromMarketService.GetByDateAsync(date);

                List<int> filteredMarkets = MarketIds.Except(moneyReceivedFromMarkets.Select(m => m.MarketId)).ToList();

                List<NotPaymentMarket> NotPaymentMarkets = new();

                for (int i = 0; i < filteredMarkets.Count; i++)
                {
                    NotPaymentMarket notPaymentMarket = new();
                    notPaymentMarket.MarketId = filteredMarkets[i];
                    notPaymentMarket.MarketName =await _marketService.GetNameByIdAsync(filteredMarkets[i]);

                    var result = await CalculateTotalAmountAndBread(date, filteredMarkets[i]);
                    
                    notPaymentMarket.TotalAmount = result.TotalAmount;
                    notPaymentMarket.GivenBread = result.TotalBread;
                    notPaymentMarket.StaleBread = await _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketIdAsync(notPaymentMarket.MarketId, date);
                    NotPaymentMarkets.Add(notPaymentMarket);
                }

                return Ok(NotPaymentMarkets);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("AddMoneyReceivedFromMarket")]
        public async Task<ActionResult> AddMoneyReceivedFromMarket(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            try
            {
                if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                if (await _moneyReceivedFromMarketService.IsExistAsync(moneyReceivedFromMarket.MarketId, moneyReceivedFromMarket.Date))
                {

                    return BadRequest(Messages.Conflict);
                }

                
                var result = await CalculateTotalAmountAndBread(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);
                decimal totalAmount = result.TotalAmount;
                if (totalAmount < moneyReceivedFromMarket.Amount)
                {
                    return BadRequest(Messages.InvalidAmount);
                }
                if (totalAmount > moneyReceivedFromMarket.Amount)
                {
                   await _debtMarketService.AddAsync(new DebtMarket
                    {
                        Amount = (totalAmount - moneyReceivedFromMarket.Amount),
                        Date = moneyReceivedFromMarket.Date,
                        MarketId = moneyReceivedFromMarket.MarketId,
                    });
                }

               await _moneyReceivedFromMarketService.AddAsync(moneyReceivedFromMarket);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

            return Ok();
        }

        [HttpDelete("DeleteMoneyReceivedFromMarket")]
        public async Task<ActionResult> DeleteMoneyReceivedFromMarket(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            try
            {
                if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }


                int debtId =await _debtMarketService.GetDebtIdByDateAndMarketIdAsync(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);
          
                if(debtId != 0)
                {
                   await _debtMarketService.DeleteByIdAsync(debtId);
                }

               await _moneyReceivedFromMarketService.DeleteByIdAsync(moneyReceivedFromMarket.Id);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }


            return Ok();
        }

        [HttpPut("UpdateMoneyReceivedFromMarket")]
        public async Task<ActionResult> UpdateMoneyReceivedFromMarket(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            try
            {
                if (moneyReceivedFromMarket == null || moneyReceivedFromMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

                if (!await _moneyReceivedFromMarketService.IsExistAsync(moneyReceivedFromMarket.MarketId, moneyReceivedFromMarket.Date))
                {

                    return BadRequest(Messages.WrongInput);
                }

                var result = await CalculateTotalAmountAndBread(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId);
                decimal totalAmount = result.TotalAmount;
                if (totalAmount < moneyReceivedFromMarket.Amount)
                {
                    return BadRequest(Messages.InvalidAmount);
                }
                if (totalAmount > moneyReceivedFromMarket.Amount)
                {
                    if (await _debtMarketService.IsExistAsync(await _debtMarketService.GetDebtIdByDateAndMarketIdAsync(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)))
                    {
                      await  _debtMarketService.UpdateAsync(new DebtMarket
                        {
                            Amount = (totalAmount - moneyReceivedFromMarket.Amount),
                            Date = moneyReceivedFromMarket.Date,
                            MarketId = moneyReceivedFromMarket.MarketId,
                            Id = await _debtMarketService.GetDebtIdByDateAndMarketIdAsync(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)
                        });
                    }
                    else
                    {
                      await  _debtMarketService.AddAsync(new DebtMarket
                        {
                            Amount = (totalAmount - moneyReceivedFromMarket.Amount),
                            Date = moneyReceivedFromMarket.Date,
                            MarketId = moneyReceivedFromMarket.MarketId,
                        });
                    }
                }
                if (totalAmount == moneyReceivedFromMarket.Amount)
                {
                  await  _debtMarketService.DeleteAsync(new DebtMarket
                    {
                        Date = moneyReceivedFromMarket.Date,
                        MarketId = moneyReceivedFromMarket.MarketId,
                        Id = await _debtMarketService.GetDebtIdByDateAndMarketIdAsync(moneyReceivedFromMarket.Date, moneyReceivedFromMarket.MarketId)
                    });
                }
             await   _moneyReceivedFromMarketService.UpdateAsync(moneyReceivedFromMarket);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }


            return Ok();
        }

        private async Task<(decimal TotalAmount, int TotalBread)> CalculateTotalAmountAndBread(DateTime date, int marketId)
        {

            List<ServiceList> serviceLists = await _serviceListService.GetByDateAsync(date);

            int TotalBread = 0;
            decimal Price = 0;
            for (int i = 0; i < serviceLists.Count; i++)
            {

                ServiceListDetail serviceListDetail = await _serviceListDetailService.GetByServiceListIdAndMarketContractIdAsync(serviceLists[i].Id, await _marketContractService.GetIdByMarketIdAsync(marketId));
                if (serviceListDetail != null)
                {
                    TotalBread += serviceListDetail.Quantity;
                    Price = serviceListDetail.Price;
                }
            }

            int StaleBreadCount = await _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketIdAsync(marketId, date);

            decimal TotalAmount = (TotalBread - StaleBreadCount) * Price;


            return (TotalAmount, TotalBread);
        }

    }





}