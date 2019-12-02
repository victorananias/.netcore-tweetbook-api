using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Domain;

namespace Tweetbook.Services
{
    public interface IPostsService
    {
        List<Post> GetAll();
        Post GetPostById(Guid postId);
        bool UpdatePost(Post postToUpdate);
    }
}
