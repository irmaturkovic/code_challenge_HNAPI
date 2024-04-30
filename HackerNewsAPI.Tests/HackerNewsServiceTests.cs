using HackerNewsAPI.Core.Interfaces;
using HackerNewsAPI.Core.Models;
using HackerNewsAPI.Core.Services;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace HackerNewsAPI.Tests
{
    public class HackerNewsServiceTests
    {
        private IHackerNewsService _hackerNewsService;
        private Mock<IHttpClientFactory> _mockHttpClientFactory;

        [SetUp]
        public void Setup()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            //_hackerNewsService = new HackerNewsService(_mockHttpClientFactory.Object);
        }

        [Test]
        public async Task GetStoryById_Returns_Story_When_Response_Is_Successful()
        {
            // Arrange
            int storyId = 8863;
            var expectedStory = new Story
            {
                By = "dhouston",
                Descendants = 71,
                Kids = [8952, 9224, 8917, 8884, 8887, 8943, 8869, 8958, 9005, 9671, 8940, 9067, 8908, 9055, 8865, 8881, 8872, 8873, 8955, 10403, 8903, 8928, 9125, 8998, 8901, 8902, 8907, 8894, 8878, 8870, 8980, 8934, 8876],
                Score = 111,
                Time = 1175714200,
                Title = "My YC app: Dropbox - Throw away your USB drive",
                Type = "story",
                Url = "http://www.getdropbox.com/u/2/screencast.html"
            };

            var jsonString = JsonSerializer.Serialize(expectedStory);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHttpMessageHandler.Protected()
                                  .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                                  .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(jsonString, Encoding.UTF8, "application/json") });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json")
            };

            _mockHttpClientFactory.Setup(factory => factory.CreateClient("HackerNewsAPI"))
                                  .Returns(httpClient);

            _hackerNewsService = new HackerNewsService(_mockHttpClientFactory.Object);

            // Act
            var result = await _hackerNewsService.GetStoryById(storyId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedStory.By, result.By);
            Assert.AreEqual(expectedStory.Title, result.Title);
        }

        [Test]
        public void GetStoryById_Throws_Exception_When_Response_Is_Not_Successful()
        {
            // Arrange
            int storyId = 0;

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHttpMessageHandler.Protected()
                                  .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                                  .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError));

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json")
            };

            _mockHttpClientFactory.Setup(factory => factory.CreateClient("HackerNewsAPI"))
                                  .Returns(httpClient);

            _hackerNewsService = new HackerNewsService(_mockHttpClientFactory.Object);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _hackerNewsService.GetStoryById(storyId));
        }

    }
}
