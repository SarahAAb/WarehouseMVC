using System.ComponentModel.DataAnnotations;

namespace WarehouseSystem.Models
{
    public class ItemDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? KUCode { get; set; }
        [Range(1,10000000, ErrorMessage = "Minimum QTY is 1")]
        public int QTY { get; set; }
        [Required(ErrorMessage = "Please enter CostPrice")]
        public double CostPrice { get; set; }

        public double? MSRPPrice { get; set; }
        [Required]
        public int Warehouse_Id { get; set; }
      public DateTime CreatedDAT { get; set; }
        public string CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDAT { get; set; }


    }
}
