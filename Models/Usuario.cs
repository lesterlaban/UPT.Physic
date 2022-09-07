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
		public bool Estado { get; set; }
		public bool TieneEncuesta { get; set; }
		
		public virtual List<PreguntaUsuario> PreguntaUsuario { get; set; }
		public virtual List<SeccionUsuario> SeccionUsuario { get; set; }
		public virtual List<RegistroConsulta> Consultas { get; set; }
		public int PuntajeEncuesta => PreguntaUsuario != null ? PreguntaUsuario.Sum(p=> p.Puntaje) : 0;
	}
	public class Rol
	{
		public int Id { get; set; }
		public string Codigo { get; set; }
		public bool Estado { get; set; }
		public virtual List<Usuario> Usuarios {get; set;}
	}
}
