using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Models
{
    public class Consulta
    {
        [DisplayName("Id")]
        public long ConsultaId { get; set; }
        [DisplayName("Data")]
        public DateTime data_hora { get; set; }
        public string Sintomas { get; set; }
        [DisplayName("Exame")]
        public long? ExameId { get; set; }
        public Exame Exame { get; set; }
    }
}
