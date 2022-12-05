using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Models
{
    public class Veterinario : Usuario
    {
        public string Crmv { get; set; }
        public IList<Consulta> Consultas { get; set; }
    }
}
