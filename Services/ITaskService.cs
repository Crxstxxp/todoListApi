using todoListApi.DTOs;

namespace todoListApi.Services
{
    public interface ITaskService
    {
        Task<List<TaskResponse>> GetAllTasks(string userId);
        Task<TaskResponse?> GetTaskById(int taskId);
        Task<TaskResponse> CreateTask(string userId, Task task);
        Task<TaskResponse?> UpdateTask(int taskId, Task task);
        Task<TaskResponse?> DeleteTask(string userId, int taskId);
    }
}
