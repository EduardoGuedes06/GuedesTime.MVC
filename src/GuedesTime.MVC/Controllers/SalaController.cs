using AutoMapper;
using GuedesTime.Domain.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class SalaController : BaseController
    {
        private readonly IMapper _mapper;

        public SalaController(IMapper mapper, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
        }


        // GET: SalaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SalaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalaController/Create
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

        // GET: SalaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalaController/Edit/5
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

        // GET: SalaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalaController/Delete/5
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
