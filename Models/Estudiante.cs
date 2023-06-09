using System;
using System.Collections.Generic;

namespace Registro_alumno.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Rut { get; set; }

    public string? Correo { get; set; }

    public int? Edad { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public virtual ICollection<Asignacionasignatura> Asignacionasignaturas { get; set; } = new List<Asignacionasignatura>();

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
