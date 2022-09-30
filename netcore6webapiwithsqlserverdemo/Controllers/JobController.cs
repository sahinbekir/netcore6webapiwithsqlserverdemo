using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netcore6webapiwithsqlserverdemo.DB;
using netcore6webapiwithsqlserverdemo.Models;

namespace netcore6webapiwithsqlserverdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly DemoDbContext _demoDbContext;
        public JobController(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }
        //List
        [HttpGet]
        public async Task<IEnumerable<Job>> Get()

            => await _demoDbContext.Jobs.ToListAsync();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var job = await _demoDbContext.Jobs.FindAsync(id);
            return job == null ? NotFound() : Ok(job);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Job j)
        {
            await _demoDbContext.Jobs.AddAsync(j);
            await _demoDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = j.Id }, j);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Job j)
        {
            if (id != j.Id) return BadRequest();
            //Console.WriteLine(personal);
            _demoDbContext.Entry(j).State = EntityState.Modified;
            await _demoDbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("search/{title}")]
        [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTitle(string name)
        {
            var job = await _demoDbContext.Jobs.SingleOrDefaultAsync(c => c.Name == name);
            return job == null ? NotFound() : Ok(job);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var JobDel = await _demoDbContext.Jobs.FindAsync(id);
            if (JobDel == null) return NotFound();

            _demoDbContext.Jobs.Remove(JobDel);
            await _demoDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
