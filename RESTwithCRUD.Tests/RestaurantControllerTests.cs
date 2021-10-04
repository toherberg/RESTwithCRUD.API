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
            //arrange
            _restRepository.Setup(a => a.GetRestaurantsAsync())
                .ReturnsAsync(_restaurants);

            //act
            _testController = new RestaurantsController(_restRepository.Object);
            var result = _testController.GetRestaurants().Result;

            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }


        [Test]
        public void TestGetRestaurantReturnsOkResultAfterSuccesfulOperation()
        {
            //arrange
            Guid idToTest = _restaurants.ElementAt(3).Id;
            _restRepository.Setup(a => a.GetRestaurantAsync(idToTest))
                .ReturnsAsync(_restaurants.FirstOrDefault(r => r.Id == idToTest));

            //act
            _testController = new RestaurantsController(_restRepository.Object);
            var result = _testController.GetRestaurant(idToTest).Result;

            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }



        [Test]
        public void TestGetRestaurantReturnsNotFoundIfNoSuchID()
        {
            //arrange
            Guid idToTest = Guid.NewGuid();
            _restRepository.Setup(a => a.GetRestaurantAsync(idToTest))
                .ReturnsAsync(_restaurants.FirstOrDefault(r => r.Id == idToTest));

            //act
            _testController = new RestaurantsController(_restRepository.Object);
            var result = _testController.GetRestaurant(idToTest).Result;

            //assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }


        //+
        [Test]
        public void TestCreateRestaurantReturnsCreatedResult()
        {
            //arrange
            var testRestaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "Hatyna",
                Cuisine = CuisineType.Ukrainian,
                Description = "Ukrainian cusine",
            };

            _restRepository.Setup(a => a.AddRestaurantAsync(testRestaurant))
                .ReturnsAsync(testRestaurant);

            //act
            _testController = new RestaurantsController(_restRepository.Object);
            var result = _testController.AddRestaurant(testRestaurant).Result;

            //assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);

        }


        [Test]
        public void TestCreateRestaurantReturnsStatusCode201IfCreated()
        {
            //arrange
            var testRestaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "Hatyna",
                Cuisine = CuisineType.Ukrainian,
                Description = "Ukrainian cusine",
            };
            _restRepository.Setup(a => a.AddRestaurantAsync(testRestaurant))
                .ReturnsAsync(testRestaurant);

            //act
            _testController = new RestaurantsController(_restRepository.Object);
            var result = _testController.AddRestaurant(testRestaurant).Result as CreatedAtActionResult;

            //assert
            Assert.IsTrue(result.StatusCode == 201);

        }


        [Test]
        public void TestDeleteRestaurantReturns200IfSucced()
        {
            //arrange
            var testRestaurant = _restaurants.ElementAt(3);
            _restRepository.Setup(a => a.GetRestaurantAsync(testRestaurant.Id)).ReturnsAsync(_restaurants.FirstOrDefault(r => r.Id == testRestaurant.Id));

            //act
            _testController = new RestaurantsController(_restRepository.Object);
            var result = _testController.DeleteRestaurant(testRestaurant.Id).Result as OkObjectResult;

            //assert
            Assert.IsTrue(result.StatusCode == 200);

        }


        [Test]
        public void TestDeleteRestaurantReturnsNotFoundIfWrongID()
        {
            //arrange
            var testRestaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "testName"
            };
            _restRepository.Setup(a => a.GetRestaurantAsync(testRestaurant.Id)).ReturnsAsync(_restaurants.FirstOrDefault(r => r.Id == testRestaurant.Id));

            //act
            _testController = new RestaurantsController(_restRepository.Object);
            var result = _testController.DeleteRestaurant(testRestaurant.Id).Result;

            //assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
        }


        [Test]
        public void TestEditRestaurantReturns200IfPatched()
        {
            //arrange
            var testRestaurant = new Restaurant
            {
                Id = _restaurants.ElementAt(3).Id,
                Name = "Hatyna",
                Cuisine = CuisineType.Ukrainian,
                Description = "Ukrainian cusine",
            };

            _restRepository.Setup(a => a.GetRestaurantAsync(testRestaurant.Id)).ReturnsAsync(_restaurants.FirstOrDefault(r => r.Id == testRestaurant.Id));
            _restRepository.Setup(a => a.EditRestaurant(testRestaurant)).ReturnsAsync(testRestaurant);

            //act
            _testController = new RestaurantsController(_restRepository.Object);
            var result = _testController.EditRestaurant(testRestaurant.Id, testRestaurant).Result;

            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }


        [Test]
        public void TestEditRestaurantReturnsNotFoundIfNoSuchID()
        {
            //arrange
            var testRestaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = "Hatyna",
                Cuisine = CuisineType.Ukrainian,
                Description = "Ukrainian cusine",
            };

            _restRepository.Setup(a => a.GetRestaurantAsync(testRestaurant.Id))
                .ReturnsAsync(_restaurants.FirstOrDefault(r => r.Id == testRestaurant.Id));

            _restRepository.Setup(a => a.EditRestaurant(testRestaurant))
                .ReturnsAsync(testRestaurant);

            //act
            _testController = new RestaurantsController(_restRepository.Object);
            var result = _testController.EditRestaurant(testRestaurant.Id, testRestaurant).Result;

            //assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);

        }







    }
}