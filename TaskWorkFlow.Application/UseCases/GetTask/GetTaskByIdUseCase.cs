using TaskWorkFlow.Application.Interfaces.Persistence;
using TaskWorkFlow.Domain.Entities;

namespace TaskWorkFlow.Application.UseCases.GetTask;

public class GetTaskByIdUseCase
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdUseCase(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskItem?> ExecuteAsync(Guid id)
    {
        return await _taskRepository.GetByIdAsync(id);
    }
}
