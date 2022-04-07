using System;
using System.Collections.Generic;

namespace ParkingManager.Models
{
    public partial class UsersToParkingSpot
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int ParkingSpotId { get; set; }
        public DateTime StartingRentDate { get; set; }
        public DateTime EndingRentDate { get; set; }

        public virtual Users User { get; set; }
        public virtual ParkingSpot ParkingSpot { get; set; }
    }
}
