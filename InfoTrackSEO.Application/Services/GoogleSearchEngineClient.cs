using InfoTrackSEO.Application.Contracts;
using InfoTrackSEO.Domain.Enums;
using InfoTrackSEO.Domain.Models;

namespace InfoTrackSEO.Application.Services;

public class GoogleSearchEngineClient : SearchEngineClientBase
{
    private readonly ISearchResultRepository _searchResultRepository;

    public GoogleSearchEngineClient(HttpClient httpClient, ISearchResultRepository searchResultRepository) :
        base(httpClient)
    {
        _searchResultRepository = searchResultRepository;
    }

    public override async Task<SearchResult> GetSearchRankingsAsync(string keywords, string url)
    {
        var searchUrl = $"https://www.google.co.uk/search?num=100&q={keywords.Replace(" ", "+")}";
        var pattern = @"<div class=""yuRUbf"">.*?</div>";

        var rankings = await GetSearchResultsAsync(searchUrl, pattern, url);

        var searchResult = new SearchResult
        {
            Keywords = keywords,
            SearchEngine = SearchEngines.Google.ToString(),
            Rankings = rankings.Any() ? string.Join(", ", rankings) : "0",
            Url = url,
            SearchDate = DateTime.Now
        };

        await _searchResultRepository.AddAsync(searchResult);

        return searchResult;
    }
}