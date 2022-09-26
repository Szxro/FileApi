global using FileUploadApi.Models;
global using Microsoft.EntityFrameworkCore;
global using FileUploadApi.Services.FileService;
using FileUploadApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddDbContext<FileContext>(m => {
    m.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseCors(m => 
{
    m.AllowAnyHeader();
    m.AllowAnyOrigin();
});
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
