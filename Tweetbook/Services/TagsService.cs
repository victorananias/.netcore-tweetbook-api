using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tweetbook.Data;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public class TagsService : ITagsService
    {
        private readonly ApplicationDbContext _context;

        public TagsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTagsFromPostAsync(Post post)
        {
            foreach (var postTag in post.Tags)
            {
                var existingTag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == postTag.TagName);

                if (existingTag != null)
                {
                    continue;
                }

                _context.Tags.Add(new Tag {Name = postTag.TagName});
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetTagByNameAsync(string name)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task<bool> CreateAsync(Tag tag)
        {
            _context.Tags.Add(tag);
            var created = await _context.SaveChangesAsync();
            return created > 0;
        }
    }
}