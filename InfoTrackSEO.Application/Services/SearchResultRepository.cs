using InfoTrackSEO.Application.Contracts;
using InfoTrackSEO.Domain.Data;
using InfoTrackSEO.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InfoTrackSEO.Application.Services;

public class SearchResultRepository : ISearchResultRepository
{
    private readonly DatabaseContext _dbContext;

    public SearchResultRepository(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(SearchResult searchResult)
    {
        await _dbContext.SearchResults.AddAsync(searchResult);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<SearchResult>> GetSearchResultsAsync()
    {
        return await _dbContext
            .SearchResults
            .OrderByDescending(result => result.SearchDate)
            .ToListAsync();
    }
}