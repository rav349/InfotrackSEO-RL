using System.ComponentModel.DataAnnotations;

namespace InfoTrackSEO.Dtos.Requests;

public class SearchRequest
{
    [Required] public string SearchEngine { get; set; }
    [Required] public string Url { get; set; }
    [Required] public string Keywords { get; set; }
}