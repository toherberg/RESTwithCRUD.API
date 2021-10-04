using NUnit.Framework;
using RESTwithCRUD.API.Models;

namespace RESTwithCRUD.Tests
{
    public class RestaurantTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestEmptyCuisineSetToNone()
        {
            //arrange
            var expected = CuisineType.None;

            //act
            var restaurant = new Restaurant
            {
                Name = "Rest1"
            };


            //assert
            Assert.IsTrue(restaurant.Cuisine.Equals(expected));
        }


        [Test]
        public void TestCuisineSetToExpected()
        {
            //act
            var restaurant = new Restaurant
            {
                Name = "Rest1",
                Cuisine = CuisineType.Italian
            };

            //assert
            Assert.IsTrue((int)restaurant.Cuisine == 1);
        }





    }
}