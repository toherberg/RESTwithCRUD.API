using NUnit.Framework;
using RESTwithCRUD.API.Models;
using RESTwithCRUD.API.Services;
using System;

namespace RESTwithCRUD.Tests
{
    class ConverterServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConverterServiceChangesRestaurantToDTO()
        {
            //arrange
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "newRest",
                Cuisine = CuisineType.French,
                Description = "info"
            };

            //act
            var restaurantDTO = ConverterService.RestaurantToDTO(restaurant);

            //assert
            Assert.IsTrue(restaurantDTO.GetType().GetProperties().Length == 3);
        }

        [Test]
        public void TestDTOFieldsDataSimilarToRestaurantFullModel()
        {
            //arrange
            var restaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "restikNew",
                Description = "new restaurant",
                Cuisine = CuisineType.Ukrainian
            };

            //act
            var restaurantDTO = ConverterService.RestaurantToDTO(restaurant);

            //assert
            Assert.IsTrue(restaurant.Id == restaurantDTO.Id
                && restaurant.Name == restaurantDTO.Name
                && restaurant.Cuisine == restaurantDTO.Cuisine);
        }


        [Test]
        public void TestConverterServiceChangesBookingToDTO()
        {
            //arrange
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                CallerName = "Tester",
            };

            //act
            var bookingDTO = ConverterService.BookingToDTO(booking);

            //assert
            Assert.IsTrue(bookingDTO.GetType().GetProperties().Length == 5);
        }


        [Test]
        public void TestDTOFieldsDataSimilarToBookingFullModel()
        {
            //arrange
            var booking = new Booking
            {
                Id = Guid.NewGuid(),
                CallerName = "Oleksandr",
                CallerNumber = "0939471025",
                GuestsQuantity = 5,
                RestaurantId = Guid.NewGuid()
            };

            //act
            var bookingDTO = ConverterService.BookingToDTO(booking);

            //assert
            Assert.IsTrue(booking.Id == bookingDTO.Id
                && booking.RestaurantId == bookingDTO.RestaurantId
                && booking.CallerName == bookingDTO.CallerName
                && booking.CallerNumber == bookingDTO.CallerNumber
                && booking.GuestsQuantity == bookingDTO.GuestsQuantity);
        }




    }
}
