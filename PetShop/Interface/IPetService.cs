using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Interface
{
    public interface IPetService
    {
        IEnumerable<Pet> GetAllPets();
        Pet GetPetById(int id);
        IEnumerable<Pet> GetPetsByBreed(string breed);
        bool OldEnoughToAdop(DateTime dob);
    }
}
