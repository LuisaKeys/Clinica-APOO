using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages.Html;

namespace Modelo.Models
{
    public class Consulta
    {
        public Consulta()
        {
            this.Exames = new HashSet<Exame>().ToList();
        }
        [DisplayName("Id")]
        public int ConsultaId { get; set; }
        [DisplayName("Data")]
        public DateTime data_hora { get; set; }
        public string Sintomas { get; set; }
        [DisplayName("Exame")]
        
        public List<Exame> Exames { get; set; }

    }
}
