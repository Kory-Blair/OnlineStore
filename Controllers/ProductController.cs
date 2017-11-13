using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            List<Models.Products> products = new List<Models.Products>();

            products.Add(new Models.Products
            {
                id = 1,
                name = "Slap",
                price = 14.99m,
                description = "Sometimes we all need a hard slap to the face to wake us up and convince us that we aren't in some all consuming nightmare. Allow our trained technicans to bring you back to reality by causing you a substancial amount of pain.",
                image = "/images/slap.jpg",
                shortDescription = "A hard slap to the face."
            });

            products.Add(new Models.Products
            {
                id = 2,
                name = "Splash of Water",
                price = 29.99m,
                description = "Nothing wakes you up from the zombie-like state that you know as your everyday life like a bucket of cold water.",
                image = "/images/bucket.jpg",
                shortDescription = "A bucket of cold water to the face."
            });

            products.Add(new Models.Products
            {
                id = 3,
                name = "Puppies",
                price = 89.99m,
                description = "What could be better for restoring your spirits and bringing joy back to your heart than cuddling with an adorable puppy?",
                image = "/images/puppies.jpg",
                shortDescription = "Puppies!!!"
            });
            
            return View(products);
        }

        [HttpPost]
        public ActionResult Index(int id, string name, decimal price)
       {
            {
                Response.Cookies.Add(new HttpCookie("productID", id.ToString()));
                Response.Cookies.Add(new HttpCookie("productName", name));
                Response.Cookies.Add(new HttpCookie("productPrice", price.ToString()));
                return RedirectToAction("Index", "Cart");
            }
        }
        
        public ActionResult Details(int id)
        {
            Products p = null;
            if(id == 1)
            {
                p = new Models.Products
                {
                    id = 1,
                    name = "Slap",
                    price = 14.99m,
                    description = "Sometimes we all need a hard slap to the face to wake us up and convince us that we aren't in some all consuming nightmare. Allow our trained technicans to bring you back to reality by causing you a substancial amount of pain.",
                    image = "/images/slap.jpg",
                    shortDescription = "A hard slap to the face."
                };
            } else if (id == 2) { 
                p = new Models.Products
                {
                    id = 2,
                    name = "Splash of Water",
                    price = 29.99m,
                    description = "Nothing wakes you up from the zombie-like state that you know as your everyday life like a bucket of cold water.",
                    image = "/images/bucket.jpg",
                    shortDescription = "A bucket of cold water to the face."
                };
            } else
            {
                p = new Models.Products
                {
                    id = 3,
                    name = "Puppies",
                    price = 89.99m,
                    description = "What could be better for restoring your spirits and bringing joy back to your heart than cuddling with an adorable puppy?",
                    image = "/images/puppies.jpg",
                    shortDescription = "Puppies!!!"
                };
            }
            return View(p);
        }
    }
}