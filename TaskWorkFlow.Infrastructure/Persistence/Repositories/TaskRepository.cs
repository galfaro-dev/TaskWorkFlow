using Microsoft.EntityFrameworkCore;
using TaskWorkFlow.Application.Interfaces.Persistence;
using TaskWorkFlow.Domain.Entities;
using TaskWorkFlow.Domain.Enums;

namespace TaskWorkFlow.Infrastructure.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _context;

    public TaskRepository(TaskDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(TaskItem task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }
    public async Task<IReadOnlyList<TaskItem>> GetByStateAsync(TaskState state)
    {
        return await _context.Tasks
            .Where(t => t.State == state)
            .OrderByDescending(t => t.CreatedAt) 
            .ToListAsync();
    }

    public async Task<IReadOnlyList<TaskItem>> GetAllAsync(TaskState? state, string? title)
    {
        var query = _context.Tasks.AsQueryable();

        if (state.HasValue)
            query = query.Where(t => t.State == state.Value);

        if (!string.IsNullOrWhiteSpace(title))
            query = query.Where(t => t.Title.Contains(title));

        return await query
            .OrderByDescending(t => t.CreatedAt) 
            .ToListAsync();
    }

    public async Task<(IReadOnlyList<TaskItem> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize, TaskState? state=null)
    {
        var query = _context.Tasks.AsNoTracking();

        // Si mandan un estado, filtramos antes de contar y paginar
        if (state.HasValue)
            query = query.Where(t => t.State == state.Value);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(t => t.CreatedAt) 
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }


}
