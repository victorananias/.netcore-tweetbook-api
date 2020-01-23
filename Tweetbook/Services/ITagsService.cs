using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public interface ITagsService
    {
        Task AddTagsFromPostAsync(Post post);
        Task<List<Tag>> GetAllAsync();
        Task<Tag> GetTagByNameAsync(string requestName);
        Task<bool> CreateAsync(Tag tag);
    }
}