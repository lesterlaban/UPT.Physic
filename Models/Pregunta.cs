using System.Collections.Generic;

namespace UPT.Physic.Models
{
	public class Pregunta
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public bool Estado{ get; set; }
		public virtual List<Encuesta> Encuestas { get; set; }
	}

	public class Encuesta
	{
		public int Id { get; set; }
		public int IdPregunta { get; set; }
		public int IdUsuario { get; set; }
		public int Puntaje { get; set; }
		public bool Estado { get; set; }
		public virtual Pregunta Pregunta {get; set;}
		public virtual Usuario Usuario {get; set;}
	}

}
