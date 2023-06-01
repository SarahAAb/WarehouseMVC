using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{
    public class SignUp
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public bool Active { get; set; }

        [Compare("ConfirmPassward")]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassward { get; set; }
        public int Warehouse_Id { get; set; }

    }
}
