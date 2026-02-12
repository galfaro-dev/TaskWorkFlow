using Microsoft.EntityFrameworkCore;
using TaskWorkFlow.Application.Interfaces.Persistence;
using TaskWorkFlow.Application.UseCases.BlockTask;
using TaskWorkFlow.Application.UseCases.CompleteTask;
using TaskWorkFlow.Application.UseCases.CreateTask;
using TaskWorkFlow.Application.UseCases.GetAllTasks;
using TaskWorkFlow.Application.UseCases.GetTask;
using TaskWorkFlow.Application.UseCases.GetTaskByState;
using TaskWorkFlow.Application.UseCases.GetTasksPaged;
using TaskWorkFlow.Application.UseCases.StartTask;
using TaskWorkFlow.Application.UseCases.UnBlockTask;
using TaskWorkFlow.Infrastructure.Persistence;
using TaskWorkFlow.Infrastructure.Persistence.Repositories;
using TaskWorkFlow.Middlewares;

var builder = WebApplication.CreateBuilder(args);

//Create Task
builder.Services.AddScoped<CreateTaskUseCase>();
//GetTaskById
builder.Services.AddScoped<GetTaskByIdUseCase>();
//StartTask
builder.Services.AddScoped<StartTaskUseCase>();
//CompleteTask
builder.Services.AddScoped<CompleteTaskUseCase>();
//Blocked
builder.Services.AddScoped<BlockTaskUseCase>();
//Unblock
builder.Services.AddScoped<UnBlockTaskUseCase>();
//GetStateByState
builder.Services.AddScoped<GetTasksByStateUseCase>();
//GetAllTasks
builder.Services.AddScoped<GetAllTasksUseCase>();
//GetPagination
builder.Services.AddScoped<GetTasksPagedUseCase>();








//DbContext
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Repository
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
