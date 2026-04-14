using GuedesTime.MVC.Areas.Admin.ViewModels;
using GuedesTime.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuedesTime.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Policy = "Permission:Admin")]
	public class UsersController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var users = await _userManager.Users
				.OrderBy(u => u.Email)
				.Take(500)
				.ToListAsync();

			var vms = new List<UserListItemVm>(users.Count);
			foreach (var u in users)
			{
				var roles = await _userManager.GetRolesAsync(u);
				vms.Add(new UserListItemVm
				{
					Id = u.Id,
					Email = u.Email ?? string.Empty,
					Nome = u.Nome ?? string.Empty,
					Roles = roles.OrderBy(r => r).ToList()
				});
			}

			return View(vms);
		}

		[HttpGet]
		public async Task<IActionResult> EditRoles(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			if (user is null) return NotFound();

			var userRoles = await _userManager.GetRolesAsync(user);
			var allRoles = await _roleManager.Roles.OrderBy(r => r.Name).Select(r => r.Name!).ToListAsync();

			var vm = new UserRolesEditVm
			{
				UserId = user.Id,
				Email = user.Email ?? string.Empty,
				AllRoles = allRoles,
				SelectedRoles = userRoles.ToList()
			};

			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditRoles(UserRolesEditVm vm)
		{
			var user = await _userManager.FindByIdAsync(vm.UserId);
			if (user is null) return NotFound();

			vm.AllRoles = await _roleManager.Roles.OrderBy(r => r.Name).Select(r => r.Name!).ToListAsync();

			var selected = (vm.SelectedRoles ?? new List<string>())
				.Where(r => !string.IsNullOrWhiteSpace(r))
				.Select(r => r.Trim())
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			// remove roles inexistentes
			selected.RemoveWhere(r => !vm.AllRoles.Contains(r));

			var current = await _userManager.GetRolesAsync(user);
			var currentSet = current.ToHashSet(StringComparer.OrdinalIgnoreCase);

			var toAdd = selected.Except(currentSet, StringComparer.OrdinalIgnoreCase).ToList();
			var toRemove = currentSet.Except(selected, StringComparer.OrdinalIgnoreCase).ToList();

			if (toAdd.Count > 0)
			{
				var addRes = await _userManager.AddToRolesAsync(user, toAdd);
				if (!addRes.Succeeded)
				{
					foreach (var err in addRes.Errors) ModelState.AddModelError(string.Empty, err.Description);
					return View(vm);
				}
			}

			if (toRemove.Count > 0)
			{
				var remRes = await _userManager.RemoveFromRolesAsync(user, toRemove);
				if (!remRes.Succeeded)
				{
					foreach (var err in remRes.Errors) ModelState.AddModelError(string.Empty, err.Description);
					return View(vm);
				}
			}

			// força revalidação do cookie para refletir roles/claims mais rápido
			await _userManager.UpdateSecurityStampAsync(user);

			TempData["success"] = "Roles do usuário atualizadas.";
			return RedirectToAction(nameof(Index));
		}
	}
}

