using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseSystem.Data;
using WarehouseSystem.Models;
using WarehouseSystem.Services;

namespace WarehouseSystem.Controllers
{
    [Authorize(Roles = "Manager")]

    public class WarehouseController : Controller
    {
        ICountryServices countryServices;
        ICityServices cityServices;
        IWarehouseServices warehouseServices;

        public WarehouseController(ICountryServices _countryServices,ICityServices _cityServices,IWarehouseServices _warehouseServices) {
            countryServices = _countryServices;
            cityServices = _cityServices;
            warehouseServices = _warehouseServices;
        }



        public IActionResult NewWarehouse()
        { VMwarehouse vm = new VMwarehouse();
          vm.countryDTOs  = countryServices.Loadall();
            vm.cityDTOs = cityServices.loadall();
            ViewData["WareHouse"] = false;
        return View("NewWarehouse",vm);
        }


        public IActionResult SaveData(VMwarehouse vmwarehouse)
        {
           bool Check = warehouseServices.CheckName(vmwarehouse.warehouseDTO.Name);
            if(Check==true){ 
            vmwarehouse.warehouseDTO.CreatedBy = Convert.ToString(TempData["Username"]);
            vmwarehouse.warehouseDTO.CreatedDAT= DateTime.Now;

                int newId = warehouseServices.insert(vmwarehouse.warehouseDTO);
                ModelState.Clear(); //for update

                VMwarehouse x = new VMwarehouse();
                x.warehouseDTO = new WarehouseDTO()
                {
                    Id=newId,
                    CityId= vmwarehouse.warehouseDTO.CityId,
                    CountryId = vmwarehouse.warehouseDTO.CountryId,
                    CreatedBy= vmwarehouse.warehouseDTO.CreatedBy,
                    CreatedDAT= vmwarehouse.warehouseDTO.CreatedDAT ,
                    Description = vmwarehouse.warehouseDTO.Description ,
                    Name= vmwarehouse.warehouseDTO.Name 
                };


                x.countryDTOs = countryServices.Loadall();
                x.cityDTOs = cityServices.loadall();
            ViewData["WareHouse"] = true;
            return View("NewWarehouse", x);

}
            else
            {

                VMwarehouse vm1 = new VMwarehouse();
                vm1.countryDTOs = countryServices.Loadall();
                vm1.cityDTOs = cityServices.loadall();
                ViewData["WareHouse"] = false;
                ViewData["results"] = "Exist Warehouse Name";

                return View("NewWarehouse", vm1);
            }
        }
        public IActionResult load(int countryId)
        {
            List<City> citylist = cityServices.load(countryId);

            return Json(citylist);
        }
        public IActionResult WarehouseList(int pg = 1)
        {
            List<WarehouseDTO> dto = warehouseServices.loadall();
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int resCount = dto.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = dto.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View("WarehouseList", data);
        }

        public IActionResult Edited(int Id) { 
            VMwarehouse vm=new VMwarehouse();
            vm.warehouseDTO=warehouseServices.Edit(Id);
            vm.countryDTOs = countryServices.Loadall();
            vm.cityDTOs = cityServices.loadall();
            ViewData["WareHouse"] = true ;

            return View("NewWarehouse", vm);

        }

        public IActionResult Updated(VMwarehouse vm1) {
            warehouseServices.Update(vm1.warehouseDTO);
            VMwarehouse vmwarehouse1 = new VMwarehouse();
            vmwarehouse1.countryDTOs = countryServices.Loadall();
            vmwarehouse1.cityDTOs = cityServices.loadall();
            ViewData["WareHouse"] = true;

            return View("NewWarehouse", vmwarehouse1);


        }

        public IActionResult Deleted(int Id)
        {
               warehouseServices.Delete(Id);
            List<WarehouseDTO> dto = warehouseServices.loadall();
            int pg = 1;
            const int pageSize = 10;
            if (pg < 1) pg = 1;
            int resCount = dto.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = dto.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View("WarehouseList", data);

        }
        public IActionResult View(int Id)
        {

            TempData["WarehouseId"]=Id;
            return RedirectToAction("ItemListView", "Item");
        }
        

    }
}
