﻿using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceStaleProductService
    {
        List<ServiceStaleProduct> GetAll();
        List<ServiceStaleProduct> GetAllByDate(DateTime date, int serviceTypeId);
        void Add(ServiceStaleProduct serviceStaleProduct);
        void DeleteById(int id);
        void Delete(ServiceStaleProduct serviceStaleProduct);
        void Update(ServiceStaleProduct serviceStaleProduct);
        ServiceStaleProduct GetById(int id);
    }
}
