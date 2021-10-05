using System;
using System.ComponentModel.DataAnnotations;

namespace RESTwithCRUD.API.Models
{
    public class Booking
    {
        public Guid Id { get; set; }

        [Required]
        public string CallerName { get; set; }

        [Phone]
        [Required]
        public string CallerNumber { get; set; }

        [Required]
        public int GuestsQuantity { get; set; }


        //foreighnKey element - restaurant to be booked
        [Required]
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }


    }
}