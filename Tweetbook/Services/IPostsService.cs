using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public interface IPostsService
    {
        Task<List<Post>> GetAllAsync();
        Task<Post> GetPostByIdAsync(Guid postId);
        Task<bool> CreateAsync(Post post);
        Task<bool> UpdatePostAsync(Post postToUpdate);
        Task<bool> DeletePostAsync(Guid postId);
    }
}
