using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tweetbook.Data;
using Tweetbook.Domain;
using Tweetbook.Services;
using Xunit;

namespace Tweetbook.UnitTests
{
    public class TagsServiceTests
    {
        private readonly ITagsService _tagsService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IdentityUser _user;

        public TagsServiceTests()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("testDb");
            
            _dbContext = new ApplicationDbContext(builder.Options);

            _tagsService = new TagsService(_dbContext);
        }

        [Fact]
        public async Task CreateAsync_InputIsValidTag()
        {
            // Arrange
            var tag = new Tag
            {
                Name = "test",
                CreatedOn = DateTime.Now,
                CreatorId = "testUser"
            };

            // Act
            await _tagsService.CreateAsync(tag);
            var tags = await _tagsService.GetAllAsync();
            
            // Assert
            Assert.Single(tags);
        }
    }
}