using PetShop.Models;
using System;

namespace PetShop.Services
{
    public class PetService
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


    }
}