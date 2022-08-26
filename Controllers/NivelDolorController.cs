using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class NivelDolorController : BaseController
	{
		public NivelDolorController(IRepository repository):base(repository)
		{
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByFilter<NivelDolor>(e => e.Estado);
				return result;
			});
		}		

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] NivelDolor entidad)
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
				var entity = await _repository.GetByKeys<NivelDolor>(id);
				await _repository.RemoveAsync(entity);
				await _repository.SaveChangesAsync();
				return true;
			});
		}	

	}
}
