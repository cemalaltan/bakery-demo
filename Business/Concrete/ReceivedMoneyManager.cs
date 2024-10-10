using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReceivedMoneyManager : IReceivedMoneyService
    {
        private readonly IReceivedMoneyDal _receivedMoneyDal;

        public ReceivedMoneyManager(IReceivedMoneyDal receivedMoneyDal)
        {
            _receivedMoneyDal = receivedMoneyDal;
        }

        public async Task AddAsync(ReceivedMoney receivedMoney)
        {
            await _receivedMoneyDal.Add(receivedMoney);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _receivedMoneyDal.DeleteById(id);
        }

        public async Task DeleteAsync(ReceivedMoney receivedMoney)
        {
            await _receivedMoneyDal.Delete(receivedMoney);
        }

        public async Task<List<ReceivedMoney>> GetAllAsync()
        {
            return await _receivedMoneyDal.GetAll();
        }

        public async Task<ReceivedMoney> GetByIdAsync(int id)
        {
            return await _receivedMoneyDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(ReceivedMoney receivedMoney)
        {
            await _receivedMoneyDal.Update(receivedMoney);
        }
    }
}
