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
		private readonly IRepository _repository;

		public RecursosController(IRepository repository)
		{
			_repository = repository;
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByFilter<Recurso>(e => e.Estado, 0, 0);
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
				var includes = new Expression<Func<Tratamiento, object>>[] { u => u.NivelDolor, u => u.ZonaDolor, u => u.Recursos};
				var consulta = await _repository.GetByKeys<RegistroConsulta>(idConsulta);
				if(consulta == null)
					throw new ApplicationException($"No se encontró la consulta con clave {idConsulta}.");
				var tratamientos = await _repository.GetByFilter<Tratamiento>(t => 
					t.IdNivelDolor == consulta.IdNivelDolor && 
					t.IdZona == consulta.IdZona && 
					consulta.Usuario.PuntajeEncuesta >= t.PuntajeMinimo && 
					consulta.Usuario.PuntajeEncuesta <= t.PuntajeMaximo, 0,0, includes);
				var result = new List<Recurso>();

				tratamientos.ForEach(tratamiento => 
					result.AddRange(tratamiento.Recursos.Select(r => r.Recurso)));

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

	}
}
