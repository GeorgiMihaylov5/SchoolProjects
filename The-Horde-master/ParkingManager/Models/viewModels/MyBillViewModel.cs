using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManager.Models.viewModels
{
    public class MyBillViewModel
    {
        public double Price { get; set; }

        public double? totalDaysInPark { get; set; }
    }
}
