using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public  class Category :IEntity
    {
    
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool Store { get; set; }
        public bool IsActive { get; set; }

    }
}
