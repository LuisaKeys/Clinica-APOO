using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Relacionamento.Models.ViewModels
{
    public class ExameViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<ConsultaViewModel> Consultas { get; set; }
    }
}