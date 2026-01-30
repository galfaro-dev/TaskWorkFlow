using TaskWorkFlow.Application.Interfaces.Persistence;

namespace TaskWorkFlow.Application.UseCases.CompleteTask
{
    public class CompleteTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public CompleteTaskUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task ExecuteAsync(Guid taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task is null)
                throw new InvalidOperationException("Task not found.");

            task.Complete();

            await _taskRepository.UpdateAsync(task);
        }
    }
}
