using Microsoft.EntityFrameworkCore;
using TaskWorkFlow.Domain.Entities;

namespace TaskWorkFlow.Infrastructure.Persistence;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}
