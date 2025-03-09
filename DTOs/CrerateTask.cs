using AutoMapper;
using todoListApi.Migrations;
using todoListApi.Models;

namespace todoListApi.DTOs
{
    public class TaskRequest
    {
        public required string Title { get; set; } = string.Empty;
        public required string Description { get; set; } = string.Empty;
        public required bool IsCompleted { get; set; } = false;
    }

    public class TaskResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskRequest, Task>();
            CreateMap<Task, TaskResponse>();
        }
    }
}