using Microsoft.EntityFrameworkCore;
using todoListApi.DTOs;
using todoListApi.Models;
using AutoMapper;
using todoListApi.data;

namespace todoListApi.Services
{
    public class TaskService : ITaskService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TaskService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TaskResponse>> GetAllTasks(string userId)
        {
            var tasks = await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
            return _mapper.Map<List<TaskResponse>>(tasks);
        }

        public async Task<TaskResponse?> GetTaskById(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            return task == null ? null : _mapper.Map<TaskResponse>(task);
        }

        public async Task<TaskResponse> CreateTask(string userId, TaskRequest data)
        {
            var task = _mapper.Map<Tasks>(data);
            task.UserId = userId;

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return _mapper.Map<TaskResponse>(task);
        }

        public async Task<TaskResponse?> UpdateTask(int taskId, Task data)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null) return null;

            var taskUpdated = _mapper.Map(data, task);
            await _context.SaveChangesAsync();

            return _mapper.Map<TaskResponse>(taskUpdated);
        }

        public async Task<TaskResponse?> DeleteTask(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null) return null;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return _mapper.Map<TaskResponse>(task);
        }
    }
}