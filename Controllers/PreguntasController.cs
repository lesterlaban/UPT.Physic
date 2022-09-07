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
				var elements = await _repository.GetByFilterString<Pregunta>(p=> p.Id == id, 
					new List<string>(){"PreguntaUsuario"});
				var deleted = elements.FirstOrDefault();
				if( deleted != null) 
				{
					await _repository.RemoveAsync(deleted);
					await _repository.SaveChangesAsync();
				}
				return true;
			});
		}	

		[HttpGet]
		[Route("RangosSeccion")]
		public async Task<IActionResult> GetRangosSeccion()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var entity = await _repository.GetByFilterString<RangoSeccion>(r => r.Estado);
				return entity;
			});
		}	

		[HttpPost]
		[Route("Usuarios")]
		public async Task<IActionResult> AddPreguntasUsuario([FromBody] List<PreguntaUsuario> entidad)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var seccionUsuario = entidad.GroupBy(l => l.Pregunta.IdEncuestaSeccion)
					.Select(cl => new SeccionUsuario
						{
							IdEncuestaSeccion = cl.FirstOrDefault()?.Pregunta?.IdEncuestaSeccion ?? 0,
							IdUsuario = cl.FirstOrDefault()?.IdUsuario ?? 0,
							Puntaje = cl.Sum(c => c.Puntaje),
							Estado = true,
						}).ToList();

				var preguntaUsuario = entidad.Select(p=> new PreguntaUsuario()
				{
					IdPregunta = p.IdPregunta,
					IdUsuario = p.IdUsuario,
					Puntaje = p.Puntaje,
					Estado = true,
				}).ToList();
				await _repository.AddList(seccionUsuario);
				await _repository.AddList(preguntaUsuario);

				await _repository.SaveChangesAsync();
				return true;
			});
		}	


	}
}
