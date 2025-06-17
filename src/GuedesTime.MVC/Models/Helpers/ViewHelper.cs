using Microsoft.AspNetCore.Mvc.Rendering;

namespace GuedesTime.MVC.Models.Helpers
{
	public static class ViewHelper
	{
		public static string IsActive(ViewContext viewContext, string controller)
		{
			var currentController = viewContext.RouteData.Values["controller"]?.ToString();
			return string.Equals(controller, currentController, StringComparison.OrdinalIgnoreCase) ? "active" : "";
		}
	}
}
