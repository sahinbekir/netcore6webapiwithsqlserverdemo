using Microsoft.EntityFrameworkCore;
using netcore6webapiwithsqlserverdemo.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DemoDbContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
// For Postgres Sql connection Dont forget! You need M.EFC.PostgreSQL
/*builder.Services.AddDbContext<DemoDbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("Npgsql")));*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
