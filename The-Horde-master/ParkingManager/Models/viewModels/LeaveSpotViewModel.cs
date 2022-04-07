using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManager.Models.viewModels
{
    public class LeaveSpotViewModel
    {
        [DisplayName("From")]
        [DataType(DataType.Date)]
        public DateTime leaveSpotStartDate { get; set; }

        [DisplayName("Until")]
        [DataType(DataType.Date)]
        public DateTime leaveSpotEndDate { get; set; }

        public int Id { get; set; }
    }
}
