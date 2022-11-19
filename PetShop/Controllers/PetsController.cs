using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using PetShop.Interface;
using PetShop.Models;
using PetShop.Models.ViewModel;
using PetShop.Services;

namespace PetShop.Controllers
{

    public class PetsController : Controller
    {
        private ApplicationDbContext db;
        private IPetService petService;

        public PetsController() { }

        public PetsController(ApplicationDbContext context, IPetService service)
        {
            this.db = context;
            this.petService = service;
        }


        /**
         * params: dateOfBirth
         * return calculateAge
         * calculates the age from the date of birth
         */
        private int CalculateAge(DateTime dateOfBirth)
        {
            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
                calculatedAge--;
            return calculatedAge;
        }

        public ActionResult GetPetsByBreed(string breed)
        {
            /**
             * accepts string breed as an argument and 
             * return the partial view along with the list pets filtered by breed
            */
            //var petService = new PetService();
            return PartialView("_PetListPartial", petService.GetPetsByBreed(breed));
        }

        // GET: Pets
        [AllowAnonymous]
        public ActionResult Index()
        {
            //return View(db.Pets.Where(p => p.Owner == null).ToList());

            List<string> model = db.Pets.Select(pet => pet.Breed).Distinct().ToList();
            return View(model);
        }

        // GET: Pets/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // GET: Pets/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var model = new CreatePetViewModel
            {
                DropDownBreed = db.Pets.
                                  Select(pet => new SelectListItem { Text = pet.Breed, Value = pet.Breed }).Distinct().ToList()
            };
            return View(model);
        }

        // POST: Pets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,Name,Breed,Age,IsMale,IsFixed")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Pets.Add(pet);
                db.SaveChanges();
                return Json(new {text = "Success"},JsonRequestBehavior.AllowGet);
            }

            return Json(new { text = "Failed" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Pets/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            var model = new EditPetViewModel();
            model.DropDownBreed = db.Pets.
                                Select(p => new SelectListItem { Text = p.Breed, Value = p.Breed })
                                              .Distinct().ToList();
            model.Id = pet.Id;
            model.Name = pet.Name;
            model.Breed = pet.Breed;
            model.Age = pet.Age;
            model.IsMale = pet.IsMale;
            model.IsFixed = pet.IsFixed;
            return View(model);
        }

        // POST: Pets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,Name,Breed,Age,IsMale,IsFixed")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pet).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { text = "Success" }, JsonRequestBehavior.AllowGet); 
            }
            return Json(new { text = "Failed" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Pets/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = db.Pets.Find(id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // POST: Pets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Pet pet = db.Pets.Find(id);
            db.Pets.Remove(pet);
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

        //creating MyPets Action Controller

        //GET: Pets/MyPets
        [Authorize]
        public ActionResult MyPets()
        {
            return View(db.Pets.Where(p => p.Owner.UserName == User.Identity.Name).ToList());
        }

        //creating Adop ActionController
        //Get: Pets/Adop
        [Authorize]
        public ActionResult Adopt(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var pet = db.Pets.Find(id);
            if (pet == null)
                return HttpNotFound();
            return View(pet);
        }

        //POST:Pets/

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Adopt(Pet pet)
        {
            var user = db.Users.Include(p => p.Pets)
               .Where(x => x.UserName == User.Identity.Name)
               .First();
            var petData = db.Pets.Find(pet.Id);

            var minAge = 18;
            var claimUser = HttpContext.User as ClaimsPrincipal;

            var dateOfBirth = Convert.ToDateTime(claimUser.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);
            if (CalculateAge(dateOfBirth) < minAge)
            {
                ViewBag.message = "You Cannot Adopt the Pet - Your Age is below Permitted One.";
                return View();
            }
            else
            {
                pet.Owner = user;
                user.Pets.Add(petData);
                db.SaveChanges();
                return RedirectToAction("MyPets");
            }
        }
    }
}
