using System.Security.Claims;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GuedesTime.MVC.Identity
{
	public static class IdentitySeeder
	{
		public static async Task SeedAsync(IServiceProvider services, bool isDevelopment)
		{
			using var scope = services.CreateScope();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			await EnsureAdminRoleAsync(roleManager);
			await EnsureAdminRoleHasAllPermissions(roleManager);

			if (isDevelopment)
			{
				await EnsureSomeUserIsAdminAsync(userManager);
			}
		}

		private static async Task EnsureAdminRoleAsync(RoleManager<IdentityRole> roleManager)
		{
			if (await roleManager.RoleExistsAsync("Admin"))
				return;

			var role = new IdentityRole("Admin");
			var result = await roleManager.CreateAsync(role);
			if (!result.Succeeded)
			{
				throw new InvalidOperationException($"Falha ao criar role Admin: {string.Join("; ", result.Errors.Select(e => e.Description))}");
			}
		}

		private static async Task EnsureAdminRoleHasAllPermissions(RoleManager<IdentityRole> roleManager)
		{
			var role = await roleManager.FindByNameAsync("Admin");
			if (role is null) return;

			var claims = await roleManager.GetClaimsAsync(role);
			var existing = claims
				.Where(c => c.Type == PermissionClaimTypes.Permission)
				.Select(c => c.Value)
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			foreach (var permission in Permissions.All)
			{
				if (existing.Contains(permission)) continue;
				await roleManager.AddClaimAsync(role, new Claim(PermissionClaimTypes.Permission, permission));
			}
		}

		private static async Task EnsureSomeUserIsAdminAsync(UserManager<ApplicationUser> userManager)
		{
			// Bootstrap DEV: se ninguém tem Admin, dá Admin ao primeiro usuário cadastrado.
			var users = await userManager.Users.OrderBy(u => u.Id).Take(50).ToListAsync();
			if (users.Count == 0) return;

			foreach (var u in users)
			{
				if (await userManager.IsInRoleAsync(u, "Admin"))
					return;
			}

			var firstUser = users[0];
			await userManager.AddToRoleAsync(firstUser, "Admin");
		}
	}
}

