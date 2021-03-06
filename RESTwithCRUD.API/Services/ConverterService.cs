using RESTwithCRUD.API.Models;

namespace RESTwithCRUD.API.Services
{
    public static class ConverterService
    {
        //extending method for Restaurant to convert into DTO
        public static RestaurantDTO RestaurantToDTO(Restaurant restaurant) =>
            new RestaurantDTO
            {
                Id = restaurant.Id,
                Name = restaurant.Name,
                Cuisine = restaurant.Cuisine
            };


        public static BookingDTO BookingToDTO(Booking booking) =>
            new BookingDTO
            {
                Id = booking.Id,
                CallerName = booking.CallerName,
                CallerNumber = booking.CallerNumber,
                GuestsQuantity = booking.GuestsQuantity,
                RestaurantId = booking.RestaurantId

            };
    }
}
