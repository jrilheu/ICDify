using ICDify.Application.Interfaces;
using ICDify.Application.UseCases;
using ICDify.Infrastructure.Mappers;
using ICDify.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// add DB context and repositories
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDrugRepository, DrugRepository>();
builder.Services.AddScoped<IIndicationMapper, MockIndicationMapper>();

// Add services
builder.Services.AddScoped<ExtractAndMapIndicationsUseCase>();

app.MapGet("/", () => "Hello World!");

app.Run();
