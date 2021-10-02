using Microsoft.EntityFrameworkCore;
using RESTwithCRUD.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTwithCRUD.API.Services
{
    public class BookingService : IBookingService
    {
        private readonly RestaurantContext _restaurantContext;

        public BookingService(RestaurantContext context)
        {
            _restaurantContext = context;
        }
        public async Task<Booking> PushBookingOrder(Booking bookingOrder)
        {
            bookingOrder.Id = Guid.NewGuid();
            _restaurantContext.Bookings.Add(bookingOrder);
            await _restaurantContext.SaveChangesAsync();
            return bookingOrder;

        }

        public async Task<bool> CancelBookingOrder(Guid id)
        {
            var existingBooking = await _restaurantContext.Bookings.FindAsync(id);

            if (existingBooking != null)
            {
                _restaurantContext.Bookings.Remove(existingBooking);
                return true;

            }

            return false;

        }

        public async Task<IEnumerable<Booking>> GetBookings()
        {
            var bookings = await _restaurantContext.Bookings.ToListAsync();
            return bookings;

        }
    }
}
