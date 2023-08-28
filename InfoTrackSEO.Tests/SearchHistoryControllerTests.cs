using InfoTrackSEO.Application.Queries;
using InfoTrackSEO.Controllers;
using InfoTrackSEO.Domain.Models;
using InfoTrackSEO.Dtos.Requests;
using InfoTrackSEO.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InfoTrackSEO.Tests;

[TestFixture]
public class SearchHistoryControllerTests
{
    private Mock<IMediator> _mockMediator;

    [SetUp]
    public void Setup()
    {
        _mockMediator = new Mock<IMediator>();
    }

    [Test]
    public async Task GetSearchHistory_Returns_Correct_SearchHistory()
    {
        // Arrange
        var controller = new SearchHistoryController(_mockMediator.Object);
        
        var searchResults = new List<SearchResult>
        {
            new() { Keywords = "Info Track", SearchEngine = "Google", Rankings = "1, 2, 3", Url = "https://infotrack.co.uk", SearchDate = DateTime.Now },
            new() { Keywords = "Hello World", SearchEngine = "Bing", Rankings = "1, 2, 3", Url = "https://github.com", SearchDate = DateTime.Now.AddHours(-1) },
            new() { Keywords = "Rav", SearchEngine = "Google", Rankings = "1, 2, 3", Url = "https://rav.com", SearchDate = DateTime.Now.AddHours(-2) }
        };
        
        _mockMediator.Setup(mediator => mediator.Send(It.IsAny<GetSearchHistoryQuery>(), CancellationToken.None)).ReturnsAsync(searchResults);

        // Act
        var result = await controller.GetSearchHistory();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result);
        var okResult = (OkObjectResult)result;
        
        Assert.IsInstanceOf<List<SearchResultDto>>(okResult.Value);
        var searchHistory = (List<SearchResultDto>)okResult.Value;
        
        Assert.AreEqual(searchResults.Count, searchHistory.Count);
        
        for (int i = 0; i < searchResults.Count; i++)
        {
            var expected = searchResults[i];
            var actual = searchHistory[i];
            
            Assert.AreEqual(expected.Keywords, actual.Keywords);
            Assert.AreEqual(expected.SearchEngine, actual.SearchEngine);
            Assert.AreEqual(expected.Rankings, actual.Rankings);
            Assert.AreEqual(expected.Url, actual.Url);
            Assert.AreEqual(expected.SearchDate.ToString("dd/MM/yyyy HH:mm:ss"), actual.SearchDate);
        }
    }
}