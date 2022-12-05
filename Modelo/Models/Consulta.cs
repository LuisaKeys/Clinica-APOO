using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Modelo.Models
{
    public class Consulta
    {
        public Consulta()
        {
            Exames = new List<Exame>();
        }
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Data")]
        public DateTime data_hora { get; set; }
        public string Sintomas { get; set; }
        public virtual ICollection<Exame> Exames { get; set; }
        public int? PetId { get; set; }
        public Pet Pet { get; set; }
        public int? VeterinarioId { get; set; }
        public Veterinario Veterinario { get; set; }

    }
}
