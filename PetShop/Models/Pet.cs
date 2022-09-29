using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Models
{
    public class Pet
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Breed { get; set; }
        public virtual bool IsMale { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}