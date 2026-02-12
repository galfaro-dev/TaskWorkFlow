using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorkFlow.Application.DTOs.Common
{
    public  class PagedResultDto<T>
    {
        public IReadOnlyList<T> Items { get; init; } = new List<T>();
        public int TotalCount { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int TotalPages { get; init; }
    }
}
