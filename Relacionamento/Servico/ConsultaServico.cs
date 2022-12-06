using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Models;
using Modelo.ViewModels;
using Persistencia.DAL;

namespace Relacionamento.Servico
{
    public class ConsultaServico
    {
        private ConsultaDAL consultaDAL = new ConsultaDAL();

        public IQueryable<Consulta> ObterConsultasClassificadasPorData()
        {
            return consultaDAL.ObterConsultasClassificadasPorData();
            
        }
        public Consulta ConsultaOriginal(ConsultaViewModel consultaViewModel)
        {
            return consultaDAL.ConsultaOriginal(consultaViewModel);
        }
        public Consulta ObterConsultaPorId(long id)
        {
            return consultaDAL.ObterConsultaPorId(id);
        }
        public ICollection<ExameVinculado> PopularExames()
        {
            return consultaDAL.PopularExames();
        }


        public void AddOrUpdateExames(Consulta consulta, IEnumerable<ExameVinculado> examesvinculados)
        {
            consultaDAL.AddOrUpdateExames(consulta, examesvinculados);
        }
        //public void AddOrUpdateKeepExistingExames(Consulta consulta, IEnumerable<ExameVinculado> examesVinculados)
        //{
        //    consultaDAL.AddOrUpdateKeepExistingExames(consulta, examesVinculados);
        //}
        public void GravarConsulta(Consulta consulta)
        {
            consultaDAL.GravarConsulta(consulta);
        }

    }
}
