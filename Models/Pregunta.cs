using System.Collections.Generic;

namespace UPT.Physic.Models
{	
	public class Encuesta
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public bool Estado{ get; set; }
		public virtual List<EncuestaSeccion> Secciones { get; set; }
	}

	public class EncuestaSeccion
	{
		public int Id { get; set; }
		public int IdEncuesta { get; set; }
		public string Nombre { get; set; }
		public string Indicadores { get; set; }
		public bool Estado{ get; set; }
		public virtual Encuesta Encuesta { get; set; }
		public virtual List<RangoSeccion> Rangos { get; set; }
		public virtual List<Pregunta> Preguntas { get; set; }
		public virtual List<SeccionUsuario> SeccionUsuario { get; set; }
		
	}

	public class RangoSeccion
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IdEncuestaSeccion { get; set; }
		public int ValorMinimo { get; set; }
		public int ValorMaximo { get; set; }
		public bool Estado{ get; set; }
		public virtual EncuestaSeccion Seccion { get; set; }
	}

	public class Pregunta
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public bool Estado{ get; set; }
		public int IdEncuestaSeccion { get; set; }
		public virtual EncuestaSeccion Seccion { get; set; }
		public virtual List<PreguntaUsuario> PreguntaUsuario { get; set; }
	}

	public class PreguntaUsuario
	{
		public int Id { get; set; }
		public int IdPregunta { get; set; }
		public int IdUsuario { get; set; }
		public int Puntaje { get; set; }
		public bool Estado{ get; set; }
		public virtual Pregunta Pregunta { get; set; }
		public virtual Usuario Usuario { get; set; }
	}

	public class SeccionUsuario
	{
		public int Id { get; set; }
		public int IdEncuestaSeccion { get; set; }
		public int IdUsuario { get; set; }
		public int Puntaje { get; set; }
		public bool Estado{ get; set; }
		public virtual EncuestaSeccion Seccion { get; set; }
		public virtual Usuario Usuario { get; set; }
	}

}
