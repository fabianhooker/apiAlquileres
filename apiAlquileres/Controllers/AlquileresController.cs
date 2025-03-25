using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace apiAlquileres.Controllers
{
    [Route("api/alquileres")]
    [ApiController]
    public class AlquileresController : ControllerBase
    {

        private readonly IAlquileresService _alquileresService;
     

        public AlquileresController(IAlquileresService alquileresService)
        {
            _alquileresService = alquileresService;
        
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<Alquileres>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Alquileres>>> GetAll()
        {
            return Ok(await _alquileresService.GetAll());
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(Alquileres), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Alquileres>> GetById(int id)
        {
            try
            {
                var alquiler = await _alquileresService.GetById(id);
                if (alquiler == null)
                    return NotFound(new { message = "Alquiler no encontrado." });

                return Ok(alquiler);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error inesperado.", error = ex.Message });
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(Alquileres), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] Alquileres alquiler)
        {
          

            if (!await _alquileresService.Create(alquiler))
                return StatusCode(500, "Error al registrar el alquiler.");

            return CreatedAtAction(nameof(GetById), new { id = alquiler.ID_Alquiler }, alquiler);
        }

        [HttpPut("Update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] Alquileres alquiler)
        {
         

            if (!await _alquileresService.Update(alquiler))
                return NotFound(new { message = "El alquiler no existe." });

            return Ok();
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _alquileresService.Delete(id))
                return NotFound(new { message = "El alquiler no existe." });

            return Ok();
        }
    }
}
