using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Modelo.Models;
using Modelo.ViewModels;
using Persistencia.Context;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web.Services.Description;
using Persistencia.DAL;
using Relacionamento.Servico;

namespace Relacionamento.Controllers
{
    public class ConsultaController : Controller
    {
        private ConsultaServico consultaServico = new ConsultaServico();
        private PetServico petServico = new PetServico();
        private VeterinarioServico veterinarioServico = new VeterinarioServico();
        private ExameDAL exameDAL = new ExameDAL();
        private EFContext context = new EFContext();

        //private ICollection<ExameVinculado> PopularExames()
        //{
        //    var exames = context.Exames;
        //    var examesvinculados = new List<ExameVinculado>();

        //    foreach (var item in exames)
        //    {
        //        examesvinculados.Add(new ExameVinculado
        //        {
        //            Id = item.Id,
        //            Descricao = item.Descricao,
        //            Vinculado = false
        //        });
        //    }

        //    return examesvinculados;
        //}

        //private void AddOrUpdateExames(Consulta consulta, IEnumerable<ExameVinculado> examesvinculados)
        //{
        //    if (examesvinculados == null) return;

        //    if (consulta.Id != 0)
        //    {
        //        // consulta existente - apaga exames existentes e adiciona novos (se tiver)
        //        foreach (var exame in consulta.Exames.ToList())
        //        {
        //            consulta.Exames.Remove(exame);
        //        }

        //        foreach (var exame in examesvinculados.Where(c => c.Vinculado))
        //        {
        //            consulta.Exames.Add(context.Exames.Find(exame.Id));
        //        }
        //    }
        //    else
        //    {
        //        // Nova Consulta
        //        foreach (var exameVinculado in examesvinculados.Where(c => c.Vinculado))
        //        {
        //            var exame = new Exame { Id = exameVinculado.Id };
        //            context.Exames.Attach(exame);
        //            consulta.Exames.Add(exame);
        //        }
        //    }
        //}

        //private void AddOrUpdateKeepExistingExames(Consulta consulta, IEnumerable<ExameVinculado> examesVinculados)
        //{
        //    var webExameVinculadoId =  examesVinculados.Where(c => c.Vinculado).Select(webExame => webExame.Id);
        //    var contextExameIDs = consulta.Exames.Select(contextExame => contextExame.Id);
        //    var exameIDs = contextExameIDs as int[] ?? contextExameIDs.ToArray();
        //    var examesToDeleteIDs = exameIDs.Where(id => !webExameVinculadoId.Contains(id)).ToList();

        //    // Apaga exames removidos
        //    foreach (var id in examesToDeleteIDs)
        //    {
        //       consulta.Exames.Remove(context.Exames.Find(id));
        //    }

        //    // Adiciona exames que não tenham sido usados
        //    foreach (var id in webExameVinculadoId)
        //    {
        //        if (!exameIDs.Contains(id))
        //        {
        //            consulta.Exames.Add(context.Exames.Find(id));
        //        }
        //    }
        //}


        // ACTIONS ABAIXO
        // GET: Consultas
        public ActionResult Index()
        {
            var consultas = consultaServico.ObterConsultasClassificadasPorData().ToList();
            var consultaViewModels = consultas.Select(consulta => consulta.ToViewModel()).ToList();
            return View(consultaViewModels);
        }

        //GET: Create
        public ActionResult Create()
        {
            ViewBag.PetId = new SelectList(petServico.ObterPetsClassificadosPorNome(),
            "PetId", "Nome");
            ViewBag.VeterinarioId = new SelectList(veterinarioServico.ObterVeterinariosClassificadosPorNome(),
            "Id", "Nome");
            var consultaViewModel = new ConsultaViewModel { Exames = consultaServico.PopularExames() };

            return View(consultaViewModel);
        }

        //POST: Create
        [HttpPost]
        public ActionResult Create(ConsultaViewModel consultaViewModel)
        {
            if (ModelState.IsValid)
            {
                var consulta = consultaViewModel.ToDomainModel();
                consultaServico.AddOrUpdateExames(consulta, consultaViewModel.Exames);
                consultaServico.GravarConsulta(consulta);

                return RedirectToAction("Index");
            }
            ViewBag.PetId = new SelectList(petServico.ObterPetsClassificadosPorNome(),
            "PetId", "Nome");
            ViewBag.VeterinarioId = new SelectList(veterinarioServico.ObterVeterinariosClassificadosPorNome(),
            "VeterinarioId", "Nome");
            return View(consultaViewModel);
        }

        //GET: Edit
        public ActionResult Edit(int id = 0)
        {
            //recupera todos os exames
            //exameDAL.TodosExamesBD().ToList();

            //adiciona ou atualiza exames mantendo original
            var consulta = context.Consultas.Include("Exames").FirstOrDefault(x => x.Id == id);
            var consultaViewModel = consulta.ToViewModel(exameDAL.TodosExamesBD().ToList());
            ViewBag.PetId = new SelectList(context.Pets.OrderBy(b => b.Nome), "PetId", "Nome");
            ViewBag.VeterinarioId = new SelectList(context.Veterinarios.OrderBy(b => b.Nome),
            "Id", "Nome");

            return View(consultaViewModel);
        }

        //POST: Edit
        [HttpPost]
        public ActionResult Edit(ConsultaViewModel consultaViewModel)
        {
            if (ModelState.IsValid)
            {
                var ConsultaOriginal = context.Consultas.Find(consultaViewModel.Id);

                // adiciona ou atualiza mantendo original
                consultaServico.AddOrUpdateKeepExistingExames(ConsultaOriginal, consultaViewModel.Exames);

                context.Entry(ConsultaOriginal).CurrentValues.SetValues(consultaViewModel);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.PetId = new SelectList(context.Pets.OrderBy(b => b.Nome), "PetId", "Nome");
            ViewBag.VeterinarioId = new SelectList(context.Veterinarios.OrderBy(b => b.Nome),
            "VeterinarioId", "Nome");
            return View(consultaViewModel);
        }

        //GET: Details
        public ActionResult Details(int id = 0)
        {
            // retorna todos os exames
            var todosExamesBD = exameDAL.TodosExamesBD().ToList();

            // retorna a consulta a ser editada e inclui os exames vinculados
            var consulta = context.Consultas.Include("Exames").FirstOrDefault(x => x.Id == id);
            var consultaViewModel = consulta.ToViewModel(todosExamesBD);

            return View(consultaViewModel);
        }
        //GET: Delete
        public ActionResult Delete(int id = 0)
        {
            var consultaIQueryable = from u in context.Consultas.Include("Exames")
                                     where u.Id == id
                                     select u;


            if (!consultaIQueryable.Any())
            {
                return HttpNotFound("Consulta não encontrada.");
            }

            var consulta = consultaIQueryable.First();
            var consultaViewModel = consulta.ToViewModel();
            ViewBag.PetId = new SelectList(context.Pets.OrderBy(b => b.Nome), "PetId", "Nome");
            ViewBag.VeterinarioId = new SelectList(context.Veterinarios.OrderBy(b => b.Nome),
            "Id", "Nome");
            return View(consultaViewModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var consulta = context.Consultas.Include("Exames").Single(u => u.Id == id);
            DeleteConsulta(consulta);

            return RedirectToAction("Index");
        }

        private void DeleteConsulta(Consulta consulta)
        {
            if (consulta.Exames != null)
            {
                foreach (var exame in consulta.Exames.ToList())
                {
                    consulta.Exames.Remove(exame);
                }
            }

            context.Consultas.Remove(consulta);
            context.SaveChanges();
        }
    }
}