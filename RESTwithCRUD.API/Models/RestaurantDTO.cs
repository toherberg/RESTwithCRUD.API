using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RESTwithCRUD.API.Models
{
    public class RestaurantDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Display(Name = "Type of food")]
        public CuisineType Cuisine { get; set; }
    }
}
