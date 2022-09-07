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
	public class PreguntasController : BaseController
	{
		public PreguntasController(IRepository repository):base(repository)
		{
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new List<string> { "Seccion", "Seccion.Encuesta"};
				var resultRepo = await _repository.GetByFilterString<Pregunta>(e => e.Estado, includes);
				var result = resultRepo.Select(r=> new Pregunta()
				{
					Id = r.Id,
					Descripcion = r.Descripcion,
					Estado = r.Estado,
					IdEncuestaSeccion = r.IdEncuestaSeccion,
					Seccion = new EncuestaSeccion()
					{
						Id = r.Seccion.Id,
						IdEncuesta = r.Seccion.IdEncuesta,
						Nombre = r.Seccion.Nombre,
						Indicadores = r.Seccion.Indicadores,
						Estado = r.Seccion.Estado,
						Encuesta = new Encuesta()
						{
							Id = r.Seccion.Encuesta.Id,
							Nombre = r.Seccion.Encuesta.Nombre,
							Estado = r.Seccion.Encuesta.Estado,
						}
					},
				});
				return result;
			});
		}		
		
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Pregunta entidad)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.Add(entidad);
				await _repository.SaveChangesAsync();
				return entidad.Id;
			});
		}		

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var entity = await _repository.GetByKeys<Pregunta>(id);
				await _repository.RemoveAsync(entity);
				await _repository.SaveChangesAsync();
				return true;
			});
		}	

		[HttpPost]
		[Route("Usuarios")]
		public async Task<IActionResult> AddPreguntasUsuario([FromBody] List<PreguntaUsuario> entidad)
		{
			return await InvokeAsyncFunction(async () =>
			{
				await _repository.AddList(entidad);
				var section = entidad.GroupBy(l => l.IdPregunta)
					.Select(cl => new SeccionUsuario
						{
							IdEncuestaSeccion = cl.FirstOrDefault()?.Pregunta?.IdEncuestaSeccion ?? 0,
							IdUsuario = cl.FirstOrDefault()?.IdUsuario ?? 0,
							Puntaje = cl.Sum(c => c.Puntaje),
						}).ToList();
				await _repository.AddList(section);
				await _repository.SaveChangesAsync();
				return true;
			});
		}	


	}
}
