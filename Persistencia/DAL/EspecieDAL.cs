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
    public class EspecieDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Especie> ObterEspeciesClassificadasPorNome()
        {
            return context.Especies.OrderBy(b => b.Nome);
        }
        public Especie ObterEspeciePorId(long id)
        {
            return context.Especies.Where(c => c.Id == id).First();
        }
        public void GravarEspecie(Especie especie)
        {
            if (especie.Id == 0)
            {
                context.Especies.Add(especie);
            }
            else
            {
                context.Entry(especie).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Especie EliminarEspeciePorId(long id)
        {
            Especie especie = ObterEspeciePorId(id);
            context.Especies.Remove(especie);
            context.SaveChanges();
            return especie;
        }
    }
}

