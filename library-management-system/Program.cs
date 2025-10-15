using Microsoft.EntityFrameworkCore;
using library_management_system.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AuthorContext>(options =>
    options.UseInMemoryDatabase("Library"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

record Author(int Id, string Name, DateOnly DateOfBirth);
record Book(int Id, string Titile, int PublishedYear, int AouthorId);