using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorkFlow.Application.DTOs.Tasks;
using TaskWorkFlow.Application.Interfaces.Persistence;
using TaskWorkFlow.Domain.Enums;

namespace TaskWorkFlow.Application.UseCases.GetAllTasks
{
    public class GetAllTasksUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public GetAllTasksUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IReadOnlyList<TaskListItemDto>> ExecuteAsync(
            TaskState? state,
            string? title)
        {
            var tasks = await _taskRepository.GetAllAsync(state, title);

            return tasks.Select(t => new TaskListItemDto
            {
                Id = t.Id,
                Title = t.Title,
                State = t.State,
                CreatedAt = t.CreatedAt
            }).ToList();
        }
    }
}
