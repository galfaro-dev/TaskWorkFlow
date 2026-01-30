
using TaskWorkFlow.Domain.Entities;

namespace TaskWorkFlow.Application.Interfaces.Persistence
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
    }
}
