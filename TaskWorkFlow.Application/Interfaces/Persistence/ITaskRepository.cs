using TaskWorkFlow.Domain.Entities;
using TaskWorkFlow.Domain.Enums;

namespace TaskWorkFlow.Application.Interfaces.Persistence
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task<IReadOnlyList<TaskItem>> GetByStateAsync(TaskState state);
    }
}
