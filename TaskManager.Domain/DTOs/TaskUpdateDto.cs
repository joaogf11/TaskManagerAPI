using System.ComponentModel.DataAnnotations;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.DTOs
{
    public class TaskUpdateDto
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }
        
        [StringLength(500)]
        public string Description { get; set; }
        
        public bool IsCompleted { get; set; }
        
        public DateTime? DueDate { get; set; }
        
        public TaskPriority Priority { get; set; }
    }
}
