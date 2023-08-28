using InfoTrackSEO.Application.Contracts;
using InfoTrackSEO.Domain.Models;
using MediatR;

namespace InfoTrackSEO.Application.Queries;

public class GetSearchHistoryQuery : IRequest<IEnumerable<SearchResult>>
{
}

public class GetSearchHistoryHandler : IRequestHandler<GetSearchHistoryQuery, IEnumerable<SearchResult>>
{
    private readonly ISearchResultRepository _searchResultRepository;

    public GetSearchHistoryHandler(ISearchResultRepository searchResultRepository)
    {
        _searchResultRepository = searchResultRepository;
    }

    public async Task<IEnumerable<SearchResult>> Handle(GetSearchHistoryQuery request,
        CancellationToken cancellationToken)
    {
        return await _searchResultRepository.GetSearchResultsAsync();
    }
}