using CLINICAAPI.Data;
using CLINICAAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicaApi.Models;

namespace CLINICAAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly ClinicaContext _context;

        public MedicoController(ClinicaContext context)
        {
            _context = context;
        }

        // GET /Medico
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedico()
        {
            if (_context.Medicos == null)
            {
                return NotFound();
            }

            return await _context.Medicos.ToListAsync();
        }

        // GET /Medico/123
        [HttpGet("{crm}")]
        public async Task<ActionResult<Medico>> GetMedico(int crm)
        {
            if (_context == null)
            {
                return Problem("Contexto nulo.");
            }

            var medico = await _context.Medicos.FindAsync(crm);

            if (medico == null)
            {
                return NotFound("Crm " + crm + " não encontrado");
            }

            return medico;
        }

        // POST /Medico
        // Exemplo JSON:
        // { "crm": 123, "nome": "Fulano", "idade": 40, "endereco": "Rua X" }
        [HttpPost]
        public async Task<ActionResult<Medico>> PostMedico(Medico medico)
        {
            if (_context.Medicos == null)
            {
                return Problem("Entidade Medicos é nula");
            }

            _context.Medicos.Add(medico);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMedico), new { crm = medico.Crm }, medico);
        }

        // PUT /Medico/123
        [HttpPut("{crm}")]
        public async Task<IActionResult> PutMedico(int crm, Medico medico)
        {
            if (crm != medico.Crm)
            {
                return BadRequest("CRM do caminho diferente do corpo.");
            }

            _context.Entry(medico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicoExists(crm))
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

        // DELETE /Medico/123
        [HttpDelete("{crm}")]
        public async Task<IActionResult> DeleteMedico(int crm)
        {
            if (_context.Medicos == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos.FindAsync(crm);
            if (medico == null)
            {
                return NotFound();
            }

            _context.Medicos.Remove(medico);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicoExists(int crm)
        {
            return (_context.Medicos?.Any(e => e.Crm == crm)).GetValueOrDefault();
        }
    }
}
