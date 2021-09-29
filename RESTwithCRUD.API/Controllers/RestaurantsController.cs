using Microsoft.AspNetCore.Mvc;
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
            Thread.Sleep(5000);
            return Ok(await _restaurantsRepo.GetRestaurantAsync(id));
        }


    }
}
