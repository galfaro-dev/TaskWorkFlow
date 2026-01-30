namespace TaskWorkFlow.Domain.Entities;

using TaskWorkFlow.Domain.Enums;

public class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public TaskState State { get; private set; }

    public Guid? AssignedUserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private TaskItem() { }

    public TaskItem(string title, string description)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        State = TaskState.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    // ===== COMPORTAMIENTO DEL DOMINIO =====

    public void Start()
    {
        if (State != TaskState.Pending)
            throw new InvalidOperationException("Only pending tasks can be started.");

        State = TaskState.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (State != TaskState.InProgress)
            throw new InvalidOperationException("Only in-progress tasks can be completed.");

        State = TaskState.Completed;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Block()
    {
        if (State == TaskState.Completed)
            throw new InvalidOperationException("Completed tasks cannot be blocked.");

        State = TaskState.Blocked;
        UpdatedAt = DateTime.UtcNow;
    }
    public void Unblock()
    {
        if (State != TaskState.Blocked)
            throw new InvalidOperationException("Only blocked tasks can be unblocked.");

        State = TaskState.InProgress;
        UpdatedAt = DateTime.UtcNow;
    }

}
