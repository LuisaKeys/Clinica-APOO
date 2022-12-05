using Persistencia.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.Models;
using Persistencia.Context;
using System.Data.Entity;

namespace Persistencia.DAL
{
    public class PetDAL
    {
        private EFContext context = new EFContext();
        public IQueryable<Pet> ObterPetsClassificadosPorNome()
        {
            return context.Pets.Include(c => c.Cliente).Include(f => f.Especie).OrderBy(n => n.Nome);
        }

        public Pet ObterPetPorId(long id)
        {
            return context.Pets.Where(b => b.PetId == id).Include(e => e.Especie).Include(c => c.Cliente).First();
        }
        public void GravarPet(Pet pet)
        {
            if (pet.PetId == 0)
            {
                context.Pets.Add(pet);
            }
            else
            {
                context.Entry(pet).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public Pet EliminarPetPorId(long id)
        {
            Pet pet = ObterPetPorId(id);
            context.Pets.Remove(pet);
            context.SaveChanges();
            return pet;
        }
    }
}
