using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using WarehouseSystem.Data;

namespace WarehouseSystem.Models
{
    public class ApplicationUsers:IdentityUser
    {
        public string FullName { get; set; }
        public bool Active { get; set; }
       public Warehouse Warehouse { get; set; }
        [ForeignKey("Warehouse")]
        public int Warehouse_Id { get; set; }
    }
}
