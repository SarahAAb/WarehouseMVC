using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{
    public class CityDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public CountryDTO countryDTO { get; set; }

    }
}
