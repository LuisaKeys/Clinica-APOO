using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Modelo.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using Persistencia.Migrations;

namespace Persistencia.Context
{
        public class EFContext : DbContext
        {
            public EFContext() : base("APOO")
            {
            Database.SetInitializer<EFContext>(new
        MigrateDatabaseToLatestVersion<EFContext, Configuration>());
            //Database.SetInitializer<EFContext>(
            //        new DropCreateDatabaseIfModelChanges<EFContext>());
        }
            public DbSet<Exame> Exames { get; set; }
            public DbSet<Consulta> Consultas { get; set; }
            public DbSet<Especie> Especies { get; set; }
            public DbSet<Veterinario> Veterinarios { get; set; }
            public DbSet<Cliente> Clientes { get; set; }
            public DbSet<Pet> Pets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            }
        }
}
