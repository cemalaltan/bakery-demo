using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndOfDayAccountController : ControllerBase
    {
        //private IPurchasedProductListDetailService _purchasedProductListDetailService;
        //private IProductService _productService;
        //private IProductionListDetailService _productionListDetailService;
        //private IProductsCountingService _productsCountingService;
        //private IStaleProductService _staleProductService;

        //private IBreadCountingService _breadCountingService;

        //private IGivenProductsToServiceService _givenProductsToServiceService;

        //private IDoughFactoryListService _doughFactoryListService;
        //private IDoughFactoryListDetailService _doughFactoryListDetailService;
        //private IDoughFactoryProductService _doughFactoryProductService;

        //private IBreadPriceService _breadPriceService;

        //private IStaleBreadService _staleBreadService;

        //private IMarketEndOfDayService _marketEndOfDayService;
        //private IMoneyReceivedFromMarketService _moneyReceivedFromMarketService;

        //private IServiceStaleProductService _serviceStaleProductService;

        //private ICashCountingService _cashCountingService;

        private IEndOfDayAccountService _endOfDayAccountService ;
        private ICreatePdfService _createPdfService;

        public EndOfDayAccountController(IEndOfDayAccountService endOfDayAccountService, ICreatePdfService createPdfService)
        {
            _endOfDayAccountService = endOfDayAccountService;
            _createPdfService = createPdfService;
        }


        //public EndOfDayAccountController(IMarketEndOfDayService marketEndOfDayService, IStaleBreadService staleBreadService, IDoughFactoryListService doughFactoryListService, IDoughFactoryListDetailService doughFactoryListDetailService, IDoughFactoryProductService doughFactoryProductService,
        //    IGivenProductsToServiceService givenProductsToServiceService, IBreadCountingService breadCountingService,
        //    IPurchasedProductListDetailService purchasedProductListDetailService, IStaleProductService staleProductService,
        //    IProductsCountingService productsCountingService, IProductService productService, IProductionListDetailService productionListDetailService,
        //    IBreadPriceService breadPriceService,
        //    IMoneyReceivedFromMarketService moneyReceivedFromMarketService,
        //    IServiceStaleProductService serviceStaleProductService,
        //    ICashCountingService cashCountingService,
        //    IEndOfDayAccountService endOfDayAccountService)
        //{
        //    _endOfDayAccountService = endOfDayAccountService;

        //    _serviceStaleProductService = serviceStaleProductService;
        //    _cashCountingService = cashCountingService;

        //    _moneyReceivedFromMarketService = moneyReceivedFromMarketService;

        //    _marketEndOfDayService = marketEndOfDayService;
        //    _purchasedProductListDetailService = purchasedProductListDetailService;
        //    _productService = productService;
        //    _productionListDetailService = productionListDetailService;
        //    _productsCountingService = productsCountingService;
        //    _staleProductService = staleProductService;
        //    _breadCountingService = breadCountingService;
        //    _givenProductsToServiceService = givenProductsToServiceService;

        //    _doughFactoryProductService = doughFactoryProductService;
        //    _doughFactoryListService = doughFactoryListService;
        //    _doughFactoryListDetailService = doughFactoryListDetailService;

        //    _staleBreadService = staleBreadService;
        //    _breadPriceService = breadPriceService;

        //}

        [HttpGet("GetEndOfDayAccountDetail")]
        public async Task<ActionResult> GetEndOfDayAccountDetail(DateTime date)
        {
            try
            {             
                var result =await _endOfDayAccountService.GetEndOfDayAccountDetailAsync(date);

                return Ok(result);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet("GetEndOfDayAccountDetailPdf")]
        public async Task<ActionResult> GetEndOfDayAccountDetailPdf(DateTime date)
        {
            try
            {             
                var endOfDayResult = await _endOfDayAccountService.GetEndOfDayAccountDetailAsync(date);
                var ProductsSoldInTheBakery = await _endOfDayAccountService.GetProductsSoldInTheBakeryAsync(date);

                byte[] pdfContent = await _createPdfService.EndOfDayAccountCreatePdfAsync(date, endOfDayResult, ProductsSoldInTheBakery);


                string formattedDate = date.ToString("dd.MM.yyyy", new CultureInfo("tr-TR"));


				string fileName = $"GünSonuHesap_{formattedDate}.pdf";
                string contentType = "application/pdf";

               
                FileContentResult fileContentResult = new FileContentResult(pdfContent, contentType)
                {
                    FileDownloadName = fileName
                };

                return fileContentResult;

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        //[HttpGet("GetServiceDetail")]
        //public ActionResult GetServiceDetail(DateTime date)
        //{
        //    try
        //    {
        //        var result = new
        //        {
        //            PaymentMarkets = _marketEndOfDayService.CalculateMarketEndOfDay(date),
        //            MoneyReceivedFromMarkets = _moneyReceivedFromMarketService.GetByDate(date)
        //        };
        //        //return Ok(_marketEndOfDayService.CalculateMarketEndOfDay(date), _moneyReceivedFromMarketService.GetByDate(date));
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, e.Message);
        //    }
        //}

        [HttpGet("GetProductsSoldInTheBakery")]
        public async Task<ActionResult> GetProductsSoldInTheBakery(DateTime date)
        {
            try
            {            
                var TotalRevenue = await _endOfDayAccountService.GetProductsSoldInTheBakeryAsync(date);
                return Ok(TotalRevenue);               
            }
            catch (Exception e)
            {
                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        //[HttpGet("GetPurchasedProductsSoldInTheBakery")]
        //public ActionResult GetPurchasedProductsSoldInTheBakery(DateTime date)
        //{
        //    try
        //    {
        //        decimal TotalRevenue = 0;

        //        List<Product> products = _productService.GetAllByCategoryId(3);
        //        //List<PurchasedProductSoldInTheBakery> purchasedProductsSoldInTheBakery = new();
        //        for (int i = 0; i < products.Count; i++)
        //        {
        //            PurchasedProductSoldInTheBakery purchasedProductSoldInTheBakery = new();
        //            purchasedProductSoldInTheBakery.ProductId = products[i].Id;
        //            purchasedProductSoldInTheBakery.ProductName = products[i].Name;


        //            purchasedProductSoldInTheBakery.Price = _purchasedProductListDetailService.GetPurchasedProductListDetailByDateAndProductId((date.CreatedAt), products[i].Id).Price;


        //            for (int j = 1; j < 6 && purchasedProductSoldInTheBakery.Price == 0; j++)
        //            {
        //                purchasedProductSoldInTheBakery.Price = _purchasedProductListDetailService.GetPurchasedProductListDetailByDateAndProductId((date.CreatedAt.AddDays(-j)), products[i].Id).Price;
        //            }

        //            purchasedProductSoldInTheBakery.RemainingYesterday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.CreatedAt.AddDays(-1)), products[i].Id);
        //            purchasedProductSoldInTheBakery.RemainingToday = _productsCountingService.GetQuantityProductsCountingByDateAndProductId((date.CreatedAt), products[i].Id);

        //            purchasedProductSoldInTheBakery.StaleProductToday = _staleProductService.GetQuantityStaleProductByDateAndProductId((date.CreatedAt), products[i].Id);

        //            purchasedProductSoldInTheBakery.PurchasedToday = _purchasedProductListDetailService.GetPurchasedProductListDetailByDateAndProductId((date.CreatedAt), products[i].Id)?.Quantity ?? 0;

        //            purchasedProductSoldInTheBakery.Revenue = purchasedProductSoldInTheBakery.Price * (purchasedProductSoldInTheBakery.RemainingYesterday + purchasedProductSoldInTheBakery.PurchasedToday - purchasedProductSoldInTheBakery.RemainingToday - purchasedProductSoldInTheBakery.StaleProductToday);

        //            TotalRevenue += purchasedProductSoldInTheBakery.Revenue;

        //           // purchasedProductsSoldInTheBakery.Add(purchasedProductSoldInTheBakery);
        //        }

        //        //return Ok(purchasedProductsSoldInTheBakery);
        //        return Ok(TotalRevenue);
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, "Daha sonra tekrar deneyin...");
        //    }

        //}

        //[HttpGet("GetBreadSold")]
        //public ActionResult GetBreadSold(DateTime date)
        //{

        //    try
        //    {
        //        double AllBreadProduced = 0;


        //        List<DoughFactoryListDto> doughFactoryListDto = _doughFactoryListService.GetByDate(date.CreatedAt);

        //        for (int i = 0; i < doughFactoryListDto.Count; i++)
        //        {

        //            List<DoughFactoryListDetail> doughFactoryListDetails = _doughFactoryListDetailService.GetByDoughFactoryList(doughFactoryListDto[i].Id);

        //            for (int j = 0; j < doughFactoryListDetails.Count; j++)
        //            {
        //                DoughFactoryProduct doughFactoryProduct = _doughFactoryProductService.GetById(doughFactoryListDetails[j].DoughFactoryProductId);
        //                AllBreadProduced += doughFactoryProduct.BreadEquivalent * doughFactoryListDetails[j].Quantity;
        //            }
        //        }

        //        List<StaleBreadDto> staleBreadDtos = _staleBreadService.GetAllByDate(date);
        //        double StaleBread = 0;

        //        for (int i = 0; i < staleBreadDtos.Count; i++)
        //        {
        //            DoughFactoryProduct doughFactoryProduct = _doughFactoryProductService.GetById(staleBreadDtos[i].DoughFactoryProductId);
        //            StaleBread += doughFactoryProduct.BreadEquivalent * staleBreadDtos[i].Quantity;
        //        }

        //        BreadSold breadSold = new();
        //        breadSold.RemainingYesterday = _breadCountingService.GetBreadCountingByDate(date.CreatedAt.AddDays(-1))?.Quantity ?? 0;
        //        breadSold.RemainingToday = _breadCountingService.GetBreadCountingByDate(date.CreatedAt)?.Quantity ?? 0;
        //        breadSold.ProductedToday = AllBreadProduced;
        //        breadSold.StaleProductToday = StaleBread;
        //        breadSold.ProductName = "Ekmek";
        //        breadSold.Price = 5;

        //        breadSold.Revenue = breadSold.Price * (breadSold.RemainingYesterday - breadSold.RemainingToday + breadSold.ProductedToday - breadSold.StaleProductToday);
        //        return Ok(breadSold);
        //    }
        //    catch (Exception e)
        //    {

        //        return StatusCode(500, "Daha sonra tekrar deneyin...");
        //    }


        //}

    }

}
