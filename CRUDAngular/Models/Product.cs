using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDAngular.Models
{
    public class Product
    {
        public int ID { get; set; }

        [MaxLength(120)]
        public string Name { get; set; }

        [MaxLength(240)]
        public string ImageUrl { get; set; }

        public decimal? Price { get; set; }
    }
}
