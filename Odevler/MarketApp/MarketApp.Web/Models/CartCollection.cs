using MarketApp.Dtos.Response;

namespace MarketApp.Web.Models
{
    public class CartItem
    {
        public GetProductsResponse Product { get; set; }
        public int Quantity { get; set; }
    }
    public class CartCollection
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public void Add (CartItem item)
        {
            var product = CartItems.Find(x => x.Product.Id == item.Product.Id);
            if (product == null)
            {
                CartItems.Add(item);
            }
            else
            {
                product.Quantity += 1;
            }

        }
        public void ClearCart() => CartItems.Clear();
        public double GetTotal() => CartItems.Sum(x => x.Product.Price * (1 - x.Product.Discount) * x.Quantity);
        public bool IsProductExist(int id)
        {
            var product = CartItems.FirstOrDefault(x => x.Product.Id == id);
            return product != null ? true : false;
        }
        public void Delete(int id) => CartItems.RemoveAll(x => x.Product.Id == id);
        public void DecreaseAmount(int id) => CartItems.Find(x => x.Product.Id == id).Quantity -= 1;
        public CartItem GetCartItem(int id) => CartItems.Find(x => x.Product.Id == id);
    }
}
