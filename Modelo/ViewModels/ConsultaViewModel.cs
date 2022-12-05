using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Modelo.Models;

namespace Modelo.ViewModels
{
    public class ConsultaViewModel
    {
        public ConsultaViewModel()
        {
            Exames = new Collection<ExameVinculado>();
        }

        public int Id { get; set; }
        public string Sintomas { get; set; }
        [DisplayName("Data")]
        public DateTime data_hora { get; set; }
        public virtual ICollection<ExameVinculado> Exames { get; set; }
        public long? PetId { get; set; }
        public Pet Pet { get; set; }
        public int? VeterinarioId { get; set; }
        
        public Veterinario Veterinario { get; set; }
    }
}