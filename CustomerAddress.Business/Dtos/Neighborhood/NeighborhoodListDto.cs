﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAddress.Business.Dtos.Neighborhood
{
    public class NeighborhoodListDto
    {
        public int Id { get; set; }
        public string TownName { get; set; }
        public string NeighborhoodName { get; set; }
    }
}