using WarehouseSystem.Models;

namespace WarehouseSystem.Services
{
    public interface IWarehouseServices
    {
        int insert(WarehouseDTO warehouse);
        List<WarehouseDTO> loadall();
        WarehouseDTO Edit(int Id);
        void Update(WarehouseDTO warehouse);
        void Delete(int Id);
        List<ItemDTO> view(int Id);
        public bool CheckName(string Name);
    }
}
