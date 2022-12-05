using Modelo.Models;
using Persistencia.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.DAL
{
    public class VeterinarioDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Veterinario> ObterVeterinariosClassificadosPorNome()
        {
            return context.Veterinarios.OrderBy(b => b.Nome);
        }
        public Veterinario ObterVeterinarioPorId(long id)
        {
            return context.Veterinarios.Where(f => f.Id == id).Include("Consultas.Veterinario").First();
        }
        public void GravarVeterinario(Veterinario veterinario)
        {
            if (veterinario.Id == 0)
            {
                context.Veterinarios.Add(veterinario);
            }
            else
            {
                context.Entry(veterinario).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Veterinario EliminarVeterinarioPorId(long id)
        {
            Veterinario veterinario = ObterVeterinarioPorId(id);
            context.Veterinarios.Remove(veterinario);
            context.SaveChanges();
            return veterinario;
        }
    }
}

