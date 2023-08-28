using InfoTrackSEO.Application.Queries;
using InfoTrackSEO.Domain.Enums;
using InfoTrackSEO.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackSEO.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly IMediator _mediator;

    public SearchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRankings([FromQuery] SearchRequest searchRequest)
    {
        if (!Enum.TryParse(searchRequest.SearchEngine, true, out SearchEngines searchEngine))
            return BadRequest("Invalid search engine specified.");

        var query = new GetSearchRankingsQuery
            { Keywords = searchRequest.Keywords, SearchEngines = searchEngine, Url = searchRequest.Url };
        var searchResponse = await _mediator.Send(query);

        return Ok(searchResponse);
    }
}