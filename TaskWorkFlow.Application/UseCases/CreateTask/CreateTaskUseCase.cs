using TaskWorkFlow.Application.Interfaces.Persistence;
using TaskWorkFlow.Domain.Entities;

namespace TaskWorkFlow.Application.UseCases.CreateTask
{
    public class CreateTaskUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<Guid> ExecuteAsync(string title, string description)
        {
            var task = new TaskItem(title, description);

            await _taskRepository.AddAsync(task);

            return task.Id;
        }
    }
}
