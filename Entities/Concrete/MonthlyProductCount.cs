﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class MonthlyProductCount : IEntity
    {
        public int Id { get; set; }

        public string Category { get; set; }
        public string Name { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

    }
}
