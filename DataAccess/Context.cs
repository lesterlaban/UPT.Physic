using Microsoft.EntityFrameworkCore;
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
            => optionsBuilder.UseNpgsql("Server=ec2-3-231-82-226.compute-1.amazonaws.com;Database=d6kli73ecg3dgd;Port=5432;User Id=scyopphzgnfcoy;Password=b89b703aa9b78efa4560aa53acaad85494fb30b7267f1d9e0d5cc4e40c69a7e0; sslmode=Require; Trust Server Certificate=true;");
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
