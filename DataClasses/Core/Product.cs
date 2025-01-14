﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses.Core
{
    public class ProductCore
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }
        public int Stock { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
