using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class SalaryPaymentReport : IEntity
    {
        public string  Name { get; set; }
        public string Role { get; set; }
        public decimal Salary { get; set; }
        public decimal Advance { get; set; }
        public decimal PaidAmount { get; set; }

    }
}
