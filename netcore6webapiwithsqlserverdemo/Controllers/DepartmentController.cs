using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netcore6webapiwithsqlserverdemo.DB;
using netcore6webapiwithsqlserverdemo.Models;

namespace netcore6webapiwithsqlserverdemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DemoDbContext _demoDbContext;
        public DepartmentController(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }
        //List
        [HttpGet]
        public async Task<IEnumerable<Department>> Get()

            => await _demoDbContext.Departments.ToListAsync();
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Department), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _demoDbContext.Departments.FindAsync(id);
            return department == null ? NotFound() : Ok(department);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Department dep)
        {
            await _demoDbContext.Departments.AddAsync(dep);
            await _demoDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = dep.Id }, dep);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Department dep)
        {
            if (id != dep.Id) return BadRequest();
            //Console.WriteLine(personal);
            _demoDbContext.Entry(dep).State = EntityState.Modified;
            await _demoDbContext.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("search/{title}")]
        [ProducesResponseType(typeof(Department), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTitle(string name)
        {
            var dep = await _demoDbContext.Departments.SingleOrDefaultAsync(c => c.Name == name);
            return dep == null ? NotFound() : Ok(dep);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var DepDel = await _demoDbContext.Departments.FindAsync(id);
            if (DepDel == null) return NotFound();

            _demoDbContext.Departments.Remove(DepDel);
            await _demoDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
