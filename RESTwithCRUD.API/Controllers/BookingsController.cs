using Microsoft.AspNetCore.Mvc;
using RESTwithCRUD.API.Models;
using RESTwithCRUD.API.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RESTwithCRUD.API.Controllers
{

    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }


        /// <summary>
        /// Finds and returns all bookings from DB
        /// </summary>
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetBookings()
        {
            var result = await _bookingService.GetBookings();
            return Ok(result.Select(b => ConverterService.BookingToDTO(b)));
        }

        /// <summary>
        /// Finds and returns all bookings filtered by restaurant from DB
        /// </summary>
        [HttpGet]
        [Route("api/[controller]/restfilter")]
        public async Task<IActionResult> GetBookingsFilteredByRestaurant(Guid restaurantId)
        {
            var result = await _bookingService.GetBookingsFilteredByRestaurant(restaurantId);
            return Ok(result.Select(b => ConverterService.BookingToDTO(b)));
        }


        /// <summary>
        /// Creates a new booking order in DB, related to specific Restaurant
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/bookings
        ///     {
        ///  "callerName": "Mykhailo",
        ///  "callerNumber": "0506509495",
        ///  "guestsQuantity": 5,
        ///  "restaurantId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        ///     }
        ///
        /// </remarks>
        /// <param name="newBookingOrder"></param>
        /// <returns>A newly created booking order</returns>
        /// <response code="201">Returns the newly created booking order</response>
        /// <response code="400">If the item is null</response>  
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> PushBooking(Booking newBookingOrder)
        {
            if (ModelState.IsValid)
            {
                var addedBookingOrder = await _bookingService.PushBookingOrder(newBookingOrder);
                return CreatedAtAction("PushBooking", ConverterService.BookingToDTO(addedBookingOrder));
            }
            return BadRequest();
        }


        /// <summary>
        /// Deletes a specific Booking by ID from DB.
        /// </summary>
        /// <param name="id"></param>    
        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteBookingFromSystem(Guid id)
        {

            if (await _bookingService.CancelBookingOrder(id))
            {
                return Ok("You sucessfully canceled your order");
            }

            return BadRequest();
        }
    }
}
