﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Service;

namespace WebMedicina.Shared.Dto {
    public class CrearPacienteDto {

        [Required(ErrorMessage = "El número de historia es obligatorio.")]
        [MaxLength(12, ErrorMessage = "El número de historia debe contener 12 dígitos.")]
        [MinLength(12, ErrorMessage = "El número de historia debe contener 12 dígitos.")]
        [RegularExpression(@"^AN\d{10}$", ErrorMessage = "El formato debe ser ANXXXXXXXXXX")]
        public string NumHistoria { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(ValidacionesRegistro), "ValidateFechaNacPaciente")]
        public DateTime? FechaNac { get; set; }

        [Required(ErrorMessage = "El campo género es obligatorio.")]
        [ValidacionLista("M", "H")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Debes especificar una talla para el paciente.")]
        [Range(50, 200, ErrorMessage = "La talla debe ser entre 50 y 200 cm.")]
        public decimal Talla { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaDiagnostico { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FechaFractalidad { get; set; }

        [Required(ErrorMessage = "Debes especificar una mutación para el paciente.")]
        public string IdMutacion { get; set; }

        public string Epilepsia { get; set; }

        public string Farmaco { get; set; } 

        public bool EnfermRaras { get; set; }

        public string DescripEnferRaras { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly FechaCreac { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly FechaUltMod { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public string? MedicoUltMod { get; set; }

        public string? MedicoCreador { get; set; }
    }
}
