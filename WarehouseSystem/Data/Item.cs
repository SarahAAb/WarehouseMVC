using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseSystem.Data
{
    [Table("Items")]
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? KUCode { get; set; }
        public int QTY { get; set; }
        [Required]
        public double CostPrice { get; set; }
        public double? MSRPPrice { get; set; }
        [Required]
        [ForeignKey("Warehouse")]
        public int Warehouse_Id { get; set; }
        public Warehouse Warehouse { get; set; }
        public string CreatedBy { get; set; }

        public DateTime CreatedDAT { get; set; }
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedDAT { get; set; }
        public bool IsDeleted { get; set; }

    }
}
