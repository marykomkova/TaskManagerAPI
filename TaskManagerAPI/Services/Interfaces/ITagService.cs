using TaskManagerAPI.Models;
using Task = TaskManagerAPI.Models.Task;

namespace TaskManagerAPI.Services.Interfaces
{
    public interface ITagService
    {
        Tag GetTag(int id);
        Tag GetTag(string name);
        List<Tag> GetTages();
        bool CreateTag(Tag tag);
        public List<string> GetAllTagNames();
        bool DeleteTag(Tag tag);
        bool TagExists(int id);
    }
}
