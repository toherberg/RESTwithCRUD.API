using RESTwithCRUD.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTwithCRUD.API.Services
{
    public interface IRestaurantRepository
    {
        Task<IEnumerable<Restaurant>> GetRestaurantsAsync();
        Task<Restaurant> GetRestaurantAsync(Guid id);
        Task<Restaurant> AddRestaurantAsync(Restaurant newRestaurant);
        void DeleteRestaurant(Restaurant restaurant);

        Task<Restaurant> EditRestaurant(Restaurant restaurant);

        public Task<bool> SaveChangesAsync();


    }
}
