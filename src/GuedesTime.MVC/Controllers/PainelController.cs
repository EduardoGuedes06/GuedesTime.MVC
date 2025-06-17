using AutoMapper;
using GuedesTime.Domain.Intefaces;
using GuedesTime.MVC.Models;
using GuedesTime.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
	[Authorize]
	public class PainelController : BaseController
	{

		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;

		public PainelController(IMapper mapper, INotificador notificador, UserManager<ApplicationUser> userManager) : base(notificador)
		{
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index()
		{
			var painelViewModel = new PainelViewModel();
			painelViewModel.TotalProfessores = 15;
			painelViewModel.TotalTurmas = 8;
			painelViewModel.TotalDisciplinas = 25;
			painelViewModel.TotalSalas = 10;
			painelViewModel.PlanosPendentes = 2;
			painelViewModel.PlanosAprovados = 5;
			painelViewModel.PlanosComConflito = 1;
			painelViewModel.ProgressoPercentual = 75;
			return View(painelViewModel);
		}
	}
}
