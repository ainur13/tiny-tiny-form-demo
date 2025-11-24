using Microsoft.EntityFrameworkCore;
using TinyTinyDynamicForm.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("TinyTinyDb"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o =>
{
    o.AddPolicy("test", p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
}); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("test");

app.MapControllers();

app.Run();
