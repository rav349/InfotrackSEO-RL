using System.Reflection;
using InfoTrackSEO.Application.Contracts;
using InfoTrackSEO.Application.Queries;
using InfoTrackSEO.Application.Services;
using InfoTrackSEO.Domain.Data;
using InfoTrackSEO.Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IRequestHandler<GetSearchRankingsQuery, SearchResult>, GetSearchRankingsHandler>();
builder.Services
    .AddTransient<IRequestHandler<GetSearchHistoryQuery, IEnumerable<SearchResult>>, GetSearchHistoryHandler>();

builder.Services.AddScoped<GoogleSearchEngineClient>();
builder.Services.AddScoped<BingSearchEngineClient>();
builder.Services.AddScoped<ISearchEngineClientFactory, SearchEngineClientFactory>();
builder.Services.AddScoped<ISearchResultRepository, SearchResultRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Enable CORS
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
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