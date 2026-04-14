namespace GuedesTime.MVC.Areas.Admin.ViewModels
{
	public class UserRolesEditVm
	{
		public string UserId { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public List<string> AllRoles { get; set; } = new();
		public List<string> SelectedRoles { get; set; } = new();
	}
}

