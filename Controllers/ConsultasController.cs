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
	public class ConsultasController : BaseController
	{
		private readonly IRepository _repository;

		public ConsultasController(IRepository repository)
		{
			_repository = repository;
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new Expression<Func<RegistroConsulta, object>>[] { u => u.NivelDolor, u => u.ZonaDolor};
				var result = await _repository.GetAll<RegistroConsulta>(includes);
				return result;
			});
		}
		
		[Route("filters")]
		[HttpGet]
		public async Task<IActionResult> GetByFilters(int idUsuario)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new Expression<Func<RegistroConsulta, object>>[] 
					{ u => u.NivelDolor, u => u.ZonaDolor};
				var result = await _repository.GetByFilter<RegistroConsulta>(c => 
					c.IdUsuario == idUsuario && c.Estado,0,0 ,includes);
				return result;
			});
		}

		[Route("{id}")]
		[HttpGet]
		public async Task<IActionResult> GetBytId(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByKeys<RegistroConsulta>(id);
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

	}
}
