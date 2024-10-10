using Business.Abstract;
using Business.AbstractAPI;
using Business.Constants;
using Entities.Concrete;
using Entities.DTOs;


namespace Business.ConcreteAPI
{

    public class DoughFactoryAPIManager : IDoughFactoryAPIService
    {

        private IDoughFactoryListService _doughFactoryListService;
        private IDoughFactoryListDetailService _doughFactoryListDetailService;
        private IDoughFactoryProductService _doughFactoryProductService;

        public DoughFactoryAPIManager(IDoughFactoryProductService doughFactoryProductService, IDoughFactoryListService doughFactoryListService, IDoughFactoryListDetailService doughFactoryListDetailService)
        {
            _doughFactoryListService = doughFactoryListService;
            _doughFactoryListDetailService = doughFactoryListDetailService;
            _doughFactoryProductService = doughFactoryProductService;
        }


        public List<DoughFactoryListDto> GetByDateDoughFactoryList(DateTime date)
        {
            var result = _doughFactoryListService.GetByDateAsync(date.Date).Result;
            return result;
        }


        public int AddDoughFactory(List<DoughFactoryListDetail> doughFactoryListDetail, int userId)
        {

            int doughFactoryListId = doughFactoryListDetail[0].DoughFactoryListId;
            bool isNewList = doughFactoryListId == 0;

            if (isNewList)
            {
                doughFactoryListId = _doughFactoryListService.AddAsync(new DoughFactoryList { UserId = userId, Date = DateTime.Now }).Result;
            }

            foreach (var detail in doughFactoryListDetail)
            {
                if (isNewList)
                {
                    detail.DoughFactoryListId = doughFactoryListId;
                    _doughFactoryListDetailService.AddAsync(detail);
                }
                else
                {
                    if (_doughFactoryListDetailService.IsExistAsync(detail.DoughFactoryProductId, doughFactoryListId).Result)
                    {
                        // return Conflict(Messages.Conflict);
                        throw new Exception(Messages.Conflict);
                    }
                    else
                    {
                        _doughFactoryListDetailService.AddAsync(detail);
                    }
                }
            }

            return doughFactoryListId;


        }

        public List<GetAddedDoughFactoryListDetailDto> GetDoughFactoryListDetail(int doughFactoryListId)
        {


            List<DoughFactoryListDetail> doughFactoryListDetails = _doughFactoryListDetailService.GetByDoughFactoryListAsync(doughFactoryListId).Result;

            List<GetAddedDoughFactoryListDetailDto> List = new();

            for (int i = 0; i < doughFactoryListDetails.Count; i++)
            {
                GetAddedDoughFactoryListDetailDto addedDoughFactoryListDetailDto = new();
                addedDoughFactoryListDetailDto.Id = doughFactoryListDetails[i].Id;

                addedDoughFactoryListDetailDto.DoughFactoryProductId = doughFactoryListDetails[i].DoughFactoryProductId;
                addedDoughFactoryListDetailDto.DoughFactoryProductName = _doughFactoryProductService.GetByIdAsync(doughFactoryListDetails[i].DoughFactoryProductId).Result.Name;

                addedDoughFactoryListDetailDto.Quantity = doughFactoryListDetails[i].Quantity;
                addedDoughFactoryListDetailDto.DoughFactoryListId = doughFactoryListDetails[i].DoughFactoryListId;

                List.Add(addedDoughFactoryListDetailDto);
            }

            return List;

        }



        public List<ProductNotAddedDto> GetMarketByServiceListId(int doughFactoryListId)
        {

            List<DoughFactoryProduct> allDoughFactoryProduct = _doughFactoryProductService.GetAllAsync().Result;

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


                List<DoughFactoryListDetail> doughFactoryListDetails = _doughFactoryListDetailService.GetByDoughFactoryListAsync(doughFactoryListId).Result;

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

            return getNotAddedDoughFactoryListDetailDto;


        }


        public void DeleteDoughFactoryListDetail(int detailId)
        {
            _doughFactoryListDetailService.DeleteByIdAsync(detailId);
        }


        public void UpdateDoughFactoryListDetail(DoughFactoryListDetail doughFactoryListDetail)
        {
            _doughFactoryListDetailService.UpdateAsync(doughFactoryListDetail);
        }


    }
}
