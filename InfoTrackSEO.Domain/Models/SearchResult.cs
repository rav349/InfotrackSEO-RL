namespace InfoTrackSEO.Domain.Models;

public class SearchResult
{
    public Guid Id { get; set; }
    public string Keywords { get; set; }
    public string SearchEngine { get; set; }
    public string Rankings { get; set; }
    public string Url { get; set; }
    public DateTime SearchDate { get; set; }
}