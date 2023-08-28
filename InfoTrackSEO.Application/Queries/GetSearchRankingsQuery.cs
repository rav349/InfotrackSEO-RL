using InfoTrackSEO.Application.Contracts;
using InfoTrackSEO.Domain.Enums;
using InfoTrackSEO.Domain.Models;
using MediatR;

namespace InfoTrackSEO.Application.Queries;

public class GetSearchRankingsQuery : IRequest<SearchResult>
{
    public string Keywords { get; set; }
    public string Url { get; set; }
    public SearchEngines SearchEngines { get; set; }
}

public class GetSearchRankingsHandler : IRequestHandler<GetSearchRankingsQuery, SearchResult>
{
    private readonly ISearchEngineClientFactory _searchEngineClientFactory;

    public GetSearchRankingsHandler(ISearchEngineClientFactory searchEngineClientFactory)
    {
        _searchEngineClientFactory = searchEngineClientFactory;
    }

    public async Task<SearchResult> Handle(GetSearchRankingsQuery request, CancellationToken cancellationToken)
    {
        var searchEngineClient = _searchEngineClientFactory.Create(request.SearchEngines);

        var searchResult = await searchEngineClient.GetSearchRankingsAsync(request.Keywords, request.Url);

        return searchResult;
    }
}