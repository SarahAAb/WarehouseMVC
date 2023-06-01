namespace WarehouseSystem.Models
{
    public class UsersDTO
    {
        public string userId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public int Warehouse_Id { get; set; }
    }
}
