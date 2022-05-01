using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarketApp.Web.Models
{
    public class CategoriesModel
    {
        public IList<string> SelectedCategories { get; set; }
        public IList<SelectListItem> AvailableCategories { get; set; }

        public CategoriesModel()
        {
            SelectedCategories = new List<string>();
            AvailableCategories = new List<SelectListItem>();
        }
    }
}
