using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;
using System.Linq;
using System;

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

		[Route("filters")]
		[HttpGet]
		public async Task<IActionResult> GetByFilters(string usuario, string password)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var list = await _repository.GetByFilter<Usuario>(u=> u.Nombre == usuario && u.Contrasenia == password);
				var result = list.FirstOrDefault();
				if(result == null)
					throw new ApplicationException("Usuario o contraseña no válidos.");
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
