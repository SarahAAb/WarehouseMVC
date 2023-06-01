using WarehouseSystem.Models;

namespace WarehouseSystem.Services
{
    public interface ICountryServices
    {
      void  Insert(CountryDTO countryDTO);
        List<CountryDTO> Loadall();
        void Delete(int Id);
        CountryDTO load(int Id);
        void Update(CountryDTO countryDTO);
    }
}
