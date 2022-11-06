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

namespace apoo_clinicavet.Controllers
{
    public class ConsultasController : Controller
    {
        private ActionResult ObterVisaoConsultaPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Consulta consulta = consultaServico.ObterConsultaPorId((long)id);
            if (consulta == null)
            {
                return HttpNotFound();
            }
            return View(consulta);
        }

        // Metodo Privado
        private void PopularViewBag(Consulta consulta = null)
        {
            if (consulta == null)
            {
                ViewBag.ExameId = new SelectList(exameServico.ObterExamesClassificadosPorDesc(),
                "ExameId", "Descricao");
            }
            else
            {
                ViewBag.ExameId = new SelectList(exameServico.ObterExamesClassificadosPorDesc(),
                "ExameId", "Descricao", consulta.ExameId);
            }
        }

        // Metodo Privado
        private ActionResult GravarConsulta(Consulta consulta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    consultaServico.GravarConsulta(consulta);
                    return RedirectToAction("Index");
                }
                return View(consulta);
            }
            catch
            {
                PopularViewBag(consulta);
                return View(consulta);
            }
        }

        private ConsultaServico consultaServico = new ConsultaServico();
        private ExameServico exameServico = new ExameServico();
        
        // GET: Produtos
        public ActionResult Index()
        { 
            return View(consultaServico.ObterConsultasClassificadasPorData());
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                consultaServico.GravarConsulta(consulta);
                return RedirectToAction("Index");
            }
            ViewBag.ExameId = new SelectList(exameServico.ObterExamesClassificadosPorDesc(), "ExameId", "Descricao", consulta.ExameId);
            return View(consulta);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(long? id)
        {
            return ObterVisaoConsultaPorId(id);
        }

        // POST: Produtos/Edit/5
        [HttpPost]
        public ActionResult Edit(Consulta consulta)
        {
            return GravarConsulta(consulta);
        }

        // GET: Produtos/Details/5
        public ActionResult Details(long? id)
        {
            return ObterVisaoConsultaPorId(id);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(long? id)
        {
            return ObterVisaoConsultaPorId(id);
        }

        // POST: Produtos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Consulta consulta = consultaServico.EliminarConsultaPorId(id);
                TempData["Message"] = "Consulta de Exame " + consulta.Exame.Descricao.ToUpper() + " foi removida";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

