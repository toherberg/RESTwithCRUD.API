using Microsoft.AspNetCore.Mvc;
using RESTwithCRUD.API.Services;
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
            return Ok(await _restaurantsRepo.GetRestaurantsAsync());
        }


    }
}
