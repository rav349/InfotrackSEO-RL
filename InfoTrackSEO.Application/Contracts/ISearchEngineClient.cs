using InfoTrackSEO.Domain.Models;

namespace InfoTrackSEO.Application.Contracts;

public interface ISearchEngineClient
{
    Task<SearchResult> GetSearchRankingsAsync(string keywords, string url);
}