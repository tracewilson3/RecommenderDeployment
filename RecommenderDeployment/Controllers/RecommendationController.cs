using Microsoft.AspNetCore.Mvc;
using RecommenderDeployment.Services; 
using RecommenderDeployment.Models;

public class HomeController : Controller
{
    private readonly ContentFilteringSimilarityService _contentSimilarityService;
    private readonly CollaborativeRecommendationService _collabService;

    public HomeController(
        ContentFilteringSimilarityService contentSimilarityService,
        CollaborativeRecommendationService collabService)
    {
        _contentSimilarityService = contentSimilarityService;
        _collabService = collabService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new RecommendationViewModel());
    }

    [HttpPost]
    public IActionResult Index(string articleId)
    {
        var model = new RecommendationViewModel
        {
            ArticleId = articleId,
            CollaborativeRecommendations = _collabService.GetRecommendations(articleId),
            ContentRecommendations = _contentSimilarityService.GetTopRecommendations(articleId)
        };

        return View(model);
    }
}