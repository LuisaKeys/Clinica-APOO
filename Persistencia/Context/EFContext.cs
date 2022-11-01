using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Modelo.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Persistencia.Context
{
        public class EFContext : DbContext
        {
            public EFContext() : base("APOO")
            {
                Database.SetInitializer<EFContext>(
                new DropCreateDatabaseIfModelChanges<EFContext>());
            }
            public DbSet<Exame> Exames { get; set; }
            public DbSet<Consulta> Consultas { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            }
        }
}
