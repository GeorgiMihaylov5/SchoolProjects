using ParkingManager.Models;
using ParkingManager.Models.viewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingManager.Services
{
    public class UserService
    {
        private readonly ParkingManagerDatabaseContext context;
        public UserService(ParkingManagerDatabaseContext context)
        {
            this.context = context;
        }




        /// <summary>
        /// Checks if a user already exists in the database. If not, adds it to the database.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        public Users AddUser(MyInfoViewModel user)
        {
            Users newUser = new Users();
            newUser.FirstName = user.firstName;
            newUser.LastName = user.lastName;
            newUser.PhoneNumber = user.phoneNumber;
            if (!CheckIfExists(newUser))
            {
                context.Users.Add(newUser);
                context.SaveChanges();
                return context.Users.FirstOrDefault(x => x.PhoneNumber == newUser.PhoneNumber);
            }
            else
            {
                return context.Users.FirstOrDefault(x => x.PhoneNumber == newUser.PhoneNumber);
            }

        }

        /// <summary>
        /// Checks if a user already exists in the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CheckIfExists(Users user)
        {
            Users userInBase = context.Users.FirstOrDefault(x => x.PhoneNumber == user.PhoneNumber);
            if (userInBase != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletes a user from the database
        /// </summary>
        /// <param name="user"></param>
        public void DeleteUser(Users user)
        {
            var userToDel = this.context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (userToDel == null)
            {
                throw new ArgumentException();
            }
            if (context.ParkingSpot.FirstOrDefault(x => x.RenterId == user.Id).RenterId != null)
            {
                context.ParkingSpot.FirstOrDefault(x => x.RenterId == user.Id).RenterId  = null;
                context.SaveChanges();
            }
            else
            {
                context.ParkingSpot.FirstOrDefault(x => x.RenterId == user.Id).OwnerId = null;
                context.SaveChanges();
            }
             
            context.Users.Remove(userToDel);
            context.SaveChanges();
        }

        //public void SetParkingSpot(int spot, User user)
        //{
        //    context.User.FirstOrDefault(u => u.PhoneNumber == user.PhoneNumber).ParkingSpot = spot;
        //    context.SaveChanges();
        //}
    }
}
