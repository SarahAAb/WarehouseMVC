using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Services
{
    public interface ICityServices
    {
        void insert(VMCityDTO cityDTO);
        List<CityDTO> loadall();
        void Update(VMCityDTO cityDTO);
        void Delete(int Id);
        CityDTO Edited(int Id);
        List<City> load(int countryid);
    }
}