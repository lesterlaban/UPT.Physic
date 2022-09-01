using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UPT.Physic.DataAccess;

namespace UPT.Physic.Controllers
{
	public class BaseController : ControllerBase
	{
        protected readonly IRepository _repository;

        public BaseController(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Method to invoke async functions 
        /// </summary>
        /// <typeparam name="T">generic type to response</typeparam>
        /// <param name="funcion">function to inovke</param>
        /// <returns>Http action result</returns>
        public async Task<IActionResult> InvokeAsyncFunction<T>(Func<Task<T>> funcion)
        {
            try
            {
                var resultado = await funcion.Invoke();
                return Ok(resultado);
            }
            catch (KeyNotFoundException excepcion)
            {
                //Logger.Write("No encontrado: " + excepcion.Message);
                return NotFound(excepcion.Message);
            }
            catch (UnauthorizedAccessException excepcion)
            {
                //Logger.Write("No autorizado: " + excepcion.Message);
                return Unauthorized(excepcion.Message);
            }
            catch (ApplicationException excepcion)
            {
                //Logger.Write("Error 500: " + excepcion.Message);
                return BadRequest(excepcion.Message);
            }
            catch (Exception excepcion)
            {
                //Logger.Write("Error 500: " + excepcion.Message);
                return BadRequest(excepcion.Message);
            }
        }

    }
}
