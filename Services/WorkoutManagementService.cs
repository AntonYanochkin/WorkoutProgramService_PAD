using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkoutProgramService.Models;
using WorkoutProgramService.Data;
namespace WorkoutProgramService.Services
{
    public class WorkoutManagementService : IWorkoutManagementService
    {
        private readonly ApplicationDbContext _context;

        public WorkoutManagementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkoutProgram>> GetAllProgramsAsync()
        {
            return await _context.WorkoutPrograms.ToListAsync();
        }

        public async Task<WorkoutProgram> GetProgramByIdAsync(int id)
        {
            return await _context.WorkoutPrograms.FindAsync(id);
        }

        public async Task<WorkoutProgram> CreateProgramAsync(WorkoutProgram program)
        {
            _context.WorkoutPrograms.Add(program);
            await _context.SaveChangesAsync();
            return program;
        }

        public async Task<bool> UpdateProgramAsync(WorkoutProgram program)
        {
            _context.WorkoutPrograms.Update(program);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProgramAsync(int id)
        {
            var program = await _context.WorkoutPrograms.FindAsync(id);
            if (program == null) return false;

            _context.WorkoutPrograms.Remove(program);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
