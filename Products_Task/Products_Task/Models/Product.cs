using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Products_Task.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage=("Please Enter Product Name"))]
        public string ProductName { get; set; }
        [Required(ErrorMessage =("Please Enter Quantity"))]
        public string QuantityPerUnit { get; set; }
        [Required(ErrorMessage =("Please Enter Recorder Level"))] 
        public int RecorderLevel { get; set; }
        [Required(ErrorMessage =("Please Enter Unit Price"))]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage =("Pleas Enter Unit In Stock"))]
        public int UnitInStock { get; set; }
        [Required(ErrorMessage =("Please Enter Units On Order"))]
        public int UnitInOrder { get; set; }

        [ForeignKey("Supplier")]
        public int SuplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
