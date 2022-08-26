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
		public ZonaDolorController(IRepository repository):base(repository)
		{
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

		[HttpDelete]
		[Route("id")]
		public async Task<IActionResult> Delete(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var entity = await _repository.GetByKeys<ZonaDolor>(id);
				await _repository.RemoveAsync(entity);
				await _repository.SaveChangesAsync();
				return true;
			});
		}	

	}
}
