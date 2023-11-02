using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagerAPI.EFCore;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services.Interfaces;

namespace TaskManagerAPI.Services
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;

        public TagService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Tag> GetTages()
        {
            return _context.Tages
            .ToList();
        }

        public Tag GetTag(int id)
        {
            if (id < 0)
                return null;

            var tag = _context.Tages
                .Include(t => t.TaskTags)
                .ThenInclude(tt => tt.Task)
                .FirstOrDefault(u => u.Id == id);

            return tag;
        }

        public Tag GetTag(string name)
        {
            return _context.Tages.Where(t => t.Name == name).FirstOrDefault();
        }

        public bool CreateTag(Tag tag)
        {
            _context.Add(tag);

            return Save();
        }

        public bool DeleteTag(Tag tag)
        {
            _context.Remove(tag);
            return Save();
        }

        public bool TagExists(int id)
        {
            return _context.Tages.Any(t => t.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public List<string> GetAllTagNames()
        {
            var tagNames = _context.Tages.Select(tag => tag.Name.ToUpper()).ToList();

            return tagNames;
        }

    }
}
