using Microsoft.CodeAnalysis.Host;
using System.Linq;
using WarehouseSystem.Data;
using WarehouseSystem.Models;

namespace WarehouseSystem.Services
{
    public class ItemServices:IItemServices
    {
        WarehouseContext context;

        public ItemServices(WarehouseContext _context)
        {
            context = _context;
        }
        public void Insert(ItemDTO item)
        {
            Item item1 = new Item()
            {
                Id = item.Id,
                Name = item.Name,
                KUCode = item.KUCode,
                QTY = item.QTY,
                CostPrice = item.CostPrice,
                MSRPPrice = item.MSRPPrice,
                Warehouse_Id = item.Warehouse_Id,
                CreatedDAT = item.CreatedDAT,
                CreatedBy = item.CreatedBy,
                IsDeleted = false
            };
            context.Items.Add(item1);
            context.SaveChanges();
        }
        public void Update(ItemDTO item)
        {
            Item item1 = new Item()
            {
                Id = item.Id,
                Name = item.Name,
                KUCode = item.KUCode,
                QTY = item.QTY,
                CostPrice = item.CostPrice,
                MSRPPrice = item.MSRPPrice,
                Warehouse_Id = item.Warehouse_Id,
                CreatedDAT = item.CreatedDAT,
                CreatedBy = item.CreatedBy,
                ModifiedDAT=DateTime.Now,
                ModifiedBy= item.ModifiedBy,
                IsDeleted = false

            };
            context.Items.Attach(item1);
            context.Entry(item1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

        }
        public List<ItemDTO> loadall()
        {
        List<Item> items=context.Items.ToList();
        List<ItemDTO> itemDTOs = new List<ItemDTO>();
            foreach (Item item in items)
            {
                itemDTOs.Add(new ItemDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    KUCode = item.KUCode,
                    QTY = item.QTY,
                    CostPrice = item.CostPrice,
                    MSRPPrice = item.MSRPPrice,
                    CreatedBy=item.CreatedBy,
                    ModifiedBy=item.ModifiedBy,
                    CreatedDAT=item.CreatedDAT,
                    ModifiedDAT=item.ModifiedDAT,
                    IsDeleted = item.IsDeleted,
                      Warehouse_Id= item.Warehouse_Id,

                });
            }
            return itemDTOs;
        }
        public ItemDTO Edit(int Id)
        { Item item1 = context.Items.Find(Id);
            ItemDTO itemDTO = new ItemDTO()
            {
                Id = item1.Id,
                Name = item1.Name,
                KUCode = item1.KUCode,
                QTY = item1.QTY,
                CostPrice = item1.CostPrice,
                MSRPPrice = item1.MSRPPrice,
                CreatedBy = item1.CreatedBy,
                CreatedDAT = item1.CreatedDAT,
                ModifiedDAT= item1.ModifiedDAT,
                ModifiedBy= item1.ModifiedBy,
                IsDeleted = false,
                Warehouse_Id = item1.Warehouse_Id,

            };
            return itemDTO;
        }
        public void SoftDelete(int Id)
        {
            Item item1=context.Items.Find(Id);
           item1.IsDeleted=true;        
            context.Items.Attach(item1);
            context.Entry(item1).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }
        public bool CheckName(string Name)
        {
            List<Item> ii = context.Items.Where(e => e.Name == Name).ToList();
            if (ii.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public List<ItemDTO> view1(int Id)
        {
            List<Item> items =context.Items.Where(e=>e.Warehouse_Id==Id).ToList();

            List<ItemDTO> itemDTOs = new List<ItemDTO>();
            foreach (var item in items)
            {
                ItemDTO itemDTO = new ItemDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    MSRPPrice = item.MSRPPrice,
                    Warehouse_Id = item.Warehouse_Id,
                    CostPrice = item.CostPrice,
                    QTY = item.QTY,
                    CreatedBy = item.CreatedBy,
                    IsDeleted = item.IsDeleted,
                    KUCode = item.KUCode,
                };
                itemDTOs.Add(itemDTO);
            }
            return itemDTOs;
        }
    }
}
