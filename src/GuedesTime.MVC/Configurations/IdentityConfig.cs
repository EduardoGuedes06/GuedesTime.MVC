using GuedesTime.MVC.Data;
using GuedesTime.MVC.Models;
using Microsoft.AspNetCore.Identity;
using GuedesTime.MVC.Security;
using Microsoft.EntityFrameworkCore;

namespace GuedesTime.MVC.Configurations
{
	public static class IdentityConfig
	{
		public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("connection");

			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.SignIn.RequireConfirmedAccount = false;
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				options.Password.RequireNonAlphanumeric = true;
				options.Lockout.AllowedForNewUsers = true;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.User.RequireUniqueEmail = true;
			})
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			services.AddAuthorization(options =>
			{
				foreach (var permission in Permissions.All)
				{
					options.AddPolicy(PermissionPolicies.Require(permission), policy =>
						policy.RequireClaim(PermissionClaimTypes.Permission, permission));
				}
			});

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Identity/Account/Login";
				options.AccessDeniedPath = "/Identity/Account/AccessDenied";
				options.LogoutPath = "/Identity/Account/Logout";
				options.SlidingExpiration = true;
				options.ExpireTimeSpan = TimeSpan.FromHours(8);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			services.Configure<SecurityStampValidatorOptions>(options =>
			{
				options.ValidationInterval = TimeSpan.FromMinutes(1);
			});

			return services;
		}
	}
}
