using OnlineStore.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return View(db.Services);
        }

        protected OnlineStoreEntities db = new OnlineStoreEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Details(int? id)
        {
            return View(db.Services.Find(id));
        }


    }
}


//        [HttpPost]
//        public ActionResult Index(Service model)
//        {
//            //TODO: Save the posted information to a database!
//            Guid cartID;
//            cart cart = null;
//            if (Request.Cookies.AllKeys.Contains("cartID"))
//            {

//                cartID = Guid.Parse(Request.Cookies["cartID"].Value);
//                cart = db.carts.Find(cartID);
//            }
//            if (cart == null)
//            {
//                cartID = Guid.NewGuid();
//                cart = new cart
//                {
//                    ID = cartID,
//                    DateCreated = DateTime.UtcNow,
//                    DateLastModified = DateTime.UtcNow
//                };
//                db.Carts.Add(cart);
//                Response.AppendCookie(new HttpCookie("cartID", cartID.ToString()));
//            }

//            CartProduct product = cart.CartProducts.FirstOrDefault(x => x.ProductID == model.Id);
//            if (product == null)
//            {
//                product = new CartProduct
//                {
//                    DateCreated = DateTime.UtcNow,
//                    DateLastModified = DateTime.UtcNow,
//                    ProductID = model.Id,
//                    Quantity = 0
//                };
//                cart.CartProducts.Add(product);
//            }

//            product.Quantity += model.Quantity ?? 1;
//            product.DateLastModified = DateTime.UtcNow;
//            cart.DateLastModified = DateTime.UtcNow;

//            db.SaveChanges();


//            TempData.Add("NewItem", model.Name);

//            //TODO: build up the cart controller!
//            return RedirectToAction("Index", "Cart");

//        }
//    }
//}
