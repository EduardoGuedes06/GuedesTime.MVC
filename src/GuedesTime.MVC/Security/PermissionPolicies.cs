namespace GuedesTime.MVC.Security
{
	public static class PermissionPolicies
	{
		public static string Require(string permission) => $"Permission:{permission}";
	}
}

