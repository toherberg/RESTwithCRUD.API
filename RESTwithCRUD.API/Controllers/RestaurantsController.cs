using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTwithCRUD.API.Models;
using RESTwithCRUD.API.Services;
using System;
using System.Linq;
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


        /// <summary>
        /// Finds and returns all restaurants DTO's from DB
        /// </summary>
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetRestaurants()
        {
            var result = await _restaurantsRepo.GetRestaurantsAsync();
            return Ok(result.Select(r => ConverterService.RestaurantToDTO(r)));
        }

        /// <summary>
        /// Finds and returns all restaurants with full info
        /// </summary>
        [HttpGet]
        [Route("api/[controller]/full")]
        public async Task<IActionResult> GetRestaurantsFull()
        {
            var result = await _restaurantsRepo.GetRestaurantsAsync();
            return Ok(result);
        }


        /// <summary>
        /// Finds and returns 1 restaurant from DB
        /// </summary>
        /// <param name="id"></param> 
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetRestaurant(Guid id)
        {
            var restaurant = await _restaurantsRepo.GetRestaurantAsync(id);
            if (restaurant != null)
            {
                return Ok(ConverterService.RestaurantToDTO(restaurant));
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
        /// <returns>A newly created restaurant DTO</returns>
        /// <response code="201">Returns the newly created item's DTO</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [Route("api/[controller]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRestaurant(Restaurant restaurant)
        {
            await _restaurantsRepo.AddRestaurantAsync(restaurant);

            return CreatedAtAction("add", ConverterService.RestaurantToDTO(restaurant));
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
                return Ok(ConverterService.RestaurantToDTO(existingRestaurant));
            }

            return NotFound($"There are no restaurants with ID - {id}");


        }


        /// <summary>
        /// Finds existing restaurant in DB by ID 
        /// and updates it with new data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="restaurant"></param>   
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
                return Ok(ConverterService.RestaurantToDTO(existingRestaurant));
            }

            return NotFound($"There are no restaurants with ID - {id}");
        }



    }
}
