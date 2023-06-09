using System;
using System.Collections.Generic;

namespace Registro_alumno.Models;

public partial class Asignatura
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Codigo { get; set; }

    public string? Descripcion { get; set; }

    public DateOnly? FechaActualizacion { get; set; }

    public virtual ICollection<Asignacionasignatura> Asignacionasignaturas { get; set; } = new List<Asignacionasignatura>();

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();
}
