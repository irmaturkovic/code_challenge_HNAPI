using HackerNewsAPI.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackerNewsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsItemsController : ControllerBase
    {
        private readonly IHackerNewsService _hackerNewsService;

        public HackerNewsItemsController(IHackerNewsService hackerNewsService)
        {
            _hackerNewsService = hackerNewsService;
        }

        // GET api/<HackerNewsItemsController>/5
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

        // GET 
        [HttpGet]
        public async Task<IActionResult> GetNewestStories()
        {
            try
            {
                var response = await _hackerNewsService.GetNewestStoriesAsync();

                return Ok(response);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST api/<HackerNewsItemsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HackerNewsItemsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HackerNewsItemsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
