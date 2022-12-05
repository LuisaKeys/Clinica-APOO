using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistencia.Context;
using Modelo.Models;


namespace Persistencia.DAL
{
    public class ExameDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Exame> ObterExamesClassificadosPorDesc()
        {
            return context.Exames.OrderBy(b => b.Descricao);
        }
        public Exame ObterExamePorId(long id)
        {
            return context.Exames.Where(c => c.Id == id).First();
        }
        public void GravarExame(Exame exame)
        {
            if (exame.Id == 0)
            {
                context.Exames.Add(exame);
            }
            else
            {
                context.Entry(exame).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Exame EliminarExamePorId(long id)
        {
            Exame exame = ObterExamePorId(id);
            context.Exames.Remove(exame);
            context.SaveChanges();
            return exame;
        }
    }
}

