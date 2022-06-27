using System.Collections.Generic;
using System.Linq;

namespace UPT.Physic.Models
{
	public class Usuario
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public string Contrasenia { get; set; }
		public int IdRol { get; set; }
		public virtual Rol Rol { get; set; }
		public bool Estado{ get; set; }
		public virtual List<Encuesta> Encuestas {get; set;}
		public bool TieneEncuesta => Encuestas == null || !Encuestas.Any() ? false : true;
		public int PuntajeEncuesta => TieneEncuesta ? Encuestas.Sum(e=> e.Puntaje) : default(int);
		public virtual List<RegistroConsulta> Consultas { get; set; }
	}
	public class Rol
	{
		public int Id { get; set; }
		public string Codigo { get; set; }
		public bool Estado { get; set; }
		public virtual List<Usuario> Usuarios {get; set;}
	}
}
