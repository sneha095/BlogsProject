using BlogsProject.Controllers;
using BlogsProject.Models;
using BlogsProject.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlogProjctTest.cs
{
    public class BlogControllerTest
    {
        private readonly Mock<IBlogRepository> _mockRepo;
        private readonly BlogController _controller;

        public BlogControllerTest()
        {
            _mockRepo = new Mock<IBlogRepository>();
            _controller = new BlogController(_mockRepo.Object);
        }

        IEnumerable<Blog> blogList = new List<Blog>
        {
            new Blog
            {
                BlogId=1, BlogName="ABC CEF", Description="Did shy say mention enabled through elderly improve. As at so believe account " +
                "evening behaved hearted is. House is tiled we aware.",

                UserId=1
            },

            new Blog
            {
                BlogId=2, BlogName="CDF EFG", Description="Did shy say mention enabled through elderly improve. As at so believe account " +
                "evening behaved hearted is. House is tiled we aware.",

                UserId=2
            },

        };

        [Fact]
        public async void GetAllBlogs()
        {
            //Arrange
            _mockRepo.Setup(repo => repo.GetBlogsAsync())
                     .Returns(Task.FromResult(blogList));

            //Act
            var result = await _controller.GetBlogs() as OkObjectResult;
            var methodResult = Assert.IsType<OkObjectResult>(result as OkObjectResult);
            var blogsList = methodResult.Value as IList<Blog>;

            //Assert
            Assert.Equal(2, blogsList.Count);
        }

        [Fact]
        public async void GetAllBlogsById_ReturnsDetailsSuccessfull()
        {
            int testId = 2;
            _mockRepo.Setup(repo => repo.GetBlogByIdAsync(testId))
                     .Returns(Task.FromResult(blogList));

            var okResult = await _controller.GetBlogById(testId) as OkObjectResult;
            var methodResult = Assert.IsType<OkObjectResult>(okResult as OkObjectResult);
            List<Blog> blogsList = methodResult.Value as List<Blog>;

            Assert.Equal(testId, blogsList[1].BlogId);
        }

        [Fact]
        public async void Add_InvalidObjectIsPassed_ReturnsBadRequest()
        {
            Blog blogObj = new Blog()
            {
               
                Description="abc der qwerty uiop ahsgdh",
                UserId=2
            };
            _mockRepo.Setup(repo => repo.AddBlogAsync(blogObj))
                .Returns(Task.FromResult(blogObj));
            _controller.ModelState.AddModelError("BlogId", "Required");

            //Act
            var badResponse = await _controller.AddBlog(blogObj);

            //Assert
            Assert.IsType<BadRequestResult>(badResponse);

        }

        [Fact]
        public async void AddBlog_ReturnsCorrectStatusCode()
        {
            Blog blogObj = new Blog()
            {
                BlogName="Abc Def hijk",
                Description = "abc der qwerty uiop ahsgdh",
                UserId = 2
            };
            _mockRepo.Setup(repo => repo.AddBlogAsync(blogObj))
                .Returns(Task.FromResult(blogObj));
            _controller.ModelState.AddModelError("BlogId", "Required");

            //Act
            var createdResponse = await _controller.AddBlog(blogObj);

            //Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);

        }

        [Fact]
        public async void Add_ValidObjectPassed_returnsCreatedItem()
        {
            Blog blogObj = new Blog()
            {
                BlogName = "Abc Def hijk",
                Description = "abc der qwerty uiop ahsgdh",
                UserId = 2
            };

            _mockRepo.Setup(repo => repo.AddBlogAsync(blogObj))
             .Returns(Task.FromResult(blogObj));

            //Act
            var createdResponse = await _controller.AddBlog(blogObj) as CreatedAtActionResult;
            var returnedBlog = createdResponse.Value as Blog;

            //Assert
            Assert.IsType<Blog>(returnedBlog);
            Assert.Equal("Abc Def hijk", returnedBlog.BlogName);

        }

    }
}
