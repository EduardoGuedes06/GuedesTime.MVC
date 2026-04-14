using System.Security.Claims;
using GuedesTime.MVC.Security;
using GuedesTime.MVC.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Policy = "Permission:Admin")]
	public class RolesController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RolesController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var roles = _roleManager.Roles
				.OrderBy(r => r.Name)
				.Select(r => new RoleListItemVm
				{
					Id = r.Id,
					Name = r.Name ?? string.Empty
				})
				.ToList();

			return View(roles);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			var vm = new RoleEditVm
			{
				AllPermissions = Permissions.All.ToList(),
				SelectedPermissions = new List<string>()
			};

			return View("Edit", vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(RoleEditVm vm)
		{
			vm.AllPermissions = Permissions.All.ToList();

			if (string.IsNullOrWhiteSpace(vm.Name))
			{
				ModelState.AddModelError(nameof(vm.Name), "Nome da role é obrigatório.");
			}

			if (!ModelState.IsValid)
				return View("Edit", vm);

			var role = new IdentityRole(vm.Name.Trim());
			var result = await _roleManager.CreateAsync(role);
			if (!result.Succeeded)
			{
				foreach (var err in result.Errors) ModelState.AddModelError(string.Empty, err.Description);
				return View("Edit", vm);
			}

			await SyncRolePermissions(role, vm.SelectedPermissions ?? new List<string>());
			TempData["success"] = "Role criada com sucesso.";
			return RedirectToAction(nameof(Index));
		}

		[HttpGet]
		public async Task<IActionResult> Edit(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if (role is null) return NotFound();

			var claims = await _roleManager.GetClaimsAsync(role);
			var selected = claims
				.Where(c => c.Type == PermissionClaimTypes.Permission)
				.Select(c => c.Value)
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToList();

			var vm = new RoleEditVm
			{
				Id = role.Id,
				Name = role.Name ?? string.Empty,
				AllPermissions = Permissions.All.ToList(),
				SelectedPermissions = selected
			};

			return View(vm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(RoleEditVm vm)
		{
			vm.AllPermissions = Permissions.All.ToList();
			if (string.IsNullOrWhiteSpace(vm.Id)) return BadRequest();

			var role = await _roleManager.FindByIdAsync(vm.Id);
			if (role is null) return NotFound();

			if (string.IsNullOrWhiteSpace(vm.Name))
			{
				ModelState.AddModelError(nameof(vm.Name), "Nome da role é obrigatório.");
				return View(vm);
			}

			role.Name = vm.Name.Trim();
			var update = await _roleManager.UpdateAsync(role);
			if (!update.Succeeded)
			{
				foreach (var err in update.Errors) ModelState.AddModelError(string.Empty, err.Description);
				return View(vm);
			}

			await SyncRolePermissions(role, vm.SelectedPermissions ?? new List<string>());
			TempData["success"] = "Role atualizada com sucesso.";
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if (role is null) return NotFound();

			if (string.Equals(role.Name, "Admin", StringComparison.OrdinalIgnoreCase))
			{
				TempData["error"] = "A role Admin não pode ser removida.";
				return RedirectToAction(nameof(Index));
			}

			var result = await _roleManager.DeleteAsync(role);
			if (!result.Succeeded)
			{
				TempData["error"] = string.Join("; ", result.Errors.Select(e => e.Description));
				return RedirectToAction(nameof(Index));
			}

			TempData["success"] = "Role removida com sucesso.";
			return RedirectToAction(nameof(Index));
		}

		private async Task SyncRolePermissions(IdentityRole role, List<string> selectedPermissions)
		{
			var normalizedSelected = selectedPermissions
				.Where(p => !string.IsNullOrWhiteSpace(p))
				.Select(p => p.Trim())
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			// Garante somente permissões conhecidas
			normalizedSelected.RemoveWhere(p => !Permissions.All.Contains(p));

			var claims = await _roleManager.GetClaimsAsync(role);
			var current = claims
				.Where(c => c.Type == PermissionClaimTypes.Permission)
				.Select(c => c.Value)
				.ToHashSet(StringComparer.OrdinalIgnoreCase);

			var toAdd = normalizedSelected.Except(current, StringComparer.OrdinalIgnoreCase).ToList();
			var toRemove = current.Except(normalizedSelected, StringComparer.OrdinalIgnoreCase).ToList();

			foreach (var p in toAdd)
				await _roleManager.AddClaimAsync(role, new Claim(PermissionClaimTypes.Permission, p));

			foreach (var p in toRemove)
				await _roleManager.RemoveClaimAsync(role, new Claim(PermissionClaimTypes.Permission, p));
		}
	}
}

