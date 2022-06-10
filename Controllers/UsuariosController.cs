using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsuariosController : BaseController
	{
		private readonly IRepository _repository;

		public UsuariosController(IRepository repository)
		{
			_repository = repository;
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetAll<Usuario>(u=> u.Rol);
				return result;
			});
		}

		[Route("Roles")]
		[HttpGet]
		public async Task<IActionResult> GetAllRols()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetAll<Rol>(u=> u.Usuarios);
				return result;
			});
		}

		[HttpPost]
		public async Task<IActionResult> Add([FromBody] Usuario entidad)
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
