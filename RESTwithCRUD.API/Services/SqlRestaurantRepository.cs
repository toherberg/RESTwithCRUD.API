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





        public async Task<Restaurant> AddRestaurantAsync(Restaurant newRestaurant)
        {
            newRestaurant.Id = Guid.NewGuid();
            _restaurantContext.Add(newRestaurant);
            await _restaurantContext.SaveChangesAsync();
            return newRestaurant;
        }

        public void DeleteRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }

        public Task<Restaurant> EditRestaurant(Restaurant restaurant)
        {
            throw new NotImplementedException();
        }


        public Task<bool> SaveChangesAsync()
        {
            return Task.Run(() => true);
        }
    }
}
