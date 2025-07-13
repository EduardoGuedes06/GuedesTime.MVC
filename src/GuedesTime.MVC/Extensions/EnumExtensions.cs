using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GuedesTime.MVC.Extensions
{
	public static class EnumExtensions
	{
		public static List<SelectListItem> ToSelectListItems<TEnum>(this TEnum enumObj, bool useDisplayName = true) where TEnum : Enum
		{
			var type = typeof(TEnum);

			var items = Enum.GetValues(type)
				.Cast<Enum>()
				.Select(e => new SelectListItem
				{
					Value = Convert.ToInt32(e).ToString(),
					Text = useDisplayName ? GetDisplayName(e) : e.ToString()
				}).ToList();

			return items;
		}

		private static string GetDisplayName(Enum enumValue)
		{
			return enumValue.GetType()
				.GetMember(enumValue.ToString())
				.First()
				.GetCustomAttribute<DisplayAttribute>()?
				.GetName() ?? enumValue.ToString();
		}
	}
}
