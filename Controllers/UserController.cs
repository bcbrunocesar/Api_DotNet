using System;
using System.Threading.Tasks;
using Dot.Database;
using Dot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dot.Controllers
{
    [ApiController]
    [Route("v1/")]
    public class UserController : ControllerBase
    {
        [HttpGet("read")]
        public async Task<IActionResult> Read([FromServices] DataContext context)
        {
            var users = await context.Users.AsNoTracking().ToListAsync();

            return Ok(users);
        }

        [HttpGet("read/{id}")]
        public async Task<IActionResult> ReadById([FromServices] DataContext context, [FromRoute] int id)
        {

            var user = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromServices] DataContext context, [FromBody] User usr)
        {
            if(ModelState.IsValid)
                return BadRequest();

            var user = new User
            {
                Name = usr.Name,
                Email = usr.Email,
                Status = usr.Status
            };

            try
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return Created($"v1/users/{user.Id}", user);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromServices] DataContext context, [FromBody] User usr)
        {
            if(ModelState.IsValid)
                return BadRequest();

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(user == null)
                return NotFound();

            try
            {
                user.Name = usr.Name;

                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromServices] DataContext context, [FromBody] User usr)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();  

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}