using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EndOfDayAccountManager : IEndOfDayAccountService
    {
        private readonly IPurchasedProductListDetailService _purchasedProductListDetailService;
        private readonly IProductService _productService;
        private readonly IProductionListDetailService _productionListDetailService;
        private readonly IProductsCountingService _productsCountingService;
        private readonly IStaleProductService _staleProductService;
        private readonly IBreadCountingService _breadCountingService;
        private readonly IGivenProductsToServiceService _givenProductsToServiceService;
        private readonly IDoughFactoryListService _doughFactoryListService;
        private readonly IDoughFactoryListDetailService _doughFactoryListDetailService;
        private readonly IDoughFactoryProductService _doughFactoryProductService;
        private readonly IBreadPriceService _breadPriceService;
        private readonly IStaleBreadService _staleBreadService;
        private readonly IMarketEndOfDayService _marketEndOfDayService;
        private readonly IMoneyReceivedFromMarketService _moneyReceivedFromMarketService;
        private readonly IServiceStaleProductService _serviceStaleProductService;
        private readonly ICashCountingService _cashCountingService;

        public EndOfDayAccountManager(
            IMarketEndOfDayService marketEndOfDayService,
            IStaleBreadService staleBreadService,
            IDoughFactoryListService doughFactoryListService,
            IDoughFactoryListDetailService doughFactoryListDetailService,
            IDoughFactoryProductService doughFactoryProductService,
            IGivenProductsToServiceService givenProductsToServiceService,
            IBreadCountingService breadCountingService,
            IPurchasedProductListDetailService purchasedProductListDetailService,
            IStaleProductService staleProductService,
            IProductsCountingService productsCountingService,
            IProductService productService,
            IProductionListDetailService productionListDetailService,
            IBreadPriceService breadPriceService,
            IMoneyReceivedFromMarketService moneyReceivedFromMarketService,
            IServiceStaleProductService serviceStaleProductService,
            ICashCountingService cashCountingService)
        {
            _marketEndOfDayService = marketEndOfDayService;
            _staleBreadService = staleBreadService;
            _doughFactoryListService = doughFactoryListService;
            _doughFactoryListDetailService = doughFactoryListDetailService;
            _doughFactoryProductService = doughFactoryProductService;
            _givenProductsToServiceService = givenProductsToServiceService;
            _breadCountingService = breadCountingService;
            _purchasedProductListDetailService = purchasedProductListDetailService;
            _staleProductService = staleProductService;
            _productsCountingService = productsCountingService;
            _productService = productService;
            _productionListDetailService = productionListDetailService;
            _breadPriceService = breadPriceService;
            _moneyReceivedFromMarketService = moneyReceivedFromMarketService;
            _serviceStaleProductService = serviceStaleProductService;
            _cashCountingService = cashCountingService;
        }

        public async Task<EndOfDayResult> GetEndOfDayAccountDetailAsync(DateTime date)
        {
            Account account = new();
            account.ServisGelir = await _marketEndOfDayService.TotalAmountFromMarketsAsync(date);
            account.KasaDevir = (await _cashCountingService.GetOneCashCountingByDateAsync(date.AddDays(-1)))?.RemainedMoney ?? 0;
            account.Devir = (await _cashCountingService.GetOneCashCountingByDateAsync(date))?.RemainedMoney ?? 0;
            account.KasaSayım = (await _cashCountingService.GetOneCashCountingByDateAsync(date))?.TotalMoney ?? 0;
            account.CreditCard = (await _cashCountingService.GetOneCashCountingByDateAsync(date))?.CreditCard ?? 0;

            //-------------------------------------

            double allBreadProduced = 0;
            List<DoughFactoryListDto> doughFactoryListDto = await _doughFactoryListService.GetByDateAsync(date.Date);

            foreach (var doughFactoryList in doughFactoryListDto)
            {
                List<DoughFactoryListDetail> doughFactoryListDetails = await _doughFactoryListDetailService.GetByDoughFactoryListAsync(doughFactoryList.Id);

                foreach (var doughFactoryListDetail in doughFactoryListDetails)
                {
                    DoughFactoryProduct doughFactoryProduct = await _doughFactoryProductService.GetByIdAsync(doughFactoryListDetail.DoughFactoryProductId);
                    allBreadProduced += doughFactoryProduct.BreadEquivalent * doughFactoryListDetail.Quantity;
                }
            }

            List<StaleBreadDto> staleBreadDtos = await _staleBreadService.GetAllByDateAsync(date);
            double staleBread = 0;

            foreach (var staleBreadDto in staleBreadDtos)
            {
                DoughFactoryProduct doughFactoryProduct = await _doughFactoryProductService.GetByIdAsync(staleBreadDto.DoughFactoryProductId);
                staleBread += doughFactoryProduct.BreadEquivalent * staleBreadDto.Quantity;
            }

            //--------------------------------------------------------------

            EndOfDayAccountForBread endOfDayAccountForBread = new()
            {
                Price = await _breadPriceService.BreadPriceByDateAsync(date),
                ProductedToday = allBreadProduced,
                RemainingYesterday = (await _breadCountingService.GetBreadCountingByDateAsync(date.Date.AddDays(-1)))?.Quantity ?? 0,
                RemainingToday = (await _breadCountingService.GetBreadCountingByDateAsync(date.Date))?.Quantity ?? 0,
                StaleBreadToday = staleBread,
                PurchasedBread = 0, // Değerler değişebilir
                EatenBread = 10 // Değerler değişebilir
            };

            List<GivenProductsToServiceTotalResultDto> givenProductsToServiceTotal = await _givenProductsToServiceService.GetTotalQuantityByDateAsync(date);
            endOfDayAccountForBread.TotalBreadGivenToGetir = givenProductsToServiceTotal
                .FirstOrDefault(item => item.ServiceTypeName == "Getir")?.TotalQuantity ?? 0;
            endOfDayAccountForBread.TotalBreadGivenToService = givenProductsToServiceTotal
                .FirstOrDefault(item => item.ServiceTypeName == "Marketler")?.TotalQuantity ?? 0;
            endOfDayAccountForBread.TotalStaleBreadFromService = 0;

            List<ServiceStaleProduct> serviceStaleProducts = await _serviceStaleProductService.GetAllByDateAsync(date, 1);
            foreach (var service in serviceStaleProducts)
            {
                endOfDayAccountForBread.TotalStaleBreadFromService += service.Quantity;
            }

            EndOfDayResult result = new EndOfDayResult
            {
                EndOfDayAccount = endOfDayAccountForBread,
                Account = account,
                PastaneRevenue = await GetProductsSoldInTheBakeryAsync(date)
            };

            return result;
        }

        public async Task<decimal> GetPastaneDailyRevenueAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetProductsSoldInTheBakeryAsync(DateTime date)
        {
            decimal totalRevenue = 0;
            List<Product> products = await _productService.GetAllByCategoryIdAsync(2);
            products.AddRange(await _productService.GetAllByCategoryIdAsync(1));
            products.AddRange(await _productService.GetAllByCategoryIdAsync(3));

            foreach (var product in products)
            {
                ProductSoldInTheBakery productSoldInTheBakery = new()
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductedToday = (await _productionListDetailService.GetProductionListDetailByDateAndProductIdAsync(date, product)).Quantity,
                    Price = (await _productionListDetailService.GetProductionListDetailByDateAndProductIdAsync(date, product)).Price
                };

                for (int j = 1; j < 6 && productSoldInTheBakery.Price == 0; j++)
                {
                    productSoldInTheBakery.Price = (await _productionListDetailService.GetProductionListDetailByDateAndProductIdAsync(date.Date.AddDays(-j), product)).Price;
                }

                productSoldInTheBakery.RemainingYesterday = await _productsCountingService.GetQuantityProductsCountingByDateAndProductIdAsync(date.Date.AddDays(-1), product.Id);
                productSoldInTheBakery.RemainingToday = await _productsCountingService.GetQuantityProductsCountingByDateAndProductIdAsync(date.Date, product.Id);
                productSoldInTheBakery.StaleProductToday = await _staleProductService.GetQuantityStaleProductByDateAndProductIdAsync(date.Date, product.Id);

                productSoldInTheBakery.Revenue = productSoldInTheBakery.Price * (productSoldInTheBakery.RemainingYesterday + productSoldInTheBakery.ProductedToday - productSoldInTheBakery.RemainingToday - productSoldInTheBakery.StaleProductToday);
                totalRevenue += productSoldInTheBakery.Revenue;
            }

            return totalRevenue;
        }
    }
}
