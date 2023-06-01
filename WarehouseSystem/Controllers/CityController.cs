using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseSystem.Models;
using WarehouseSystem.Services;

namespace WarehouseSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        ICityServices cityServices;

        ICountryServices CountryServices;

        public CityController(ICityServices _cityServices,ICountryServices countryServices)
        {
            cityServices = _cityServices;
            CountryServices = countryServices;
        }
        public IActionResult NewCity()
        { VMCityDTO cityDTO = new VMCityDTO();
          cityDTO.countries=CountryServices.Loadall();
            ViewData["City"] = false;
            return View("NewCity",cityDTO);
        }
        public IActionResult SaveData(VMCityDTO vm)
        {
         cityServices.insert(vm);
            VMCityDTO cityDTO = new VMCityDTO();
            cityDTO.countries= CountryServices.Loadall();
         ViewData["City"] =true;

            return View("NewCity", cityDTO);
        }
        
        public IActionResult CityList(int pg = 1)
        {
            List<CityDTO> cityList = cityServices.loadall();
           
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int resCount = cityList.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = cityList.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View("CityList", data);
        }
        public IActionResult Deleted(int id, int pg = 1)
        {
            cityServices.Delete(id);
            List<CityDTO> cityList = cityServices.loadall();
            //int pg = 1;
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int resCount =cityList.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = cityList.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View("CityList", data);
        }
        public IActionResult Edited(int Id)
        { VMCityDTO vm=new VMCityDTO();
           vm.City= cityServices.Edited(Id);
           vm.countries= CountryServices.Loadall();
            ViewData["City"] = true;
            return View("NewCity",vm);
        }
        public IActionResult Update(VMCityDTO vm)
        {
            cityServices.Update(vm);
            vm.countries = CountryServices.Loadall();
            ViewData["City"] = true;

            return View("NewCity", vm);
        }
    }
}
