using Core.Entities;

namespace Entities.Concrete
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
    }
}
