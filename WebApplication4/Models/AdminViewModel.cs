using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Domain.Entities;

namespace WebApplication4.Models
{
     public class AdminViewModel
     {
          public List<DBUserTable> Users { get; set; }  
          public List<DBProductTable> Products { get; set; } 
          //public List<DBNewsTable> News { get; set; } 
     }

}