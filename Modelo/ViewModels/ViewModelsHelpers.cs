using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Modelo.Models;

namespace Modelo.ViewModels
{
    public static class ViewModelHelpers
    {
        public static ConsultaViewModel ToViewModel(this Consulta consulta)
        {
            var consultaViewModel = new ConsultaViewModel
            {
                Sintomas = consulta.Sintomas,
                Id = consulta.Id,
                data_hora = consulta.data_hora,
                Veterinario = consulta.Veterinario,
                Pet = consulta.Pet,
                PetId = consulta.PetId,
                VeterinarioId = consulta.VeterinarioId
            };

            foreach (var exame in consulta.Exames)
            {
                consultaViewModel.Exames.Add(new ExameVinculado
                {
                    Id = exame.Id,
                    Descricao = exame.Descricao,
                    Vinculado = true
                });
            }


            return consultaViewModel;
        }

        public static ConsultaViewModel ToViewModel(this Consulta consulta, ICollection<Exame> todosExamesBD)
        {
            var consultaViewModel = new ConsultaViewModel
            {
                Sintomas = consulta.Sintomas,
                Id = consulta.Id,
                data_hora = consulta.data_hora,
                Veterinario = consulta.Veterinario,
                Pet = consulta.Pet,
                PetId = consulta.PetId,
                VeterinarioId = consulta.VeterinarioId
            };

            // Collection com todos os exames (Exames Vinculados inclusos)
            ICollection<ExameVinculado> todosExames = new List<ExameVinculado>();

            foreach (var c in todosExamesBD)
            {
                //Criar novo ExameVinculado por cada Exame e setar Vinculado = true se a consulta tiver exame
                var exameVinculado = new ExameVinculado
                {
                    Id = c.Id,
                    Descricao = c.Descricao,
                    Vinculado = consulta.Exames.FirstOrDefault(x => x.Id == c.Id) != null
                };

                todosExames.Add(exameVinculado);
            }

            consultaViewModel.Exames = todosExames;

            return consultaViewModel;
        }

        public static Consulta ToDomainModel(this ConsultaViewModel consultaViewModel)
        {
            var consulta = new Consulta
            {
                Sintomas = consultaViewModel.Sintomas,
                Id = consultaViewModel.Id,
                data_hora = consultaViewModel.data_hora,
                Veterinario = consultaViewModel.Veterinario,
                Pet = consultaViewModel.Pet,
                PetId = consultaViewModel.PetId,
                VeterinarioId = consultaViewModel.VeterinarioId
            };

            return consulta;
        }
    }
}