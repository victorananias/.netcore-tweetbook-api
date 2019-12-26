using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cosmonaut;
using Cosmonaut.Extensions;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public class CosmosPostService : IPostsService
    {
        private readonly ICosmosStore<CosmosPostDto> _cosmosStore;
        public CosmosPostService(ICosmosStore<CosmosPostDto> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }

        public async Task<List<Post>> GetAllAsync()
        {
            var posts = await _cosmosStore.Query().ToListAsync();
            return posts.Select(p => new Post {Id = Guid.Parse(p.Id), Name = p.Name}).ToList();
        }

        public Task<Post> GetPostByIdAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateAsync(Post post)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePostAsync(Guid postId)
        {
            throw new NotImplementedException();
        }
    }
}
