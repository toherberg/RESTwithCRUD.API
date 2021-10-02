using RESTwithCRUD.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTwithCRUD.API.Services
{
    public interface IBookingService
    {

        Task<IEnumerable<Booking>> GetBookings();

        Task<Booking> PushBookingOrder(Booking bookingOrder);

        Task<bool> CancelBookingOrder(Guid Id);


    }
}
