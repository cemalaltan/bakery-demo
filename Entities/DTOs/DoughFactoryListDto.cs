﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{

       public class DoughFactoryListDto : IDto
    {


        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

        public DateTime Date { get; set; }

    }
}
