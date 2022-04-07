using ParkingManager.Models;
using ParkingManager.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManager.Services
{
    public class ParkingService
    {
        private ParkingManagerDatabaseContext context;
        public ParkingService(ParkingManagerDatabaseContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Creates a new parking spot with owner or a new parking spot without owner, which can be set later on. 
        /// </summary>
        /// <param name="parkingSpot"></param>
        public void CreateParkingSpot(CreateSpotViewModel parkingSpot)
        {
            ParkingSpot newSpot = new ParkingSpot();
            newSpot.OwnerId = parkingSpot.OwnerId;
            newSpot.ParkingSpotNumber = parkingSpot.ParkingSpotNumber;

            context.ParkingSpot.Add(newSpot);
            context.SaveChanges();
        }

        /// <summary>
        /// If the chosen parking spot exists, it is being removed from the database.
        /// </summary>
        /// <param name="parkingSpot"></param>
        public void DeleteParkingSpot(ParkingSpot parkingSpot)
        {
            var parkingSpotToDelete = this.context.ParkingSpot.FirstOrDefault(x => x.Id == parkingSpot.Id);
            if (parkingSpotToDelete == null)
            {
                throw new ArgumentException();
            }
            context.ParkingSpot.Remove(parkingSpotToDelete);
            context.SaveChanges();
        }

        /// <summary>
        /// Checks if a parking sprot is free to rent.
        /// </summary>
        /// <param name="parkingSpot"></param>
        /// <param name="usersToParkingSpots"></param>
        /// <returns>true or false</returns>
        public bool IsFree(ParkingSpot parkingSpot, UsersToParkingSpot usersToParkingSpots)
        {
            if (parkingSpot.OwnerId == null && usersToParkingSpots.StartingRentDate == null)
            {
                return true;
            }
            if (parkingSpot.OwnerId != null && usersToParkingSpots.StartingRentDate == null)
            {
                return false;
            }
            if (parkingSpot.OwnerId != null && usersToParkingSpots.StartingRentDate == DateTime.Now)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Sets the owner, that corresponds to the number in ownerId, to the spot, that corresponds to the number in ParkingSpotNumber. 
        /// </summary>
        /// <param name="ParkingSpotNumber"></param>
        /// <param name="ownerId"></param>
        public void SetOwner(int ParkingSpotNumber, int ownerId)
        {
            ParkingSpot parkingSpots = context.ParkingSpot.FirstOrDefault(ps => ps.ParkingSpotNumber == ParkingSpotNumber);
            parkingSpots.OwnerId = ownerId;
            context.SaveChanges();
        }

        /// <summary>
        /// Sets the renter, that corresponds to the number in userId, to the spot, that corresponds to the number in parkingSpotNumber. 
        /// </summary>
        /// <param name="parkingSpotNumber"></param>
        /// <param name="userId"></param>
        public void SetRenter(int parkingSpotNumber, int userId)
        {
            ParkingSpot parkingSpot = context.ParkingSpot.FirstOrDefault(ps => ps.ParkingSpotNumber == parkingSpotNumber);
            UsersToParkingSpot usersToParkingSpots = context.UsersToParkingSpot.FirstOrDefault(ups => ups.ParkingSpotId == parkingSpot.Id);
            usersToParkingSpots.UserId = userId;
            context.SaveChanges();
        }

        /// <summary>
        /// If there have been made changes to a parking spot, this method updates the user, related to this spot, so there isn't difference between the status of the spot and the information contained in it's user.  
        /// </summary>
        /// <param name="command"></param>
        /// <param name="userId"></param>
        /// <param name="parkingSpotNumber"></param>
        public void ChangeUserStatusForSpot(string command, int? userId, int? parkingSpotNumber)
        {
            if (userId != null)
            {
                switch (command)
                {
                    case "Create":
                        var CreatedUser = context.Users.FirstOrDefault(x => x.Id == userId);
                        CreatedUser.ParkingSpotNumber = parkingSpotNumber.ToString();
                        context.SaveChanges();
                        break;
                    case "Delete":
                        var userWithThisSpot = context.Users.FirstOrDefault(x => x.Id == userId);
                        userWithThisSpot.ParkingSpotNumber = null;
                        context.SaveChanges();
                        break;
                    case "Edit":
                        var editedUser = context.Users.FirstOrDefault(x => x.Id == userId);
                        editedUser.ParkingSpotNumber = parkingSpotNumber.ToString();
                        context.SaveChanges();
                        break;
                }
            }
        }
    }
}
