using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{
    public class WarehouseDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int CountryId { get; set; }
        [Required]
        public int CityId { get; set; }
        public DateTime CreatedDAT { get; set; }
        public string CreatedBy { get; set; }
    }
}
