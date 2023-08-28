using System.Text.RegularExpressions;
using InfoTrackSEO.Application.Contracts;
using InfoTrackSEO.Domain.Models;

namespace InfoTrackSEO.Application.Services;

public abstract class SearchEngineClientBase : ISearchEngineClient
{
    private readonly HttpClient _httpClient;

    protected SearchEngineClientBase(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public abstract Task<SearchResult> GetSearchRankingsAsync(string keywords, string url);

    protected async Task<List<int>> GetSearchResultsAsync(string searchUrl, string pattern, string url)
    {
        _httpClient.DefaultRequestHeaders.Add("User-Agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36");
        var htmlContent = await _httpClient.GetStringAsync(searchUrl);

        var searchResults = Regex.Matches(htmlContent, pattern, RegexOptions.IgnoreCase)
            .Select(match => match.Value)
            .ToList();

        var matchingIndices = new List<int>();

        for (var i = 0; i < searchResults.Count; i++)
            if (searchResults[i].Contains(url, StringComparison.OrdinalIgnoreCase))
                matchingIndices.Add(i + 1);

        return matchingIndices;
    }
}