using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Persistencia.Context;
using Modelo.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web.Services.Description;
using Relacionamento.Servico;



namespace Relacionamento.Controllers
{
    public class EspecieController : Controller
    {
        private EspecieServico especieServico = new EspecieServico();
        private ActionResult ObterVisaoEspeciePorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Especie especie = especieServico.ObterEspeciePorId((long)id);
            if (especie == null)
            {
                return HttpNotFound();
            }
            return View(especie);
        }

        private ActionResult GravarEspecie(Especie especie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    especieServico.GravarEspecie(especie);
                    return RedirectToAction("Index");
                }
                return View(especie);
            }
            catch
            {
                return View(especie);
            }
        }
        // GET: Especies
        public ActionResult Index()
        {
            return View(especieServico.ObterEspeciesClassificadasPorNome());
        }
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Especie especie)
        {
            return GravarEspecie(especie);
        }
        // GET: Edit
        public ActionResult Edit(long? id)
        {
            return ObterVisaoEspeciePorId(id);
        }
        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Especie especie)
        {
            return GravarEspecie(especie);
        }

        // GET: Delete
        public ActionResult Delete(long? id)
        {
            return ObterVisaoEspeciePorId(id);
        }
        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Especie especie = especieServico.EliminarEspeciePorId(id);
                TempData["Message"] = "Espécie " + especie.Nome.ToUpper() + " foi removida";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
