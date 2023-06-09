using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Registro_alumno.Models;

public partial class RegistroAlumnoContext : DbContext
{
    public RegistroAlumnoContext()
    {
    }

    public RegistroAlumnoContext(DbContextOptions<RegistroAlumnoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignacionasignatura> Asignacionasignaturas { get; set; }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Nota> Notas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Asignacionasignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignacionasignaturas");

            entity.HasIndex(e => e.AsignaturaId, "fk_asignatura_has_estudiante_asignatura_idx");

            entity.HasIndex(e => e.EstudianteId, "fk_asignatura_has_estudiante_estudiante1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AsignaturaId).HasColumnType("int(11)");
            entity.Property(e => e.EstudianteId).HasColumnType("int(11)");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.Asignacionasignaturas)
                .HasForeignKey(d => d.AsignaturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_asignatura_has_estudiante_asignatura");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Asignacionasignaturas)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_asignatura_has_estudiante_estudiante1");
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("asignatura");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Codigo).HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estudiante");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Edad).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rut).HasMaxLength(100);
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notas");

            entity.HasIndex(e => e.AsignaturaId, "fk_Notas_Asignatura1_idx");

            entity.HasIndex(e => e.EstudianteId, "fk_Notas_Estudiante1_idx");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.AsignaturaId).HasColumnType("int(11)");
            entity.Property(e => e.EstudianteId).HasColumnType("int(11)");

            entity.HasOne(d => d.Asignatura).WithMany(p => p.Nota)
                .HasForeignKey(d => d.AsignaturaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Notas_Asignatura1");

            entity.HasOne(d => d.Estudiante).WithMany(p => p.Nota)
                .HasForeignKey(d => d.EstudianteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Notas_Estudiante1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
