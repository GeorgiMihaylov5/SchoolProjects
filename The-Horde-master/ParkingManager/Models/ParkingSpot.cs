using System;
using System.Collections.Generic;

namespace ParkingManager.Models
{
    public partial class ParkingSpot
    {
        public ParkingSpot()
        {
            UsersToParkingSpot = new HashSet<UsersToParkingSpot>();
        }

        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public int? RenterId { get; set; }
        public int ParkingSpotNumber { get; set; }
        public byte IsFree { get; set; }

        public virtual ICollection<UsersToParkingSpot> UsersToParkingSpot { get; set; }
    }
}
