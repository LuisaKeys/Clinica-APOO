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
        public long ExameId { get; set; }
        [DisplayName("Exame")]
        public string Descricao { get; set; }
        public virtual ICollection<Consulta> Consultas { get; set; }
    }
}
