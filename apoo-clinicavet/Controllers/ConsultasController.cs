using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Modelo.Models;
using Persistencia.Context;

namespace apoo_clinicavet.Controllers
{
    public class ConsultasController : Controller
    {
        private EFContext context = new EFContext();

        // GET: Consultas
        public ActionResult Index()
        {
            var consultas = context.Consultas.Include(c => c.Exame);
            return View(consultas.ToList());
        }

        // GET: Consultas/Create
        public ActionResult Create()
        {
            ViewBag.ExameId = new SelectList(context.Exames, "ExameId", "Descricao");
            return View();
        }

        // POST: Consultas/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsultaId,data_hora,Sintomas,ExameId")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                context.Consultas.Add(consulta);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExameId = new SelectList(context.Exames, "ExameId", "Descricao", consulta.ExameId);
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = context.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExameId = new SelectList(context.Exames, "ExameId", "Descricao", consulta.ExameId);
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConsultaId,data_hora,Sintomas,ExameId")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                context.Entry(consulta).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExameId = new SelectList(context.Exames, "ExameId", "Descricao", consulta.ExameId);
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = context.Consultas.Find(id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Consulta consulta = context.Consultas.Find(id);
            context.Consultas.Remove(consulta);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
