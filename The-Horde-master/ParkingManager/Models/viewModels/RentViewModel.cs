using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManager.Models.viewModels
{
    public class RentViewModel
    {
        [DisplayName("From")]
        [DataType(DataType.Date)]
        public DateTime rentStartDate { get; set; }

        [DisplayName("Until")]
        [DataType(DataType.Date)]
        public DateTime rentEndDate { get; set; }


        public int Id { get; set; }
    }
}
