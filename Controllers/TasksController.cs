using Microsoft.AspNetCore.Mvc;
using todoListApi.DTOs;
using todoListApi.Services;

[Route("api/tasks")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetTasks(string userId)
    {
        var tasks = await _taskService.GetAllTasks(userId);

        if (tasks == null)
            return NotFound();

        return Ok(tasks);
    }


    [HttpGet("{taskId:int}")]
    public async Task<IActionResult> GetTask(int taskId)
    {
        var task = await _taskService.GetTaskById(taskId);

        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost("{userId}")]
    public async Task<IActionResult> CreateTask([FromRoute] string userId, [FromBody] TaskRequest request)
    {
        var task = await _taskService.CreateTask(userId, request);
        return CreatedAtAction(nameof(GetTask), new { taskId = task.Id }, task);
    }

    [HttpPut("{taskId:int}")]
    public async Task<IActionResult> UpdateTask([FromRoute] int taskId, [FromBody] Task task)
    {
        var taskUpdated = await _taskService.UpdateTask(taskId, task);

        if (taskUpdated == null)
            return NotFound();

        return Ok(taskUpdated);
    }

    [HttpDelete("{taskId:int}")]
    public async Task<IActionResult> DeleteTask(int taskId)
    {
        var task = await _taskService.DeleteTask(taskId);

        if (task == null)
            return NotFound();

        return Ok(task);
    }
}