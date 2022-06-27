using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EncuestasController : BaseController
	{
		private readonly IRepository _repository;

		public EncuestasController(IRepository repository)
		{
			_repository = repository;
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByFilter<Encuesta>(e => e.Estado);
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
