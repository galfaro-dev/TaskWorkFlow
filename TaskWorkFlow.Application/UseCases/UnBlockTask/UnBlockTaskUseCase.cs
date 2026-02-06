using TaskWorkFlow.Application.Interfaces.Persistence;
using TaskWorkFlow.Domain.Exceptions;

namespace TaskWorkFlow.Application.UseCases.UnBlockTask
{
    public class UnBlockTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public UnBlockTaskUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task ExecuteAsync(Guid taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task is null)
                throw new NotFoundException("Task not found.");

            task.Unblock();

            await _taskRepository.UpdateAsync(task);
        }
    }
}
