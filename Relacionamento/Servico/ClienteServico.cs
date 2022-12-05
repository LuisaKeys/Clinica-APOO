using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Persistencia.DAL;
using Modelo.Models;

namespace Relacionamento.Servico
{
    public class ClienteServico
    {
        private ClienteDAL clienteDAL = new ClienteDAL();
        public IQueryable<Cliente> ObterClientesClassificadosPorNome()
        {
            return clienteDAL.ObterClientesClassificadosPorNome();
        }
        public Cliente ObterClientePorId(long id)
        {
            return clienteDAL.ObterClientePorId(id);
        }
        public void GravarCliente(Cliente cliente)
        {
            clienteDAL.GravarCliente(cliente);
        }
        public Cliente EliminarClientePorId(long id)
        {
            Cliente cliente = clienteDAL.ObterClientePorId(id);
            clienteDAL.EliminarClientePorId(id);
            return cliente;
        }
    }
}