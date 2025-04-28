using TaskManager.BLL.Services.Interfaces;
using TaskManager.DAL.Repositories.Interfaces;
using TaskManager.Domain.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync(string userId)
        {
            var tasks = await _taskRepository.GetAllByUserIdAsync(userId);
            return tasks.Select(MapToTaskDto);
        }

        public async Task<TaskDto> GetTaskByIdAsync(int id, string userId)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            
            if (task == null || task.UserId != userId)
                return null;
                
            return MapToTaskDto(task);
        }

        public async Task<TaskDto> CreateTaskAsync(TaskCreateDto taskDto, string userId)
        {
            var task = new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                DueDate = taskDto.DueDate,
                Priority = taskDto.Priority,
                UserId = userId
            };

            var createdTask = await _taskRepository.CreateAsync(task);
            return MapToTaskDto(createdTask);
        }

        public async Task<ServiceResultDto> UpdateTaskAsync(int id, TaskUpdateDto taskDto, string userId)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            
            if (task == null)
                return new ServiceResultDto { Success = false, Message = "Task not found" };
                
            if (task.UserId != userId)
                return new ServiceResultDto { Success = false, Message = "You don't have permission to update this task" };

            task.Title = taskDto.Title;
            task.Description = taskDto.Description;
            task.IsCompleted = taskDto.IsCompleted;
            task.DueDate = taskDto.DueDate;
            task.Priority = taskDto.Priority;

            var result = await _taskRepository.UpdateAsync(task);
            
            if (!result)
                return new ServiceResultDto { Success = false, Message = "Failed to update task" };
                
            return new ServiceResultDto 
            { 
                Success = true, 
                Message = "Task updated successfully",
                Data = MapToTaskDto(task)
            };
        }

        public async Task<ServiceResultDto> DeleteTaskAsync(int id, string userId)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            
            if (task == null)
                return new ServiceResultDto { Success = false, Message = "Task not found" };
                
            if (task.UserId != userId)
                return new ServiceResultDto { Success = false, Message = "You don't have permission to delete this task" };

            var result = await _taskRepository.DeleteAsync(id);
            
            if (!result)
                return new ServiceResultDto { Success = false, Message = "Failed to delete task" };
                
            return new ServiceResultDto { Success = true, Message = "Task deleted successfully" };
        }

        private TaskDto MapToTaskDto(TaskItem task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                Priority = task.Priority
            };
        }
    }
}
