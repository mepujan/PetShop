using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetShop.Attributes
{
    public class NoDigitsAttribute : ValidationAttribute
    {

        public NoDigitsAttribute():base("No Numbers Allowed!!") { }

        public override bool IsValid(object value)
        {
            if(value is null)
            {
                return false;
            }
            var name = (string) value;
            return !name.Any(char.IsDigit);
        }
    }
}