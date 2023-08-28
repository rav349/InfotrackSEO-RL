using InfoTrackSEO.Application.Contracts;
using InfoTrackSEO.Domain.Enums;
using InfoTrackSEO.Domain.Models;

namespace InfoTrackSEO.Application.Services;

public class BingSearchEngineClient : SearchEngineClientBase
{
    private readonly ISearchResultRepository _searchResultRepository;

    public BingSearchEngineClient(HttpClient httpClient, ISearchResultRepository searchResultRepository) :
        base(httpClient)
    {
        _searchResultRepository = searchResultRepository;
    }

    public override async Task<SearchResult> GetSearchRankingsAsync(string keywords, string url)
    {
        var searchUrl = $"https://www.bing.com/search?q={keywords.Replace(" ", "+")}&count=100";
        var pattern = @"<a\s+class=""tilk""\s+href=""(https?://[^""]+)""";

        var rankings = await GetSearchResultsAsync(searchUrl, pattern, url);

        var searchResult = new SearchResult
        {
            Keywords = keywords,
            SearchEngine = SearchEngines.Bing.ToString(),
            Rankings = rankings.Any() ? string.Join(", ", rankings) : "0",
            Url = url,
            SearchDate = DateTime.Now
        };

        await _searchResultRepository.AddAsync(searchResult);

        return searchResult;
    }
}