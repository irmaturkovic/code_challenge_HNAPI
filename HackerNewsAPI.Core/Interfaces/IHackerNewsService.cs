using HackerNewsAPI.Core.Models;

namespace HackerNewsAPI.Core.Interfaces
{
    public interface IHackerNewsService
    {
        Task<IEnumerable<Story>> GetStoriesByTypeAsync(string type);
        Task<Story> GetStoryById(int id);
    }
}
