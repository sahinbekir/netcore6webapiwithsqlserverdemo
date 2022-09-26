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
    }
}
