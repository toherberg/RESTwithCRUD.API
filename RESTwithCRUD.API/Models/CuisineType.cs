using System.Text.Json.Serialization;

namespace RESTwithCRUD.API.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CuisineType
    {
        None,
        Italian,
        Indian,
        French,
        Ukrainian,
        Asian,
    }
}
