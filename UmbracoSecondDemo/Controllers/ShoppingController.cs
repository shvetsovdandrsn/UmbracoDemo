using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace UmbracoSecondDemo.Controllers
{
    public static class CartItemsKeeper
    {
        public static List<string> CartItems = new List<string>();
    }

    public class ShoppingController : SurfaceController
    {
        [HttpGet]
        public ActionResult CartIndex()
        {
            var viewModel = CartItemsKeeper.CartItems;
            return View("CartIndexPage", viewModel);
        }

        // GET: Shopping
        [HttpGet]
        [NotChildAction]
        public bool AddToCart(string itemId)
        {
            CartItemsKeeper.CartItems.Add(itemId);
            return true;
        }
    }
}