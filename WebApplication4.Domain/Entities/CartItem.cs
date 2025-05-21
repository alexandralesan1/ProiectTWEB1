using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

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
          public int ProductId { get; set; }

          [ForeignKey("ProductId")]
          public DBProductTable Product { get; set; }

          [Required]
          public int Quantity { get; set; }
          public decimal FinalPrice => Product != null ? Product.Price * Quantity : 0;



     }
}
