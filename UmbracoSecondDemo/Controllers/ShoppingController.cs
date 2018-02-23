using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace UmbracoSecondDemo.Controllers
{
    public static class DataStorage
    {
        public static Dictionary<string, int> CartItems = new Dictionary<string, int>();
    }

    public class CartEditorController : SurfaceController
    {
        // GET: Shopping
        [HttpGet]
        [NotChildAction]
        public bool AddToCart(string itemId)
        {
            int itemCurrentCount;
            var ifItemAlreadyInList = DataStorage.CartItems.TryGetValue(itemId, out itemCurrentCount);

            itemCurrentCount++;

            if (ifItemAlreadyInList)
                DataStorage.CartItems[itemId] = itemCurrentCount;
            else
                DataStorage.CartItems.Add(itemId, itemCurrentCount);

            return true;
        }

        [HttpPost]
        [NotChildAction]
        public bool RemoveItem(string itemId)
        {
            int itemCurrentCount;
            var isItemInList = DataStorage.CartItems.TryGetValue(itemId, out itemCurrentCount);
        
            if (isItemInList)
            {
                if(itemCurrentCount > 1)
                    DataStorage.CartItems[itemId] = --itemCurrentCount;
                else
                    DataStorage.CartItems.Remove(itemId);
            }
                
            return true;
        }

        [HttpPost]
        [NotChildAction]
        public bool ResetCartItems()
        {
            DataStorage.CartItems.Clear();

            return true;
        }
    }

    public class CartController : RenderMvcController
    {
        public override ActionResult Index(RenderModel model)
        {
            var viewModel = new CartItemsViewModel(model.Content, model.CurrentCulture);
            viewModel.Items = DataStorage.CartItems;

            return base.Index(viewModel);
        }
    }

    public class CartItemsViewModel : RenderModel
    {
        public Dictionary<string, int> Items { get; set; }

        public CartItemsViewModel(IPublishedContent content, CultureInfo culture) : base(content, culture)
        {
        }
    }
}