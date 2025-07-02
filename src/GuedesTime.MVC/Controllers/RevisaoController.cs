using AutoMapper;
using GuedesTime.Domain.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class RevisaoController : BaseController
    {
        private readonly IMapper _mapper;

        public RevisaoController(IMapper mapper, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
        }


        // GET: RestricaoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RestricaoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RestricaoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RestricaoController/Create
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

        // GET: RestricaoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RestricaoController/Edit/5
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

        // GET: RestricaoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RestricaoController/Delete/5
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
