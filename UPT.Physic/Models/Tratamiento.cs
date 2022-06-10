using System;
using System.Collections.Generic;

namespace UPT.Physic.Models
{
	public class Tratamiento
	{
		public int Id { get; set; }
		public int IdZona { get; set; }
		public int IdNivelDolor { get; set; }
		public int PuntajeMinimo { get; set; }
		public int PuntajeMaximo { get; set; }
		public bool Estado{ get; set; }
		public virtual NivelDolor NivelDolor { get; set; }
		public virtual ZonaDolor ZonaDolor { get; set; }
		public virtual List<TratamientoRecurso> Recursos { get; set; }
	}

	public class TratamientoRecurso
	{
		public int Id { get; set; }
		public int IdTratamiento { get; set; }
		public int IdRecurso { get; set; }
		public virtual Tratamiento Tratamiento { get; set; }
		public virtual Recurso Recurso { get; set; }
	}

	public class RegistroConsulta
	{
		public int Id { get; set; }
		public int IdUsuario { get; set; }
		public int IdZona { get; set; }
		public int IdNivelDolor { get; set; }
		public int PuntajeMinimo { get; set; }
		public DateTime Fecha { get; set; }
		public bool Estado{ get; set; }
		public virtual NivelDolor NivelDolor { get; set; }
		public virtual ZonaDolor ZonaDolor { get; set; }
		public virtual Usuario Usuario {get; set;}
	}

}
