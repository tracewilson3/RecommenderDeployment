using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using RecommenderDeployment.Models;

namespace RecommenderDeployment.Services;

public class CollaborativeRecommendationService
{
    private readonly Dictionary<string, List<string>> _recommendations;

    public CollaborativeRecommendationService(string csvPath)
    {
        _recommendations = new();

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null
        };

        using var reader = new StreamReader(csvPath);
        using var csv = new CsvReader(reader, config);

        var records = csv.GetRecords<CollaborativeRecommendationRow>();
        foreach (var row in records)
        {
            if (!string.IsNullOrEmpty(row.ArticleId))
            {
                _recommendations[row.ArticleId] = row.GetRecommendations();
            }
        }
    }

    public List<string> GetRecommendations(string articleId)
    {
        return _recommendations.TryGetValue(articleId, out var recs)
            ? recs
            : new List<string>();
    }
}
