using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;


namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();

            builder.RegisterType<MonthlyProductCountManager>().As<IMonthlyProductCountService>();
            builder.RegisterType<EfMonthlyProductCountDal>().As<IMonthlyProductCountDal>();

            builder.RegisterType<AdvanceManager>().As<IAdvanceService>();
            builder.RegisterType<EfAdvanceDal>().As<IAdvanceDal>();

            builder.RegisterType<SalaryPaymentManager>().As<ISalaryPaymentService>();
            builder.RegisterType<EfSalaryPaymentDal>().As<ISalaryPaymentDal>();

            builder.RegisterType<EmployeeManager>().As<IEmployeeService>();
            builder.RegisterType<EfEmployeeDal>().As<IEmployeeDal>();

            builder.RegisterType<AccumulatedMoneyDeliveryManager>().As<IAccumulatedMoneyDeliveryService>();
            builder.RegisterType<EfAccumulatedMoneyDeliveryDal>().As<IAccumulatedMoneyDeliveryDal>();

            builder.RegisterType<AccumulatedMoneyManager>().As<IAccumulatedMoneyService>();
            builder.RegisterType<EfAccumulatedMoneyDal>().As<IAccumulatedMoneyDal>();


            builder.RegisterType<CreatePdfManager>().As<ICreatePdfService>();

            builder.RegisterType<EndOfDayAccountManager>().As<IEndOfDayAccountService>();


            builder.RegisterType<ReceivedMoneyFromServiceManager>().As<IReceivedMoneyFromServiceService>();
            builder.RegisterType<EfReceivedMoneyFromServiceDal>().As<IReceivedMoneyFromServiceDal>();

            builder.RegisterType<SystemAvailabilityTimeManager>().As<ISystemAvailabilityTimeService>();
            builder.RegisterType<EfSystemAvailabilityTimeDal>().As<ISystemAvailabilityTimeDal>();

            builder.RegisterType<ServiceStaleProductManager>().As<IServiceStaleProductService>();
            builder.RegisterType<EfServiceStaleProductDal>().As<IServiceStaleProductDal>();

            builder.RegisterType<StaleProductManager>().As<IStaleProductService>();
            builder.RegisterType<EfStaleProductDal>().As<IStaleProductDal>();

            builder.RegisterType<StaleBreadManager>().As<IStaleBreadService>();
            builder.RegisterType<EfStaleBreadDal>().As<IStaleBreadDal>();

            builder.RegisterType<GivenProductsToServiceManager>().As<IGivenProductsToServiceService>();
            builder.RegisterType<EfGivenProductsToServiceDal>().As<IGivenProductsToServiceDal>();

            builder.RegisterType<ExpenseManager>().As<IExpenseService>();
            builder.RegisterType<EfExpenseDal>().As<IExpenseDal>();

            builder.RegisterType<PurchasedProductListDetailManager>().As<IPurchasedProductListDetailService>();
            builder.RegisterType<EfPurchasedProductListDetailDal>().As<IPurchasedProductListDetailDal>();
            
            builder.RegisterType<ProductsCountingManager>().As<IProductsCountingService>();
            builder.RegisterType<EfProductsCountingDal>().As<IProductsCountingDal>();

            builder.RegisterType<CashCountingManager>().As<ICashCountingService>();
            builder.RegisterType<EfCashCountingDal>().As<ICashCountingDal>();

            builder.RegisterType<BreadCountingManager>().As<IBreadCountingService>();
            builder.RegisterType<EfBreadCountingDal>().As<IBreadCountingDal>();

            builder.RegisterType<DebtMarketManager>().As<IDebtMarketService>();
            builder.RegisterType<EfDebtMarketDal>().As<IDebtMarketDal>();
            
            builder.RegisterType<MoneyReceivedFromMarketManager>().As<IMoneyReceivedFromMarketService>();
            builder.RegisterType<EfMoneyReceivedFromMarketDal>().As<IMoneyReceivedFromMarketDal>();
            
            builder.RegisterType<StaleBreadReceivedFromMarketManager>().As<IStaleBreadReceivedFromMarketService>();
            builder.RegisterType<EfStaleBreadReceivedFromMarketDal>().As<IStaleBreadReceivedFromMarketDal>();
            
            builder.RegisterType<ServiceListManager>().As<IServiceListService>();
            builder.RegisterType<EfServiceListDal>().As<IServiceListDal>();

            builder.RegisterType<ServiceListDetailManager>().As<IServiceListDetailService>();
            builder.RegisterType<EfServiceListDetailDal>().As<IServiceListDetailDal>();

            builder.RegisterType<ServiceProductManager>().As<IServiceProductService>();
            builder.RegisterType<EfServiceProductDal>().As<IServiceProductDal>();

            builder.RegisterType<MarketContractManager>().As<IMarketContractService>();
            builder.RegisterType<EfMarketContractDal>().As<IMarketContractDal>();

            builder.RegisterType<MarketManager>().As<IMarketService>();
            builder.RegisterType<EfMarketDal>().As<IMarketDal>();

            builder.RegisterType<BreadPriceManager>().As<IBreadPriceService>();
            builder.RegisterType<EfBreadPriceDal>().As<IBreadPriceDal>();

            builder.RegisterType<ProductManager>().As<IProductService>();
            builder.RegisterType<EfProductDal>().As<IProductDal>();

            builder.RegisterType<ProductionListManager>().As<IProductionListService>();
            builder.RegisterType<EfProductionListDal>().As<IProductionListDal>();

            builder.RegisterType<ProductionListDetailManager>().As<IProductionListDetailService>();
            builder.RegisterType<EfProductionListDetailDal>().As<IProductionListDetailDal>();

            builder.RegisterType<DoughFactoryProductManager>().As<IDoughFactoryProductService>();
            builder.RegisterType<EfDoughFactoryProductDal>().As<IDoughFactoryProductDal>(); 
            
            builder.RegisterType<DoughFactoryListManager>().As<IDoughFactoryListService>();
            builder.RegisterType<EfDoughFactoryListDal>().As<IDoughFactoryListDal>();

            builder.RegisterType<DoughFactoryListDetailManager>().As<IDoughFactoryListDetailService>();
            builder.RegisterType<EfDoughFactoryListDetailDal>().As<IDoughFactoryListDetailDal>();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();


            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
