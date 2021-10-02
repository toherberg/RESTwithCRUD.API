using NUnit.Framework;
using RESTwithCRUD.API.Models;
using RESTwithCRUD.API.Services;
using System;

namespace RESTwithCRUD.Tests
{
    public class RestaurantTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestRestaurantToDTO()
        {
            var restaurant = new Restaurant();
            var restaurantDTO = ConverterService.RestaurantToDTO(restaurant);

            Assert.IsTrue(restaurantDTO.GetType().GetProperties().Length == 3);
        }


        [Test]
        public void TestRestaurantDescriptionCanBeAddedToModel()
        {
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "rest1",
                Cuisine = CuisineType.Indian,
                Description = "topsecret"
            };

            var expected = "topsecret";

            Assert.IsTrue(restaurant.Description.Equals(expected));
        }


        [Test]
        public void TestEmptyCuisineSetToNone()
        {
            var restaurant = new Restaurant
            {
                Name = "Rest1"
            };

            var expected = CuisineType.None;

            Assert.IsTrue(restaurant.Cuisine.Equals(expected));
        }


        [Test]
        public void TestCuisineSetToExpected()
        {
            var restaurant = new Restaurant
            {
                Name = "Rest1",
                Cuisine = CuisineType.Italian
            };

            Assert.IsTrue((int)restaurant.Cuisine == 1);
        }





    }
}