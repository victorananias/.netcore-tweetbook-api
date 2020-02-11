using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tweetbook.Data;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public class PostsService : IPostsService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITagsService _tagsService;

        public PostsService(ApplicationDbContext context, ITagsService tagsService)
        {
            _context = context;
            _tagsService = tagsService;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            return await _context.Posts.Include(p => p.Tags).ToListAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            return await _context.Posts.Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<bool> CreateAsync(Post post)
        {
            await _context.Posts.AddAsync(post);

            await _tagsService.AddTagsFromPostAsync(post);

            var created = await _context.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            _context.Posts.Update(postToUpdate);

            var updated = await _context.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var post = await GetPostByIdAsync(postId);

            if (post == null)
            {
                return false;
            }

            _context.Posts.Remove(post);

            var deleted = await _context.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<bool> UserOwnsPostAsync(Guid postId, string userId)
        {
            var post = await _context.Posts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == postId);
            return post == null || post.UserId != userId;
        }
    }
}