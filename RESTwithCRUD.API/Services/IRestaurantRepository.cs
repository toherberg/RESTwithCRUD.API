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
        void AddRestaurant(Restaurant newRestaurant);

        void DeleteRestaurant(Guid id);

        void EditRestaurant(Restaurant restaurant, Guid id);


    }
}
