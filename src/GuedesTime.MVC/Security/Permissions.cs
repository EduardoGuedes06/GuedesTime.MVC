using System.Collections.Generic;
using System.Linq;

namespace GuedesTime.MVC.Security
{
	public static class Permissions
	{
		public const string Admin = "Admin";

		public const string RolesRead = "Roles.Read";
		public const string RolesWrite = "Roles.Write";

		public const string UsersRead = "Users.Read";
		public const string UsersWrite = "Users.Write";

		public static readonly IReadOnlyList<string> All = new[]
		{
			Admin,
			RolesRead,
			RolesWrite,
			UsersRead,
			UsersWrite
		}.Distinct().OrderBy(x => x).ToList();
	}
}

