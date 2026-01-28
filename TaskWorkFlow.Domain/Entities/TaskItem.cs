using TaskWorkFlow.Domain.Enums;

namespace TaskWorkFlow.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public TaskState Status { get; private set; }

    public Guid? AssignedUserId { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    private TaskItem() { } // requerido para EF (más adelante)

    public TaskItem(string title, string description)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Status = TaskState.Pending;
        CreatedAt = DateTime.UtcNow;
    }
}
