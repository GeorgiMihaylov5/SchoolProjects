using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManager.Models.viewModels
{
    public class CreateSpotViewModel
    {
        [Required]
        [Display(Name = "Owner ID")]
        public int OwnerId { get; set; }

        [Required]
        [Display(Name = "Parking Spot Number")]
        public int ParkingSpotNumber { get; set; }
    }
}
