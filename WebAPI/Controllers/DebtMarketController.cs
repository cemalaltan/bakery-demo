using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtMarketController : ControllerBase
    {

        private IDebtMarketService _debtMarketService;
        private IMarketService _marketService;

        public DebtMarketController(IMarketService marketService, IDebtMarketService debtMarketService)
        {
            _debtMarketService = debtMarketService;
            _marketService = marketService;
        }

        [HttpGet("GetDebtsOfMarkets")]
        public async Task<ActionResult> GetDebtsOfMarkets()
        {
            try
            {
                List<DebtsOfMarkets> debtsOfMarkets = new();

                Dictionary<int, decimal> totalDebtsForMarkets = await _debtMarketService.GetTotalDebtsForMarketsAsync();
                foreach (var totalDebtForMarket in totalDebtsForMarkets)
                {
                    DebtsOfMarkets debtsOfMarket = new();
                    debtsOfMarket.MarketId = totalDebtForMarket.Key;
                    debtsOfMarket.Amount = totalDebtForMarket.Value;
                    debtsOfMarket.MarketName = await _marketService.GetNameByIdAsync(debtsOfMarket.MarketId);

                    debtsOfMarkets.Add(debtsOfMarket);
                }
                return Ok(debtsOfMarkets);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetDebtByMarketId")]
        public async Task<ActionResult> GetDebtByMarketId(int marketId)
        {

            try
            {

                var result = await _debtMarketService.GetDebtByMarketIdAsync(marketId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {

            try
            {
                if (id <= 0)
                {

                    return BadRequest(Messages.WrongInput);
                }
                var result = await _debtMarketService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }



        }

        [HttpPost("AddNewDebt")]
        public async Task<ActionResult> AddNewDebt(DebtMarket debtMarket)
        {
            try
            {
                if (debtMarket == null || debtMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
                await _debtMarketService.AddAsync(debtMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }


        }

        [HttpPost("PayDebt")]
        public async Task<ActionResult> PayDebt(DebtMarket debtMarket)
        {
            try
            {
                if (debtMarket == null || debtMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
                debtMarket.Amount *= -1;
               await _debtMarketService.AddAsync(debtMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("DeleteDebtMarket")]
        public async Task<ActionResult> DeleteDebtMarket(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
               await _debtMarketService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateDebtMarket")]
        public async Task<ActionResult> UpdateDebtMarket(DebtMarket debtMarket)
        {
            try
            {
                if (debtMarket == null || debtMarket.Amount < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
                var debtMarketFromDb = await _debtMarketService.GetByIdAsync(debtMarket.Id);
                if (debtMarketFromDb != null && debtMarketFromDb.Amount < 0)
                {
                    debtMarket.Amount *= -1;
                }
               await  _debtMarketService.UpdateAsync(debtMarket);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}