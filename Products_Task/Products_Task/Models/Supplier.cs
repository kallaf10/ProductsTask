using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Products_Task.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }
        [Required(ErrorMessage ="Please Enter Supplier Name")]
        public string SupplierName { get; set; }
        public List<Product> ?Products { get; set; }
        
    }
}
