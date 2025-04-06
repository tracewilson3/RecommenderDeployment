namespace RecommenderDeployment.Models;

using CsvHelper.Configuration.Attributes;

public class ContentFilteringRow
{
    [Name("article_id")]
    public string ArticleId { get; set; }

    [Name("recommendations")]
    public string RawRecommendations { get; set; }

    public List<string> GetRecommendations()
    {
        return RawRecommendations
            .Trim('[', ']')             // remove square brackets
            .Split(',')                 // split on commas
            .Select(r => r.Trim())      // trim whitespace
            .ToList();
    }
}