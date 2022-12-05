using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Modelo.Models;
using Relacionamento.Servico;

namespace Relacionamento.Controllers
{
    public class PetController : Controller
    {
        private ActionResult ObterVisaoPetPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = petServico.ObterPetPorId((long)id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }

        // Metodo Privado
        private ActionResult GravarPet(Pet pet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    petServico.GravarPet(pet);
                    return RedirectToAction("Index");
                }
                return View(pet);
            }
            catch
            {
                return View(pet);
            }
        }

        private PetServico petServico = new PetServico();
        private ClienteServico clienteServico = new ClienteServico();
        private EspecieServico especieServico = new EspecieServico();

        // GET: Pets
        public ActionResult Index()
        {
            return View(petServico.ObterPetsClassificadosPorNome());
        }

        // GET: Pets/Details/5
        public ActionResult Details(long? id)
        {
            return ObterVisaoPetPorId(id);
        }

        // GET: Pets/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(clienteServico.ObterClientesClassificadosPorNome(), "Id", "Nome");
            ViewBag.EspecieId = new SelectList(especieServico.ObterEspeciesClassificadasPorNome(), "Id", "Nome");
            return View();
        }

        // POST: Pets/Create
        [HttpPost]
        public ActionResult Create(Pet pet)
        {
            ViewBag.ClienteId = new SelectList(clienteServico.ObterClientesClassificadosPorNome(), "Id", "Nome");
            ViewBag.EspecieId = new SelectList(especieServico.ObterEspeciesClassificadasPorNome(), "Id", "Nome");
            return GravarPet(pet);
        }

        // GET: Pets/Edit/5
        public ActionResult Edit(long? id)
        {
            ViewBag.ClienteId = new SelectList(clienteServico.ObterClientesClassificadosPorNome(), "Id", "Nome");
            ViewBag.EspecieId = new SelectList(especieServico.ObterEspeciesClassificadasPorNome(), "Id", "Nome");
            return ObterVisaoPetPorId(id);
        }

        // POST: Pets/Edit/5
        [HttpPost]
        public ActionResult Edit(Pet pet)
        {
            ViewBag.ClienteId = new SelectList(clienteServico.ObterClientesClassificadosPorNome(), "ClienteId", "Nome");
            ViewBag.EspecieId = new SelectList(especieServico.ObterEspeciesClassificadasPorNome(), "EspecieId", "Nome");
            return GravarPet(pet);
        }

        // GET: Pets/Delete/5
        public ActionResult Delete(long? id)
        {
            return ObterVisaoPetPorId(id);
        }

        // POST: Pets/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pet pet = petServico.EliminarPetPorId(id);
                TempData["Message"] = "Pet " + pet.Nome.ToUpper() + " foi removido";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}