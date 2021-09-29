using System;
using System.ComponentModel.DataAnnotations;

namespace RESTwithCRUD.API.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Type of food")]
        public CuisineType Cuisine { get; set; }

        public string Description { get; set; }
    }
}

