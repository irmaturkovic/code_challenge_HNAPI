using HackerNewsAPI.Core.Interfaces;
using HackerNewsAPI.Core.Models;
using HackerNewsAPI.Core.Services;
using Moq;
using System.Net;
using System.Text.Json;

namespace HackerNewsAPI.Tests
{
    public class HackerNewsServiceTests
    {
        private Mock<IHttpClientFactory> httpClientFactoryMock;
        private Mock<HttpClient> httpClientMock;
        private HackerNewsService storyService;

        public void Setup()
        {
            

        }

        [Test]
        public async Task GetStoryById_Success_ReturnsStory()
        {
            httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientMock = new Mock<HttpClient>();
            storyService = new HackerNewsService(httpClientFactoryMock.Object);

            // Arrange
            var storyId = 8863;
            var expectedStory = new Story { Id = storyId, Title = "Test Story" , By = "dhouston", Time = 1175714200, Type = "story" };
            var jsonString = JsonSerializer.Serialize(expectedStory);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(jsonString)
            };
            httpClientMock
                .Setup(client => client.GetAsync($"item/{storyId}.json"))
                .ReturnsAsync(response);

            // Act
            var result = await storyService.GetStoryById(storyId);

            // Assert
            Assert.AreEqual(expectedStory.Id, result.Id);
            Assert.AreEqual(expectedStory.Title, result.Title);
        }
    }
}
