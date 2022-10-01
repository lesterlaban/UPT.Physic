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

        public static void SetEncuestaEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Encuesta>(entity =>
            {
                entity.ToTable("encuesta");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nombre).HasColumnName("nombre");
                entity.Property(e => e.Estado).HasColumnName("estado");
            });
        }

        public static void SetSeccionEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EncuestaSeccion>(entity =>
            {
                entity.ToTable("encuesta_seccion");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdEncuesta).HasColumnName("idencuesta");
                entity.Property(e => e.Nombre).HasColumnName("nombre");
                entity.Property(e => e.Indicadores).HasColumnName("indicadores");
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasOne<Encuesta>(e => e.Encuesta)
                    .WithMany(g => g.Secciones)
                    .HasForeignKey(s => s.IdEncuesta);
                entity.Ignore(e=> e.RangoValido);
                entity.Ignore(e => e.Puntaje);
            });
        }

        public static void SetRangoSeccionEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RangoSeccion>(entity =>
            {
                entity.ToTable("rango_seccion");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdEncuestaSeccion).HasColumnName("idencuestaseccion");
                entity.Property(e => e.Nombre).HasColumnName("nombre");
                entity.Property(e => e.ValorMinimo).HasColumnName("valorminimo");
                entity.Property(e => e.ValorMaximo).HasColumnName("valormmaximo");
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasOne<EncuestaSeccion>(e => e.Seccion)
                    .WithMany(g => g.Rangos)
                    .HasForeignKey(s => s.IdEncuestaSeccion);
            });
        }

        public static void SetPreguntaEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pregunta>(entity =>
            {
                entity.ToTable("pregunta");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdEncuestaSeccion).HasColumnName("idencuestaseccion");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasOne<EncuestaSeccion>(e => e.Seccion)
                    .WithMany(g => g.Preguntas)
                    .HasForeignKey(s => s.IdEncuestaSeccion);
            });
        }

        public static void SetPreguntaUsuarioEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PreguntaUsuario>(entity =>
            {
                entity.ToTable("pregunta_usuario");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdPregunta).HasColumnName("idpregunta");
                entity.Property(e => e.IdUsuario).HasColumnName("idusuario");
                entity.Property(e => e.Puntaje).HasColumnName("puntaje");
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasOne<Pregunta>(e => e.Pregunta)
                    .WithMany(g => g.PreguntaUsuario)
                    .HasForeignKey(s => s.IdPregunta);
                entity.HasOne<Usuario>(e => e.Usuario)
                    .WithMany(g => g.PreguntaUsuario)
                    .HasForeignKey(s => s.IdUsuario);
            });
        }
        
        public static void SetSeccionUsuarioEntity(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SeccionUsuario>(entity =>
            {
                entity.ToTable("encuesta_seccion_usuario");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IdEncuestaSeccion).HasColumnName("idencuestaseccion");
                entity.Property(e => e.IdUsuario).HasColumnName("idusuario");
                entity.Property(e => e.Puntaje).HasColumnName("puntaje");
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasOne<EncuestaSeccion>(e => e.Seccion)
                    .WithMany(g => g.SeccionUsuario)
                    .HasForeignKey(s => s.IdEncuestaSeccion);
                entity.HasOne<Usuario>(e => e.Usuario)
                    .WithMany(g => g.SeccionUsuario)
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
                entity.Property(e => e.IdEncuestaSeccion).HasColumnName("idencuestaseccion");
                entity.Property(e => e.PuntajeMaximo).HasColumnName("puntajemaximo");
                entity.Property(e => e.PuntajeMinimo).HasColumnName("puntajeminimo");
                entity.Property(e => e.Estado).HasColumnName("estado");
                entity.HasOne<NivelDolor>(e => e.NivelDolor)
                    .WithMany(g => g.Tratamientos)
                    .HasForeignKey(s => s.IdNivelDolor);
                entity.HasOne<ZonaDolor>(e => e.ZonaDolor)
                    .WithMany(g => g.Tratamientos)
                    .HasForeignKey(s => s.IdZona);
                entity.HasOne<EncuestaSeccion>(e => e.EncuestaSeccion)
                    .WithMany(g => g.Tratamientos)
                    .HasForeignKey(s => s.IdEncuestaSeccion);
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
