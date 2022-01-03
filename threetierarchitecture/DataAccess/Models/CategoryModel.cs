﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.DataAccess.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public VehicleCategories CategoryType { get; set; }

        public enum VehicleCategories
        {
            SMALLCAR,
            MEDIUMCAR,
            TRUCK
        }
    }

}
