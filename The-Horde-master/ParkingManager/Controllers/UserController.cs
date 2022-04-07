using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkingManager.Models;
using ParkingManager.Models.viewModels;
using ParkingManager.Services;
using Microsoft.AspNetCore.Http;

namespace ParkingManager.Controllers
{
    public class UserController : Controller
    {
        public ParkingManagerDatabaseContext _context;
        public ParkingManagerSevice parkingManagerService;
        public UserService userService;

        public UserController(ParkingManagerDatabaseContext _context)
        {
            this._context = _context;
            this.parkingManagerService = new ParkingManagerSevice(_context);
            this.userService = new UserService(_context);
        }

        /// <summary>
        /// Renders the MyInfo view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MyInfo()
        {
            return View();
        }
        /// <summary>
        /// Takes the data from the MyInfo view and creates a new user with it. 
        /// Sets the current session's data to that of the new user's
        /// </summary>
        /// <param name="myinfo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> MyInfo(MyInfoViewModel myinfo)
        {
            Users addedUser = userService.AddUser(myinfo);

            HttpContext.Session.SetString("firstName", addedUser.FirstName);
            HttpContext.Session.SetString("lastName", addedUser.LastName);
            HttpContext.Session.SetString("phoneNumber", addedUser.PhoneNumber);
            HttpContext.Session.SetString("Id", addedUser.Id.ToString());
            
            //parkingManagerService.SyncParkingSpotUsage();

           // parkingManagerService.SyncParkingSpotUsage();
            //HttpContext.Session.SetString("totalDaysInPark", myinfo.totalDaysInPark);
            //HttpContext.Session.SetString("parkingSpotNumber", myinfo.parkingSpotNumber);
            //HttpContext.Session.SetString("Id", myinfo.Id);

            return RedirectToAction("Spots", "ParkingSpot");
        }

        /// <summary>
        /// Renders the MyBill view
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task <IActionResult> MyBill(int? id)
        {
            id = int.Parse(HttpContext.Session.GetString("Id"));
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            
            if (user == null)
            {
                return NotFound();
            }

            MyBillViewModel viewModel = new MyBillViewModel();
            viewModel.totalDaysInPark = user.TotalDaysInPark;
            viewModel.Price = parkingManagerService.CalculateRent(user);


            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> MyBill()
        {
            return View();
        }

        /// <summary>
        /// Renders the AdminPanelUsers view
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> AdminPanelUsers()
        {
            return View(await _context.Users.ToListAsync());
        }

        /// <summary>
        /// CRUD operation for deleting a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        /// <summary>
        /// CRUD operation for deleting a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminPanelUsers));
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

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        /// <summary>
        /// CRUD operation for editing a parking spot
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,PhoneNumber,ParkingSpotNumber")] Users user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingSpotsExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminPanelUsers));
            }
            return View(user);
        }
    }
}
