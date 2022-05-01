using bootShop.Dtos.Responses;
using System.Collections.Generic;
using System.Linq;

namespace bootShop.Web.Models
{
    public class CartItem
    {
        public GetProductsResponse Product { get; set; }
        public int Quantity { get; set; }
    }

    public class CartCollection
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public void Add(CartItem item)
        {
            var finding = CartItems.Find(x => x.Product.Id == item.Product.Id);
            if (finding == null)
            {
                CartItems.Add(item);
            }
            else
            {
                finding.Quantity += item.Quantity;
            }
        }
        public void ClearAll () => CartItems.Clear();
        public double GetTotalPrice() => CartItems.Sum(x => x.Product.Price * (1-x.Product.Discount.Value) * x.Quantity);
        public void Delete(int id) => CartItems.RemoveAll(x => x.Product.Id == id);
    }
}
