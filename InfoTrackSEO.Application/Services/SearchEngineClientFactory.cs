using InfoTrackSEO.Application.Contracts;
using InfoTrackSEO.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace InfoTrackSEO.Application.Services;

public class SearchEngineClientFactory : ISearchEngineClientFactory
{
    private readonly IServiceProvider _serviceProvider;

    public SearchEngineClientFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ISearchEngineClient Create(SearchEngines searchEngines)
    {
        return searchEngines switch
        {
            SearchEngines.Google => _serviceProvider.GetRequiredService<GoogleSearchEngineClient>(),
            SearchEngines.Bing => _serviceProvider.GetRequiredService<BingSearchEngineClient>(),
            _ => throw new ArgumentOutOfRangeException(nameof(searchEngines), "Unsupported search engine")
        };
    }
}