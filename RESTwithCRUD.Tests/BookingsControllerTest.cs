using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RESTwithCRUD.API.Controllers;
using RESTwithCRUD.API.Models;
using RESTwithCRUD.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RESTwithCRUD.Tests
{
    class BookingsControllerTest
    {

        private Mock<IBookingService> _bookingService;
        private IEnumerable<Booking> _bookings;
        private BookingsController _testController;

        [SetUp]
        public void Setup()
        {
            _bookingService = new Mock<IBookingService>();
            var testRestaurantToBook = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "MyRestaurant"
            };
            _bookings = new List<Booking>()
            {
                new Booking { Id = Guid.NewGuid(), CallerName = "John", CallerNumber = "0500503525", GuestsQuantity = 1, RestaurantId = testRestaurantToBook.Id },
                new Booking { Id = Guid.NewGuid(), CallerName = "McDonald", CallerNumber = "4563253543", GuestsQuantity = 5, RestaurantId = testRestaurantToBook.Id },
                new Booking { Id = Guid.NewGuid(), CallerName = "Alfredo", CallerNumber = "3453451617", GuestsQuantity = 3, RestaurantId = testRestaurantToBook.Id },
            };
        }

        [Test]
        public void TestGetBookingsReturnsOkResult()
        {
            //arrange
            _bookingService.Setup(a => a.GetBookings()).ReturnsAsync(_bookings);

            //act
            _testController = new BookingsController(_bookingService.Object);
            var result = _testController.GetBookings().Result;

            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }


        [Test]
        public void TestGetBookingsReturnsAllBookingsDTOs()
        {
            //arrange
            _bookingService.Setup(a => a.GetBookings()).ReturnsAsync(_bookings);

            //act
            _testController = new BookingsController(_bookingService.Object);
            var testActionResult = _testController.GetBookings().Result as OkObjectResult;
            var result = testActionResult.Value as IEnumerable<BookingDTO>;
            //assert
            Assert.IsTrue(result.Count() == 3);
        }

        [Test]
        public void TestPushBookingStatusCode201IfSuceed()
        {
            //arrange
            var testBooking = new Booking
            {
                Id = Guid.NewGuid(),
                CallerName = "Giovanni",
                CallerNumber = "45172517",
                GuestsQuantity = 5,
                RestaurantId = _bookings.FirstOrDefault().RestaurantId
            };
            _bookingService.Setup(a => a.PushBookingOrder(testBooking)).ReturnsAsync(testBooking);

            //act
            _testController = new BookingsController(_bookingService.Object);
            var result = _testController.PushBooking(testBooking).Result;

            //assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public void TestDeleteBookingFromSystemOkResult()
        {
            //arrange
            var idToTest = _bookings.FirstOrDefault().Id;
            _bookingService.Setup(a => a.CancelBookingOrder(idToTest)).ReturnsAsync(true);

            //act
            _testController = new BookingsController(_bookingService.Object);
            var result = _testController.DeleteBookingFromSystem(idToTest).Result;

            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);

        }

        [Test]
        public void TestDeleteBookingFromSystemNotFound()
        {
            //arrange
            var idToTest = Guid.NewGuid();
            _bookingService.Setup(a => a.CancelBookingOrder(idToTest)).ReturnsAsync(false);

            //act
            _testController = new BookingsController(_bookingService.Object);
            var result = _testController.DeleteBookingFromSystem(idToTest).Result;

            //assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }


    }
}
