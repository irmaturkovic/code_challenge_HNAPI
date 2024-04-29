using HackerNewsAPI.Core.Interfaces;
using HackerNewsAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackerNewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsItemsController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;
        private readonly IMemoryCache _cache;

        public HackerNewsItemsController(IHackerNewsService hackerNewsService, IMemoryCache memoryCache)
        {
            _hackerNewsService = hackerNewsService;
            _cache = memoryCache;
        }

        // GET /api/HackerNewsItems/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoriesById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Id");
                }

                var response = await _hackerNewsService.GetStoryById(id);
                if (response == null)
                {
                    return NotFound("Story with provided Id doesn't exist");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/HackerNewsItems
        [HttpGet]
        public async Task<IActionResult> GetNewestStories([FromQuery] string type/*, int pageSize, int pageNumber*/)
        {
            try
            {
                //this way we can do paggination on backend side of application 
                //string cacheKey; /*= $"data-{pageNumber}-{pageSize}";*/

                if (!_cache.TryGetValue<IEnumerable<Story>>("data", out var cachedData))
                {
                    var stories = await _hackerNewsService.GetStoriesByTypeAsync(type);

                    _cache.Set("data", stories, TimeSpan.FromSeconds(60));

                    return Ok(stories);
                }
                else
                {
                    // Data is cached, return it
                    return Ok(cachedData);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
