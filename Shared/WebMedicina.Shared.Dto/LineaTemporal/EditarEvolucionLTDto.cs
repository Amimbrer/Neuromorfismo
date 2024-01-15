﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMedicina.Shared.Dto.LineaTemporal
{
    public class EditarEvolucionLTDto {
        [Required]
        public int Id { get; init; }

        [Required]
        public int IdPaciente { get; init; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public bool Confirmado { get; set; }

        // Propiedad readonly
        public DateTime Fecha { get; init; } = DateTime.Today;

        public int IdMedicoUltModif { get; set; }

        [Required]
        public int IdEtapa { get; init; }

    }
}