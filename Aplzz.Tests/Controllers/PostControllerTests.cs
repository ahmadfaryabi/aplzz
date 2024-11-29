using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Aplzz.Controllers;
using Aplzz.DAL;
using Aplzz.Models;
using System.Threading.Tasks;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;

namespace Aplzz.Tests.Controllers
{
    public class PostControllerTests
    {
        private readonly Mock<IPostRepository> _mockPostRepository;
        private readonly Mock<ILogger<PostController>> _mockLogger;
        private readonly PostController _controller;

        public PostControllerTests()
        {
            _mockPostRepository = new Mock<IPostRepository>();
            _mockLogger = new Mock<ILogger<PostController>>();
            _controller = new PostController(_mockPostRepository.Object, _mockLogger.Object);

            // Setup HttpContext with Session
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            _controller.TempData = tempData;
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }

        private void SetupSession(string userId = "1", string username = "testbruker")
        {
            var sessionItems = new Dictionary<string, object>
            {
                { "id", userId },
                { "username", username }
            };

            var session = new Mock<ISession>();
            session.Setup(s => s.TryGetValue(It.IsAny<string>(), out It.Ref<byte[]>.IsAny))
                .Callback((string key, out byte[] value) =>
                {
                    if (sessionItems.TryGetValue(key, out var objValue))
                    {
                        value = System.Text.Encoding.UTF8.GetBytes(objValue.ToString());
                    }
                    else
                    {
                        value = null;
                    }
                });

            var context = new DefaultHttpContext();
            context.Session = session.Object;
            _controller.ControllerContext.HttpContext = context;
        }

        [Fact]
        public async Task Create_WithValidPost_ReturnsRedirectToIndex()
        {
            // Arrange
            SetupSession();
            var post = new Post
            {
                Content = "Test innhold",
                UserId = 1
            };
            
            _mockPostRepository.Setup(repo => repo.Create(It.IsAny<Post>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(post, null);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task AddComment_WithValidData_ReturnsJsonResult()
        {
            // Arrange
            SetupSession();
            var postId = 1;
            var commentText = "Test kommentar";
            var currentTime = DateTime.Now;
            
            _mockPostRepository.Setup(repo => repo.AddComment(It.IsAny<Comment>()))
                .ReturnsAsync(true);

            // Act
            var result = await _controller.AddComment(postId, commentText);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var resultValue = jsonResult.Value;
            Assert.Equal(commentText, resultValue.GetType().GetProperty("text").GetValue(resultValue, null));
            Assert.NotNull(resultValue.GetType().GetProperty("commentedAt").GetValue(resultValue, null));
        }

        [Fact]
        public async Task Delete_WithUnauthorizedUser_ReturnsNoAccessView()
        {
            // Arrange
            SetupSession();
            var post = new Post
            {
                PostId = 1,
                UserId = 2, // Different from session user ID (1)
                Content = "Test innhold"
            };

            _mockPostRepository.Setup(repo => repo.GetPostById(1))
                .ReturnsAsync(post);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("NoAccess", viewResult.ViewName);
        }

        [Fact]
        public async Task LikePost_TogglesLikeStatus_ReturnsCorrectJson()
        {
            // Arrange
            SetupSession();
            var postId = 1;
            var userId = 1;
            var initialLikeStatus = false;
            var newLikeCount = 1;

            _mockPostRepository.Setup(repo => repo.HasUserLikedPost(postId, userId))
                .ReturnsAsync(initialLikeStatus);
            _mockPostRepository.Setup(repo => repo.AddLike(It.IsAny<Like>()))
                .ReturnsAsync(true);
            _mockPostRepository.Setup(repo => repo.GetLikeCount(postId))
                .ReturnsAsync(newLikeCount);

            // Act
            var result = await _controller.LikePost(postId);

            // Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            var resultValue = jsonResult.Value;
            Assert.Equal(newLikeCount, resultValue.GetType().GetProperty("likesCount").GetValue(resultValue, null));
            Assert.True((bool)resultValue.GetType().GetProperty("isLiked").GetValue(resultValue, null));
        }

        [Fact]
        public async Task Update_WithValidPost_ReturnsRedirectToIndex()
        {
            // Arrange
            SetupSession();
            var postId = 1;
            var originalPost = new Post
            {
                PostId = postId,
                UserId = 1,
                Content = "Originalt innhold"
            };
            var updatedPost = new Post
            {
                PostId = postId,
                UserId = 1,
                Content = "Oppdatert innhold"
            };

            _mockPostRepository.Setup(repo => repo.GetPostById(postId))
                .ReturnsAsync(originalPost);
            _mockPostRepository.Setup(repo => repo.Update(It.IsAny<Post>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(postId, updatedPost, null);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
            _mockPostRepository.Verify(repo => repo.Update(It.Is<Post>(p => p.Content == "Oppdatert innhold")), Times.Once);
        }

        [Fact]
        public async Task Index_WhenExceptionOccurs_ReturnsBadRequest()
        {
            // Arrange
            _mockPostRepository.Setup(repo => repo.GetAll())
                .ThrowsAsync(new Exception("Database connection failed"));

            // Act
            var result = await _controller.Index();

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Failed to fetch posts", badRequestResult.Value);
            
            // Verify that the error was logged
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString().Contains("Failed to fetch posts")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once
            );
        }

        [Fact]
        public async Task AddComment_WithEmptyText_ReturnsBadRequest()
        {
            // Arrange
            SetupSession();
            var postId = 1;
            var emptyCommentText = "";

            // Act
            var result = await _controller.AddComment(postId, emptyCommentText);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var resultValue = Assert.IsType<Dictionary<string, string>>(badRequestResult.Value);
            Assert.Equal("Kommentartekst kan ikke være tom", resultValue["error"]);
        }

        [Fact]
        public async Task AddComment_WhenCommentFails_ReturnsBadRequest()
        {
            // Arrange
            SetupSession();
            var postId = 1;
            var commentText = "Test kommentar";
            
            _mockPostRepository.Setup(repo => repo.AddComment(It.IsAny<Comment>()))
                .ReturnsAsync(false);

            // Act
            var result = await _controller.AddComment(postId, commentText);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var resultValue = Assert.IsType<Dictionary<string, string>>(badRequestResult.Value);
            Assert.Equal("Kunne ikke legge til kommentar", resultValue["error"]);
        }
    }
}