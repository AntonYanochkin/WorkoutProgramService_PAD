using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WorkoutProgramService.Models;

namespace WorkoutProgramService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
    }
}
