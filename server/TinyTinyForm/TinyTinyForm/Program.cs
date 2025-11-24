using Microsoft.EntityFrameworkCore;
using TinyTinyForm.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("TinyTinyDb"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(o =>
{
    o.AddPolicy("local", p =>
    {
        p.WithOrigins(
                "http://localhost:5173",
                "https://localhost:5173",
                "http://localhost:4173",
                "https://localhost:4173"
            )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    });
}); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("local");

app.MapControllers();

app.Run();