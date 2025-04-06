namespace RecommenderDeployment.Models;

using CsvHelper.Configuration.Attributes;

public class CollaborativeRecommendationRow
{
    [Name("Article ID")]
    public string ArticleId { get; set; }

    [Name("Rec ID 1")]
    public string Rec1 { get; set; }

    [Name("Rec ID 2")]
    public string Rec2 { get; set; }

    [Name("Rec ID 3")]
    public string Rec3 { get; set; }

    [Name("Rec ID 4")]
    public string Rec4 { get; set; }

    [Name("Rec ID 5")]
    public string Rec5 { get; set; }

    public List<string> GetRecommendations()
    {
        return new List<string> { Rec1, Rec2, Rec3, Rec4, Rec5 };
    }
}