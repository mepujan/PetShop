using PetShop.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetShop.Models.ViewModel
{
    public class EditPetViewModel
    {
        public List<SelectListItem> DropDownBreed { get; set; }
        public virtual int Id { get; set; }

        [NoDigits]
        public virtual string Name { get; set; }
        public virtual string Breed { get; set; }
        [NonNegative]
        public virtual int Age { get; set; }
        public virtual bool IsMale { get; set; }
        public virtual bool IsFixed { get; set; }
    }
}