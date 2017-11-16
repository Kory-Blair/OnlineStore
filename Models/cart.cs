using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineStore.Models

{
    public class cart
    {
        public Products[] Products { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal ShippingAndHandling { get; set; }
        public decimal Total { get; set; }
        public int Quantity { get; set; }

        public static cart BuildCart(HttpRequestBase request)
        {
            cart cart = new cart();
            cart.Products = new Products[1];
            //For the moment, getting product data from cookies.
            //TODO: Pull this out of a database at some point!
            cart.Products[0] = new Products();
            cart.Products[0].id = int.Parse(request.Cookies["productID"].Value);
            cart.Products[0].name = request.Cookies["productName"].Value;
            cart.Products[0].price = decimal.Parse(request.Cookies["productPrice"].Value);
            cart.Products[0].quantity = int.Parse(request.Cookies["productQuantity"].Value);

            cart.SubTotal = cart.Products.Sum(x => x.price * x.quantity);

            cart.Tax = cart.SubTotal * .1025m;
            cart.ShippingAndHandling = cart.Products.Sum(x => x.quantity) * 1m;
            cart.Total = cart.SubTotal + cart.Tax + cart.ShippingAndHandling;
            return cart;
        }
    }
}