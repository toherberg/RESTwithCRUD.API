using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RESTwithCRUD.API.Models
{
    public class Restaurant
    {

        public Restaurant()
        {
            Bookings = new List<Booking>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(54, ErrorMessage = "Name can only be 54 characters long!")]
        public string Name { get; set; }

        [Display(Name = "Type of food")]
        public CuisineType Cuisine { get; set; }

        public string Description { get; set; }

        public ICollection<Booking> Bookings { get; set; }


    }
}

