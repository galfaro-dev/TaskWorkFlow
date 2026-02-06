using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TaskWorkFlow.Infrastructure.Persistence
{

    public class TaskDbContextFactory
        : IDesignTimeDbContextFactory<TaskDbContext>
    {
        public TaskDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TaskDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=TaskWorkFlowDb;Trusted_Connection=True;TrustServerCertificate=True"
            );

            return new TaskDbContext(optionsBuilder.Options);
        }
    }
}