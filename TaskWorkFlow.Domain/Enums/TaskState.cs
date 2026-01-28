using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWorkFlow.Domain.Enums
{
    public enum TaskState
    {
        Pending = 1,
        InProgress = 2,
        Blocked = 3,
        Completed = 4
    }
}
