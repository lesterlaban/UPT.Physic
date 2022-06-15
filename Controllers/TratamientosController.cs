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
				var result = await _repository.GetAll<Tratamiento>(includes);
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
