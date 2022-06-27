using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;
using System.Linq;
using System;
using System.Linq.Expressions;

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
				var includes = new Expression<Func<Usuario, object>>[] 
				{
					a => a.Encuestas, a => a.Rol,
				};
				var usuarioUpper = string.IsNullOrEmpty(usuario) ? string.Empty : usuario.Trim().ToUpper();
				var list = await _repository.GetByFilter<Usuario>(u => 
					u.Nombre.Trim().ToUpper() ==  usuarioUpper && 
					u.Contrasenia == password && 
					u.Estado, 
					0,0,includes);
				var result = list.FirstOrDefault();
				if(result == null)
					throw new ApplicationException($"Usuario o contraseña no válidos.");
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
				var usuarioUpper = string.IsNullOrEmpty(entidad.Nombre) ? string.Empty : entidad.Nombre.Trim().ToUpper();
				var existing = await _repository.GetByFilter<Usuario>(u => 
					u.Nombre.Trim().ToUpper() == usuarioUpper);
				if(existing.Any())
					throw new ApplicationException($"Usuario con código {entidad.Nombre} ya existe, favor digite otro usuario.");
				var result = await _repository.Add(entidad);
				await _repository.SaveChangesAsync();
				return entidad.Id;
			});
		}		


	}
}
