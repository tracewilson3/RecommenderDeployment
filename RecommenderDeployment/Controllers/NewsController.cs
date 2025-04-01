using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RecommenderDeployment.Models;

namespace RecommenderDeployment.Controllers;

public class NewsController : Controller
{
    private readonly HttpClient _httpClient;

    public NewsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("recommend")]
    public async Task<IActionResult> GetRecommendations()
    {
        string azureEndpoint = "https://your-azureml-endpoint.com/score";
        var requestBody = new { /* Add any required input data */ };

        var response = await _httpClient.PostAsync(
            azureEndpoint,
            new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json")
        );

        if (!response.IsSuccessStatusCode)
        {
            return BadRequest("Failed to get recommendations.");
        }

        var result = await response.Content.ReadAsStringAsync();
        return Ok(JsonSerializer.Deserialize<object>(result));
    }
}
