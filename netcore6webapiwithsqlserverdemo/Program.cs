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


var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder => {
        //builder.WithOrigins("http://localhost:800").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
        //builder.SetIsOriginAllowed(origin => true);
    });
});

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(devCorsPolicy);

}

app.UseHttpsRedirection();


app.UseAuthorization();
//app.UseCors(prodCorsPolicy);

app.MapControllers();

app.Run();
