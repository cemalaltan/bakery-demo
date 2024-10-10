using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SystemAvailabilityTimeManager : ISystemAvailabilityTimeService
    {
        private readonly ISystemAvailabilityTimeDal _systemAvailabilityTimeDal;

        public SystemAvailabilityTimeManager(ISystemAvailabilityTimeDal systemAvailabilityTime)
        {
            _systemAvailabilityTimeDal = systemAvailabilityTime;
        }

        public async Task<SystemAvailabilityTime> GetSystemAvailabilityTimeAsync()
        {
            return await _systemAvailabilityTimeDal.Get(s => s.Id == 1);
        }

        public async Task UpdateSystemAvailabilityTimeAsync(SystemAvailabilityTime systemAvailabilityTime)
        {
            await _systemAvailabilityTimeDal.Update(systemAvailabilityTime);
        }
    }
}
