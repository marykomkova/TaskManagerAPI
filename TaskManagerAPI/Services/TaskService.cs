using System;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.EFCore;
using TaskManagerAPI.Services.Interfaces;
using Task = TaskManagerAPI.Models.Task;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data;
using TaskManagerAPI.Models;

namespace UsersCRUDAPI.Helpers
{
	public class TaskService : ITaskService
	{
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Task> GetTasks()
        {
            return _context.Tasks
                .Include(t => t.TaskTags)
                .ThenInclude(tt => tt.Tag)
                .ToList();
        }

        public Task GetTask(int id)
        {
            if (id < 0)
                return null;

            var task = _context.Tasks
                .Include(t => t.TaskTags)
                .ThenInclude(tt => tt.Tag)
                .FirstOrDefault(u => u.Id == id);

            return task;
        }

        public Task GetTask(string name)
        {
            return _context.Tasks.Where(t => t.Name == name).FirstOrDefault();
        }

        public bool CreateTask(Task task)
        {
            _context.Add(task);

            return Save();
        }

        public bool DeleteTask(Task task)
        {
            _context.Remove(task);
            return Save();
        }

        public bool AddTagToTask(int taskId, string tagName)
        {
            if (!TaskExists(taskId))
            {
                return false;
            }

            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);

            var existingTag = _context.Tages.FirstOrDefault(t => t.Name == tagName);

            if (existingTag == null)
            {
                existingTag = new Tag { Name = tagName };
                _context.Tages.Add(existingTag);
            }

            var userRole = new TaskTag { TaskId = task.Id, TagId = existingTag.Id };

            _context.Add(userRole);

            return Save();
        }

        public bool TaskExists(int id)
        {
            return _context.Tasks.Any(t => t.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
