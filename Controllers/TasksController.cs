using Microsoft.AspNetCore.Mvc;
using ToDoApp.Repositories;
using ToDoApp.Request;

namespace ToDoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetData()
        {
            var listData = await _taskRepository.GetTasks();
            return Ok(listData);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddData(AddTask request)
        {
            await _taskRepository.AddTask(request.Name, request.IsComplete);
            return NoContent();
        }

        [HttpPut("MarkAsComplete/{id}")]
        public async Task<IActionResult> MarkAsComplete([FromRoute] string id)
        {
            await _taskRepository.MarkAsComplete(id);
            return NoContent();
        }

    }
}
