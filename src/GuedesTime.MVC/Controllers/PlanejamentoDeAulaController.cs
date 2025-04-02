using AutoMapper;
using GuedesTime.Domain.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class PlanejamentoDeAulaController : BaseController
    {
        private readonly IMapper _mapper;

        public PlanejamentoDeAulaController(IMapper mapper, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
        }


        // GET: PlanejamentoDeAulaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PlanejamentoDeAulaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlanejamentoDeAulaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlanejamentoDeAulaController/Create
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

        // GET: PlanejamentoDeAulaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlanejamentoDeAulaController/Edit/5
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

        // GET: PlanejamentoDeAulaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlanejamentoDeAulaController/Delete/5
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
