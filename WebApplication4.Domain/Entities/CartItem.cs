using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication4.Domain.Entities
{
    public class DBCartItemsTable
    {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }

          [Required]
          public int UserId { get; set; }
          [Required]
          public DBProductTable Product { get; set; }

          [Required]
          public int Quantity { get; set; }
          public decimal FinalPrice => Product.Price * Quantity;
    }
}
