namespace WarehouseSystem.Models
{
    public class VMwarehouse
    {
        public WarehouseDTO warehouseDTO { get; set; }
        public List<CountryDTO> countryDTOs { get; set; }
        public List<CityDTO> cityDTOs { get; set; }
    }
}
