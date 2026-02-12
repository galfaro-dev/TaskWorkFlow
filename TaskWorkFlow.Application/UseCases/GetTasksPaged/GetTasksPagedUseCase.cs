using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorkFlow.Application.DTOs.Common;
using TaskWorkFlow.Application.DTOs.Tasks;
using TaskWorkFlow.Application.Interfaces.Persistence;

namespace TaskWorkFlow.Application.UseCases.GetTasksPaged
{
    public class GetTasksPagedUseCase
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksPagedUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<PagedResultDto<TaskListItemDto>> ExecuteAsync(int pageNumber,int pageSize)
        {
            if (pageNumber <= 0)
                throw new ArgumentException("Page number must be greater than 0.");

            if (pageSize <= 0 || pageSize > 100)
                throw new ArgumentException("Page size must be between 1 and 100.");

            var (items, totalCount) =
                await _taskRepository.GetPagedAsync(pageNumber, pageSize);

            var dtoItems = items.Select(t => new TaskListItemDto
            {
                Id = t.Id,
                Title = t.Title,
                State = t.State,
                CreatedAt = t.CreatedAt
            }).ToList();

            return new PagedResultDto<TaskListItemDto>
            {
                Items = dtoItems,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalCount / pageSize)
            };
        }
    }
}
