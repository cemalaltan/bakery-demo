using Core.Entities;

namespace Entities.Concrete
{
    public class AccumulatedMoneyDelivery :IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public decimal Amount { get; set; }
        public decimal AccumulatedAmount { get; set; }
        public int Type { get; set; } 
    }
}
