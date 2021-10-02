using RESTwithCRUD.API.Models;
using System;
using System.Threading.Tasks;

namespace RESTwithCRUD.API.Services
{
    public interface IBookingService
    {

        Task<Booking> PushBookingOrder(Booking bookingOrder);

        Task<bool> CancelBookingOrder(Guid Id);


    }
}
