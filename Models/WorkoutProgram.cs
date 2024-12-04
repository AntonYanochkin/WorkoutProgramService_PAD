using System;
using System.ComponentModel.DataAnnotations;

namespace WorkoutProgramService.Models
{

    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
    public class WorkoutProgram
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public DifficultyLevel Level { get; set; } 
        public int DurationInMinutes { get; set; }

        public string Content { get; set; } 

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string UserId { get; set; } // ID пользователя, который создал программу
    }

}
