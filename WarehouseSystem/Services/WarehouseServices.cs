using System.Net.Mime;
using System.Xml.Linq;
using WarehouseSystem.Data;
using WarehouseSystem.Models;
using Microsoft.AspNetCore.Identity;
namespace WarehouseSystem.Services
{
    public class WarehouseServices : IWarehouseServices
    {
      WarehouseContext context;
        IItemServices itemServices;

        public WarehouseServices(WarehouseContext _context,IItemServices _itemServices)
        {
            context = _context;
            itemServices = _itemServices;
        }
        public int insert(WarehouseDTO warehouse)
        {
            Warehouse warehouse1 = new Warehouse()
            {

                Name = warehouse.Name,
                Description = warehouse.Description,
                CountryId = warehouse.CountryId,
                CityId = warehouse.CityId,
                CreatedBy =warehouse.CreatedBy,               
                CreatedDAT = warehouse.CreatedDAT,
            };
            context.Warehouses.Add(warehouse1);
            context.SaveChanges();
            return warehouse1.Id;


        }
        public void Update(WarehouseDTO warehouse)
        {
            Warehouse warehouse1 = new Warehouse()
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Description = warehouse.Description,
                CountryId = warehouse.CountryId,
                CityId = warehouse.CityId,
                CreatedBy = warehouse.CreatedBy,
                CreatedDAT = warehouse.CreatedDAT,
            };
            context.Warehouses.Attach(warehouse1);
            context.Entry(warehouse1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
        public List<WarehouseDTO> loadall()
        {
            List<Warehouse> warehouses= context.Warehouses.ToList();
            List<WarehouseDTO> warehouseDTOs = new List<WarehouseDTO>();
            foreach(Warehouse item in warehouses)
            {
                WarehouseDTO ll= new WarehouseDTO();
                ll.Id = item.Id;
                ll.Name = item.Name;
                ll.Description = item.Description;
                ll.CountryId = item.CountryId;
                ll.CityId = item.CityId;
                ll.CreatedBy = item.CreatedBy;
                ll.CreatedDAT = item.CreatedDAT;
                warehouseDTOs.Add(ll);
            }
            return warehouseDTOs;
        }
        public WarehouseDTO Edit(int Id)
        {
            Warehouse warehouse = context.Warehouses.Find(Id);
            WarehouseDTO warehouseDTO = new WarehouseDTO()
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Description = warehouse.Description,
                CountryId = warehouse.CountryId,
                CityId = warehouse.CityId,
                CreatedBy = warehouse.CreatedBy,
                CreatedDAT =warehouse.CreatedDAT,

            };

            return warehouseDTO;
        }
        public void Delete(int Id)
        {
            Warehouse warehouse = context.Warehouses.Find(Id);
            context.Warehouses.Remove(warehouse);
            context.SaveChanges();

        }
        public List<ItemDTO> view(int Id)
        {
            List<Item> items = context.Items.Where(e => e.Warehouse_Id == Id).ToList();
            List<ItemDTO> itemDTOs = new List<ItemDTO>();
            foreach(Item item in items)
            {
                ItemDTO itemDTO = new ItemDTO()
                {
                    Id= item.Id,
                    Name = item.Name,
                    MSRPPrice= item.MSRPPrice,
                    Warehouse_Id= item.Warehouse_Id,
                    CostPrice= item.CostPrice,
                    QTY=item.QTY,
                    CreatedBy= item.CreatedBy,
                    IsDeleted=item.IsDeleted,
                    KUCode=item.KUCode,
                };
              itemDTOs.Add(itemDTO);
            }
            return itemDTOs;
        }
            public bool CheckName(string Name)
    {
           List <Warehouse> warehouse = context.Warehouses.Where(e=>e.Name==Name).ToList();
            if (warehouse.Count==0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
