using Microsoft.EntityFrameworkCore;
using RESTwithCRUD.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTwithCRUD.API.Services
{
    public class SqlRestaurantRepository : IRestaurantRepository
    {

        private readonly RestaurantContext _restaurantContext;
        public SqlRestaurantRepository(RestaurantContext restaurantContext)
        {
            _restaurantContext = restaurantContext;
        }


        //get all
        public async Task<IEnumerable<Restaurant>> GetRestaurantsAsync()
        {
            return await _restaurantContext.Restaurants.ToListAsync();
        }


        //get specified by id
        public async Task<Restaurant> GetRestaurantAsync(Guid id)
        {
            return await _restaurantContext.Restaurants.FindAsync(id);
        }


        //add restaurant

        public async Task<Restaurant> AddRestaurantAsync(Restaurant newRestaurant)
        {
            newRestaurant.Id = Guid.NewGuid();
            _restaurantContext.Add(newRestaurant);
            await _restaurantContext.SaveChangesAsync();
            return newRestaurant;
        }

        //delete restaurant
        public void DeleteRestaurant(Restaurant restaurant)
        {
            _restaurantContext.Restaurants.Remove(restaurant);
            _restaurantContext.SaveChanges();
        }

        //edit restaurant
        public async Task<Restaurant> EditRestaurant(Restaurant restaurant)
        {
            var existingRestaurant = await _restaurantContext.Restaurants.FindAsync(restaurant.Id);
            if (existingRestaurant != null)
            {
                _restaurantContext.Restaurants.Update(restaurant);
                await _restaurantContext.SaveChangesAsync();
            }

            return restaurant;
        }

    }
}
