using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProEShop.Common;

public static class CommonExtensionMethods
{
    public static List<SelectListItem> CreateSelectListItem<T>(
        this List<T> items,
        object selectedItem = null,
        bool addChooseOneItem = true,
        string firstItemText = "انتخاب کنید",
        string firstItemValue = "0"
    )
    {
        var result = new List<SelectListItem>();
        if (addChooseOneItem)
            result.Add(new SelectListItem(firstItemText, firstItemValue));
        if (items.Any())
        {
            var modelType = items.First().GetType();

            var idProperty = modelType.GetProperty("Id") ?? modelType.GetProperty("Key");
            var titleProperty = modelType.GetProperty("Title") ?? modelType.GetProperty("Value");
            if (idProperty is null || titleProperty is null)
                throw new ArgumentNullException(
                    $"{typeof(T).Name} must have ```Id (Key)``` and ```Title (Value)``` propeties");
            foreach (var item in items)
            {
                var id = idProperty.GetValue(item)?.ToString();
                var text = titleProperty.GetValue(item)?.ToString();
                var selected = selectedItem?.ToString() == id;
                result.Add(new SelectListItem(text, id, selected));
            }
        }

        return result;
    }

    public static List<SelectListItem> CreateSelectListItem(
        this Dictionary<long, string> items,
        object selectedItem = null,
        bool addChooseOneItem = true,
        string firstItemText = "انتخاب کنید",
        string firstItemValue = "0"
        )
    {
        return CreateSelectListItem(items.ToList(), selectedItem, addChooseOneItem, firstItemText, firstItemValue);
    }
}
