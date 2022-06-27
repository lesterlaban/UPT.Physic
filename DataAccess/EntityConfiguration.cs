using Microsoft.EntityFrameworkCore;
using UPT.Physic.Models;

namespace UPT.Physic.DataAccess
{
	public static class EntityConfiguration
	{
        public static void SetUsuarioEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre");
                entity.Property(e => e.Contrasenia).HasColumnName("contrasenia");
                entity.Property(e => e.IdRol).HasColumnName("idrol");
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.Ignore(e => e.TieneEncuesta);
                entity.Ignore(e => e.PuntajeEncuesta);
                entity.HasOne<Rol>(e => e.Rol)
                    .WithMany(g => g.Usuarios)
                    .HasForeignKey(s => s.IdRol);
            });
        }

        public static void SetRolEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("rol");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Codigo).HasColumnName("codigo");
                entity.Property(e => e.Estado).HasColumnName("estado");
            });
        }

        public static void SetPreguntaEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.ToTable("pregunta");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                entity.Property(e => e.Estado).HasColumnName("estado");
            });
        }

        public static void SetEncuestaEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Encuesta>(entity =>
            {
                entity.ToTable("encuesta");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdPregunta).HasColumnName("idpregunta");
                entity.Property(e => e.IdUsuario).HasColumnName("idusuario");
                entity.Property(e => e.Puntaje).HasColumnName("puntaje");
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasOne<Pregunta>(e => e.Pregunta)
                    .WithMany(g => g.Encuestas)
                    .HasForeignKey(s => s.IdPregunta);
                entity.HasOne<Usuario>(e => e.Usuario)
                    .WithMany(g => g.Encuestas)
                    .HasForeignKey(s => s.IdUsuario);
            });
        }
    
        public static void SetNivelDolorEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NivelDolor>(entity =>
            {
                entity.ToTable("nivel_dolor");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                entity.Property(e => e.Estado).HasColumnName("estado");
            });
        }

        public static void SetZonaDolorEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ZonaDolor>(entity =>
            {
                entity.ToTable("zona_dolor");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                entity.Property(e => e.Estado).HasColumnName("estado");
            });
        }


        public static void SetRecursoEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recurso>(entity =>
            {
                entity.ToTable("recurso");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Titulo).HasColumnName("titulo");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                entity.Property(e => e.Url).HasColumnName("url");
                entity.Property(e => e.Estado).HasColumnName("estado");
            });
        }

        public static void SetTratamientoEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tratamiento>(entity =>
            {
                entity.ToTable("tratamiento");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdZona).HasColumnName("idzona");
                entity.Property(e => e.IdNivelDolor).HasColumnName("idniveldolor");
                entity.Property(e => e.PuntajeMaximo).HasColumnName("puntajemaximo");
                entity.Property(e => e.PuntajeMinimo).HasColumnName("puntajeminimo");
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasOne<NivelDolor>(e => e.NivelDolor)
                    .WithMany(g => g.Tratamientos)
                    .HasForeignKey(s => s.IdNivelDolor);
                entity.HasOne<ZonaDolor>(e => e.ZonaDolor)
                    .WithMany(g => g.Tratamientos)
                    .HasForeignKey(s => s.IdZona);
            });
        }
        public static void SetTratamientoRecursoEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TratamientoRecurso>(entity =>
            {
                entity.ToTable("tratamiento_recurso");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdTratamiento).HasColumnName("idtratamiento");
                entity.Property(e => e.IdRecurso).HasColumnName("idrecurso");
                entity.HasOne<Tratamiento>(e => e.Tratamiento)
                    .WithMany(g => g.Recursos)
                    .HasForeignKey(s => s.IdTratamiento);
                entity.HasOne<Recurso>(e => e.Recurso)
                    .WithMany(g => g.Tratamientos)
                    .HasForeignKey(s => s.IdRecurso);
            });
        }

        public static void SetRegistroConsultaEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegistroConsulta>(entity =>
            {
                entity.ToTable("registro_consulta");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdUsuario).HasColumnName("idusuario");
                entity.Property(e => e.IdZona).HasColumnName("idzona");
                entity.Property(e => e.IdNivelDolor).HasColumnName("idniveldolor");
                entity.Property(e => e.PuntajeMinimo).HasColumnName("puntajeminimo");
                entity.Property(e => e.Fecha).HasColumnName("fecha");
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasOne<NivelDolor>(e => e.NivelDolor)
                    .WithMany(g => g.Consultas)
                    .HasForeignKey(s => s.IdNivelDolor);
                entity.HasOne<ZonaDolor>(e => e.ZonaDolor)
                    .WithMany(g => g.Consultas)
                    .HasForeignKey(s => s.IdZona);
                entity.HasOne<Usuario>(e => e.Usuario)
                    .WithMany(g => g.Consultas)
                    .HasForeignKey(s => s.IdUsuario);


            });
        }

    }
}
