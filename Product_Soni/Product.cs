using System.ComponentModel.DataAnnotations;

namespace Product_Soni
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }  
        public string Name { get; set; }
         public string Description { get; set; }
        public float Price { get; set; }
        public string Category { get; set; }    
    }
}
