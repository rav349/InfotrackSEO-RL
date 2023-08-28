using InfoTrackSEO.Domain.Models;

namespace InfoTrackSEO.Application.Contracts;

public interface ISearchResultRepository
{
    Task AddAsync(SearchResult searchResult);
    Task<IEnumerable<SearchResult>> GetSearchResultsAsync();
}