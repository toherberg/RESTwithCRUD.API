using RESTwithCRUD.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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


        //get list of restaurant from "db"
        public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync()
        {
            return await Task.Run(() => restaurants.FindAll(b => true));
        }


        //get restaurant by guid
        public async Task<Restaurant> GetRestaurantAsync(Guid id)
        {
            return await Task.Run(() => restaurants.SingleOrDefault(b => b.Id == id));
        }




        public async Task<Restaurant> AddRestaurantAsync(Restaurant newRestaurant)
        {
            newRestaurant.Id = Guid.NewGuid();
            await Task.Run(() => restaurants.Add(newRestaurant));
            return newRestaurant;
        }





        public void DeleteRestaurant(Restaurant restaurant)
        {
            restaurants.Remove(restaurant);
        }

        public async Task<Restaurant> EditRestaurant(Restaurant restaurant)
        {
            var existingRestaurant = await GetRestaurantAsync(restaurant.Id);
            existingRestaurant.Name = restaurant.Name;
            existingRestaurant.Cuisine = restaurant.Cuisine;
            existingRestaurant.Description = restaurant.Description;
            return restaurant;
        }


    }
}
