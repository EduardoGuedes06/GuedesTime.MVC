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
	public class PlanoDeAulaController : BaseController
	{

		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;

		public PlanoDeAulaController(IMapper mapper, INotificador notificador, UserManager<ApplicationUser> userManager) : base(notificador)
		{
			_mapper = mapper;
			_userManager = userManager;
		}

		public ActionResult Index()
		{
			return View();
		}
	}
}
