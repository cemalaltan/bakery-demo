using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class MarketContractDto : IDto
    {
        public int Id { get; set; }
        public int ServiceProductId { get; set; }
        public decimal Price { get; set; }
        public int MarketId { get; set; }
        public string MarketName { get; set; }
        public bool IsActive { get; set; }
    }
}
