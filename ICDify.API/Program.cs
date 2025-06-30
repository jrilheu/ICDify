using ICDify.Application.Interfaces;
using ICDify.Application.UseCases;
using ICDify.Infrastructure.Mappers;
using ICDify.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// register services 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add DB context and repositories
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDrugRepository, DrugRepository>();
builder.Services.AddScoped<IIndicationMapper, MockIndicationMapper>();

// Add services
builder.Services.AddScoped<ExtractAndMapIndicationsUseCase>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v0", new OpenApiInfo
    {
        Title = "ICDify API",
        Version = "v0",
        Description = "Extracts drug indications and maps them to ICD-10 codes"
    });
});

var app = builder.Build();

// Enable middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v0/swagger.json", "ICDify API v0");
        c.RoutePrefix = string.Empty;
    });
}

app.MapControllers();

app.Run();
