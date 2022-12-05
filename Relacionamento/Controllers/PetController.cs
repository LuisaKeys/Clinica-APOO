using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Relacionamento.Servico;
using Persistencia.DAL;
using Modelo.Models;
using System.Net;

namespace Relacionamento.Controllers
{
    public class PetController : Controller
    {
        private PetServico petServico = new PetServico();
        private ClienteServico clienteServico = new ClienteServico();
        private EspecieServico especieServico = new EspecieServico();

        //Pega os detalhes do produto de acordo com o id, serve para diminuir a redundância na hora de mostrar vz
        private ActionResult ObterVisaoPetPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pet pet = petServico.ObterPetPorId(long? id);
            if (pet == null)
            {
                return HttpNotFound();
            }
            return View(pet);
        }
        // Serve para popular combobox
        private void PopularViewBag(Pet pet = null)
        {
            if (pet == null)
            {
                ViewBag.ClienteId = new SelectList(clienteServico.ObterClientesClassificadosPorNome(),
                "CategoriaId", "Nome");
                ViewBag.EspecieId = new SelectList(especieServico.ObterEspeciesClassificadasPorNome(),
                "EstudioId", "Nome");
            }
            else
            {
                ViewBag.ClienteId = new SelectList(clienteServico.ObterClientesClassificadosPorNome(),
                "CategoriaId", "Nome", pet.ClienteId);
                ViewBag.EstudioId = new SelectList(especieServico.ObterEspeciesClassificadasPorNome(),
                "EspecieId", "Nome", pet.EspecieId);
            }
        }

        // Salva os produtos
        private ActionResult GravarPet(Pet pet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    petServico.GravarPet(pet);
                    return RedirectToAction("Index");
                }
                petServico.GravarPet(pet);
                PopularViewBag(pet);
                return View(pet);
            }
            catch
            {
                PopularViewBag(pet);
                return View(pet);
            }
        }

        //---------------------- ACTIONS ABAIXO -----------------------//
        //GET: Produtos
        [Authorize(Roles = "Administradores")]
        public ActionResult Index()
        {
            return View(petServico.ObterPetsClassificadosPorNome());
        }


        //// GET: Produto/Details/5
        //[Authorize]
        //public ActionResult Details(long? id)
        //{
        //    return ObterVisaoProdutoPorId(id);
        //}

        // GET: Produto/Create
        [Authorize]
        public ActionResult Create()
        {
            PopularViewBag();
            return View();
        }

        // POST: Produtos/Create
        [HttpPost]
        public ActionResult Create(Pet pet)
        {
            return GravarPet(pet);
        }

        //// GET: Produto/Edit/5
        //[Authorize]
        //public ActionResult Edit(long? id)
        //{
        //    PopularViewBag(produtoServico.ObterProdutoPorId((long)id));
        //    return ObterVisaoProdutoPorId(id);
        //}

        //// POST: Produto/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Produto produto, HttpPostedFileBase upimg = null, string chkRemoverImagem = null)
        //{
        //    return GravarProduto(produto, upimg, chkRemoverImagem);
        //}

        //// GET: Produto/Delete/5
        //[Authorize]
        //public ActionResult Delete(long? id)
        //{
        //    return ObterVisaoProdutoPorId(id);
        //}

        //// POST: Produto/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        Produto produto = produtoServico.EliminarProdutoPorId(id);
        //        TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}