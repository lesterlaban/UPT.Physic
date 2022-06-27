using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ZonaDolorController : BaseController
	{
		private readonly IRepository _repository;

		public ZonaDolorController(IRepository repository)
		{
			_repository = repository;
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByFilter<ZonaDolor>(e => e.Estado, 0, 0);
				return result;
			});
		}		

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] ZonaDolor entidad)
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
