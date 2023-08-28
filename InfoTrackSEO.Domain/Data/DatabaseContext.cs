using InfoTrackSEO.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InfoTrackSEO.Domain.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<SearchResult> SearchResults { get; set; }
}