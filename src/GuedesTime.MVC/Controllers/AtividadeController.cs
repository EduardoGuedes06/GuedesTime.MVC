using AutoMapper;
using GuedesTime.Domain.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class AtividadeController : BaseController
    {
        private readonly IMapper _mapper;

        public AtividadeController(IMapper mapper, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
        }


        // GET: AtividadeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AtividadeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AtividadeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AtividadeController/Create
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

        // GET: AtividadeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AtividadeController/Edit/5
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

        // GET: AtividadeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AtividadeController/Delete/5
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
