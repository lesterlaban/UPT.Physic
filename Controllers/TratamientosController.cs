using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TratamientosController : BaseController
	{
		public TratamientosController(IRepository repository):base(repository)
		{
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new List<string>() { "NivelDolor", "ZonaDolor"};
				var resultQuery = await _repository.GetByFilterString<Tratamiento>(e => e.Estado, includes);
				var result = resultQuery.Select(r=> new Tratamiento()
				{
					Id = r.Id,
					IdEncuesta = r.IdEncuesta,
					IdZona = r.IdZona,
					IdNivelDolor = r.IdNivelDolor,
					PuntajeMinimo = r.PuntajeMinimo,
					PuntajeMaximo = r.PuntajeMaximo,
					Estado = r.Estado,
					NivelDolor = new NivelDolor()
					{
						Id = r.IdNivelDolor,
						Descripcion = r.NivelDolor.Descripcion,
						Estado = r.NivelDolor.Estado,
					},
					ZonaDolor= new ZonaDolor()
					{
						Id = r.IdZona,
						Descripcion = r.ZonaDolor.Descripcion,
						Estado = r.ZonaDolor.Estado,
					}
				});
				return result;
			});
		}

		[Route("filters")]
		[HttpGet]
		public async Task<IActionResult> GetByFilters(int idConsulta)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var consulta = await _repository.GetByKeys<RegistroConsulta>(idConsulta);
				if(consulta == null)
					throw new ApplicationException($"No se encontró la consulta con clave {idConsulta}.");

				var includes = new List<string>() { "NivelDolor", "ZonaDolor", "Recursos" };
				var result = await _repository.GetByFilterString<Tratamiento>(t => 
					t.IdNivelDolor == consulta.IdNivelDolor && 
					t.IdZona == consulta.IdZona,
					//consulta.Usuario.PuntajeEncuesta >= t.PuntajeMinimo && 
					//consulta.Usuario.PuntajeEncuesta <= t.PuntajeMaximo, 
					includes);
				return result;
			});
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Tratamiento entidad)
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
				var elements = await _repository.GetByFilterString<Tratamiento>(p=> p.Id == id,
					new List<string>(){"Recursos"});
				var deleted = elements.FirstOrDefault();
				if( deleted != null) 
				{
					await _repository.RemoveAsync(deleted);
					await _repository.SaveChangesAsync();
				}
				return true;
			});
		}		
		
		[Route("{id}")]
		[HttpGet]
		public async Task<IActionResult> GetBytId(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByFilterString<Tratamiento>(p=> p.Id == id,
					new List<string>(){"Recursos"});
				return result.FirstOrDefault();
			});
		}

	}
}
