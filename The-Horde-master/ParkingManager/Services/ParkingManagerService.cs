using Microsoft.EntityFrameworkCore.Internal;
using ParkingManager.Models;
using ParkingManager.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ParkingManager.Services
{
    public class ParkingManagerSevice
    {
        private ParkingManagerDatabaseContext context;
        private UserService userService;
        private ParkingService parkingService;
        private const double pricePerMonth = 25;
        public ParkingManagerSevice(ParkingManagerDatabaseContext context)
        {
            this.context = context;
            userService = new UserService(context);
            parkingService = new ParkingService(context);
        }


        /// <summary>
        /// This method calculates the rental period.
        /// </summary>
        /// <param name="startingDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Returns the difference between the starting and the ending date</returns>
        private double? CalculateDays(DateTime startingDate, DateTime endDate)
        {
            double result = (endDate - startingDate).TotalDays;

            return result;
        }
        /// <summary>
        /// This method checks if the user owns a parking spot. 
        /// If no the method sets the user as a renter to the spot.
        /// Changes the id of the user and the rent dates of the spot.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="parkingSpotName"></param>
        /// <param name="rentStartDate"></param>
        /// <param name="rentEndDate"></param>
        public void Rent(Users renter, string parkingSpotName, DateTime rentStartDate, DateTime rentEndDate)
        {
            ParkingSpot parkingSpotToRent = context.ParkingSpot
                .FirstOrDefault(ps => ps.ParkingSpotNumber == int.Parse(parkingSpotName));
            UsersToParkingSpot rentedPlace;

            if (renter.ParkingSpotNumber == null)
            {
                try
                {
                    rentedPlace = context.UsersToParkingSpot
                       .LastOrDefault(s => s.ParkingSpotId == parkingSpotToRent.Id);
                }
                catch (Exception)
                {
                    rentedPlace = new UsersToParkingSpot();
                    rentedPlace.UserId = renter.Id;
                    rentedPlace.StartingRentDate = rentStartDate;
                    rentedPlace.EndingRentDate = rentEndDate;
                    renter.ParkingSpotNumber = parkingSpotToRent.ParkingSpotNumber.ToString();
                    rentedPlace.ParkingSpotId = parkingSpotToRent.Id;
                    context.UsersToParkingSpot.Add(rentedPlace);
                }
                if (rentedPlace != null)
                {
                    rentedPlace.UserId = renter.Id;
                    rentedPlace.StartingRentDate = rentStartDate;
                    rentedPlace.EndingRentDate = rentEndDate;
                    renter.ParkingSpotNumber = parkingSpotToRent.ParkingSpotNumber.ToString();
                    rentedPlace.ParkingSpotId = parkingSpotToRent.Id;
                }
                parkingSpotToRent.RenterId = renter.Id;
                parkingSpotToRent.IsFree = 0;
                rentedPlace.User = renter;
                if (renter.TotalDaysInPark == null)
                {
                    renter.TotalDaysInPark = CalculateDays(rentStartDate, rentEndDate);
                }
                else
                {
                    renter.TotalDaysInPark += CalculateDays(rentStartDate, rentEndDate);
                }
                renter.ParkingSpotNumber = parkingSpotName;
                context.SaveChanges();
            }


            else 
            {


               throw new ArgumentException();

            }
        }

        /// <summary>
        /// This method changes the state of the parking space to free.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="parkingSpotName"></param>
        /// <param name="rentStartDate"></param>
        /// <param name="rentEndDate"></param>
        public void LeaveSpot(Users user, string parkingSpotName, DateTime rentStartDate, DateTime rentEndDate)
        {
            ParkingSpot parkingSpot = context.ParkingSpot.FirstOrDefault(ps => ps.ParkingSpotNumber == int.Parse(parkingSpotName));
            string userParkingSpotNumber = user.ParkingSpotNumber;
            UsersToParkingSpot freePlace = new UsersToParkingSpot();
            freePlace.ParkingSpotId = parkingSpot.Id;
            freePlace.StartingRentDate = rentStartDate;
            freePlace.EndingRentDate = rentEndDate;
            parkingSpot.IsFree = 1;
            freePlace.UserId = user.Id;
            context.UsersToParkingSpot.Add(freePlace);
            if (parkingSpot.RenterId == user.Id)
            {
                user.ParkingSpotNumber = null;
            }
           
            context.SaveChanges();
            

            //if (parkingSpot.RenterId == null)
            //{
            //    rentedPlace = new UsersToParkingSpot();
            //    //rentedPlace.ParkingSpotId = parkingSpot.Id;
            //    parkingSpot.RenterId = user.Id;
            //    context.SaveChanges();

            //}

            //else
            //{
            //    rentedPlace = context.UsersToParkingSpot.FirstOrDefault(ups => ups.ParkingSpotId == parkingSpot.Id);
            //}
        }

        /// <summary>
        /// This method checks if any parking spots are free.
        /// Checks if the ending rent date is after the current moment for each parking spot. 
        /// Then checks if the renter/owner and the user are the same person.
        /// Sets the value of IsFree.
        /// </summary>
        /// <param name="dbContext"></param>
        public void SyncParkingSpotUsage()
        {
            foreach (var spot in context.UsersToParkingSpot)
            {
                if (spot.EndingRentDate > DateTime.Now)
                {
                    var placeWithNoOwner = context.ParkingSpot.FirstOrDefault(x => x.RenterId == spot.UserId);
                    //var placeWithOwner = context.ParkingSpot.FirstOrDefault(x => x.OwnerId == spot.UserId);

                    if (placeWithNoOwner != null)
                    {
                        placeWithNoOwner.IsFree = 1;
                        context.Users.Find(placeWithNoOwner.RenterId).ParkingSpotNumber = null;
                        placeWithNoOwner.RenterId = null;
                        context.Remove(spot);
                        context.SaveChanges();
                    }
                    //if (placeWithOwner != null)
                    //{
                    //    placeWithOwner.IsFree = 0;
                    //    spot.UserId = placeWithOwner.OwnerId;
                    //    context.Remove(spot);
                    //    context.SaveChanges();
                    //}
                }
            }

        }

        public void CalculateTotalDaysInParkForOwners(DateTime date)
        {
            foreach (var spot in context.ParkingSpot)
            {
                if (date > DateTime.Now)
                {
                    if (spot.OwnerId != null && spot.RenterId == null)
                    {

                        context.Users.FirstOrDefault(x => x.Id == spot.OwnerId).TotalDaysInPark += 1;
                        context.SaveChanges();
                    }
                }

            }
        }
        /// <summary>
        /// This method checks if the current user owns a parking space
        /// </summary>
        /// <param name="user"></param>
        /// <returns> Returns "true" if the user has ParkingSpotNumber </returns>
        /// <returns> Returns "false" if the ParkingSpotNumber is null </returns>
        public bool UserHasPArkingSpot(Users user)
        {
            if (user.ParkingSpotNumber != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This method calculates the user's bill. 
        /// Uses the number of days the user has used the parking space and the price per month divided by 30
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pricePerMonth"></param>
        /// <returns>Returns the total price that user has to pay for this month</returns>
        public double CalculateRent(Users user)
        {
            if (userService.CheckIfExists(user))
            {
                if (user.TotalDaysInPark == null)
                {
                    return 0;
                }
                double totalDays = (double)user.TotalDaysInPark;
                return (totalDays / 30) * pricePerMonth;
            }
            return 0;
        }

        /// <summary>
        /// Shows all parking spots, sorted by availability (for the user)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ParkingSpot> getallfreespots()
        {
            var innerJoinQuery =
            from ps in context.ParkingSpot
                //join us in context.Users on ps.OwnerId equals us.Id //into names
                //from us in names.DefaultIfEmpty()
            select new ParkingSpot { Id = ps.Id, OwnerId = ps.OwnerId, ParkingSpotNumber = ps.ParkingSpotNumber, RenterId = ps.RenterId, IsFree = ps.IsFree, UsersToParkingSpot = ps.UsersToParkingSpot };

            return innerJoinQuery.ToList();
        }
    }
}
