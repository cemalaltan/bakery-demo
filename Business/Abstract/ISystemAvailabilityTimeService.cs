using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISystemAvailabilityTimeService
    {
        Task<SystemAvailabilityTime> GetSystemAvailabilityTimeAsync();
        Task UpdateSystemAvailabilityTimeAsync(SystemAvailabilityTime systemAvailabilityTime);


    }
}
