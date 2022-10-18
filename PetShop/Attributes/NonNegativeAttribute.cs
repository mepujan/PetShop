using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Attributes
{
    public class NonNegativeAttribute : ValidationAttribute
    {

        public NonNegativeAttribute():base("Number Cannot Be Negative."){}

        public override bool IsValid(object value)
        {
            return (int) value > 0;
        }
    }
}