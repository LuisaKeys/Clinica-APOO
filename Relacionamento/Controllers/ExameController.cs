using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Modelo.Models;
using Persistencia.Context;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web.Services.Description;
using Relacionamento.Servico;
using Modelo.Models;
using Persistencia.Context;

namespace Relacionamento.Controllers
{
    public class ExameController : Controller
    {
        private ExameServico exameServico = new ExameServico();
        private ActionResult ObterVisaoExamePorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Exame exame = exameServico.ObterExamePorId((long)id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        private ActionResult GravarExame(Exame exame)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    exameServico.GravarExame(exame);
                    return RedirectToAction("Index");
                }
                return View(exame);
            }
            catch
            {
                return View(exame);
            }
        }
        // GET: Exames
        public ActionResult Index()
        {
            return View(exameServico.ObterExamesClassificadosPorDesc());
        }
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Exame exame)
        {
            return GravarExame(exame);
        }
        // GET: Edit
        public ActionResult Edit(long? id)
        {
            return ObterVisaoExamePorId(id);
        }
        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Exame exame)
        {
            return GravarExame(exame);
        }
        
        // GET: Delete
        public ActionResult Delete(long? id)
        {
            return ObterVisaoExamePorId(id);
        }
        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            try
            {
                Exame exame = exameServico.EliminarExamePorId(id);
                TempData["Message"] = "Exame " + exame.Descricao.ToUpper() + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}