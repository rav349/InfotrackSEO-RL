using InfoTrackSEO.Application.Services;
using InfoTrackSEO.Domain.Enums;
using Moq;

namespace InfoTrackSEO.Tests;

[TestFixture]
public class SearchEngineClientFactoryTests
{
    [SetUp]
    public void Setup()
    {
        _mockGoogleSearchEngineClient = new GoogleSearchEngineClient(_httpClient, _searchResultRepository);
        _mockBingSearchEngineClient = new BingSearchEngineClient(_httpClient, _searchResultRepository);

        _mockServiceProvider = new Mock<IServiceProvider>();
        _mockServiceProvider.Setup(provider => provider.GetService(typeof(GoogleSearchEngineClient)))
            .Returns(_mockGoogleSearchEngineClient);
        _mockServiceProvider.Setup(provider => provider.GetService(typeof(BingSearchEngineClient)))
            .Returns(_mockBingSearchEngineClient);
    }

    private Mock<IServiceProvider> _mockServiceProvider;
    private GoogleSearchEngineClient _mockGoogleSearchEngineClient;
    private BingSearchEngineClient _mockBingSearchEngineClient;
    private HttpClient _httpClient;
    private SearchResultRepository _searchResultRepository;

    [Test]
    [TestCase(SearchEngines.Google, typeof(GoogleSearchEngineClient))]
    [TestCase(SearchEngines.Bing, typeof(BingSearchEngineClient))]
    public void Create_Returns_Correct_Client(SearchEngines searchEngines, Type expectedType)
    {
        // Arrange
        var factory = new SearchEngineClientFactory(_mockServiceProvider.Object);

        // Act
        var result = factory.Create(searchEngines);

        // Assert
        Assert.IsInstanceOf(expectedType, result);
    }

    [Test]
    public void Create_Throws_Exception_For_Unsupported_Search_Engine()
    {
        // Arrange
        var factory = new SearchEngineClientFactory(_mockServiceProvider.Object);
        const SearchEngines unsupportedEngine = (SearchEngines)100;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => factory.Create(unsupportedEngine));
    }
}