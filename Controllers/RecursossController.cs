using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RecursossController : BaseController
	{
		private readonly IRepository _repository;

		public RecursossController(IRepository repository)
		{
			_repository = repository;
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetAll<Recurso>();
				return result;
			});
		}		
		[Route("{id}")]
		[HttpGet]
		public async Task<IActionResult> GetById(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByKeys<Recurso>(id);
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


	}
}
