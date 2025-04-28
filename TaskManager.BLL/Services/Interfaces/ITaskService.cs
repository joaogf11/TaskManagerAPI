using TaskManager.Domain.DTOs;

namespace TaskManager.BLL.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync(string userId);
        Task<TaskDto> GetTaskByIdAsync(int id, string userId);
        Task<TaskDto> CreateTaskAsync(TaskCreateDto taskDto, string userId);
        Task<ServiceResultDto> UpdateTaskAsync(int id, TaskUpdateDto taskDto, string userId);
        Task<ServiceResultDto> DeleteTaskAsync(int id, string userId);
    }
}
