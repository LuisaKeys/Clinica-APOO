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
using apoo_clinicavet.Servico;
using Servico.Cadastros;
using System.Runtime.Remoting.Contexts;
using System.Web.Services.Description;

namespace apoo_clinicavet.Controllers
{
    public class ConsultasController : Controller
    {
        private EFContext context = new EFContext();

        // GET: Consultas
        public ActionResult Index()
        {
            return View(context.Consultas.ToList());
        }

        // GET: Consultas/Create
        public ActionResult Create()
        {
            ViewBag.Exames = context.Exames.ToList();
            return View();
        }

        // POST: Consultas/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsultaId,data_hora,Sintomas,ExameId")] Consulta consulta)
        {
            var lstExames = Request.Form["chkExames"];
            if (!string.IsNullOrEmpty(lstExames))
            {
                int[] splExames = lstExames.Split(',').Select(Int32.Parse).ToArray();

                if (splExames.Count() > 0)
                {
                    var PostExames = context.Exames.Where(w => splExames.Contains(w.ExameId)).ToList();

                    consulta.Exames.AddRange(PostExames);
                }
            }
            if (ModelState.IsValid)
            {
                context.Consultas.Add(consulta);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

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

