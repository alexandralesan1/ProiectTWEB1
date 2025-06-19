using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Domain.Entities;

namespace WebApplication4.Models
{
     public class HomePageViewModel
     {
          public List<DBProductTable> NewProducts { get; set; }
          public DBNewsTable LatestNews { get; set; }


     }
}