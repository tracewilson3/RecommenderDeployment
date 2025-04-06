namespace RecommenderDeployment.Models;

public class RecommendationViewModel
{
    public string ArticleId { get; set; }
    public List<string> CollaborativeRecommendations { get; set; }
    public List<string> ContentRecommendations { get; set; }
}