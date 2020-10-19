using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RentARide.Models;

namespace RentARide.Controllers
{
    public class BookingsController : Controller
    {
        string messageView = null;
        private Rent_Entities db = new Rent_Entities();

        // GET: Bookings
       [Authorize]
        public ActionResult Index()
        {
            ViewBag.message = messageView;
            var bookings = db.Bookings.Include(b => b.Car).Include(b => b.Branch).Include(b => b.Branch1);
           
                return View(bookings.ToList());
            
        }

        [Authorize]
        public ActionResult BookingProspective(Booking booking)
        {
            if (booking == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.DropBranch = new SelectList(db.Branches, "BranchId", "Name");
            ViewBag.PickUpBranch = new SelectList(db.Branches, "BranchId", "Name");
            booking.Car = db.Cars.Find(booking.CarId);
            booking.UserId = User.Identity.GetUserId();
            booking.CarId = db.Bookings.Max(m => m.BookingId);
            booking.BookingId += 1;
            //db.Bookings.Add(booking);
            return View(booking);
        }

        // GET: Bookings/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "CarId");
            ViewBag.DropBranch = new SelectList(db.Branches, "BranchId", "BranchId");
            ViewBag.PickupBranch = new SelectList(db.Branches, "BranchId", "BranchId");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "StartDate,EndDate,Price,Status,CarId,PickupBranch,DropBranch,Comment,Rating")] Booking booking)
        {
            booking.BookingId = db.Bookings.Max(model => model.BookingId);
            booking.BookingId += 1;
            booking.UserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                List<Booking> bookingList = (db.Bookings.Include(b => b.Car).Include(b => b.Branch).Include(b => b.Branch1)).ToList();
                foreach (Booking b in bookingList)
                {
                    if (b.CarId.Equals(booking.CarId))
                    {
                        if ((booking.EndDate < b.EndDate && booking.EndDate < b.StartDate) || 
                            (booking.StartDate > b.EndDate && booking.StartDate > b.StartDate))
                        {

                        }
                        else
                        {
                            ViewBag.Message = "Car has been booked for the dates chosen";
                            return RedirectToAction("Index");
                        }
                    }
                }
                ViewBag.message = "Car has benn booked for the dates chosen";
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "CarId", "CarId", booking.CarId);
            ViewBag.DropBranch = new SelectList(db.Branches, "BranchId", "BranchId", booking.DropBranch);
            ViewBag.PickupBranch = new SelectList(db.Branches, "BranchId", "BranchId", booking.PickupBranch);
            return View(booking);
        }

        [Authorize]
        public ActionResult Create1() {
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "CarId");
            ViewBag.DropBranch = new SelectList(db.Branches, "BranchId", "BranchId");
            ViewBag.PickupBranch = new SelectList(db.Branches, "BranchId", "BranchId");

            return View();
        }

        [Authorize]
        public ActionResult Create2([Bind(Include = "StartDate,EndDate,Price,Status,PickupBranch,DropBranch,Comment,Rating")] Booking booking)
        {
            booking.BookingId = db.Bookings.Max(model => model.BookingId);
            booking.BookingId += 1;
            booking.UserId = User.Identity.GetUserId();
            booking.CarId = ClassStatic.carId;
            booking.Car = db.Cars.Find(booking.CarId);
   
            
                List<Booking> bookingList = (db.Bookings.Include(b => b.Car).Include(b => b.Branch).Include(b => b.Branch1)).ToList();
                foreach (Booking b in bookingList)
                {
                    if (b.CarId.Equals(booking.CarId))
                    {
                        if ((booking.EndDate < b.EndDate && booking.EndDate < b.StartDate) ||
                            (booking.StartDate > b.EndDate && booking.StartDate > b.StartDate))
                        {

                        }
                        else
                        {
                            ViewBag.Message = "Car has been booked for the dates chosen";
                            return RedirectToAction("Index");
                        }
                    }
                }
                ViewBag.message = "Car has benn booked for the dates chosen";
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            

            ViewBag.CarId = new SelectList(db.Cars, "CarId", "CarId", booking.CarId);
            ViewBag.DropBranch = new SelectList(db.Branches, "BranchId", "BranchId", booking.DropBranch);
            ViewBag.PickupBranch = new SelectList(db.Branches, "BranchId", "BranchId", booking.PickupBranch);
            return View(booking);
        }

        [Authorize] // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model", booking.CarId);
            ViewBag.DropBranch = new SelectList(db.Branches, "BranchId", "Name", booking.DropBranch);
            ViewBag.PickupBranch = new SelectList(db.Branches, "BranchId", "Name", booking.PickupBranch);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,UserId,StartDate,EndDate,Price,Status,CarId,PickupBranch,DropBranch,Comment,Rating")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model", booking.CarId);
            ViewBag.DropBranch = new SelectList(db.Branches, "BranchId", "Name", booking.DropBranch);
            ViewBag.PickupBranch = new SelectList(db.Branches, "BranchId", "Name", booking.PickupBranch);
            return View(booking);
        }

        [Authorize]// GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Make(Booking booking)
        {
            int recentId = db.Bookings.Max(m => m.BookingId);
            db.Bookings.Find(recentId).Branch = booking.Branch;
            db.Bookings.Find(recentId).Branch1 = booking.Branch1;
            db.Bookings.Find(recentId).PickupBranch = booking.PickupBranch;
            db.Bookings.Find(recentId).DropBranch = booking.DropBranch;
            db.Bookings.Find(recentId).StartDate = booking.StartDate;
            db.Bookings.Find(recentId).EndDate = booking.EndDate;
            db.Bookings.Find(recentId).Status = booking.Status;
            booking.Car = db.Cars.Find(booking.CarId);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View("Details", booking);
        }
        [Authorize]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
