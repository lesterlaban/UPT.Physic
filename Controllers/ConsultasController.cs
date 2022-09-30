using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Extensions;
using UPT.Physic.Models;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ConsultasController : BaseController
	{
		public ConsultasController(IRepository repository):base(repository)
		{
		}


		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new List<string>() { "NivelDolor", "ZonaDolor" };
				var result = await _repository.GetByFilterString<RegistroConsulta>(r=> r.Estado, includes);
				return result;
			});
		}
		
		[Route("filters")]
		[HttpGet]
		public async Task<IActionResult> GetByFilters([FromQuery]List<DateTime> fechas, int idUsuario)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new List<string>() { "NivelDolor", "ZonaDolor" };
				Expression<Func<RegistroConsulta, bool>> expression = x => true;
				expression = expression.AndAlso(c => c.IdUsuario == idUsuario);
				expression = expression.AndAlso(c => c.Estado);

				if(fechas.Any())
					expression = expression.AndAlso(c => fechas.Select(f=> f.Date).Contains(c.Fecha.Date));

				var resultQuery = await _repository.GetByFilterString<RegistroConsulta>(expression, includes);



				var result = resultQuery.Select(r => new RegistroConsulta()
				{
					Id = r.Id,
					IdUsuario = r.IdUsuario,
					IdZona = r.IdZona,
					IdNivelDolor = r.IdNivelDolor,
					PuntajeMinimo = r.PuntajeMinimo,
					Fecha = r.Fecha,
					Estado = r.Estado,
					ZonaDolor = new ZonaDolor()
					{
						Id = r.ZonaDolor.Id,
						Descripcion = r.ZonaDolor.Descripcion,
					},
					NivelDolor = new NivelDolor()
					{
						Id = r.NivelDolor.Id,
						Descripcion = r.NivelDolor.Descripcion,
					},
				});
				return result;
			});
		}

		[Route("{id}")]
		[HttpGet]
		public async Task<IActionResult> GetBytId(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new List<string>() { "NivelDolor", "ZonaDolor" };
				var resultList = await _repository.GetByFilterString<RegistroConsulta>(c => 
					c.Id == id ,includes);
				var result = resultList.FirstOrDefault();
				return result;
			});
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] RegistroConsulta entidad)
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
				var elements = await _repository.GetByFilterString<RegistroConsulta>(p=> p.Id == id);
				var deleted = elements.FirstOrDefault();
				if( deleted != null) 
				{
					await _repository.RemoveAsync(deleted);
					await _repository.SaveChangesAsync();
				}
				return true;
			});
		}	
	}
}
