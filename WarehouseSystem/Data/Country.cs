using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseSystem.Data
{
    [Table("Country")]
    public class Country
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<City> Cities { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }
}
