using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Relacionamento.Servico;
using Modelo.Models;
using System.Net;

namespace Relacionamento.Controllers
{
    public class VeterinarioController : Controller
    {
        private VeterinarioServico veterinarioServico = new VeterinarioServico();
        private ActionResult ObterVisaoVeterinarioPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Veterinario veterinario = veterinarioServico.ObterVeterinarioPorId((long)id);
            if (veterinario == null)
            {
                return HttpNotFound();
            }
            return View(veterinario);
        }

        private ActionResult GravarVeterinario(Veterinario veterinario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    veterinarioServico.GravarVeterinario(veterinario);
                    return RedirectToAction("Index");
                }
                return View(veterinario);
            }
            catch
            {
                return View(veterinario);
            }
        }
        // GET: Veterinarios
        public ActionResult Index()
        {
            return View(veterinarioServico.ObterVeterinariosClassificadosPorNome());
        }
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Veterinario veterinario)
        {
            return GravarVeterinario(veterinario);
        }
        // GET: Edit
        public ActionResult Edit(long? id)
        {
            return ObterVisaoVeterinarioPorId(id);
        }
        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Veterinario veterinario)
        {
            return GravarVeterinario(veterinario);
        }

        // GET: Delete
        public ActionResult Delete(int? id)
        {
            return ObterVisaoVeterinarioPorId(id);
        }
        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Veterinario veterinario = veterinarioServico.EliminarVeterinarioPorId(id);
                TempData["Message"] = "Veterinario " + veterinario.Nome.ToUpper() + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}