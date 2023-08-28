using InfoTrackSEO.Domain.Enums;

namespace InfoTrackSEO.Application.Contracts;

public interface ISearchEngineClientFactory
{
    ISearchEngineClient Create(SearchEngines searchEngines);
}