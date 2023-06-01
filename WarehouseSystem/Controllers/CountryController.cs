using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseSystem.Data;
using WarehouseSystem.Models;
using WarehouseSystem.Services;

namespace WarehouseSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountryController : Controller
    {
        WarehouseContext context;
        ICountryServices countryServices;

       public CountryController(ICountryServices _countryServices,WarehouseContext _context) {
            countryServices = _countryServices;
            context=_context;
        }
        public IActionResult NewCountry()
        {
            ViewData["Edit"] = false;
            return View("NewCountry");
        }
        public IActionResult SaveData(CountryDTO countryDTO)
        {
            countryServices.Insert(countryDTO);
            ViewData["Edit"] = true;
            return View("NewCountry");

        }

        public IActionResult CountryList(int pg=1)
        {
            List<CountryDTO> countryDTOs = countryServices.Loadall();
            //paging Code
            const int pageSize= 10;
            if (pg < 1) pg = 1;
            int resCount = countryDTOs.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = countryDTOs.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View("CountryList", data);
        }
        public IActionResult Deleted(int Id)
        {
            countryServices.Delete(Id);

            List<CountryDTO> countryDTOs = countryServices.Loadall();
           //paging Code
           int pg = 1;
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int resCount = countryDTOs.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = countryDTOs.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View("CountryList", data);

        }
        public IActionResult Edited(int Id)
        {
            ViewData["Edit"] =true;
            CountryDTO countryDTO = countryServices.load(Id);
            
            return View("NewCountry",countryDTO);
        }
        public IActionResult Update(CountryDTO countryDTO)
        {
            countryServices.Update(countryDTO);
            ViewData["Edit"] = true;
            return View("NewCountry");

        }
    }
}
