using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Services
{
    public class CountryServices:ICountryServices
    {
        //important
        WarehouseContext context;
       public CountryServices(WarehouseContext _context) {

           context = _context;
        }

        public void Insert(CountryDTO countryDTO)
        {
            Country country = new Country();
            country.Id= countryDTO.Id;
            country.Name= countryDTO.Name;
            context.Add(country);
            context.SaveChanges();
            
        }
        public List<CountryDTO> Loadall()
        {
            List<Country> countryList = new List<Country>();
            countryList = context.Countries.ToList();
            List <CountryDTO> countryli= new List<CountryDTO>();
            foreach (Country country in countryList)
            {
                CountryDTO country1= new CountryDTO();
                country1.Id= country.Id;
                country1.Name= country.Name;
                countryli.Add(country1);
            }
            return countryli;
        }
        public void Delete(int Id)
        {
            Country countryd=context.Countries.Find(Id);
            context.Countries.Remove(countryd);
            context.SaveChanges();
        }
        public CountryDTO load(int Id)
        {
            Country country = context.Countries.Find(Id);
            CountryDTO countryDTO = new CountryDTO()
            {
                Id = country.Id,
                Name = country.Name
            };
            return countryDTO;
        }
        public void Update(CountryDTO countryDTO)
        {
            Country country = new Country();

            country.Id = countryDTO.Id;
            country.Name = countryDTO.Name;
            context.Countries.Attach(country);
            context.Entry(country).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
