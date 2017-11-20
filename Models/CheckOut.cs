using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class CheckOut
    {
        public Purchase CurrentCart { get; set; }

        [Display(Name = "Service Name")]
        public string ServiceName { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string ContactEmail { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ContactName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string ShippingAddress { get; set; }

        [Required]
        [Display(Name = "City")]
        public string ShippingCity { get; set; }

        [Required]
        [Display(Name = "State")]
        public string ShippingState { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ShippingPostalCode { get; set; }

        
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        
        [Display(Name = "Date Last Modified")]
        public DateTime DateLastModified { get; set; }

        
        [Display(Name = "Tracking Number")]
        public string TrackingNumber { get; set; }

        
        [Display(Name = "Shipping and Handling")]
        public decimal ShippingAndHandling { get; set; }

        [Display(Name = "Subtotal")]
        public decimal SubTotal { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [Display(Name = "Tax")]
        public decimal Tax { get; set; }

        [Display(Name = "Tax Rate")]
        public decimal TaxRate { get; set; }

        [Display(Name = "Price Modifer")]
        public decimal PriceModifer { get; set; }
    }
}