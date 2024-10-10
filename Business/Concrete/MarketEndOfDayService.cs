using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MarketEndOfDayService : IMarketEndOfDayService
    {
        private readonly IMoneyReceivedFromMarketService _moneyReceivedFromMarketService;
        private readonly IMarketService _marketService;
        private readonly IStaleBreadReceivedFromMarketService _staleBreadReceivedFromMarketService;
        private readonly IServiceListDetailService _serviceListDetailService;
        private readonly IServiceListService _serviceListService;
        private readonly IMarketContractService _marketContractService;

        public MarketEndOfDayService(
            IMarketContractService marketContractService,
            IMoneyReceivedFromMarketService moneyReceivedFromMarketService,
            IMarketService marketService,
            IStaleBreadReceivedFromMarketService staleBreadReceivedFromMarketService,
            IServiceListService serviceListService,
            IServiceListDetailService serviceListDetailService)
        {
            _marketContractService = marketContractService;
            _moneyReceivedFromMarketService = moneyReceivedFromMarketService;
            _marketService = marketService;
            _staleBreadReceivedFromMarketService = staleBreadReceivedFromMarketService;
            _serviceListService = serviceListService;
            _serviceListDetailService = serviceListDetailService;
        }

        public async Task<List<PaymentMarket>> CalculateMarketEndOfDayAsync(DateTime date)
        {
            List<MoneyReceivedFromMarket> moneyReceivedFromMarkets = await _moneyReceivedFromMarketService.GetByDateAsync(date);
            List<PaymentMarket> paymentMarkets = new();

            for (int i = 0; i < moneyReceivedFromMarkets.Count; i++)
            {
                PaymentMarket paymentMarket = new();
                paymentMarket.MarketId = moneyReceivedFromMarkets[i].MarketId;
                paymentMarket.id = moneyReceivedFromMarkets[i].Id;
                paymentMarket.Amount = moneyReceivedFromMarkets[i].Amount;
                paymentMarket.MarketName = await _marketService.GetNameByIdAsync(moneyReceivedFromMarkets[i].MarketId);

                var result = await CalculateTotalAmountAndBreadAsync(date, moneyReceivedFromMarkets[i].MarketId);

                paymentMarket.TotalAmount = result.TotalAmount;
                paymentMarket.GivenBread = result.TotalBread;
                paymentMarket.StaleBread = await _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketIdAsync(paymentMarket.MarketId, date);
                paymentMarkets.Add(paymentMarket);
            }

            return paymentMarkets;
        }

        public async Task<List<MarketBreadDetails>> MarketsEndOfDayCalculationWithDetailAsync(DateTime date)
        {
            List<MoneyReceivedFromMarket> moneyReceivedFromMarkets = await _moneyReceivedFromMarketService.GetByDateAsync(date);
            List<MarketBreadDetails> marketsBreadDetails = new();

            for (int i = 0; i < moneyReceivedFromMarkets.Count; i++)
            {
                MarketBreadDetails marketBreadDetails = new();
                marketBreadDetails.MarketId = moneyReceivedFromMarkets[i].MarketId;
                marketBreadDetails.id = moneyReceivedFromMarkets[i].Id;
                marketBreadDetails.Amount = moneyReceivedFromMarkets[i].Amount;
                marketBreadDetails.MarketName = await _marketService.GetNameByIdAsync(moneyReceivedFromMarkets[i].MarketId);

                var result = await CalculateTotalAmountAndBreadAsync(date, moneyReceivedFromMarkets[i].MarketId);

                marketBreadDetails.TotalAmount = result.TotalAmount;
                marketBreadDetails.GivenBread = result.TotalBread;
                marketBreadDetails.StaleBread = await _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketIdAsync(marketBreadDetails.MarketId, date);
                marketBreadDetails.BreadGivenByEachService = await BreadGivenByEachServiceAsync(date, moneyReceivedFromMarkets[i].MarketId);
                marketsBreadDetails.Add(marketBreadDetails);
            }

            return marketsBreadDetails;
        }

        public async Task<decimal> TotalAmountFromMarketsAsync(DateTime date)
        {
            List<MoneyReceivedFromMarket> moneyReceivedFromMarkets = await _moneyReceivedFromMarketService.GetByDateAsync(date);
            decimal totalAmount = 0;

            for (int i = 0; i < moneyReceivedFromMarkets.Count; i++)
            {
                var result = await CalculateTotalAmountAndBreadAsync(date, moneyReceivedFromMarkets[i].MarketId);
                totalAmount += result.TotalAmount;
            }
            return totalAmount;
        }

        private async Task<Dictionary<string, int>> BreadGivenByEachServiceAsync(DateTime date, int marketId)
        {
            Dictionary<string, int> breadGivenByEachService = new();
            List<ServiceList> serviceLists = await _serviceListService.GetByDateAsync(date);
            for (int i = 0; i < serviceLists.Count; i++)
            {
                ServiceListDetail serviceListDetail = await _serviceListDetailService.GetByServiceListIdAndMarketContractIdAsync(serviceLists[i].Id, await _marketContractService.GetIdByMarketIdAsync(marketId));
                string keyName = $"{i + 1}. Servis";
                breadGivenByEachService[keyName] = serviceListDetail != null ? serviceListDetail.Quantity : 0;
            }
            return breadGivenByEachService;
        }

        private async Task<(decimal TotalAmount, int TotalBread)> CalculateTotalAmountAndBreadAsync(DateTime date, int marketId)
        {
            List<ServiceList> serviceLists = await _serviceListService.GetByDateAsync(date);
            int totalBread = 0;
            decimal price = 0;

            for (int i = 0; i < serviceLists.Count; i++)
            {
                ServiceListDetail serviceListDetail = await _serviceListDetailService.GetByServiceListIdAndMarketContractIdAsync(serviceLists[i].Id, await _marketContractService.GetIdByMarketIdAsync(marketId));
                if (serviceListDetail != null)
                {
                    totalBread += serviceListDetail.Quantity;
                    price = serviceListDetail.Price;
                }
            }

            int staleBreadCount = await _staleBreadReceivedFromMarketService.GetStaleBreadCountByMarketIdAsync(marketId, date);
            decimal totalAmount = (totalBread - staleBreadCount) * price;

            return (totalAmount, totalBread);
        }
    }
}
