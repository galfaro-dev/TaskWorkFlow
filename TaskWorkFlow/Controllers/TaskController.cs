using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskWorkFlow.Application.UseCases.BlockTask;
using TaskWorkFlow.Application.UseCases.CompleteTask;
using TaskWorkFlow.Application.UseCases.CreateTask;
using TaskWorkFlow.Application.UseCases.GetTask;
using TaskWorkFlow.Application.UseCases.StartTask;
using TaskWorkFlow.Application.UseCases.UnBlockTask;

namespace TaskWorkFlow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly CreateTaskUseCase _createTaskUseCase;
        private readonly GetTaskByIdUseCase _getTaskByIdUseCase;
        private readonly StartTaskUseCase _startTaskUseCase;
        private readonly CompleteTaskUseCase _completeTaskUseCase;
        private readonly BlockTaskUseCase _blockTaskUseCase;
        private readonly UnBlockTaskUseCase _unBlockTaskUseCase;



        public TaskController(CreateTaskUseCase createTaskUseCase,
                            GetTaskByIdUseCase getTaskByIdUseCase,
                            StartTaskUseCase startTaskUseCase,
                            CompleteTaskUseCase completeTaskUseCase,
                            BlockTaskUseCase blockTaskUseCase,
                            UnBlockTaskUseCase unBlockTaskUseCase)
        {
            _createTaskUseCase = createTaskUseCase;
            _getTaskByIdUseCase = getTaskByIdUseCase;
            _startTaskUseCase = startTaskUseCase;
            _completeTaskUseCase = completeTaskUseCase;
            _blockTaskUseCase = blockTaskUseCase;
            _unBlockTaskUseCase = unBlockTaskUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskRequest request)
        {
            var taskId = await _createTaskUseCase.ExecuteAsync(
                request.Title,
                request.Description
            );

            return CreatedAtAction(
                nameof(GetById),
                new { id = taskId },
                null
            );

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _getTaskByIdUseCase.ExecuteAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }
        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(Guid id)
        {
            await _startTaskUseCase.ExecuteAsync(id);
            return NoContent();
        }
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(Guid id)
        {
            await _completeTaskUseCase.ExecuteAsync(id);
            return NoContent();
        }
        [HttpPut("{id}/block")]
        public async Task<IActionResult> Block(Guid id)
        {
            await _blockTaskUseCase.ExecuteAsync(id);
            return NoContent();
        }

        [HttpPut("{id}/unblock")]
        public async Task<IActionResult> Unblock(Guid id)
        {
            await _unBlockTaskUseCase.ExecuteAsync(id);
            return NoContent();
        }











    }
    public record CreateTaskRequest(string Title, string Description);
}
