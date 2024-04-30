using HackerNewsAPI.Core.Interfaces;
using HackerNewsAPI.Core.Models;
using System.Text.Json;

namespace HackerNewsAPI.Core.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        HttpClient client = new HttpClient();

        public HackerNewsService(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("HackerNewsAPI");
        }
        public async Task<IEnumerable<Story>> GetStoriesByTypeAsync(string type)
        {
            //we would use pageSize and pageNumber params here 
            //int startIndex = (pageNumber - 1) * pageSize;

            HttpResponseMessage response = await client.GetAsync($"{type}.json");

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var storyIds = JsonSerializer.Deserialize<IEnumerable<int>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                //var paginatedStoryIds = storyIds?.Skip(startIndex).Take(pageSize);

                var tasks = storyIds?.Select(id => GetStoryById(id)).ToList();
                var stories = await Task.WhenAll(tasks);

                return stories.Where(story => story != null);
            }
            else
            {
                throw new Exception("Failed to fetch newest stories.");
            }
        }

        public async Task<Story> GetStoryById(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"item/{id}.json");

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();

                var story = JsonSerializer.Deserialize<Story>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return story;
            }
            else
            {
                throw new Exception($"Failed to fetch story with id {id}.");
            }
        }
    }
}
