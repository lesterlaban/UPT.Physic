using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EncuestasController : BaseController
	{
		public EncuestasController(IRepository repository):base(repository)
		{
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new List<string> { "Secciones", "Secciones.Preguntas"};
				var resultQuery = await _repository.GetByFilterString<Encuesta>(e => e.Estado, includes);
				var result = resultQuery.Select(r=> new Encuesta()
				{
					Id = r.Id,
					Nombre = r.Nombre,
					Estado = r.Estado,
					Secciones = r.Secciones.Select(s => new EncuestaSeccion() 
					{
						Id = s.Id,
						IdEncuesta = s.IdEncuesta,
						Nombre = s.Nombre,
						Indicadores = s.Indicadores,
						Estado = s.Estado,
						Preguntas = s.Preguntas.Select(p=> new Pregunta
						{
							Id = p.Id,
							Descripcion = p.Descripcion,
							IdEncuestaSeccion = p.IdEncuestaSeccion,
							Estado = p.Estado
						}).ToList(),

					}).ToList(),
				});

				return result;
			});
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] List<Encuesta> entidad)
		{
			return await InvokeAsyncFunction(async () =>
			{
				await _repository.AddList(entidad);
				await _repository.SaveChangesAsync();
				return true;
			});
		}

	}
}
