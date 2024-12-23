using Microsoft.EntityFrameworkCore;
using Notes.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NotesDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("NotesDbConnectionString"));
});

var app = builder.Build();

app.UseCors(policyOptions => policyOptions.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod());

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
