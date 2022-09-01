using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PreguntasController : BaseController
	{
		public PreguntasController(IRepository repository):base(repository)
		{
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByFilterString<Pregunta>(e => e.Estado);
				return result;
			});
		}		
		
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Pregunta entidad)
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
				var entity = await _repository.GetByKeys<Pregunta>(id);
				await _repository.RemoveAsync(entity);
				await _repository.SaveChangesAsync();
				return true;
			});
		}	

	}
}
