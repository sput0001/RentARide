using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RentARide.Models;

namespace RentARide.Controllers
{
    public class CarsController : Controller
    {
        private Rent_Entities db = new Rent_Entities();

        // GET: Cars
        public ActionResult Index()
        {
            return View(db.Cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        public ActionResult Book(Car car)
        {
            Booking booking = new Booking();
            booking.Car = car;
            booking.CarId = car.CarId;
            ClassStatic.carId = car.CarId;
            return RedirectToAction("Create1", "Bookings");
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Car car, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                car.CarId = db.Cars.Max(m => m.CarId);
                car.CarId += 1;
                string serverPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                string fileExtension = Path.GetExtension(imageFile.ContentType);
                List<string> name = imageFile.FileName.Split('\\').ToList();
                string name1 = name.Last();
     
                string filePath = name1 + fileExtension;
                imageFile.SaveAs(serverPath + filePath);
                car.PicPath = filePath;
                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Cars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarId,Model,Manufacturer,Price,Description,Year,PicPath,Status")] Car car)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Car car = db.Cars.Find(id);
            List<Booking> bs = db.Bookings.Where(model => model.CarId == id).ToList();
            foreach  (Booking b in bs)
            {
                db.Bookings.Find(b.BookingId);
                db.Bookings.Remove(b);
                db.SaveChanges();

            }
            db.Cars.Remove(car);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
