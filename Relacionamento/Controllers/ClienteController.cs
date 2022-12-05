using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Persistencia.Context;
using Relacionamento.Servico;
using Modelo.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web.Services.Description;
using Modelo.Models;
using Persistencia.Context;
using Relacionamento.Servico;

namespace Relacionamento.Controllers
{
    public class ClienteController : Controller
    {
        private ClienteServico clienteServico = new ClienteServico();
        private ActionResult ObterVisaoClientePorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(
                HttpStatusCode.BadRequest);
            }
            Cliente cliente = clienteServico.ObterClientePorId((long)id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        private ActionResult GravarCliente(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    clienteServico.GravarCliente(cliente);
                    return RedirectToAction("Index");
                }
                return View(cliente);
            }
            catch
            {
                return View(cliente);
            }
        }
        // GET: Clientes
        public ActionResult Index()
        {
            return View(clienteServico.ObterClientesClassificadosPorNome());
        }
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {
            return GravarCliente(cliente);
        }
        // GET: Edit
        public ActionResult Edit(long? id)
        {
            return ObterVisaoClientePorId(id);
        }
        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Cliente cliente)
        {
            return GravarCliente(cliente);
        }
        //GET: Details
        public ActionResult Details(int id)
        {
            return ObterVisaoClientePorId(id);
        }

        // GET: Delete
        public ActionResult Delete(int? id)
        {
            return ObterVisaoClientePorId(id);
        }
        // POST: Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Cliente cliente = clienteServico.EliminarClientePorId(id);
                TempData["Message"] = "Cliente " + cliente.Nome.ToUpper() + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
