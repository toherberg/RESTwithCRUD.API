using Microsoft.AspNetCore.Http;
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

        private readonly IRestaurantRepository _restaurantsRepo;

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


        /// <summary>
        /// Creates a Restaurant.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/restaurants
        ///     {
        ///        "name": "Restaurant1",
        ///        "cuisine": 1,
        ///        "description": "this is new restaurant
        ///     }
        ///
        /// </remarks>
        /// <param name="restaurant"></param>
        /// <returns>A newly created restaurant</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [Route("api/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRestaurant(Restaurant restaurant)
        {

            await _restaurantsRepo.AddRestaurantAsync(restaurant);

            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + restaurant.Id, restaurant);

        }


        /// <summary>
        /// Deletes a specific Restaurant from DB.
        /// </summary>
        /// <param name="id"></param>    
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteRestaurant(Guid id)
        {

            var existingRestaurant = await _restaurantsRepo.GetRestaurantAsync(id);
            if (existingRestaurant != null)
            {
                _restaurantsRepo.DeleteRestaurant(existingRestaurant);
                return Ok(existingRestaurant);
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
                existingRestaurant.Name = restaurant.Name;
                existingRestaurant.Description = restaurant.Description;
                existingRestaurant.Cuisine = restaurant.Cuisine;
                await _restaurantsRepo.EditRestaurant(existingRestaurant);
                return Ok("Successfully changed");
            }

            return NotFound($"There are no restaurants with ID - {id}");
        }



    }
}
