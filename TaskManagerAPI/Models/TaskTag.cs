using System;
namespace TaskManagerAPI.Models
{
	public class TaskTag
    {
        public int TaskId { get; set; }
        public int TagId { get; set; }
        public virtual Task Task { get; set; }
        public virtual  Tag Tag { get; set; }
    }
}

