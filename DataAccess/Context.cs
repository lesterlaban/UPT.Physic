﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UPT.Physic.DataAccess
{
	public class Context : DbContext
	{
        private readonly IConfiguration _configuracion;

        public Context(IConfiguration configuracion)
        {
            _configuracion = configuracion;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_configuracion["upt-physic"]);
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SetRolEntity();
            modelBuilder.SetUsuarioEntity();

            modelBuilder.SetEncuestaEntity();
            modelBuilder.SetSeccionEntity();
            modelBuilder.SetRangoSeccionEntity();
            modelBuilder.SetPreguntaEntity();
            modelBuilder.SetPreguntaUsuarioEntity();
            modelBuilder.SetSeccionUsuarioEntity();
            

            modelBuilder.SetNivelDolorEntity();
            modelBuilder.SetZonaDolorEntity();
            modelBuilder.SetRecursoEntity();
            modelBuilder.SetTratamientoEntity();
            modelBuilder.SetTratamientoRecursoEntity();
            modelBuilder.SetRegistroConsultaEntity();
        }
    }
}
