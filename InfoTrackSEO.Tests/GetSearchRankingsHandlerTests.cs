using InfoTrackSEO.Application.Contracts;
using InfoTrackSEO.Application.Queries;
using InfoTrackSEO.Domain.Enums;
using Moq;

namespace InfoTrackSEO.Tests;

[TestFixture]
public class GetSearchRankingsHandlerTests
{
    [SetUp]
    public void Setup()
    {
        _mockSearchEngineClient = new Mock<ISearchEngineClient>();
        _mockSearchEngineClientFactory = new Mock<ISearchEngineClientFactory>();
        _mockSearchEngineClientFactory.Setup(factory => factory.Create(It.IsAny<SearchEngines>()))
            .Returns(_mockSearchEngineClient.Object);
    }

    private Mock<ISearchEngineClientFactory> _mockSearchEngineClientFactory;
    private Mock<ISearchEngineClient> _mockSearchEngineClient;

    [Test]
    public async Task Handle_Calls_SearchEngineClientFactory_Correctly()
    {
        // Arrange
        var handler = new GetSearchRankingsHandler(_mockSearchEngineClientFactory.Object);
        var query = new GetSearchRankingsQuery
            { Keywords = "Info Track", SearchEngines = SearchEngines.Google, Url = "https://infotrack.co.uk" };

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        _mockSearchEngineClientFactory.Verify(factory => factory.Create(SearchEngines.Google), Times.Once);
    }

    [Test]
    public async Task Handle_Calls_SearchEngineClient_Correctly()
    {
        // Arrange
        var handler = new GetSearchRankingsHandler(_mockSearchEngineClientFactory.Object);
        var query = new GetSearchRankingsQuery
            { Keywords = "Info Track", SearchEngines = SearchEngines.Google, Url = "https://infotrack.co.uk" };

        // Act
        await handler.Handle(query, CancellationToken.None);

        // Assert
        _mockSearchEngineClient.Verify(client => client.GetSearchRankingsAsync(query.Keywords, query.Url), Times.Once);
    }
}