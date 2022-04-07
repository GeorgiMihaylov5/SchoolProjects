using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ParkingManager.Models;
using ParkingManager.Models.viewModels;
using ParkingManager.Services;


namespace ParkingManager.Controllers
{
    public class ParkingSpotController : Controller
    {
        private readonly ILogger<ParkingSpotController> _logger;
        private ParkingManagerDatabaseContext _context;
        public ParkingManagerSevice service;
        public ParkingService parkingService;
        
        public List<string> UserData()
        {
            List<string> result = new List<string>();
            result.Add(HttpContext.Session.GetString("firstName"));
            result.Add(HttpContext.Session.GetString("lastName"));
            result.Add(HttpContext.Session.GetString("phoneNumber"));
            result.Add(HttpContext.Session.GetString("Id"));

            return result;
        }
        //public ParkingSpotsController(ILogger<ParkingSpotsController> logger)
        //{
        //    _logger = logger;
        //}

        public ParkingSpotController(ParkingManagerDatabaseContext _context)
        {
            this._context = _context;
            this.service = new ParkingManagerSevice(_context);
            this.parkingService = new ParkingService(_context);
        }


        /// <summary>
        /// Shows all parking spots (for the user)
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Spots()
        {
            return View(service.getallfreespots());
        }

        /// <summary>
        /// Renders the Rent View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Rent()
        {

            return View();
        }

        /// <summary>
        /// Rents a parking spot by calling ParkingManagerService.Rent
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rentStartDate"></param>
        /// <param name="rentEndDate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Rent(int id, DateTime rentStartDate, DateTime rentEndDate)
        {
            //string firstName = HttpContext.Session.GetString("firstName");
            //string lastName = HttpContext.Session.GetString("lastName");
            //string phoneNumber = HttpContext.Session.GetString("phoneNumber");
            //string totalDaysInPark = HttpContext.Session.GetString("totalDaysInPark");
            //string parkingSpotNumber = HttpContext.Session.GetString("parkingSpotNumber");
            //string Id = HttpContext.Session.GetString("Id");

            //HttpContext.Session.GetString("totalDaysInPark");
            //HttpContext.Session.GetString("parkingSpotNumber");
            //HttpContext.Session.GetString("Id");

            var parkingSpot = _context.ParkingSpot.Find(id);
            if (parkingSpot.OwnerId != null)
            {
                var usersToParkingspot = _context.UsersToParkingSpot.FirstOrDefault(x => x.ParkingSpotId == parkingSpot.Id);
                if (usersToParkingspot.EndingRentDate < rentEndDate)
                {
                    return NotFound($"The last day possible for rent is : {usersToParkingspot.EndingRentDate}");
                }

            }
            Users user = new Users();
            user.Id = int.Parse(UserData()[3]);
            user.TotalDaysInPark += (rentEndDate - rentStartDate).TotalDays;

            Users UserInBase = _context.Users.FirstOrDefault(x => x.Id == user.Id);
            var placeNumber = _context.ParkingSpot.FirstOrDefault(x => x.Id == id).ParkingSpotNumber.ToString();
            try
            {
                service.Rent(UserInBase, placeNumber, rentStartDate, rentEndDate);
            }
            catch (ArgumentException)
            {
                return RedirectToAction(nameof(LeaveSpot));
            }
            return RedirectToAction("Spots", "ParkingSpot");
        }

        /// <summary>
        /// Renders the LeaveSpot View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LeaveSpot()
        {
            return View();
        }
        /// <summary>
        ///  This method changes the state of the parking space to free 
        ///  by calling service.LeaveSpot
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rentStartDate"></param>
        /// <param name="rentEndDate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> LeaveSpot(DateTime leaveSpotStartDate, DateTime leaveSpotEndDate)
        {
            //Users user = new Users();
            //user.FirstName = UserData()[0];
            //user.LastName = UserData()[1];
            //user.PhoneNumber = UserData()[2];

            int Id = int.Parse(UserData()[3]);
            Users user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Id);

            service.LeaveSpot(user, user.ParkingSpotNumber, leaveSpotStartDate, leaveSpotEndDate);

            return View();
        }

        /// <summary>
        /// Shows all parking spots (for the admin)
        /// Allows to edit and delete them
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AdminPanelSpots()
        {
            return View(await _context.ParkingSpot.ToListAsync());
        }

        /// <summary>
        /// Calls the Create View of the parking spot
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateSpot()
        {
            return View();
        }
        /// <summary>
        /// Creates a parking spot
        /// </summary>
        /// <param name="parkingSpots"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OwnerId,RenterId,ParkingSpotNumber")] ParkingSpot parkingSpots)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingSpots);
                await _context.SaveChangesAsync();
                parkingService.ChangeUserStatusForSpot("Create", parkingSpots.OwnerId, parkingSpots.ParkingSpotNumber);
                return RedirectToAction(nameof(AdminPanelSpots));
            }
            return View(parkingSpots);
        }

        /// <summary>
        /// CRUD operation for editing a parking spot
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpots = await _context.ParkingSpot.FindAsync(id);
            if (parkingSpots == null)
            {
                return NotFound();
            }
            return View(parkingSpots);
        }

        /// <summary>
        /// CRUD operation for editing a parking spot
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parkingSpots"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerId,RenterId,ParkingSpotNumber,IsFree")] ParkingSpot parkingSpots)
        {
            if (id != parkingSpots.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingSpots);
                    parkingService.ChangeUserStatusForSpot("Edit", parkingSpots.OwnerId, parkingSpots.ParkingSpotNumber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingSpotsExists(parkingSpots.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminPanelSpots));
            }
            return View(parkingSpots);
        }

        /// <summary>
        /// CRUD operation for deleting a parking spot
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingSpots = await _context.ParkingSpot
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingSpots == null)
            {
                return NotFound();
            }

            return View(parkingSpots);
        }
        /// <summary>
        /// CRUD operation for deleting a parking spot
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkingSpots = await _context.ParkingSpot.FindAsync(id);
            parkingService.ChangeUserStatusForSpot("Delete", parkingSpots.OwnerId, parkingSpots.ParkingSpotNumber);
            _context.ParkingSpot.Remove(parkingSpots);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminPanelSpots));
        }

        /// <summary>
        /// Checks if the parking spot exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ParkingSpotsExists(int id)
        {
            return _context.ParkingSpot.Any(e => e.Id == id);
        }
    }
}
