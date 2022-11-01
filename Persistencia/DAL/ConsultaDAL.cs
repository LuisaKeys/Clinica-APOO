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
   public class ConsultaDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Consulta> ObterConsultasClassificadosPorNome()
        {
            return context.Consultas.Include(c => c.Exame).OrderBy(n => n.data_hora);
        }
        public Consulta ObterConsultaPorId(long id)
        {
            return context.Consultas.Where(p => p.ConsultaId == id).Include(c => c.Exame).First();
        }
        public void GravarProduto(Consulta consulta)
        {
            if (consulta.ConsultaId == null)
            {
                context.Consultas.Add(consulta);
            }
            else
            {
                context.Entry(consulta).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Consulta EliminarConsultaPorId(long id)
        {
            Consulta consulta = ObterConsultaPorId(id);
            context.Consultas.Remove(consulta);
            context.SaveChanges();
            return consulta;
        }
    }
}
