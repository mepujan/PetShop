using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetShop.Services;
using System;

namespace PetShop.Test
{
    [TestClass]
    public class PetServiceTests
    {
        /**
         * Returns true if the user age is above 18 and can adopt the pet
         */
        [TestMethod]
        public void PetService_IsOldEnoughToAdop_Test()
        {
            DateTime dob = DateTime.Parse("2000-01-01");
            var expectedResult = true;
            var petService = new PetService();
            var result = petService.OldEnoughToAdop(dob);
            Assert.AreEqual(expectedResult, result);
        }

        /**
         * Returns false if the user age is below 18 and cannot adopt the pet
         */

        [TestMethod]
        public void PetService_IsNotOldEnoughToAdop_Test()
        {
            DateTime dob = DateTime.Parse("2015-01-01");
            var expectedResult = false;
            var petService = new PetService();
            var result = petService.OldEnoughToAdop(dob);
            Assert.AreEqual(expectedResult, result);
        }
    }
}
