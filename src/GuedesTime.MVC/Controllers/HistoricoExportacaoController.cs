using AutoMapper;
using GuedesTime.Domain.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuedesTime.MVC.Controllers
{
    [Authorize]
    public class HistoricoExportacao : BaseController
    {
        private readonly IMapper _mapper;

        public HistoricoExportacao(IMapper mapper, INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
        }


        // GET: HistoricoExportacao
        public ActionResult Index()
        {
            return View();
        }

        // GET: HistoricoExportacao/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HistoricoExportacao/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HistoricoExportacao/Create
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

        // GET: HistoricoExportacao/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HistoricoExportacao/Edit/5
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

        // GET: HistoricoExportacao/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HistoricoExportacao/Delete/5
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
