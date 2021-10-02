using System;
using System.ComponentModel.DataAnnotations;

namespace RESTwithCRUD.API.Models
{
    public class BookingDTO
    {

        public Guid Id { get; set; }

        public Guid RestaurantId { get; set; }

        [Required]
        public string CallerName { get; set; }

        [Phone]
        [Required]
        public string CallerNumber { get; set; }

        [Required]
        public int GuestsQuantity { get; set; }


    }
}
