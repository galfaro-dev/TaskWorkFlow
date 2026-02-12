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
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title is required.");

            if (title.Length > 200)
                throw new ArgumentException("Title cannot exceed 200 characters.");

            if (!string.IsNullOrEmpty(description) && description.Length > 1000)
                throw new ArgumentException("Description cannot exceed 1000 characters.");

            var task = new TaskItem(title, description);

            await _taskRepository.AddAsync(task);

            return task.Id;
        }
    }
}
