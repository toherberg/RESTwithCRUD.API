using NUnit.Framework;


namespace RESTwithCRUD.API.Tests
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
            var restaurantDTO = restaurant.RestaurantToDTO();

            Assert.IsTrue(restaurantDTO.GetType().GetProperties().Length == 3);
        }


        [Test]
        public void TestRestaurantSecretInfoCanBeAddedToModel()
        {
            var restaurant = new Restaurant
            {
                Id = 1,
                Name = "Rest1",
                Cuisine = CuisineType.Indian,
                SecretInfo = "TopSecret"
            };

            var expected = "TopSecret";

            Assert.IsTrue(restaurant.SecretInfo.Equals(expected));
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