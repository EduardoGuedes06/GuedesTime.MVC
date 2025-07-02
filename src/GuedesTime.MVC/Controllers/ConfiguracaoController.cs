using AutoMapper;
using GuedesTime.Domain.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class ConfiguracaoController : BaseController
    {
        private readonly IMapper _mapper;

        public ConfiguracaoController(IMapper mapper, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
        }


        // GET: ConfiguracoesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ConfiguracoesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ConfiguracoesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ConfiguracoesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConfiguracoesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ConfiguracoesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ConfiguracoesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ConfiguracoesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
