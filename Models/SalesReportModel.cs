using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.Models
{
    public class SalesReportModel
    {
        public string SelectedState { get; set; }
        public string[] States { get; set; }

        public TopSaleByDollar[] TopSalesByDollar { get; set; }
        public TopSaleByQuantity[] TopSalesByQuantity {get;set;}
    }
    public class TopSaleByQuantity
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        
    }

    public class TopSaleByDollar
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Total { get; set; }
    }
}