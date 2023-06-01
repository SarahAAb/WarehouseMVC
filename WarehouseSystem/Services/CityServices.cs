using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Services
{
    public class CityServices:ICityServices
    {
     WarehouseContext context;

        public CityServices(WarehouseContext _context)
        {
            context = _context;
        }
        public void insert(VMCityDTO cityDTO)
        {
            City city = new City()
            {
                Id= cityDTO.City.Id,
                Name = cityDTO.City.Name,
                CountryId = cityDTO.City.CountryId,
            };
            context.Cities.Add(city);
            context.SaveChanges();
        }
        public void Update(VMCityDTO cityDTO)
        {
            City city = new City()
            {
                Id = cityDTO.City.Id,
                Name = cityDTO.City.Name,
                CountryId = cityDTO.City.CountryId,
            };
            context.Cities.Attach(city);
            context.Entry(city).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
        public List<CityDTO> loadall()
        {
            List<City> citylist = context.Cities.Include("Country").ToList();
            List<CityDTO> cityDTOs= new List<CityDTO>();
            foreach (City city in citylist)
            {
                CityDTO cityDTO = new CityDTO();
                cityDTO.Id = city.Id;
                cityDTO.Name = city.Name;
                cityDTO.CountryId = city.CountryId;
                cityDTO.countryDTO = new CountryDTO()
                {
                    Id = city.Country.Id,
                    Name = city.Country.Name,
                };
                
                cityDTOs.Add(cityDTO);

            }
            return cityDTOs;
        }
        public void Delete(int Id)
        {
            City city = context.Cities.Find(Id);
            context.Cities.Remove(city);
            context.SaveChanges();
        }
        public CityDTO Edited(int Id)
        {
            City city= context.Cities.Find(Id);
            CityDTO cityDTO= new CityDTO();
            cityDTO.Id = city.Id;
            cityDTO.Name=city.Name;
            cityDTO.CountryId=city.CountryId;
            return cityDTO;
        }
        public List<City> load(int countryid)
        {
            List<City> citylist = context.Cities.Where(e => e.CountryId == countryid).ToList();
            return citylist;
        }

    }
}
