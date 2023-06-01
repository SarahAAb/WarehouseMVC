using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WarehouseSystem.Migrations;
using WarehouseSystem.Models;
using WarehouseSystem.Services;

namespace WarehouseSystem.Controllers
{
    public class itemController : Controller
    {
        IWarehouseServices warehouseServices;
        IItemServices itemServices;

        IAccountServices AccountServices; 

        public itemController(IWarehouseServices _warehouseServices,IItemServices _itemServices,IAccountServices _accountServices) {
            warehouseServices = _warehouseServices;
            itemServices = _itemServices;
            AccountServices = _accountServices;
        }
        [Authorize(Roles = "Employee")]

        public IActionResult NewItem()
        {   VMItemDTO vm= new VMItemDTO();
            vm.warehouseDTOs=warehouseServices.loadall();
            ViewData["Item"] = false;

            return View("NewItem",vm);
        }
        [Authorize(Roles = "Employee")]

        public IActionResult SaveData(VMItemDTO vm)
        { bool var = itemServices.CheckName(vm.itemDTO.Name);
            if (var == true)
            {
                vm.itemDTO.CreatedBy= Convert.ToString(TempData["Username"]);
                vm.itemDTO.CreatedDAT = DateTime.Now;
                itemServices.Insert(vm.itemDTO);
            vm.warehouseDTOs = warehouseServices.loadall();
            ViewData["Item"] = true;
            return View("NewItem", vm);

            }
            else
            {
                VMItemDTO mItemDTO= new VMItemDTO();
                mItemDTO.warehouseDTOs = warehouseServices.loadall();
                ViewData["Item"] = false;
                ViewData["results"] = "Exist Item Name";
                return View("NewItem", mItemDTO);

            }
        }
        [Authorize(Roles = "Employee")]
        public IActionResult Updated(VMItemDTO vm)
        {
           
                vm.itemDTO.ModifiedBy = Convert.ToString(TempData["Username"]);
                itemServices.Update(vm.itemDTO);
             vm.warehouseDTOs = warehouseServices.loadall();
               ViewData["Item"] = true;

                return View("NewItem", vm);
           
        }

        [Authorize(Roles = "Employee,Manager")]
        public IActionResult ItemList(int pg = 1)
        {
           List<ItemDTO> items= itemServices.loadall();
            //paging Code
            //  int pg = 1;
            const int pageSize = 9;
            if (pg < 1) pg = 1;
            int resCount = items.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = items.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View("ItemList", data);
        }
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ItemList1(String Name)
        {
            var user = await AccountServices.GetuserInfo(Name);

            List<ItemDTO> items = itemServices.view1(user.Warehouse_Id);
          
            return View("ItemList", items);
        }
        [Authorize(Roles = "Employee")]
        public IActionResult Edited(int Id)
        {
            VMItemDTO vm = new VMItemDTO();
            vm.itemDTO= itemServices.Edit(Id);
            vm.warehouseDTOs = warehouseServices.loadall();
            ViewData["Item"] = true;
            return View("NewItem", vm);
        }
        [Authorize(Roles = "Employee")]
        public IActionResult Deleted(int Id, int pg = 1)
        {
            itemServices.SoftDelete(Id);
            List<ItemDTO> items = itemServices.loadall();

            //paging Code
            //int pg = 1;
            const int pageSize = 9;
            if (pg < 1) pg = 1;
            int resCount = items.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = items.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View("ItemList", data);

        }
        [Authorize(Roles = "Manager")]
        public IActionResult ItemListView()
        {

            int warehouseId = Convert.ToInt32( TempData["WarehouseId"]);
            List<ItemDTO> items = warehouseServices.view(warehouseId);
            //paging Code
             int pg = 1;
            const int pageSize = 9;
            if (pg < 1) pg = 1;
            int resCount = items.Count();
            var pager = new Pager(resCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = items.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.pager = pager;
            return View("ItemList", data);
        }
    }
}
