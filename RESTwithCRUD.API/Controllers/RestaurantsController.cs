using Microsoft.AspNetCore.Mvc;
using RESTwithCRUD.API.Models;
using RESTwithCRUD.API.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RESTwithCRUD.API.Controllers
{
    [ApiController]
    public class RestaurantsController : ControllerBase
    {

        private IRestaurantRepository _restaurantsRepo;

        public RestaurantsController(IRestaurantRepository repository)
        {
            _restaurantsRepo = repository;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetRestaurants()
        {
            Thread.Sleep(5000);
            return Ok(await _restaurantsRepo.GetRestaurantsAsync());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetRestaurant(Guid id)
        {
            var restaurant = await _restaurantsRepo.GetRestaurantAsync(id);
            Thread.Sleep(5000);
            if (restaurant != null)
            {
                return Ok(restaurant);
            }

            return NotFound($"There are no restaurants with ID - {id}");
        }


        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddRestaurant(Restaurant restaurant)
        {

            _restaurantsRepo.AddRestaurant(restaurant);
            await _restaurantsRepo.SaveChangesAsync();

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + restaurant.Id, restaurant);

        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {

            var restaurant = await _restaurantsRepo.GetRestaurantAsync(id);
            if (restaurant != null)
            {
                _restaurantsRepo.DeleteRestaurant(restaurant);
                return Ok(await _restaurantsRepo.SaveChangesAsync());
            }

            return NotFound($"There are no restaurants with ID - {id}");


        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> EditRestaurant(Guid id, Restaurant restaurant)
        {
            var existingRestaurant = await _restaurantsRepo.GetRestaurantAsync(id);
            if (existingRestaurant != null)
            {
                restaurant.Id = existingRestaurant.Id;
                await _restaurantsRepo.EditRestaurant(restaurant);
                return Ok(restaurant);
            }

            return NotFound($"There are no restaurants with ID - {id}");
        }



    }
}
