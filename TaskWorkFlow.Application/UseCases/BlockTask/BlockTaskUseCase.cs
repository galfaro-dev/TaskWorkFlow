
using TaskWorkFlow.Application.Interfaces.Persistence;
using TaskWorkFlow.Domain.Exceptions;

namespace TaskWorkFlow.Application.UseCases.BlockTask
{
    public class BlockTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public BlockTaskUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task ExecuteAsync(Guid taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task is null)
                throw new NotFoundException("Task not found.");

            task.Block();

            await _taskRepository.UpdateAsync(task);
        }
    }
}
