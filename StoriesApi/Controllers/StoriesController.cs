using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoriesApi.Models;

namespace StoriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly StoryContext _context;

        public StoriesController(StoryContext context)
        {
            _context = context;
        }

        // GET: api/Stories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Story>>> GetStories()
        {
            return await _context.Stories.ToListAsync();
        }

        // GET: api/Stories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Story>> GetStory(int id)
        {
            var story = await _context.Stories.FindAsync(id);

            if (story == null)
            {
                return NotFound();
            }

            return story;
        }

        // PUT: api/Stories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStory(int id, Story story)
        {
            if (id != story.StoryId)
            {
                return BadRequest();
            }

            _context.Entry(story).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Stories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Story>> PostStory(Story story)
        {
            _context.Stories.Add(story);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStory", new { id = story.StoryId }, story);
        }

        // DELETE: api/Stories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStory(int id)
        {
            var story = await _context.Stories.FindAsync(id);
            if (story == null)
            {
                return NotFound();
            }

            _context.Stories.Remove(story);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoryExists(int id)
        {
            return _context.Stories.Any(e => e.StoryId == id);
        }
    }
}
