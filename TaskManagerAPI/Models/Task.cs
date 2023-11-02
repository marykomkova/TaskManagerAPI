using System;
namespace TaskManagerAPI.Models
{
	public class Task
	{
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
        public List<TaskTag> TaskTags { get; set; }
    }
}

