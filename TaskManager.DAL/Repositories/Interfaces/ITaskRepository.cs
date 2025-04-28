using TaskManager.Domain.Entities;

namespace TaskManager.DAL.Repositories.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskItem>> GetAllByUserIdAsync(string userId);
        Task<TaskItem> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem task);
        Task<bool> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);
        Task<bool> BelongsToUserAsync(int taskId, string userId);
    }
}
