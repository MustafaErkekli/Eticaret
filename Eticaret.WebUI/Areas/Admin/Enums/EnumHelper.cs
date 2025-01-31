using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Eticaret.WebUI.Areas.Admin.Enums
{
    public static class EnumHelper
    {
        public static SelectList GetSelectList<TEnum>() where TEnum : Enum
        {
            var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Select(e => new
            {
                Value = Convert.ToInt32(e),
                Text = e.GetType()
                        .GetField(e.ToString())
                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                        .FirstOrDefault() is DisplayAttribute displayAttribute
                            ? displayAttribute.Name
                            : e.ToString()
            }).ToList();

            return new SelectList(values, "Value", "Text");
        }
    }
}
