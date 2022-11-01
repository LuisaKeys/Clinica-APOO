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
    public class ExamesController : Controller
    {
        private EFContext context = new EFContext();

 
        // GET: Exames
        public ActionResult Index()
        {
            return View(context.Exames.ToList());
        }

        // GET: Exames/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExameId,Descricao")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                context.Exames.Add(exame);
                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exame);
        }

        // GET: Exames/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exame exame = context.Exames.Find(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // POST: Exames/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExameId,Descricao")] Exame exame)
        {
            if (ModelState.IsValid)
            {
                context.Entry(exame).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exame);
        }

        // GET: Exames/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exame exame = context.Exames.Find(id);
            if (exame == null)
            {
                return HttpNotFound();
            }
            return View(exame);
        }

        // POST: Exames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Exame exame = context.Exames.Find(id);
            context.Exames.Remove(exame);
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
