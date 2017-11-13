using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{

    public class Products
    {
        public string name { get; set; }
        public int id { get; set; }

        [DataType(DataType.Currency)]
        public decimal price { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public decimal quantity { get; set; }
        public string shortDescription { get; set; }
        
    }
}