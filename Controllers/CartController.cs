using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Models.cart.BuildCart(Request);

            return View(cart);
        }

        [HttpPost]
        public ActionResult Index(Models.cart model)
        {
            Response.AppendCookie(new HttpCookie("productQuantity", model.Products[0].quantity.ToString()));

            model.SubTotal = model.Products.Sum(x => x.price * x.quantity);

            model.Tax = model.SubTotal * .1025m;
            model.ShippingAndHandling = model.Products.Sum(x => x.quantity) * 1m;
            model.Total = model.SubTotal + model.Tax + model.ShippingAndHandling;
            return View(model);
        }
    }
}