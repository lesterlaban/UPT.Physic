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
	public class RecursosController : BaseController
	{
		public RecursosController(IRepository repository):base(repository)
		{
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByFilterString<Recurso>(e => e.Estado);
				return result;
			});
		}	
			
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Recurso entidad)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.Add(entidad);
				await _repository.SaveChangesAsync();
				return entidad.Id;
			});
		}	

		[Route("filters")]
		[HttpGet]
		public async Task<IActionResult> GetByFilters(int idConsulta)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includesRegistro = new List<string>() { "Usuario" };
				var list = await _repository.GetByFilterString<RegistroConsulta>(c=> c.Id == idConsulta, includesRegistro);
				var consulta = list.FirstOrDefault();
				if(consulta == null)
					throw new ApplicationException($"No se encontró la consulta con clave {idConsulta}.");

				var includes = new List<string>() { "Recurso" };
				var tratamientoRecurso = await _repository.GetByFilterString<TratamientoRecurso>(t => 
					t.Tratamiento.IdNivelDolor == consulta.IdNivelDolor && 
					t.Tratamiento.IdZona == consulta.IdZona,
					//consulta.Usuario.PuntajeEncuesta >= t.Tratamiento.PuntajeMinimo && 
					//consulta.Usuario.PuntajeEncuesta <= t.Tratamiento.PuntajeMaximo, 
					includes);

				var result = tratamientoRecurso.ToList().Select(r => r.Recurso);
	
				return result.Distinct();
			});
		}	

		[Route("{id}")]
		[HttpGet]
		public async Task<IActionResult> GetBytId(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByKeys<Recurso>(id);
				return result;
			});
		}

		[HttpDelete]
		[Route("id")]
		public async Task<IActionResult> Delete(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var entity = await _repository.GetByKeys<Recurso>(id);
				await _repository.RemoveAsync(entity);
				await _repository.SaveChangesAsync();
				return true;
			});
		}	

	}
}
