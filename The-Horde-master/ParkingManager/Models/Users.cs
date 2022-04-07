using System;
using System.Collections.Generic;

namespace ParkingManager.Models
{
    public partial class Users
    {
        public Users()
        {
            UsersToParkingSpot = new HashSet<UsersToParkingSpot>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ParkingSpotNumber { get; set; }
        public double? TotalDaysInPark { get; set; }

        public virtual ICollection<UsersToParkingSpot> UsersToParkingSpot { get; set; }
    }
}
