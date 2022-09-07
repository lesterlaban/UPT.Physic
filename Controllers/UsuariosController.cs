using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsuariosController : BaseController
	{
		public UsuariosController(IRepository repository):base(repository)
		{
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new List<string>() { "Rol" };
				var result = await _repository.GetByFilterString<Usuario>(u=> u.Estado, includes);
				return result;
			});
		}

		[Route("filters")]
		[HttpGet]
		public async Task<IActionResult> GetByFilters(string usuario, string password)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new List<string>() { "Rol", "PreguntaUsuario" };
				var usuarioUpper = string.IsNullOrEmpty(usuario) ? string.Empty : usuario.Trim().ToUpper();
				var list = await _repository.GetByFilterString<Usuario>(u => 
					u.Nombre.Trim().ToUpper() ==  usuarioUpper && 
					u.Contrasenia == password && 
					u.Estado, 
					includes);
				var result = list.FirstOrDefault();
				if(result == null)
					throw new ApplicationException($"Usuario o contraseña no válidos.");
				var usuarioResult = new Usuario()
				{
					Id = result.Id,
					Nombre = result.Nombre,
					Contrasenia = result.Contrasenia,
					IdRol = result.IdRol,
					Rol = new Rol()
					{
						Id = result.Rol.Id,
						Codigo = result.Rol.Codigo,
						Estado = result.Rol.Estado,
					},
					Estado = result.Estado,
					TieneEncuesta = result.PreguntaUsuario == null ? false : (result.PreguntaUsuario.Any() ? true : false),
				};
				return usuarioResult;
			});
		}

		[Route("{id}")]
		[HttpGet]
		public async Task<IActionResult> GetBytId(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new List<string>() { "SeccionUsuario", "SeccionUsuario.Seccion", "SeccionUsuario.Seccion.Encuesta", "SeccionUsuario.Seccion.Rangos"};
				var resultList = await _repository.GetByFilterString<Usuario>(c => 
					c.Id == id ,includes);
				var result = resultList.FirstOrDefault();
				if(result == null)
					throw new ApplicationException($"Usuario no encontrado con id {id}.");

				var resultUser = new Usuario()
				{
					Id = result.Id,
					Nombre = result.Nombre,
					IdRol = result.IdRol,					
					SeccionUsuario = result.SeccionUsuario.Select(s=> new SeccionUsuario()
					{
						Id = s.Id,
						IdEncuestaSeccion = s.IdEncuestaSeccion,
						IdUsuario = s.IdUsuario,
						Puntaje = s.Puntaje,
						Estado = s.Estado,
						Seccion = new EncuestaSeccion()
						{
							Id = s.Seccion.Id,
							IdEncuesta = s.Seccion.IdEncuesta,
							Nombre = s.Seccion.Nombre,
							Indicadores = s.Seccion.Indicadores,
							Encuesta = new Encuesta()
							{
								Nombre = s.Seccion.Encuesta?.Nombre,
								Id = s.Seccion.Encuesta?.Id ?? 0,
							},
							RangoValido = s.Seccion.Rangos.FirstOrDefault(r=> s.Puntaje >= r.ValorMinimo && s.Puntaje <= r.ValorMaximo),
						},
					}).ToList(),
				};
				resultUser.SeccionUsuario.ForEach(s=>
				{
					if(s.Seccion?.RangoValido != null)
						s.Seccion.RangoValido.Seccion = null;
				} );
				return resultUser;
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
				var existing = await _repository.GetByFilterString<Usuario>(u => 
					u.Nombre.Trim().ToUpper() == usuarioUpper);
				if(existing.Any())
					throw new ApplicationException($"Usuario con código {entidad.Nombre} ya existe, favor digite otro usuario.");
				var result = await _repository.Add(entidad);
				await _repository.SaveChangesAsync();
				return entidad.Id;
			});
		}	

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var elements = await _repository.GetByFilterString<Usuario>(p=> p.Id == id,
					new List<string>(){"PreguntaUsuario", "SeccionUsuario", "Consultas"});
				var deleted = elements.FirstOrDefault();
				if( deleted != null) 
				{
					await _repository.RemoveAsync(deleted);
					await _repository.SaveChangesAsync();
				}
				return true;
			});
		}		

	}
}
