using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Models
{
    public class Exame
    {
        public int ExameId { get; set; }
        [DisplayName("Exame")]
        public string Descricao { get; set; }
        public IList<Consulta> Consultas { get; set; }
    }
}
