using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public class PostsService: IPostsService
    {
        private readonly List<Post> _posts;

        public PostsService()
        {
            _posts = new List<Post>();

            for (int i = 0; i < 6; i++)
            {
                _posts.Add(new Post { Id = Guid.NewGuid(), Name = $"Post name: {i}" });
            }
        }

        public List<Post> GetAll()
        {
            return _posts;
        }

        public Post GetPostById(Guid postId)
        {
            return _posts.SingleOrDefault(p => p.Id == postId);
        }

        public bool UpdatePost(Post postToUpdate)
        {
            var exists = GetPostById(postToUpdate.Id) != null;

            if (!exists)
            {
                return false;
            }

            var index = _posts.FindIndex(p => p.Id.Equals(postToUpdate.Id));

            _posts[index] = postToUpdate;

            return true;
        }
    }
}
