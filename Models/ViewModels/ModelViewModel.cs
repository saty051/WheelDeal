using Microsoft.AspNetCore.Mvc.Rendering;

namespace WheelDeal.Models.ViewModels
{
    public class ModelViewModel
    {
        public Model Model { get; set; } 
        public IEnumerable<Make> Makes { get; set; }
        public IEnumerable<SelectListItem> CSelectListItem(IEnumerable<Make> Items)
        {
            if (Items == null || !Items.Any())
            {
                // Return a dropdown with just the default "Select" option
                return new List<SelectListItem>
                {
                  new SelectListItem { Text = "----Select----", Value = "0" }
                };
            }

            return Items.Select(make => new SelectListItem
            {
                Text = make.Name,
                Value = make.Id.ToString()
            }).Prepend(new SelectListItem
            { Text = "----Select----", Value = "0" }).ToList();
        }

    }
}
