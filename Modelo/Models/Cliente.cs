using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Models
{
    public class Cliente : Usuario
    {
        public string cpf { get; set; }
        public IList<Pet> Pets { get; set; }
    }
}
