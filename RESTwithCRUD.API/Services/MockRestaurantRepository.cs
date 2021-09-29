using RESTwithCRUD.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTwithCRUD.API.Services
{
    public class MockRestaurantRepository : IRestaurantRepository
    {

        readonly List<Restaurant> restaurants;

        public MockRestaurantRepository()
        {
            restaurants = new List<Restaurant>()
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

        public void AddRestaurant(Restaurant newRestaurant)
        {
            throw new NotImplementedException();
        }

        public void DeleteRestaurant(Guid id)
        {
            throw new NotImplementedException();
        }

        public void EditRestaurant(Restaurant restaurant, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Restaurant> GetRestaurant(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Restaurant>> GetRestaurantsAsync()
        {
            return await Task.Run(() => restaurants);
        }
    }
}
