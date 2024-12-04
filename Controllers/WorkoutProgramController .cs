using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutProgramService.Data;
using WorkoutProgramService.Models;
using WorkoutProgramService.Services;

namespace WorkoutProgramService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutProgramController : ControllerBase
    {
        private readonly IWorkoutManagementService _service;

        public WorkoutProgramController(IWorkoutManagementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutProgram>>> GetAllPrograms()
        {
            return Ok(await _service.GetAllProgramsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutProgram>> GetProgramById(int id)
        {
            var program = await _service.GetProgramByIdAsync(id);
            if (program == null) return NotFound();
            return Ok(program);
        }

        [HttpPost]
        public async Task<ActionResult<WorkoutProgram>> CreateProgram([FromBody] WorkoutProgram program)
        {
            var createdProgram = await _service.CreateProgramAsync(program);
            return CreatedAtAction(nameof(GetProgramById), new { id = createdProgram.Id }, createdProgram);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgram(int id, [FromBody] WorkoutProgram program)
        {
            if (id != program.Id) return BadRequest();

            var result = await _service.UpdateProgramAsync(program);
            if (!result) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            var result = await _service.DeleteProgramAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
    }
}
