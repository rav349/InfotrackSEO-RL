using InfoTrackSEO.Application.Queries;
using InfoTrackSEO.Controllers;
using InfoTrackSEO.Domain.Enums;
using InfoTrackSEO.Domain.Models;
using InfoTrackSEO.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InfoTrackSEO.Tests;

[TestFixture]
public class SearchControllerTests
{
    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
    }

    private Mock<IMediator> _mockMediator;

    [Test]
    public async Task GetRankings_Returns_Correct_Search_Rankings()
    {
        // Arrange
        var controller = new SearchController(_mockMediator.Object);
        var searchRequest = new SearchRequest
        {
            Keywords = "Info Track", SearchEngine = SearchEngines.Google.ToString(), Url = "https://infotrack.co.uk"
        };
        var query = new GetSearchRankingsQuery
            { Keywords = searchRequest.Keywords, SearchEngines = SearchEngines.Google, Url = searchRequest.Url };

        var expectedResult = new SearchResult
        {
            Keywords = searchRequest.Keywords,
            SearchEngine = searchRequest.SearchEngine,
            Rankings = "1, 2, 3",
            Url = searchRequest.Url
        };

        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetSearchRankingsQuery>(), CancellationToken.None))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await controller.GetRankings(searchRequest);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;

        Assert.AreEqual(expectedResult, okResult.Value);

        _mockMediator.Verify(
            mediator => mediator.Send(
                It.Is<GetSearchRankingsQuery>(q => q.Keywords == query.Keywords && q.Url == query.Url),
                CancellationToken.None), Times.Once);
    }

    [Test]
    public async Task GetRankings_Returns_BadRequest_For_Invalid_SearchEngine()
    {
        // Arrange
        var controller = new SearchController(_mockMediator.Object);
        var invalidRequest = new SearchRequest
            { Keywords = "Info Track", SearchEngine = "InvalidEngine", Url = "https://infotrack.co.uk" };

        // Act
        var result = await controller.GetRankings(invalidRequest);

        // Assert
        Assert.IsInstanceOf<BadRequestObjectResult>(result);
        var badRequestResult = (BadRequestObjectResult)result;
        Assert.AreEqual("Invalid search engine specified.", badRequestResult.Value);
    }
}