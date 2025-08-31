using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoriesApi.Models;
using StoriesApi.Utils;
using StoriesApi.Models.Requests;
using StoriesApi.Models.Responses;
using StoriesApi.Models;

namespace StoriesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly StoryContext _context;

        public UsersController(StoryContext context)
        {
            _context = context;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // Get multiple users at once
        [HttpPost("multiget")]
        public async Task<MultigetUserResponse> MultigetUser(MultigetRequest request)
        {
            var users = await _context.Users.Where(user => request.Ids.Contains(user.UserId)).ToListAsync();
            var userDict = users.ToDictionary(x => x.UserId);

            return new MultigetUserResponse
            {
                Users = userDict
            };
        }

        // Login user - api/Users/login
        [HttpPost("login")]
        public async Task<User?> LoginUser(LoginRequest request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(user => user.Username == request.Username && user.Password == request.Password);

            return user;
        }

        // PUT: api/Users/5
        // To protect from overusering attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null) {
                return NotFound();
            }

            existingUser.Username = user.Username;
            existingUser.Password = user.Password;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Users
        // To protect from overusering attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> UserUser(User user)
        {
            // does username already exist?
            if (_context.Users.Any(existingUser => existingUser.Username == user.Username))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = nameof(ErrorCode.UsernameTaken) });
            }

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
