using System.ComponentModel.DataAnnotations;

namespace GuedesTime.MVC.Areas.Admin.ViewModels
{
	public class RoleEditVm
	{
		public string Id { get; set; } = string.Empty;

		[Display(Name = "Nome")]
		public string Name { get; set; } = string.Empty;

		public List<string> AllPermissions { get; set; } = new();
		public List<string> SelectedPermissions { get; set; } = new();
	}
}

