using PetShop.Interface;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PetShop.Services
{
    public class PetService : IPetService
    {
        private ApplicationDbContext _context;
        public PetService() {
            _context = new ApplicationDbContext();
        }



        public PetService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public bool OldEnoughToAdop(DateTime dob)
        {

            int calculatedAge = DateTime.Today.Year - dob.Year;
            if (dob > DateTime.Today.AddYears(-calculatedAge))
                calculatedAge--;
            if (calculatedAge < 18)
            {
                return false;
            }
            return true;
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _context.Pets.ToList();
        }

        public Pet GetPetById(int id)
        {
            return _context.Pets.Where(pet => pet.Id == id).First();
        }

        public IEnumerable<Pet> GetPetsByBreed(string breed)
        {
            return _context.Pets.Where(pet => pet.Breed.Equals(breed)).ToList();
        }

    }
}