using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWorkFlow.Domain.Enums;

namespace TaskWorkFlow.Application.DTOs.Tasks
{
    public  class TaskListItemDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public TaskState State { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
