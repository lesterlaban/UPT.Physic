using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class EncuestasController : BaseController
	{
		public EncuestasController(IRepository repository):base(repository)
		{
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includes = new List<string> { "Secciones", "Secciones.Preguntas"};
				var result = await _repository.GetByFilterString<Encuesta>(e => e.Estado, includes);
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
