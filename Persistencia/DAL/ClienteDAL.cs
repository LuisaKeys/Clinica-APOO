using Persistencia.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Models;

namespace Persistencia.DAL
{
    public class ClienteDAL
    {
        private EFContext context = new EFContext();
        public IQueryable <Cliente> ObterClientesClassificadosPorNome()
        {
            return context.Clientes.OrderBy(b => b.Nome);
        }
        public Cliente ObterClientePorId(long id)
        {
            return context.Clientes.Where(f => f.Id == id).Include("Pets.Cliente").First();
        }
        public void GravarCliente(Cliente cliente)
        {
            if (cliente.Id == 0)
            {
                context.Clientes.Add(cliente);
            }
            else
            {
                context.Entry(cliente).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Cliente EliminarClientePorId(long id)
        {
            Cliente cliente = ObterClientePorId(id);
            context.Clientes.Remove(cliente);
            context.SaveChanges();
            return cliente;
        }
    }
}
