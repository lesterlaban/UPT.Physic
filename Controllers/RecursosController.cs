﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;
using UPT.Physic.Models;

namespace UPT.Physic.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class RecursosController : BaseController
	{
		public RecursosController(IRepository repository):base(repository)
		{
		}

		[Route("")]
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByFilterString<Recurso>(e => e.Estado);
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

		[Route("filters")]
		[HttpGet]
		public async Task<IActionResult> GetByFilters(int idConsulta)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var includesRegistro = new List<string>() { "Usuario.SeccionUsuario.Seccion" };
				var list = await _repository.GetByFilterString<RegistroConsulta>(c=> c.Id == idConsulta, includesRegistro);
				var consulta = list.FirstOrDefault();
				if(consulta == null)
					throw new ApplicationException($"No se encontró la consulta con clave {idConsulta}.");
			
				var includes = new List<string>() { "Recurso" , "Tratamiento"};
				var tratamientoRecurso = await _repository.GetByFilterString<TratamientoRecurso>(t => 
					t.Tratamiento.IdNivelDolor == consulta.IdNivelDolor && 
					t.Tratamiento.IdZona == consulta.IdZona ,
					includes);

				var encuestasPuntaje = consulta.Usuario.SeccionUsuario.GroupBy(s => s.IdEncuestaSeccion)
					.Select(e => new EncuestaSeccion
					{
						Id = e.FirstOrDefault()?.Seccion?.Id ?? 0,
						Puntaje = e.Sum(c => c.Puntaje),
						Estado = true,
					}).ToList();

				List<TratamientoRecurso> resultFitler = new List<TratamientoRecurso>();
				encuestasPuntaje.ForEach(e => 
					resultFitler.AddRange(tratamientoRecurso.Where(
						t => e.Puntaje >= t.Tratamiento.PuntajeMinimo 
						&& e.Puntaje <= t.Tratamiento.PuntajeMaximo
						&& e.Id == t.Tratamiento.IdEncuestaSeccion)));
			
				var result = resultFitler.ToList().Select(r => r.Recurso).Distinct();
				var newResult = result.Select(r=> new Recurso()
				{
					Id = r.Id,
					Titulo = r.Titulo,
					Descripcion = r.Descripcion,
					Url = r.Url,
				});
				return newResult;
			});
		}	

		[Route("{id}")]
		[HttpGet]
		public async Task<IActionResult> GetBytId(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var result = await _repository.GetByKeys<Recurso>(id);
				return result;
			});
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return await InvokeAsyncFunction(async () =>
			{
				var elements = await _repository.GetByFilterString<Recurso>(p=> p.Id == id,
					new List<string>(){"Tratamientos"});
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
