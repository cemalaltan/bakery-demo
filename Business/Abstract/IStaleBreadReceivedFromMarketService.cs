﻿using Entities.Concrete;

namespace Business.Abstract
{
    public interface IStaleBreadReceivedFromMarketService
    {
        List<StaleBreadReceivedFromMarket> GetAll();
        List<StaleBreadReceivedFromMarket> GetByDate(DateTime date);
        StaleBreadReceivedFromMarket GetByMarketId(int id, DateTime date);
        int  GetStaleBreadCountByMarketId(int MarketId, DateTime date);
        void Add(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket);
        void DeleteById(int id);
        void DeleteByDateAndMarketId(DateTime date, int marketId);
        void Delete(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket);
        void Update(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket);
        StaleBreadReceivedFromMarket GetById(int id);

        bool IsExist(int marketId, DateTime date);
    }
}
