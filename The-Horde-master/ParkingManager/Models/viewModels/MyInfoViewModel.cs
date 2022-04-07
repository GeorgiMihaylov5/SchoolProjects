using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManager.Models.viewModels
{
    public class MyInfoViewModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]      
        [Display(Name = "Name")]
        [StringLength(40)]
        public string firstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        [Display(Name = "Last Name")]
        [StringLength(40)]
        public string lastName { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [MaxLength(12)]
        [MinLength(10)]
        [Phone(ErrorMessage = "Invalid phone number ")]
        public string phoneNumber { get; set; }

        public string totalDaysInPark { get; set; }
        public string parkingSpotNumber { get; set; }
        public string Id { get; set; }
    }
}
