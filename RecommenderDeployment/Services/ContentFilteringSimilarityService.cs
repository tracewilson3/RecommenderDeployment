using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

public class ContentFilteringSimilarityService
{
    private readonly Dictionary<string, Dictionary<string, float>> _similarityMatrix;

    public ContentFilteringSimilarityService(string csvPath)
    {
        _similarityMatrix = new();

        using var reader = new StreamReader(csvPath);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true
        });

        // Read the header row: these are the article IDs
        csv.Read();
        csv.ReadHeader();
        var articleIds = csv.Context.Reader.HeaderRecord!;


        int rowIndex = 0;

        while (csv.Read())
        {
            if (rowIndex >= articleIds.Length)
                break;

            var currentArticleId = articleIds[rowIndex];
            var innerDict = new Dictionary<string, float>();

            for (int colIndex = 0; colIndex < articleIds.Length; colIndex++)
            {
                var colArticleId = articleIds[colIndex];
                var value = csv.GetField(colIndex);

                if (float.TryParse(value, out float similarity))
                {
                    innerDict[colArticleId] = similarity;
                }
            }

            _similarityMatrix[currentArticleId] = innerDict;
            rowIndex++;
        }
    }

    public List<string> GetTopRecommendations(string articleId, int topN = 5)
    {
        articleId = articleId.Trim();

        if (!_similarityMatrix.TryGetValue(articleId, out var similarities))
            return new List<string>();

        return similarities
            .Where(kvp => kvp.Key != articleId)
            .OrderByDescending(kvp => kvp.Value)
            .Take(topN)
            .Select(kvp => kvp.Key)
            .ToList();
    }
}