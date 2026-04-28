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

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


//DbContext
// Localiza tu configuración de DbContext y déjala así:
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptionsAction: sqlOptions =>
        {
            // Esto es lo profesional: si falla, reintenta automáticamente
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        }
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


// BLOQUE PROFESIONAL DE MIGRACIONES
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<TaskDbContext>();
        // Esto aplica las migraciones pendientes al iniciar el contenedor
        context.Database.Migrate();
        Console.WriteLine("--> Base de datos migrada con éxito.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"--> Error aplicando migraciones: {ex.Message}");
    }
}


app.UseCors("AngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
