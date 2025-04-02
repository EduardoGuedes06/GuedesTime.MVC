using AutoMapper;
using GuedesTime.Domain.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class Feriado : BaseController
    {
        private readonly IMapper _mapper;

        public Feriado(IMapper mapper, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
        }


        // GET: Feriado
        public ActionResult Index()
        {
            return View();
        }

        // GET: Feriado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Feriado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feriado/Create
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

        // GET: Feriado/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Feriado/Edit/5
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

        // GET: Feriado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Feriado/Delete/5
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
