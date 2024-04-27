using HackerNewsAPI.Core.Interfaces;
using HackerNewsAPI.Core.Models;
using HackerNewsAPI.Core.Services;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text.Json;

namespace HackerNewsAPI.Tests
{
    public class HackerNewsServiceTests
    {
        [Test]
        public async Task GetStoryById_Success_ReturnsStory()
        {
            // Arrange
            int id = 8863;
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

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var mockHttpClient = new Mock<IHttpClientFactory>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonString),
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri($"https://hacker-news.firebaseio.com/v0/item/{id}.json"),
            };
            mockHttpClientFactory
                .Setup(factory => factory.CreateClient("HackerNewsAPI"))
                .Returns(httpClient);

            var hackerNewestService = new HackerNewsService(mockHttpClientFactory.Object);

            // Act
            var result = await hackerNewestService.GetStoryById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equals(expectedStory.Kids, result.Kids);
            Assert.Equals(expectedStory.Score, result.Score);
            Assert.Equals(expectedStory.Url, result.Url);
        }
    }
}
