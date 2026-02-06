using TaskWorkFlow.Application.Interfaces.Persistence;
using TaskWorkFlow.Domain.Entities;
using TaskWorkFlow.Domain.Exceptions;

namespace TaskWorkFlow.Application.UseCases.StartTask
{
    public class StartTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public StartTaskUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task ExecuteAsync(Guid taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task is null)
                throw new NotFoundException("Task not found.");

            task.Start();

            await _taskRepository.UpdateAsync(task);
        }
    }
}
