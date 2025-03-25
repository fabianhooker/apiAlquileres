using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;



namespace apiAlquileres.Controllers
{
    [Route("api/Libros")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ILibrosService _librosService;


        public LibrosController(ILibrosService libroService)
        {
            _librosService = libroService;
        
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(typeof(IEnumerable<Libros>), 200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Libros>>> GetAll()
        {
            return Ok(await _librosService.GetAll());
        }

        [HttpGet("GetById")]
        [ProducesResponseType(typeof(Libros), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Libros>> GetById(int id)
        {
            try
            {
                var libro = await _librosService.GetById(id);
                if (libro == null)
                    return NotFound(new { message = "Libro no encontrado." });

                return Ok(libro);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error inesperado.", error = ex.Message });
            }
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(Libros), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Create([FromBody] Libros libro)
        {
            

            if (!await _librosService.Create(libro))
                return StatusCode(500, "Error al crear el libro.");

            return CreatedAtAction(nameof(GetById), new { id = libro.ID_Libro }, libro);
        }

        [HttpPut("Update")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Update([FromBody] Libros libro)
        {


            if (!await _librosService.Update(libro))
                return NotFound(new { message = "El libro no existe." });

            return Ok();
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await _librosService.Delete(id))
                return NotFound(new { message = "El libro no existe." });

            return Ok();
        }
    }
}
