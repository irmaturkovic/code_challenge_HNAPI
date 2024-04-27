using HackerNewsAPI.Core.Models;

namespace HackerNewsAPI.Core.Interfaces
{
    public interface IHackerNewsService
    {
        Task<IEnumerable<Story>> GetNewestStoriesAsync();
        Task<Story> GetStoryById(int id);
    }
}
