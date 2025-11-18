using CLINICAAPI.Data;
using CLINICAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicaApi.Models;

namespace CLINICAAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClinicaController : ControllerBase
    {
        private readonly ClinicaContext _context;

        public ClinicaController(ClinicaContext context)
        {
            _context = context;
        }

        // GET /Clinica
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clinica>>> GetClinica()
        {
            if (_context.Clinicas == null)
            {
                return NotFound();
            }

            return await _context.Clinicas.ToListAsync();
        }

        // GET /Clinica/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Clinica>> GetClinica(int id)
        {
            if (_context == null)
            {
                return Problem("Contexto nulo.");
            }

            var clinica = await _context.Clinicas.FindAsync(id);

            if (clinica == null)
            {
                return NotFound("Id " + id + " não encontrado");
            }

            return clinica;
        }

        [HttpGet("Medicos/{id}")]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedico(int id)
        {
            if (_context == null)
            {
                return Problem("Contexto nulo.");
            }

            var medico = await _context.Medicos.Where(m => m.ClinicaId == id).ToListAsync();

            if (medico == null)
            {
                return NotFound("Clinica " + id + " não foram encontrados médicos na clinica");
            }

            return medico;
        }


        // POST /Clinica
        // { "nome": "Clínica ABC", "endereco": "Av. Central, 100" }
        [HttpPost]
        public async Task<ActionResult<Clinica>> PostClinica(Clinica clinica)
        {
            if (_context.Clinicas == null)
            {
                return Problem("Entidade Clinicas é nula");
            }

            _context.Clinicas.Add(clinica);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClinica), new { id = clinica.Id }, clinica);
        }

        // PUT /Clinica/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinica(int id, Clinica clinica)
        {
            if (id != clinica.Id)
            {
                return BadRequest("Id do caminho diferente do corpo.");
            }

            _context.Entry(clinica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE /Clinica/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinica(int id)
        {
            if (_context.Clinicas == null)
            {
                return NotFound();
            }

            var clinica = await _context.Clinicas.FindAsync(id);
            if (clinica == null)
            {
                return NotFound();
            }

            _context.Clinicas.Remove(clinica);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClinicaExists(int id)
        {
            return (_context.Clinicas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
