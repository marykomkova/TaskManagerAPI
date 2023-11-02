using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;
using Task = TaskManagerAPI.Models.Task;

namespace TaskManagerAPI.Services.Interfaces
{
    public interface ITaskService
    {
        Task GetTask(int id);
        Task GetTask(string name);
        List<Task> GetTasks();
        bool AddTagToTask(int taskId, string tag);
        bool CreateTask(Task task);
        bool DeleteTask(Task task); 
        bool TaskExists(int id);
    }
}
