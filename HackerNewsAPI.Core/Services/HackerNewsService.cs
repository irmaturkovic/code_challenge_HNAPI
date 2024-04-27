using HackerNewsAPI.Core.Interfaces;
using HackerNewsAPI.Core.Models;
using System.Text.Json;

namespace HackerNewsAPI.Core.Services
{
    public class HackerNewsService : IHackerNewsService
    {
        HttpClient client = new HttpClient();
        private static Dictionary<int, Story> storyCache = new Dictionary<int, Story>();

        public HackerNewsService(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient("HackerNewsAPI");
        }
        public async Task<IEnumerable<Story>> GetNewestStoriesAsync()
        {
            HttpResponseMessage response = await client.GetAsync("newstories.json");

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();

                var storyIds = JsonSerializer.Deserialize<IEnumerable<int>>(jsonString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                var stories = await FetchStoriesAsync(storyIds);

                return stories;
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

        private async Task<IEnumerable<Story>> FetchStoriesAsync(IEnumerable<int> storyIds)
        {
            var stories = new List<Story>();

            foreach (var id in storyIds)
            {
                Story story;

                if (storyCache.TryGetValue(id, out story))
                {
                    stories.Add(story); // Add cached story to the list
                }
                else
                {
                    // Fetch the story from the API
                    story = await GetStoryById(id);

                    // Cache the fetched story
                    if (story != null)
                    {
                        storyCache[id] = story;
                        stories.Add(story);
                    }
                }
            }
            return stories;
        }
    }
}
