using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WheelDeal.Extensions
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class CollectionExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectListItems<T>(this IEnumerable<T> items)
        {
            if (items == null || !items.Any())
            {
                // Return a dropdown with just the default "Select" option
                return new List<SelectListItem>
            {
                new SelectListItem { Text = "----Select----", Value = "0" }
            };
            }

            // Use reflection to find properties named "Name" and "Id"
            var textProperty = typeof(T).GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
            var valueProperty = typeof(T).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);

            if (textProperty == null || valueProperty == null)
            {
                throw new MissingMemberException($"The type '{typeof(T).Name}' must have public 'Name' and 'Id' properties.");
            }

            return items.Select(item => new SelectListItem
            {
                Text = textProperty.GetValue(item)?.ToString(),
                Value = valueProperty.GetValue(item)?.ToString()
            }).Prepend(new SelectListItem
            {
                Text = "----Select----",
                Value = "0"
            }).ToList();
        }
    }

}

//How to Use the Generic Extension Method

//@model WheelDeal.Models.ViewModels.ModelViewModel

//@{
//    var makesDropdown = Model.Makes.ToSelectListItems();
//}

//< select asp -for= "Model.MakeFK" asp - items = "makesDropdown" class= "form-control" ></ select >
