using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Domain;
using Xunit;

namespace Tweetbook.IntegrationTests
{
    public class PostsControllerTests: IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnsEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();
            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Posts.GetAll);
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(await response.Content.ReadAsAsync<List<Post>>());
        }

        [Fact]
        public async Task Get_ReturnsPost_WhenPostExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateAsync();
            var createdPost = await CreatePostAsync(new CreatePostRequest { Name = "Test post"});
            
            // Act
            var response = await TestClient.GetAsync(
                ApiRoutes.Posts.Get.Replace("{postId}", createdPost.Id.ToString())
            );
            var post = await response.Content.ReadAsAsync<PostResponse>();
            // Assert
            Assert.Equal(createdPost.Name, post.Name);
        }
    }
}