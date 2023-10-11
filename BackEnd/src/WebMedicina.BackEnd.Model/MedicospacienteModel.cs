﻿using System;
using System.Collections.Generic;

namespace WebMedicina.BackEnd.Model;

/// <summary>
/// Relacion de que medicos pueden editar que pacientes
/// </summary>
public partial class MedicospacienteModel
{
    public string Id { get; set; } = null!;

    public string IdMedico { get; set; } = null!;

    public int IdPaciente { get; set; }

    public virtual MedicosModel IdMedicoNavigation { get; set; } = null!;

    public virtual PacientesModel IdPacienteNavigation { get; set; } = null!;
}