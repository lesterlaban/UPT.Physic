using System.Collections.Generic;

namespace UPT.Physic.Models
{
	public class NivelDolor
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public bool Estado{ get; set; }
		public virtual List<Tratamiento> Tratamientos { get; set; }
		public virtual List<RegistroConsulta> Consultas { get; set; }
	}
	public class ZonaDolor
	{
		public int Id { get; set; }
		public string Descripcion { get; set; }
		public bool Estado{ get; set; }
		public virtual List<Tratamiento> Tratamientos { get; set; }
		public virtual List<RegistroConsulta> Consultas { get; set; }
	}

	public class Recurso
	{
		public int Id { get; set; }
		public string Titulo { get; set; }
		public string Descripcion { get; set; }
		public string Url { get; set; }
		public bool Estado{ get; set; }
		public virtual List<TratamientoRecurso> Tratamientos { get; set; }
	}
}
