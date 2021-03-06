using Microsoft.AspNetCore.Mvc;
using System;
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
		private readonly IRepository _repository;

		public TratamientosController(IRepository repository)
		{
			_repository = repository;
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new Expression<Func<Tratamiento, object>>[] { u => u.NivelDolor, u => u.ZonaDolor};
				var result = await _repository.GetByFilter<Tratamiento>(e => e.Estado, 0, 0, includes);
				return result;
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
				var result = await _repository.GetByFilter<Tratamiento>(t => 
					t.IdNivelDolor == consulta.IdNivelDolor && 
					t.IdZona == consulta.IdZona && 
					consulta.Usuario.PuntajeEncuesta >= t.PuntajeMinimo && 
					consulta.Usuario.PuntajeEncuesta <= t.PuntajeMaximo, 0,0, includes);
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

	}
}
