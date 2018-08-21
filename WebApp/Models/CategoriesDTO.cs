﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApp.Models
{
    public class CategoriesDTO
    {
        public long CategoriesId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<ProductsDTO> ProductsDTO { get; set; }

    }
}
