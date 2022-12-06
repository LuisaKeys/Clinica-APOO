﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Models
{ 
    public class Pet
    {
        public long PetId { get; set; }
        public string Nome { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Apenas números positivos")]
        public int Idade { get; set; }
        public TipoSexo Sexo { get; set; }
        public int EspecieId { get; set; }
        public Especie Especie { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

    }
    public enum TipoSexo
    {
        Masculino,
        Feminino
    }
}
