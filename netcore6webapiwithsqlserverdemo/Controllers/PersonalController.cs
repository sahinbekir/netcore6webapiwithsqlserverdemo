using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netcore6webapiwithsqlserverdemo.DB;
using netcore6webapiwithsqlserverdemo.Models;

namespace netcore6webapiwithsqlserverdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalController : ControllerBase
    {
        private readonly DemoDbContext _demoDbContext;
        public PersonalController(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }
        //List
        [HttpGet]
        public async Task<IEnumerable<Personal>> Get()
        
            => await _demoDbContext.Personals.ToListAsync();

        //SearchId
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Personal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var personal = await _demoDbContext.Personals.FindAsync(id);
            return personal == null ? NotFound() : Ok(personal);
        }
        
        //Search Name
        [HttpGet("search/{title}")]
        [ProducesResponseType(typeof(Personal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTitle(string name)
        {
            var personal = await _demoDbContext.Personals.SingleOrDefaultAsync(c => c.Name == name);
            return personal == null ? NotFound() : Ok(personal);
        }
        //Add
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Personal personal)
        {
            await _demoDbContext.Personals.AddAsync(personal);
            await _demoDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = personal.Id }, personal);
        }
        //Update
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Personal personal)
        {
            if (id != personal.Id) return BadRequest();

            _demoDbContext.Entry(personal).State = EntityState.Modified;
            await _demoDbContext.SaveChangesAsync();

            return NoContent();
        }
        //Delete with Id
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var PerDel = await _demoDbContext.Personals.FindAsync(id);
            if (PerDel == null) return NotFound();

            _demoDbContext.Personals.Remove(PerDel);
            await _demoDbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
