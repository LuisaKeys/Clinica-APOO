using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Models;
using Persistencia.DAL;

namespace apoo_clinicavet.Servico
{
    public class ExameServico
    {
        private ExameDAL exameDAL = new ExameDAL();
        public IQueryable<Exame> ObterExamesClassificadosPorDesc()
        {
            return exameDAL.ObterExamesClassificadosPorDesc();
        }
        public Exame ObterExamePorId(long id)
        {
            return exameDAL.ObterExamePorId(id);
        }
        public void GravarExame(Exame exame)
        {
            exameDAL.GravarExame(exame);
        }
        public Exame EliminarExamePorId(long id)
        {
            Exame exame = exameDAL.ObterExamePorId(id);
            exameDAL.EliminarExamePorId(id);
            return exame;
        }
    }
}