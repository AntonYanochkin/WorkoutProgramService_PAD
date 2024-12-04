using WorkoutProgramService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorkoutProgramService.Services
{
    public interface IWorkoutManagementService
    {
        Task<IEnumerable<WorkoutProgram>> GetAllProgramsAsync();
        Task<WorkoutProgram> GetProgramByIdAsync(int id);
        Task<WorkoutProgram> CreateProgramAsync(WorkoutProgram program);
        Task<bool> UpdateProgramAsync(WorkoutProgram program);
        Task<bool> DeleteProgramAsync(int id);
    }
}
