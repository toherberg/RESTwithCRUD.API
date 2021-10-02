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
    public class RestaurantControllerTests
    {
        private Mock<IRestaurantRepository> _restRepository;
        private IEnumerable<Restaurant> _restaurants;
        private RestaurantsController _testController;


        [SetUp]
        public void Setup()
        {
            _restRepository = new Mock<IRestaurantRepository>();
            _restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = Guid.NewGuid(), Name = "Pizza HATA", Cuisine = CuisineType.Italian,
                    Description = "First restaurant of italian cuisine in my street"},
                new Restaurant { Id = Guid.NewGuid(),Name = "Croissants", Cuisine = CuisineType.French,
                    Description = "Fresh croissants with glass of wine every morning"},
                new Restaurant { Id = Guid.NewGuid(),Name = "Pera i Peysy", Cuisine = CuisineType.Indian,
                    Description = "Enjoy fresh exotic indian food in this amazing restaurants"},
                new Restaurant { Id = Guid.NewGuid(),Name = "Borsch i Galushky", Cuisine = CuisineType.Ukrainian,
                    Description = "Authentic ukrainian dishes made by ukrainians"},
                new Restaurant { Id = Guid.NewGuid(),Name = "Bo Fo Food", Cuisine = CuisineType.Asian,
                    Description = "Chinese dishes - tom yam and fried dogs"}

            };
        }

        [Test]
        public void TestGetRestaurantsReturns200StatusCodeIfSuccessfullAsync()
        {
            _restRepository.Setup(a => a.GetRestaurantsAsync()).ReturnsAsync(_restaurants);
            _testController = new RestaurantsController(_restRepository.Object);
            Assert.IsInstanceOf<OkObjectResult>(_testController.GetRestaurants().Result);

        }


        [Test]
        public void TestGetRestaurantReturnsOkResultAfterSuccesfulOperation()
        {
            Guid idToTest = _restaurants.ElementAt(3).Id;
            _restRepository.Setup(a => a.GetRestaurantAsync(idToTest)).ReturnsAsync(_restaurants.FirstOrDefault(r => r.Id == idToTest));
            _testController = new RestaurantsController(_restRepository.Object);
            Assert.IsInstanceOf<OkObjectResult>(_testController.GetRestaurant(idToTest).Result);
        }



        [Test]
        public void TestGetRestaurantReturnsNotFoundIfNoSuchID()
        {
            Guid idToTest = Guid.NewGuid();
            _restRepository.Setup(a => a.GetRestaurantAsync(idToTest)).ReturnsAsync(_restaurants.FirstOrDefault(r => r.Id == idToTest));
            _testController = new RestaurantsController(_restRepository.Object);
            Assert.IsInstanceOf<NotFoundObjectResult>(_testController.GetRestaurant(idToTest).Result);
        }


        ////not finished
        //[Test]
        //public void TestCreateRestaurantReturns201IfCreated()
        //{
        //    var testRestaurant = new Restaurant
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Hatyna",
        //        Cuisine = CuisineType.Ukrainian,
        //        Description = "Ukrainian cusine"
        //    };

        //    _restRepository.Setup(a => a.AddRestaurantAsync(testRestaurant)).ReturnsAsync(testRestaurant);
        //    _testController = new RestaurantsController(_restRepository.Object);
        //    Assert.Fail();


        //}


        //[Test]
        //public void TestCreateRestaurantReturns400IfPostEmpty()
        //{
        //    Assert.Fail();

        //}


        //[Test]
        //public void TestDeleteRestaurantReturns200IfSucced()
        //{
        //    Assert.Fail();

        //}


        //[Test]
        //public void TestDeleteRestaurantReturnsNotFoundIfWrongID()
        //{
        //    Assert.Fail();

        //}


        //[Test]
        //public void TestEditRestaurantReturns200IfPatched()
        //{
        //    Assert.Fail();

        //}


        //[Test]
        //public void TestEditRestaurantReturnsNotFoundIfNoSuchID()
        //{
        //    Assert.Fail();

        //}







    }
}