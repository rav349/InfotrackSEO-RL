using System.Globalization;
using InfoTrackSEO.Application.Queries;
using InfoTrackSEO.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackSEO.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchHistoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SearchHistoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetSearchHistory()
    {
        var query = new GetSearchHistoryQuery();
        
        var searchResults = await _mediator.Send(query);

        var searchHistory = searchResults.Select(result => new SearchResultDto
        {
            Keywords = result.Keywords,
            SearchEngine = result.SearchEngine,
            Rankings = result.Rankings,
            Url = result.Url,
            SearchDate = result.SearchDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture)
        }).ToList();

        return Ok(searchHistory);
    }
}